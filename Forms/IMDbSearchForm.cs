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
using TMDbLib.Objects.Languages;
using MediaApi.Structure;

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
        /// �������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tipsForm = new System.Windows.Forms.ToolTip();
            LoadTheme();
            textBox1.PlaceholderText = "IMDb search (eng only)";
            tipsForm.SetToolTip(button2, "����� ���� � ��������� ��������� ��� �� ���������� ������");
            tipsForm.SetToolTip(button3, "���������� ����� ������ ��� ������ �����(�� ��� ������) ���������� � excel");
            tipsForm.SetToolTip(button4, "���������� ���������� ���� �������� ������� � ����� ���� ���� � �����");
            tipsForm.SetToolTip(button5, "����������� ���� ���������� ������������ � ������");
            tipsForm.SetToolTip(listBox1, "���� ��� ����������� ���������� ������ ��� �������� �������");
            tipsForm.SetToolTip(pictureBox1, "������");
            tipsForm.SetToolTip(OnlyNewCheckBox, "���������� ��� ��������� excel ����� � ���� ������� ������� ���� ������");
            tipsForm.SetToolTip(AllAddCheckBox, "���������� �������� ����� �������� ���� ������ ��������� ������� � �����");
            tipsForm.SetToolTip(textBox1, "������������ ��� ������ �� IMDB");
            tipsForm.SetToolTip(cmbCountry, "����� ������ ��� ������ ����� �������");
            tipsForm.SetToolTip(ArchiveSearchCheckBox, "��������� ��� ������ ������ � ������");
            tipsForm.SetToolTip(SearchTit, "��������� ���, ���, ���� ������, �����, ��������� � ������ ��� ���������� ������");
            tipsForm.SetToolTip(SearchButton, "������ ��� ������ �� IMDB ��� ������");
            tipsForm.SetToolTip(dateFrom, "���� ������ ���������");
            tipsForm.SetToolTip(dateTo, "���� �������� ���������");
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
            label4.ForeColor = ThemeColour.SecondaryColor;
            label5.ForeColor = ThemeColour.PrimaryColor;
            OnlyNewCheckBox.ForeColor = ThemeColour.SecondaryColor;
        }


        /// <summary>
        /// ���������� ������ �����
        /// </summary>
        private async void FillInCountry()
        {

            var country = Language.MakeCountry();
            cmbCountry.DataSource = country.MyList;
            cmbCountry.DisplayMember = "Country";
            cmbCountry.ValueMember = "Symbol";
        }

        /// <summary>
        /// ������ search ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && !ArchiveSearchCheckBox.Checked)
            {
                MessageBox.Show("������ ��� ������ ������");
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
        /// ������������ ����� �� ������� � �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button2_Click(object sender, EventArgs e)
        {
            var fromD = dateFrom.Text.Split(".");
            var toD = dateTo.Text.Split(".");
            if (string.IsNullOrEmpty(fromD[2]) || string.IsNullOrEmpty(fromD[1]) || string.IsNullOrEmpty(toD[2]) || string.IsNullOrEmpty(toD[1]))
            {
                MessageBox.Show("�� ������� ���� ��� ������ �������");
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
                try
                {

                    // Parse the JSON response
                    releases = await client.GetStringAsync(apiUrlForNew + uriDate + "&countries=" + language);

                    //SaveReleasesToExcel(releases);
                    var a = JsonConvert.DeserializeObject<FilmData>(releases);

                    foreach (var item in a.Results)
                    {
                        item.Countries = Language.countryCodeDictionary[language];
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
        /// ��������� ���������� ���� ������ ��� ���� � ���� EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
            ExcelPlace.SaveReleasesToExcel(savedJson, OnlyNewCheckBox.Checked, newAddedJson);
        }

        /// <summary>
        /// ��������� ����� � ��, ���� ������� "���", �� ��������� ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
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
                //��������� ������ ���������� � excel � ���� json
                var y = activeJson.Results[listBox1.SelectedIndex];
                sameFilms += AddNewItemToJson(y);
            }
            else
            {
                MessageBox.Show("�� ������ �������� ��� �����������?", "�����?");
                AllAddCheckBox.Checked = false;
                return;
            }

            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
            if (!string.IsNullOrEmpty(sameFilms))
            {
                //MessageBox.Show("������ ������� ��� ���������� �����:\n" + sameFilms.Substring(0,sameFilms.Length - 1));
                var answer = FlexibleMessageBox.Show("������ ������� ��� ���� ���������� �����: " + sameFilms.Substring(0, sameFilms.Length - 1),
                                        "�������� ����������?",
                                        buttons: MessageBoxButtons.YesNo);
                //TODO : �������� ���������� ��� ��������� �� ����� �������
                if (answer == DialogResult.Yes)
                {
                    UpdateSimilar(sameFilms.Substring(0, sameFilms.Length - 1).Split(";"));
                }
            }
            AllAddCheckBox.Checked = false;
            MessageBox.Show("���������");
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
        /// ���� ����������� ���������� �� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //��������� ������ ����������� ���� ����� ����
            sameJson = true;
            savedJson = HardTool.GetSavedJson("imdb");
            activeJson = savedJson;

            UpdateListOfMeta(activeJson);
        }

        /// <summary>
        /// ����� �� ������ �������
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
        /// ���������� ���� � ��������� ���������� ������
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
                              $"Rating:         {activeJson.Results[i].ContentRating}\n" +
                              $"Genres:         {activeJson.Results[i].Genres}\n" +
                              $"Languages:      {activeJson.Results[i].Languages}\n" +
                              $"Countries of orgin: {activeJson.Results[i].Countries}\n" +
                              $"Release in country: {activeJson.Results[i].LocationSearch}\n" +
                              $"Directors:      {activeJson.Results[i].Directors}\n" +
                              $"Stars:          {activeJson.Results[i].Stars}";
                //$"Describtion:    {activeJson.Results[i].Description}\n" +
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
        /// ��������� �� ��������� coming soon
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
        /// ��������� ���� � �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchTit_Click(object sender, EventArgs e)
        {
            try
            {
                var i = listBox1.SelectedIndex;
                FilmData info;
                if (AllAddCheckBox.Checked)
                {
                    if (savedJson.Results == null || savedJson.Results.Count() < 1)
                    {
                        //info = Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + item.Id));
                        foreach (JsonData el in activeJson.Results)
                        {
                            savedJson.Results.Add(el);
                        }
                        HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
                    }
                    else
                    {
                        foreach (var item in savedJson.Results)
                        {
                            info = await GetInfoIDAsync(item.Id);
                            SearchTitAdditionalInfoAdd(info.Results[0]);
                        }
                    }
                }
                else if (i != -1)
                {
                    info = await GetInfoIDAsync(activeJson.Results[i].Id);
                    SearchTitAdditionalInfoAdd(info.Results[0]);
                }
                else
                {
                    MessageBox.Show("������� ����� �� ������");
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
            AllAddCheckBox.Checked = false;

        }

        /// <summary>
        /// �������� ��� �������� titla �� ID imdb
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<FilmData> GetInfoIDAsync(string id)
        {
            return Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + id));
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
            }
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text);
        }

        /// <summary>
        /// ������ �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scndForm_Click(object sender, EventArgs e)
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
                listBox1.DataSource = activeJson.Results;
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
            var a = MessageBox.Show("����� ������������ ������� �����?", "���������", MessageBoxButtons.OKCancel);
            if (a == DialogResult.OK)
            {
                HardTool.MakeAnArchive("imdb");
            }
            savedJson = HardTool.GetSavedJson("imdb");
            activeJson = savedJson;
            UpdateListOfMeta(activeJson);
            if (activeForm2 != null) IMDbArchiveForm.instance.UpdateList();
        }

        /// <summary>
        /// ��� ���������� ������� ����������
        /// </summary>
        public void UpdateSameJson(bool availible)
        {
            sameJson = availible;
        }

        /// <summary>
        /// ����� ������� � ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInCountryRls_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbCountry.Text))
            {
                MessageBox.Show("������ ������");
                return;
            }

            UpdateSameJson(false);
            string country = cmbCountry.SelectedValue.ToString();
            CalendarData cdD = await CalendarIMDB.GetIMDBCalendarAsync(country);
            FilmData newJ = Converter.CalendarToData(cdD, Language.countryCodeDictionary[country]);

            UpdateListOfMeta(newJ);
        }
    }
}