using IMDbApiLib;
using IMDbApiLib.Models;
using TMDbLib;
using TMDbLib.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using JR.Utils.GUI.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml.DataValidation;
using Button = System.Windows.Forms.Button;

namespace IMDbApi.Forms
{
    //Non-commercial WinForms to get new releases and other information using IMDB APi
    //https://imdb-api.com/API/AdvancedSearch/
    //title_type=tv_movie,tv_series,tv_episode,documentary,video&
    //release_date=2023-04-10,2023-06-10&
    //countries=af&
    //languages=eu,ca,zh,eo,fr,de,hi,ko,kvk,ru,rsl,es,ssp,ta,te,tr
    public partial class SearchForm : Form
    {
        public readonly Dictionary<string, int> locationForm = new Dictionary<string, int>();
        public static SearchForm instance;

        public SearchForm(Dictionary<string, int> data)
        {
            locationForm = data;
            InitializeComponent();
            instance = this;
            FillInCountry();
            _api = new ApiLib(KeysAccess.GetRandomValue());
            //_apiTMDB = new TMDbLib.Client.TMDbClient(KeysAccess.GetKeyTMDB());
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tipsForm = new System.Windows.Forms.ToolTip();
            LoadTheme();
            tipsForm.SetToolTip(button2, "Поиск сета в выбранном диапозоне дат по выбрранное стране");
            tipsForm.SetToolTip(button3, "Сохранение всего архива или только новых(за эту сессию) метаданных в excel");
            tipsForm.SetToolTip(button4, "Добавление метаданных либо выбраной позиции в листе либо всех в архив");
            tipsForm.SetToolTip(button5, "Отображение всех метаданных содержащихся в архиве");
            tipsForm.SetToolTip(listBox1, "Поле для отображения информации поиска или архивных записей");
            tipsForm.SetToolTip(pictureBox1, "Постер");
            tipsForm.SetToolTip(OnlyNewCheckBox, "Необходимо для получения excel файла с мета данными тайтлов этой сессии");
            tipsForm.SetToolTip(AllAddCheckBox, "Необходимо выделить чтобы добавить весь список найденных позиций в архив");
            tipsForm.SetToolTip(textBox1, "Наименование для поиска по IMDB");
            tipsForm.SetToolTip(comboBox1, "Выбор страны для поиска новых релизов");
            tipsForm.SetToolTip(ArchiveSearchCheckBox, "Неохоидмо для поиска тайтла в архиве");
            tipsForm.SetToolTip(SearchTit, "Добавляет тип, год, дату релиза, языки, режиссера и другое для выбранного тайтла");
            tipsForm.SetToolTip(SearchButton, "Кнопка для поиска по IMDB или архиву");
            tipsForm.SetToolTip(dateFrom, "Дата начала интервала");
            tipsForm.SetToolTip(dateTo, "Дата конечная интервала");
            savedJson = HardTool.GetSavedJson();
        }

        #region Global variables

        public WebProxy WebProxy { get; }
        private ApiLib _api;
        //private TMDbLib.Client.TMDbClient _apiTMDB;
        private string releases;
        private FilmData activeJson;
        private List<string> newAddedJson = new List<string>();
        private FilmData savedJson;
        private Boolean sameJson;

        private string apiUrlForNew = $"https://imdb-api.com/API/AdvancedSearch/{KeysAccess.GetRandomValue()}/?title_type=tv_movie,tv_series,tv_episode,documentary,video";
        private string TitleUrl = $"https://imdb-api.com/en/API/Title/{KeysAccess.GetRandomValue()}/";

