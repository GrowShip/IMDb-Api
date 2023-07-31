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
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml.DataValidation;
using Button = System.Windows.Forms.Button;
using MediaApi.IMDB;
using MediaApi.Structure;
using System.Xml.Serialization;

namespace MediaApi.Forms
{
    //Non-commercial WinForms to get new releases and other information using IMDB APi
    //https://imdb-api.com/API/AdvancedSearch/
    //title_type=tv_movie,tv_series,tv_episode,documentary,video&
    //release_date=2023-04-10,2023-06-10&
    //countries=af&
    //languages=eu,ca,zh,eo,fr,de,hi,ko,kvk,ru,rsl,es,ssp,ta,te,tr
    public partial class IMDbSearchForm : Form
    {
        public readonly Dictionary<string, int> locationForm = new Dictionary<string, int>();
        public static IMDbSearchForm instance;

        public IMDbSearchForm(Dictionary<string, int> data)
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
            txtBoxSearch.PlaceholderText = "IMDb search (eng only)";
            tipsForm.SetToolTip(btnSrchCountryDate, "Поиск сета в выбранном диапозоне дат по выбрранное стране");
            tipsForm.SetToolTip(btnExcelSave, "Сохранение всего архива или только новых(за эту сессию) метаданных в excel");
            tipsForm.SetToolTip(btnAddTitle, "Добавление метаданных либо выбраной позиции в листе либо всех в архив");
            tipsForm.SetToolTip(btnFromArchive, "Отображение всех метаданных содержащихся в архиве");
            tipsForm.SetToolTip(listTitles, "Поле для отображения информации поиска или архивных записей");
            tipsForm.SetToolTip(pictPoster, "Постер");
            tipsForm.SetToolTip(OnlyNewCheckBox, "Необходимо для получения excel файла с мета данными тайтлов этой сессии");
            tipsForm.SetToolTip(AllAddCheckBox, "Необходимо выделить чтобы добавить весь список найденных позиций в архив");
            tipsForm.SetToolTip(txtBoxSearch, "Наименование для поиска по IMDB");
            tipsForm.SetToolTip(cmbCountry, "Выбор страны для поиска новых релизов");
            tipsForm.SetToolTip(ArchiveSearchCheckBox, "Неохоидмо для поиска тайтла в архиве");
            tipsForm.SetToolTip(btnSearchExtension, "Добавляет тип, год, дату релиза, языки, режиссера и другое для выбранного тайтла");
            tipsForm.SetToolTip(btnSearch, "Кнопка для поиска по IMDB или архиву");
            tipsForm.SetToolTip(dateFrom, "Дата начала интервала");
            tipsForm.SetToolTip(dateTo, "Дата конечная интервала");
            savedJson = HardTool.GetSavedJson("imdb");
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
        public Form activeForm2;

        private string apiUrlForNew = $"https://imdb-api.com/API/AdvancedSearch/{KeysAccess.GetRandomValue()}/?title_type=tv_movie,tv_series,tv_episode,documentary,video";
        private static string TitleUrl = $"https://imdb-api.com/en/API/Title/{KeysAccess.GetRandomValue()}/";

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
            AllAddCheckBox.ForeColor = ThemeColour.SecondaryColor;
            lblCountry.ForeColor = ThemeColour.SecondaryColor;
            lblReleaseDate.ForeColor = ThemeColour.PrimaryColor;
            OnlyNewCheckBox.ForeColor = ThemeColour.SecondaryColor;
        }


        /// <summary>
        /// Заполнение списка стран
        /// </summary>
        private async void FillInCountry()
        {

            var country = Structure.Language.MakeCountry();
            cmbCountry.DataSource = country.MyList;
            cmbCountry.DisplayMember = "Country";
            cmbCountry.ValueMember = "Symbol";
        }

