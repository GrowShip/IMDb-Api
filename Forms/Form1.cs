using IMDbApiLib;
using IMDbApiLib.Models;
using TMDbLib;
using TMDbLib.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.ComponentModel;
using System.Net;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using JR.Utils.GUI.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml.DataValidation;

namespace IMDbApi
{
    //Non-commercial WPG to get new releases and other information using IMDB APi
    //https://imdb-api.com/API/AdvancedSearch/
    //title_type=tv_movie,tv_series,tv_episode,documentary,video&
    //release_date=2023-04-10,2023-06-10&
    //countries=af&
    //languages=eu,ca,zh,eo,fr,de,hi,ko,kvk,ru,rsl,es,ssp,ta,te,tr
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillInCountry();
            _api = new ApiLib(KeysAccess.GetRandomValue());
            _apiTMDB = new TMDbLib.Client.TMDbClient(KeysAccess.GetKeyTMDB());
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tipsForm = new System.Windows.Forms.ToolTip();
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
            tipsForm.SetToolTip(MmFromText, "Месяц начала интервала");
            tipsForm.SetToolTip(YyFromText, "Год начала интервала");
            tipsForm.SetToolTip(MmToText, "Месяц конца интервала");
            tipsForm.SetToolTip(YyToText, "Год конца интервала");
            GetSavedJson();
        }

        public WebProxy WebProxy { get; }
        private ApiLib _api;
        private TMDbLib.Client.TMDbClient _apiTMDB;
        private string releases;
        private FilmData activeJson;
        private List<string> newAddedJson = new List<string>();
        private FilmData savedJson;
        private Boolean sameJson;

        private string apiUrlForNew = $"https://imdb-api.com/API/AdvancedSearch/{KeysAccess.GetRandomValue()}/?title_type=tv_movie,tv_series,tv_episode,documentary,video";
        private string TitleUrl = $"https://imdb-api.com/en/API/Title/{KeysAccess.GetRandomValue()}/";
        private string UpComingUrl = $"https://imdb-api.com/en/API/ComingSoon/{KeysAccess.GetRandomValue()}";

        //private const string apiUrl = "https://imdb-api.com/ru/API/ComingSoon/k_yl5q767w";
        //private const string apiUrl = "https://imdb-api.com/ru/API/ComingSoon/k_pj17x8n5";

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

            listBox1.DataSource = activeJson.Results;
            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "Id";
        }

        /// <summary>
        /// Поиск по архиву ДЖИСОНА
        /// </summary>
        /// <param name="title"></param>
        private void SearchTitleInArchive(string title)
        {
            activeJson = new FilmData();
            activeJson.Results = new List<JsonData>();
            GetSavedJson();
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
                label2.Text = $"Type:   {activeJson.Results[i].Type} \n" +
                              $"Title:  {activeJson.Results[i].Title} \n" +
                              $"Rating: {activeJson.Results[i].ContentRating} \n" +
                              $"Genres: {activeJson.Results[i].Genres} \n" +
                              $"Describtion:     {activeJson.Results[i].Description} \n" +
                              $"Release date:    {activeJson.Results[i].ReleaseDate} \n" +
                              $"Release country: {activeJson.Results[i].LocationSearch}";
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
        /// Осуществляет поиск по странам и датам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MmFromText.Text) || string.IsNullOrEmpty(YyFromText.Text) || string.IsNullOrEmpty(MmToText.Text) || string.IsNullOrEmpty(YyToText.Text))
            {
                MessageBox.Show("Не введены даты для поиска релизов");
                return;
            }
            // &release_date=2023-04-01,2023-06-01
            sameJson = false;
            string countryLang = comboBox1.SelectedValue.ToString();
            Window_Loaded(sender, e, countryLang);
        }

        private async void Window_Loaded(object sender, EventArgs e, string language)
        {
            try
            {
                string uriDate = $"&release_date={YyFromText.Text}-{MmFromText.Text}-10,{YyToText.Text}-{MmToText.Text}-10";

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

        public async Task<NewMovieData> ComingSoonAsync()
        {
            HttpClient client = new HttpClient();
            try
            {
                return await ApiUtils.GetObjectAsync<NewMovieData>(JsonConvert.DeserializeObject<string>(await client.GetStringAsync(apiUrlForNew)));
            }
            catch (Exception ex)
            {
                return new NewMovieData
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        private void SaveReleasesToExcel()
        {
            SaveJson(JsonConvert.SerializeObject(savedJson));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("All");

                // Set column headers
                worksheet.Cells[1, 1].Value = "Type";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 4].Value = "Genre";
                worksheet.Cells[1, 5].Value = "Country";
                worksheet.Cells[1, 6].Value = "Year";
                worksheet.Cells[1, 7].Value = "Rating IMDB";
                worksheet.Cells[1, 10].Value = "Release Date";
                worksheet.Cells[1, 11].Value = "Release Country";
                worksheet.Cells[1, 12].Value = "Snopsys";
                worksheet.Cells[1, 13].Value = "Id";
                worksheet.Cells[1, 14].Value = "Runtime";
                worksheet.Cells[1, 15].Value = "Description";
                worksheet.Cells[1, 16].Value = "Directors";
                worksheet.Cells[1, 17].Value = "Stars";

                // Parse the JSON response and populate the Excel worksheet
                // Replace this code with your own JSON parsing logic
                // Here, we assume the JSON response is an array of objects,
                // each representing a release with 'name', 'year', 'synopsis', and 'other' properties

                //AdvancedSearchData releases = JsonConvert.DeserializeObject<AdvancedSearchData>(responseJson);
                int row = 2;
                if (OnlyNewCheckBox.Checked)
                {
                    try
                    {
                        foreach (var release in newAddedJson)
                        {
                            worksheet.Cells[row, 1].Value = savedJson.Results.Find(f => f.Id == release).Type;
                            worksheet.Cells[row, 2].Value = savedJson.Results.Find(f => f.Id == release).Title;
                            worksheet.Cells[row, 4].Value = savedJson.Results.Find(f => f.Id == release).Genres;
                            worksheet.Cells[row, 5].Value = savedJson.Results.Find(f => f.Id == release).LocationSearch;
                            worksheet.Cells[row, 6].Value = savedJson.Results.Find(f => f.Id == release).Year;
                            worksheet.Cells[row, 7].Value = savedJson.Results.Find(f => f.Id == release).IMDbRating;
                            worksheet.Cells[row, 10].Value = savedJson.Results.Find(f => f.Id == release).ReleaseDate;
                            worksheet.Cells[row, 11].Value = savedJson.Results.Find(f => f.Id == release).Countries;
                            worksheet.Cells[row, 12].Value = savedJson.Results.Find(f => f.Id == release).Plot;
                            worksheet.Cells[row, 13].Value = savedJson.Results.Find(f => f.Id == release).Id;
                            worksheet.Cells[row, 14].Value = savedJson.Results.Find(f => f.Id == release).RuntimeStr;
                            worksheet.Cells[row, 15].Value = savedJson.Results.Find(f => f.Id == release).Description;
                            worksheet.Cells[row, 16].Value = savedJson.Results.Find(f => f.Id == release).Directors;
                            worksheet.Cells[row, 17].Value = savedJson.Results.Find(f => f.Id == release).Stars;
                            row++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Я не смог добавить: \r" + ex.Message);
                        throw;
                    }
                }
                else
                {
                    foreach (var release in savedJson.Results)
                    {

                        worksheet.Cells[row, 1].Value = release.Type;
                        worksheet.Cells[row, 2].Value = release.Title;
                        worksheet.Cells[row, 4].Value = release.Genres;
                        worksheet.Cells[row, 5].Value = release.LocationSearch;
                        worksheet.Cells[row, 6].Value = release.Year;
                        worksheet.Cells[row, 7].Value = release.IMDbRating;
                        worksheet.Cells[row, 10].Value = release.ReleaseDate;
                        worksheet.Cells[row, 11].Value = release.Countries;
                        worksheet.Cells[row, 12].Value = release.Plot;
                        worksheet.Cells[row, 13].Value = release.Id;
                        worksheet.Cells[row, 14].Value = release.RuntimeStr;
                        worksheet.Cells[row, 15].Value = release.Description;
                        worksheet.Cells[row, 17].Value = release.Stars;
                        row++;
                    }
                }


                // Save the Excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save Releases";
                saveFileDialog.ShowDialog();

                if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    FileInfo file = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(file);
                    MessageBox.Show("Releases saved successfully!");
                }
            }
        }

        public static void SaveJson(string json)
        {
            try
            {
                File.WriteAllText(@"JSON\file.json", json);
                Console.WriteLine("JSON file saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON file: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveJson(JsonConvert.SerializeObject(savedJson));
            SaveReleasesToExcel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sameFilms = "";
            GetSavedJson();
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
                MessageBox.Show("Ты хочешь добавить уже добавленное?");
                return;
            }
            SaveJson(JsonConvert.SerializeObject(savedJson));
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

        private void GetSavedJson()
        {
            string json = "";
            if (File.Exists(@"JSON\file.json"))
            {
                json = File.ReadAllText(@"JSON\file.json");
            }
            else
            {
                string path = @"JSON";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                MessageBox.Show("Архивного файла нет");
            }

            //SaveReleasesToExcel(json);
            if (!string.IsNullOrEmpty(json))
            {
                savedJson = JsonConvert.DeserializeObject<FilmData>(json);
            }
            else
            {
                savedJson = new FilmData();
                savedJson.Results = new List<JsonData>();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //прописать логику отображения всех фалов олда
            sameJson = true;
            GetSavedJson();
            activeJson = savedJson;

            try
            {
                listBox1.DataSource = savedJson.Results;
                listBox1.DisplayMember = "Title";
                listBox1.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

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

            SaveJson(JsonConvert.SerializeObject(savedJson));
        }

        private void MmFromText_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var release = JsonConvert.DeserializeObject<NewMovieData>(await ApiUtils.GetStringAsync(UpComingUrl));
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

        private void scndForm_Click(object sender, EventArgs e)
        {
            Forms.Form2 fr2 = new Forms.Form2();
            fr2.Show();
            fr2.Location = new Point(fr2.Left = this.Right + SystemInformation.BorderSize.Width, this.Location.Y);
        }
    }
}