        //private string UpComingUrl = $"https://imdb-api.com/en/API/ComingSoon/{KeysAccess.GetRandomValue()}";
        //private const string apiUrl = "https://imdb-api.com/ru/API/ComingSoon/k_yl5q767w";
        //private const string apiUrl = "https://imdb-api.com/ru/API/ComingSoon/k_pj17x8n5";
        #endregion

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColour.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColour.SecondaryColor;
                }
            }
            ArchiveSearchCheckBox.ForeColor = ThemeColour.SecondaryColor;
            label1.ForeColor = ThemeColour.PrimaryColor;
            AllAddCheckBox.ForeColor = ThemeColour.SecondaryColor;
            label4.ForeColor = ThemeColour.SecondaryColor;
            label5.ForeColor = ThemeColour.PrimaryColor;
            OnlyNewCheckBox.ForeColor = ThemeColour.SecondaryColor;
        }


        /// <summary>
        /// Заполнение списка стран
        /// </summary>
        private async void FillInCountry()
        {

            var country = Language.MakeCountry();
            comboBox1.DataSource = country.MyList;
            comboBox1.DisplayMember = "Country";
            comboBox1.ValueMember = "Symbol";
        }

        /// <summary>
        /// Кнопка search клик
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && !ArchiveSearchCheckBox.Checked)
            {
                MessageBox.Show("Строка для поиска пустая");
                return;
            }
            sameJson = false;
            if (ArchiveSearchCheckBox.Checked)
            {
                SearchTitleInArchive(textBox1.Text);
            }
            else
            {
                AdvancedSearchInput adv = new AdvancedSearchInput();
                adv.Title = textBox1.Text;
                activeJson = Converter.AdvToData(await _api.AdvancedSearchAsync(adv), "");
            }

            //api.SearchMovieAsync("expression");
            //ai.SearchSeriesAsync("expression");

            UpdateListOfMeta(activeJson);

            listBox1.DataSource = activeJson.Results;
            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "Id";
        }

        /// <summary>
        /// Осуществляет поиск по странам и датам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var fromD = dateFrom.Text.Split(".");
            var toD = dateTo.Text.Split(".");
            if (string.IsNullOrEmpty(fromD[2]) || string.IsNullOrEmpty(fromD[1]) || string.IsNullOrEmpty(toD[2]) || string.IsNullOrEmpty(toD[1]))
            {
                MessageBox.Show("Не введены даты для поиска релизов");
                return;
            }
            // &release_date=2023-04-01,2023-06-01
            sameJson = false;
            string countryLang = comboBox1.SelectedValue.ToString();
            Window_Loaded(sender, e, countryLang, fromD, toD);
        }

        private async void Window_Loaded(object sender, EventArgs e, string language, string[] fromD, string[] toD)
        {
            try
            {
                string uriDate = $"&release_date={fromD[2]}-{fromD[1]}-10,{toD[2]}-{toD[1]}-10";

                activeJson = await GetReleaseTitles(uriDate, language);
                // Bind the titles to the ListBox control
                listBox1.DataSource = activeJson.Results;
                listBox1.DisplayMember = "Title";
                listBox1.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async Task<FilmData> GetReleaseTitles(string uriDate, string language)
        {

            using (HttpClient client = new HttpClient())
            {
                //HttpResponseMessage response = await client.GetAsync(apiUrl+"ru");

                //if (response.IsSuccessStatusCode)
                try
                {
                    //string responseJson = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    releases = await client.GetStringAsync(apiUrlForNew + uriDate + "&countries=" + language);

                    //SaveReleasesToExcel(releases);
                    var a = JsonConvert.DeserializeObject<FilmData>(releases);
                    //JsonConvert.DeserializeObject<AdvancedSearchData>(releases);
                    foreach (var item in a.Results)
                    {
                        item.LocationSearch = Language.countryCodeDictionary[language];
                    }
                    return a;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return new FilmData();
                    //MessageBox.Show("Failed to retrieve release information. Please try again.");
                }
                //return new AdvancedSearchData();
            }
        }

        /// <summary>
        /// Реализует сохранение мета сессии или всех в файл EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson));
            ExcelPlace.SaveReleasesToExcel(savedJson, OnlyNewCheckBox.Checked, newAddedJson);
        }

        /// <summary>
        /// Добавляет тайтл в БД, если выбрано "все", то добавляет все
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string sameFilms = "";
            savedJson = HardTool.GetSavedJson();
            if (AllAddCheckBox.Checked && !sameJson)
            {
                foreach (var el in activeJson.Results)
                {
                    sameFilms += AddNewItemToJson(el);
                }
            }
            else if (!sameJson)
            {
                //Прописать логику добавления в excel и файл json
                var y = activeJson.Results[listBox1.SelectedIndex];
                sameFilms += AddNewItemToJson(y);
            }
            else
            {
                MessageBox.Show("Ты хочешь добавить уже добавленное?", "Зачем?");
                return;
            }

            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson));
            if (!string.IsNullOrEmpty(sameFilms))
            {
                //MessageBox.Show("Фильмы которые уже добавленны ранее:\n" + sameFilms.Substring(0,sameFilms.Length - 1));
                var answer = FlexibleMessageBox.Show("Фильмы которые уже были добавленны ранее: " + sameFilms.Substring(0, sameFilms.Length - 1),
                                        "Обновить метаданные?",
                                        buttons: MessageBoxButtons.YesNo);
                //TODO : дописать обновление уже внесенных до этого тайтлов
            }
            MessageBox.Show("Добавлено");
        }

        private string AddNewItemToJson(JsonData a)
        {
            string sameFilm = "";
            int indexO = savedJson.Results.FindIndex(f => f.Id == a.Id);
            if (indexO == -1)
            {
                savedJson.Results.Add(a);
                newAddedJson.Add(a.Id);
            }
            else if (!string.IsNullOrEmpty(a.LocationSearch))
            {
                if (!savedJson.Results[indexO].LocationSearch.Contains(a.LocationSearch))
                {
                    string.Concat(savedJson.Results[indexO].LocationSearch, "/" + a.LocationSearch);
                }
                sameFilm = "\n" + a.Id + "_" + a.Title + ";";
            }
            else sameFilm = "\n" + a.Id + "_" + a.Title + ";";
            return sameFilm;
        }

        /// <summary>
        /// Дает возможность посмотреть на архив
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //прописать логику отображения всех фалов олда
            sameJson = true;
            savedJson = HardTool.GetSavedJson();
            activeJson = savedJson;

            UpdateListOfMeta(activeJson);
        }

        /// <summary>
        /// Поиск по архиву ДЖИСОНА
        /// </summary>
        /// <param name="title"></param>
        private void SearchTitleInArchive(string title)
        {
            activeJson = new FilmData();
            activeJson.Results = new List<JsonData>();
            savedJson = HardTool.GetSavedJson();
            List<JsonData> ab = savedJson.Results.FindAll(f => f.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase));
            foreach (var elem in ab)
            {
                activeJson.Results.Add(elem);
            }
        }

        /// <summary>
        /// Раскрывает инфу в описаниии выбранного тайтла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var i = listBox1.SelectedIndex;
                label2.Text = $"ID:             {activeJson.Results[i].Id}\n" +
                              $"Title:          {activeJson.Results[i].Title}\n" +
                              $"Type:           {activeJson.Results[i].Type}\n" +
                              $"Year:           {activeJson.Results[i].Year}\n" +
                              $"Release date:   {activeJson.Results[i].ReleaseDate}\n" +
                              $"RunTime Mins:   {activeJson.Results[i].RuntimeStr}\n" +
                              $"Rating:         {activeJson.Results[i].IMDbRating}\n" +
                              $"Genres:         {activeJson.Results[i].Genres}\n" +
                              $"Languages:      {activeJson.Results[i].Languages}\n" +
                              $"Countries of orgin: {activeJson.Results[i].Countries}\n" +
                              $"Describtion:    {activeJson.Results[i].Description}\n" +
                              $"Release country:{activeJson.Results[i].LocationSearch}\n" +
                              $"Directors:      {activeJson.Results[i].Directors}\n" +
                              $"Stars:          {activeJson.Results[i].Stars}";
                label3.Text = activeJson.Results[i].Plot;
                linkLabel1.Text = activeJson.Results[i].Image;
                pictureBox1.ImageLocation = activeJson.Results[i].Image;
            }
            catch (Exception ex)
            {
                new AdvancedSearchData
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Получение из категории coming soon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CmngSoonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var release = await ComingSoonAsync();
                activeJson = Converter.NewMovieToData(release);

                listBox1.DataSource = activeJson.Results;
                listBox1.DisplayMember = "Title";
                listBox1.ValueMember = "Id";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<NewMovieData> ComingSoonAsync()
        {
            HttpClient client = new HttpClient();
            try
            {
                return await _api.ComingSoonAsync();
            }
            catch (Exception ex)
            {
                return new NewMovieData
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Добавляем инфо в тайтл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchTit_Click(object sender, EventArgs e)
        {
            try
            {
                var i = listBox1.SelectedIndex;
                FilmData info;
                if (i != -1)
                {
                    info = Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + activeJson.Results[i].Id));
                    foreach (var item in info.Results)
                    {
                        SearchTitAdditionalInfoAdd(item);
                    }
                }
                else
                {
                    MessageBox.Show("Никакой тайтл не выбран");
                    return;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SearchTitAdditionalInfoAdd(JsonData item)
        {
            var indexO = savedJson.Results.FindIndex(f => f.Id == item.Id);
            if (indexO == -1)
            {
                savedJson.Results.Add(item);
            }
            else
            {
                savedJson.Results[indexO].Type = item.Type;
                savedJson.Results[indexO].Year = item.Year;
                savedJson.Results[indexO].ReleaseDate = item.ReleaseDate;
                savedJson.Results[indexO].RuntimeStr = item.RuntimeStr;
                savedJson.Results[indexO].Awards = item.Awards;
                savedJson.Results[indexO].Directors = item.Directors;
                savedJson.Results[indexO].Companies = item.Companies;
                savedJson.Results[indexO].Languages = item.Languages;
            }

            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson));
        }

        /// <summary>
        /// Копирует ссылку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text);
        }

        /// <summary>
        /// Вторая формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scndForm_Click(object sender, EventArgs e)
        {

            Form2 fr2 = new Form2();
            fr2.Show();

            fr2.Location = new Point(fr2.Left = locationForm["x"] + locationForm["width"] + SystemInformation.BorderSize.Width, locationForm["y"]);
        }

        public void ChangeSourceList(string path)
        {
            activeJson = JsonConvert.DeserializeObject<FilmData>(File.ReadAllText(path));

            UpdateListOfMeta(activeJson);
        }

        private void UpdateListOfMeta(FilmData jsonnn)
        {
            try
            {
                listBox1.DataSource = jsonnn.Results;
                listBox1.DisplayMember = "Title";
                listBox1.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnClearAchive_Click(object sender, EventArgs e)
        {
            var a = MessageBox.Show("Точно архивировать рабочую смету?", "Архивация", MessageBoxButtons.OKCancel);
            if (a == DialogResult.OK)
            {
                HardTool.MakeAnArchive();
            }
            savedJson = HardTool.GetSavedJson();
            activeJson = savedJson;
            UpdateListOfMeta(activeJson);
        }
    }
}