        /// <summary>
        /// Кнопка search клик
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxSearch.Text) && !ArchiveSearchCheckBox.Checked)
            {
                MessageBox.Show("Строка для поиска пустая");
                return;
            }
            sameJson = false;
            if (ArchiveSearchCheckBox.Checked)
            {
                SearchTitleInArchive(txtBoxSearch.Text);
            }
            else
            {
                AdvancedSearchInput adv = new AdvancedSearchInput();
                adv.Title = txtBoxSearch.Text;
                activeJson = Converter.AdvToData(await _api.AdvancedSearchAsync(adv), "");
            }

            //api.SearchMovieAsync("expression");
            //ai.SearchSeriesAsync("expression");

            UpdateListOfMeta(activeJson);

            listTitles.DataSource = activeJson.Results;
            listTitles.DisplayMember = "Title";
            listTitles.ValueMember = "Id";
        }

        /// <summary>
        /// Осуществляет поиск по странам и датам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSrchCountryDate_Click(object sender, EventArgs e)
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
            string countryLang = cmbCountry.SelectedValue.ToString();

            string uriDate = $"&release_date={fromD[2]}-{fromD[1]}-10,{toD[2]}-{toD[1]}-10";

            activeJson = await GetReleaseTitles(uriDate, countryLang);

            UpdateListOfMeta(activeJson);
            //Window_Loaded(sender, e, countryLang, fromD, toD);
        }

        //private async void Window_Loaded(object sender, EventArgs e, string language, string[] fromD, string[] toD)
        //{
        //    try
        //    {
        //        string uriDate = $"&release_date={fromD[2]}-{fromD[1]}-10,{toD[2]}-{toD[1]}-10";

        //        activeJson = await GetReleaseTitles(uriDate, language);
        //        // Bind the titles to the ListBox control
        //        listBox1.DataSource = activeJson.Results;
        //        listBox1.DisplayMember = "Title";
        //        listBox1.ValueMember = "Id";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}");
        //    }
        //}

        private async Task<FilmData> GetReleaseTitles(string uriDate, string language)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    // Parse the JSON response
                    releases = await client.GetStringAsync(apiUrlForNew + uriDate + "&countries=" + language);

                    //SaveReleasesToExcel(releases);
                    var a = JsonConvert.DeserializeObject<FilmData>(releases);

                    foreach (var item in a.Results)
                    {
                        item.Countries = Structure.Language.countryCodeDictionary[language];
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
        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
            ExcelPlace.SaveReleasesToExcel(savedJson, OnlyNewCheckBox.Checked, newAddedJson);
        }

        /// <summary>
        /// Добавляет тайтл в БД, если выбрано "все", то добавляет все
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTitle_Click(object sender, EventArgs e)
        {
            string sameFilms = "";
            savedJson = HardTool.GetSavedJson("imdb");
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
                var y = activeJson.Results[listTitles.SelectedIndex];
                sameFilms += AddNewItemToJson(y);
            }
            else
            {
                MessageBox.Show("Ты хочешь добавить уже добавленное?", "Зачем?");
                AllAddCheckBox.Checked = false;
                return;
            }

            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
            if (!string.IsNullOrEmpty(sameFilms))
            {
                //MessageBox.Show("Фильмы которые уже добавленны ранее:\n" + sameFilms.Substring(0,sameFilms.Length - 1));
                var answer = FlexibleMessageBox.Show("Фильмы которые уже были добавленны ранее: " + sameFilms.Substring(0, sameFilms.Length - 1),
                                        "Обновить метаданные?",
                                        buttons: MessageBoxButtons.YesNo);
                //TODO : дописать обновление уже внесенных до этого тайтлов
                if (answer == DialogResult.Yes)
                {
                    UpdateSimilar(sameFilms.Substring(0, sameFilms.Length - 1).Split(";"));
                }
            }
            AllAddCheckBox.Checked = false;
            MessageBox.Show("Добавлено");
        }

        private string AddNewItemToJson(JsonData a)
        {
            string sameFilm = "";
            int indexO;

            if (savedJson.Results == null)
            {
                savedJson.Results = new List<JsonData>();
                indexO = -1;
            }
            else indexO = savedJson.Results.FindIndex(f => f.Id == a.Id);


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

        private async void UpdateSimilar(string[] similarTitle)
        {
            for (int i = 0; i < similarTitle.Length; i++)
            {
                string id = similarTitle[i].Split("_")[0].Replace("\n", "");
                FilmData data = await GetInfoIDAsync(id);
                SearchTitAdditionalInfoAdd(data.Results[0]);
            }
        }

        /// <summary>
        /// Дает возможность посмотреть на архив
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFromArchive_Click(object sender, EventArgs e)
        {
            //прописать логику отображения всех фалов олда
            sameJson = true;
            savedJson = HardTool.GetSavedJson("imdb");
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
            savedJson = HardTool.GetSavedJson("imdb");
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
                var i = listTitles.SelectedIndex;
                lblInfo.Text = $"ID:             {activeJson.Results[i].Id}\n" +
                              $"Title:          {activeJson.Results[i].Title}\n" +
                              $"Type:           {activeJson.Results[i].Type}\n" +
                              $"Year:           {activeJson.Results[i].Year}\n" +
                              $"Release date:   {activeJson.Results[i].ReleaseDate}\n" +
                              $"RunTime Mins:   {activeJson.Results[i].RuntimeStr}\n" +
                              $"Rating:         {activeJson.Results[i].ContentRating}\n" +
                              $"Genres:         {activeJson.Results[i].Genres}\n" +
                              $"Languages:      {activeJson.Results[i].Languages}\n" +
                              $"Countries of orgin: {activeJson.Results[i].Countries}\n" +
                              $"Release in country: {activeJson.Results[i].LocationSearch}\n" +
                              $"Directors:      {activeJson.Results[i].Directors}\n" +
                              $"Stars:          {activeJson.Results[i].Stars}";
                //$"Describtion:    {activeJson.Results[i].Description}\n" +
                lblSynopsis.Text = activeJson.Results[i].Plot;
                linkLabel1.Text = activeJson.Results[i].Image;
                pictPoster.ImageLocation = activeJson.Results[i].Image;

                if (activeJson.Results[i].All is not null)
                {
                    lblDates.Text = $"Russia         {activeJson.Results[i].All.RU.releaseDate}\n" +
                                    $"United States  {activeJson.Results[i].All.US.releaseDate}\n" +
                                    $"Germany        {activeJson.Results[i].All.DE.releaseDate}\n" +
                                    $"Italy          {activeJson.Results[i].All.IT.releaseDate}\n" +
                                    $"Spain          {activeJson.Results[i].All.ES.releaseDate}\n" +
                                    $"United Kingdom {activeJson.Results[i].All.GB.releaseDate}\n" +
                                    $"France         {activeJson.Results[i].All.FR.releaseDate}\n" +
                                    $"China          {activeJson.Results[i].All.CN.releaseDate}\n";
                }
                else lblDates.Text = "-";
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

                listTitles.DataSource = activeJson.Results;
                listTitles.DisplayMember = "Title";
                listTitles.ValueMember = "Id";
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
        private async void btnSearchExtension_Click(object sender, EventArgs e)
        {
            try
            {
                var i = listTitles.SelectedIndex;
                FilmData info;
                if (AllAddCheckBox.Checked)
                {
                    if (savedJson.Results == null || savedJson.Results.Count() < 1)
                    {
                        //info = Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + item.Id));
                        foreach (JsonData el in activeJson.Results)
                        {
                            var obj = await UploadReleasesDates(el, el.Id);
                            savedJson.Results.Add(obj);
                        }
                        HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
                    }
                    else
                    {
                        foreach (var item in savedJson.Results)
                        {
                            info = await GetInfoIDAsync(item.Id);
                            var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                            SearchTitAdditionalInfoAdd(obj);
                        }
                    }
                }
                else if (i != -1)
                {
                    info = await GetInfoIDAsync(activeJson.Results[i].Id);
                    var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                    SearchTitAdditionalInfoAdd(obj);
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
            AllAddCheckBox.Checked = false;

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
                savedJson.Results[indexO].ContentRating = item.ContentRating;
                savedJson.Results[indexO].Countries = item.Countries;
                savedJson.Results[indexO].IMDbRating = item.IMDbRating;
                savedJson.Results[indexO].Plot = item.Plot;
                savedJson.Results[indexO].All = item.All;
            }
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
        }

        /// <summary>
        /// Получает все сведения titla по ID imdb
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<FilmData> GetInfoIDAsync(string id)
        {
            return Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + id));
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
        private void btnOpenArchive_Click(object sender, EventArgs e)
        {
            if (activeForm2 == null)
            {
                activeForm2 = new IMDbArchiveForm();
                activeForm2.Show();

                activeForm2.Location = new Point(activeForm2.Left = locationForm["x"] + locationForm["width"] + SystemInformation.BorderSize.Width, locationForm["y"]);
            }
            else IMDbArchiveForm.instance.UpdateList();

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
                activeJson = jsonnn;
                listTitles.DataSource = activeJson.Results;
                listTitles.DisplayMember = "Title";
                listTitles.ValueMember = "Id";
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
                HardTool.MakeAnArchive("imdb");
                sameJson = false;
            }
            savedJson = HardTool.GetSavedJson("imdb");
            activeJson = savedJson;
            UpdateListOfMeta(activeJson);
            if (activeForm2 != null) IMDbArchiveForm.instance.UpdateList();
        }

        /// <summary>
        /// Для обновления функции добавления
        /// </summary>
        public void UpdateSameJson(bool availible)
        {
            sameJson = availible;
        }

        /// <summary>
        /// Поиск релизов В стране
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInCountryRls_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbCountry.Text))
            {
                MessageBox.Show("Выбери страну");
                return;
            }

            UpdateSameJson(false);
            string country = cmbCountry.SelectedValue.ToString();
            CalendarData cdD = await CalendarIMDB.GetIMDBCalendarAsync(country, "Movie");
            FilmData newJ = Converter.CalendarToData(cdD, Structure.Language.countryCodeDictionary[country]);

            UpdateListOfMeta(newJ);
        }

        /// <summary>
        /// Догружает даты релизов в разных странах
        /// </summary>
        /// <param name="date"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private async Task<JsonData> UploadReleasesDates(JsonData date, string title)
        {
            CalendarD initital = await ReleasesDates.GetReleasesToData(title);
            date = Converter.EnterReleasesDates(initital, date);
            return date;
        }
    }
}