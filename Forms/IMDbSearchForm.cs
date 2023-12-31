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
using System.Reflection;
using System.Text;

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
            txtBoxSearch.PlaceholderText = "IMDb search (eng only)";
            tipsForm.SetToolTip(btnSrchCountryDate, "����� ���� � ��������� ��������� ��� �� ���������� ������");
            tipsForm.SetToolTip(btnExcelSave, "���������� ����� ������ ��� ������ �����(�� ��� ������) ���������� � excel");
            tipsForm.SetToolTip(btnAddTitle, "���������� ���������� ���� �������� ������� � ����� ���� ���� � �����");
            tipsForm.SetToolTip(btnFromArchive, "����������� ���� ���������� ������������ � ������");
            tipsForm.SetToolTip(listTitles, "���� ��� ����������� ���������� ������ ��� �������� �������");
            tipsForm.SetToolTip(pictPoster, "������");
            tipsForm.SetToolTip(OnlyNewCheckBox, "���������� ��� ��������� excel ����� � ���� ������� ������� ���� ������");
            tipsForm.SetToolTip(AllAddCheckBox, "���������� �������� ����� �������� ���� ������ ��������� ������� � �����");
            tipsForm.SetToolTip(txtBoxSearch, "������������ ��� ������ �� IMDB");
            tipsForm.SetToolTip(cmbCountry, "����� ������ ��� ������ ����� �������");
            tipsForm.SetToolTip(ArchiveSearchCheckBox, "���������� ��� ������ ������ � ������");
            tipsForm.SetToolTip(btnSearchExtension, "��������� ���, ���, ���� ������, �����, ��������� � ������ ��� ���������� ������");
            tipsForm.SetToolTip(btnSearch, "������ ��� ������ �� IMDB ��� ������");
            tipsForm.SetToolTip(dateFrom, "���� ������ ���������");
            tipsForm.SetToolTip(dateTo, "���� �������� ���������");
            savedJson = HardTool.GetSavedJson("imdb");
        }

        #region Global variables

        public WebProxy WebProxy { get; }
        private ApiLib _api;
        private string releases;
        private FilmData activeJson;
        private List<string> newAddedJson = new List<string>();
        private FilmData savedJson;
        private Boolean sameJson;
        public Form activeForm2;

        private string apiUrlForNew = $"https://imdb-api.com/API/AdvancedSearch/{KeysAccess.GetRandomValue()}/?";
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
        /// ���������� ������ �����
        /// </summary>
        private async void FillInCountry()
        {

            var country = Structure.Language.MakeCountry();
            cmbCountry.DataSource = country.MyList;
            cmbCountry.DisplayMember = "Country";
            cmbCountry.ValueMember = "Symbol";
        }

        /// <summary>
        /// ������ search ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            WaitForm a = new WaitForm();
            a.Show();

            if (string.IsNullOrEmpty(txtBoxSearch.Text) && !ArchiveSearchCheckBox.Checked)
            {
                MessageBox.Show("������ ��� ������ ������");
                return;
            }
            sameJson = false;
            if (ArchiveSearchCheckBox.Checked)
            {
                SearchTitleInArchive(txtBoxSearch.Text);
            }
            else
            {
                //AdvancedSearchInput adv = new AdvancedSearchInput();
                //adv.Title = txtBoxSearch.Text;
                //activeJson = Converter.AdvToData(await _api.AdvancedSearchAsync(adv), "");

                activeJson = Converter.SrchTitToData(await _api.SearchTitleAsync(txtBoxSearch.Text));
            }

            //api.SearchMovieAsync("expression");
            //ai.SearchSeriesAsync("expression");

            UpdateListOfMeta(activeJson);

            listTitles.DataSource = activeJson.Results;
            listTitles.DisplayMember = "Title";
            listTitles.ValueMember = "Id";

            a.Hide();
        }

        /// <summary>
        /// ������������ ����� �� ������� � �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSrchCountryDate_Click(object sender, EventArgs e)
        {
            var fromD = dateFrom.Text.Split(".");
            var toD = dateTo.Text.Split(".");
            if (string.IsNullOrEmpty(fromD[2]) || string.IsNullOrEmpty(fromD[1]) || string.IsNullOrEmpty(toD[2]) || string.IsNullOrEmpty(toD[1]))
            {
                MessageBox.Show("�� ������� ���� ��� ������ �������");
                return;
            }

            WaitForm a = new WaitForm();
            a.Show();

            // &release_date=2023-04-01,2023-06-01
            sameJson = false;
            string countryLang = cmbCountry.SelectedValue.ToString();

            string uriDate = $"&release_date={fromD[2]}-{fromD[1]}-10,{toD[2]}-{toD[1]}-10";

            activeJson = await GetReleaseTitles(uriDate, countryLang);

            UpdateListOfMeta(activeJson);
            //Window_Loaded(sender, e, countryLang, fromD, toD);

            a.Hide();
        }

        private async Task<FilmData> GetReleaseTitles(string uriDate, string language)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (language == "")
                        releases = await client.GetStringAsync(apiUrlForNew + uriDate + "&count=250");
                    else
                        // Parse the JSON response
                        releases = await client.GetStringAsync(apiUrlForNew + uriDate + "&countries=" + language);

                    //SaveReleasesToExcel(releases);
                    var a = JsonConvert.DeserializeObject<FilmData>(releases);
                    if (language != "")
                    {
                        foreach (var item in a.Results)
                        {
                            item.Countries = Structure.Language.countryCodeDictionary[language];
                        }
                        return a;
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
        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
            ExcelPlace.SaveReleasesToExcel(savedJson, OnlyNewCheckBox.Checked, newAddedJson);
        }

        /// <summary>
        /// ��������� ����� � ��, ���� ������� "���", �� ��������� ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTitle_Click(object sender, EventArgs e)
        {
            WaitForm a = new WaitForm();
            a.Show();

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
                var y = activeJson.Results[listTitles.SelectedIndex];
                sameFilms += AddNewItemToJson(y);
            }
            else
            {
                MessageBox.Show("�� ������ �������� ��� �����������?", "�����?");
                AllAddCheckBox.Checked = false;
                a.Hide();
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
            //MessageBox.Show("���������");
            a.Hide();
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
                //TODO �������� ��������� 2�� �������
                //CompareTwoJsonFile(byref savedJson.Results[indexO], a);
                if (savedJson.Results[indexO].LocationSearch is null)
                {
                    sameFilm = "\n" + a.Id + "_" + a.Title + ";";
                }
                string.Concat(savedJson.Results[indexO].LocationSearch, "/" + a.LocationSearch);
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
        private void btnFromArchive_Click(object sender, EventArgs e)
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
        /// ���������� ���� ���������� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            prgProccess.Visible = false;
            try
            {
                var i = listTitles.SelectedIndex;
                lblInfo.Text = $"ID:             {activeJson.Results[i].Id}\n" +
                              $"Title Origin:   {activeJson.Results[i].Title}\n" +
                              $"Title RUS:      {activeJson.Results[i].TitleRus}\n" +
                              $"Type:           {activeJson.Results[i].Type}\n" +
                              $"Year:           {activeJson.Results[i].Year}\n" +
                              $"Info:    {activeJson.Results[i].Description}\n" +
                              $"Awards:   {activeJson.Results[i].Awards}\n" +
                              $"Gross:   {activeJson.Results[i].GrossWorld}\n" +
                              //$"Release date:   {activeJson.Results[i].ReleaseDate}\n" +
                              $"RunTime Mins:   {activeJson.Results[i].RuntimeStr}\n" +
                              $"Rating:         {activeJson.Results[i].ContentRating}\n" +
                              $"Genres:         {activeJson.Results[i].Genres}\n" +
                              $"Languages:      {activeJson.Results[i].Languages}\n" +
                              $"Countries of orgin: {activeJson.Results[i].Countries}\n" +
                              //$"Release in country: {activeJson.Results[i].LocationSearch}\n" +
                              $"Directors:      {activeJson.Results[i].Directors}\n" +
                              $"Stars:          {activeJson.Results[i].Stars}";
                lblSynopsis.Text = activeJson.Results[i].Plot;
                linkLabel1.Text = activeJson.Results[i].Image;
                pictPoster.ImageLocation = activeJson.Results[i].Image;

                if (activeJson.Results[i].CounrtyReleaseAll is not null)
                {
                    var tempA = new StringBuilder();
                    foreach (var item in activeJson.Results[i].CounrtyReleaseAll)
                    {
                        tempA.Append(item.Value.country + "   " + item.Value.releaseDate + "\n");
                    }
                    //lblDates.Text = $"Russia         {activeJson.Results[i].CounrtyReleaseAll["RU"].releaseDate}\n" +
                    //                $"United States  {activeJson.Results[i].CounrtyReleaseAll["US"].releaseDate}\n" +
                    //                $"Germany        {activeJson.Results[i].CounrtyReleaseAll["DE"].releaseDate}\n" +
                    //                $"Italy          {activeJson.Results[i].CounrtyReleaseAll["IT"].releaseDate}\n" +
                    //                $"Spain          {activeJson.Results[i].CounrtyReleaseAll["ES"].releaseDate}\n" +
                    //                $"United Kingdom {activeJson.Results[i].CounrtyReleaseAll["GB"].releaseDate}\n" +
                    //                $"France         {activeJson.Results[i].CounrtyReleaseAll["FR"].releaseDate}\n" +
                    //                $"China          {activeJson.Results[i].CounrtyReleaseAll["CN"].releaseDate}\n";
                    lblDates.Text = tempA.ToString(0, tempA.Length - 1);
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
        /// ��������� �� ��������� coming soon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CmngSoonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                sameJson = false;
                var release = await ComingSoonAsync();
                activeJson = Converter.NewMovieToData(release);

                listTitles.DataSource = activeJson.Results;
                listTitles.DisplayMember = "Title";
                listTitles.ValueMember = "Id";
            }
            catch (Exception)
            {
                sameJson = true;
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
        private async void btnSearchExtension_Click(object sender, EventArgs e)
        {
            WaitForm a = new WaitForm();
            a.Show();
            int i;
            try
            {
                i = listTitles.SelectedIndex;
                
                if (AllAddCheckBox.Checked)
                {
                    if (savedJson.Results == null || savedJson.Results.Count() < 1)
                    {
                        prgProccess.Visible = true;
                        //info = Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + item.Id));
                        prgProccess.Minimum = 0;
                        prgProccess.Maximum = activeJson.Results.Count();
                        for (int l = 0; l < activeJson.Results.Count; l++)
                        {
                            prgProccess.PerformStep();
                            var obj = await UploadReleasesDates(activeJson.Results[l], activeJson.Results[l].Id);
                            if (obj is null) break;
                            savedJson.Results.Add(obj);
                        }
                        
                        //foreach (JsonData el in activeJson.Results)
                        //{
                        //    var obj = await UploadReleasesDates(el, el.Id);
                        //    if (obj is null) break;
                        //    savedJson.Results.Add(obj);
                        //}
                        HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
                    }
                    else
                    {
                        prgProccess.Visible = true;
                        //info = Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + item.Id));
                        prgProccess.Minimum = 0;
                        prgProccess.Maximum = savedJson.Results.Count();
                        for (int l = 0; l < savedJson.Results.Count; l++)
                        {
                            prgProccess.PerformStep();
                            var info = await GetInfoIDAsync(savedJson.Results[l].Id);
                            var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                            SearchTitAdditionalInfoAdd(obj);
                        }

                        //foreach (var item in savedJson.Results)
                        //{
                        //    var info = await GetInfoIDAsync(item.Id);
                        //    var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                        //    SearchTitAdditionalInfoAdd(obj);
                        //}
                    }
                }
                else if (OnlyNewCheckBox.Checked)
                {
                    prgProccess.Visible = true;
                    //info = Converter.TitleToData(await ApiUtils.GetObjectAsync<TitleData>(TitleUrl + item.Id));
                    prgProccess.Minimum = 0;
                    prgProccess.Maximum = newAddedJson.Count();
                    for (int l = 0; l < newAddedJson.Count; l++)
                    {
                        prgProccess.PerformStep();
                        var info = await GetInfoIDAsync(newAddedJson[l]);
                        var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                        SearchTitAdditionalInfoAdd(obj);
                    }

                    //foreach (string id in newAddedJson)
                    //{
                    //    var info = await GetInfoIDAsync(id);
                    //    var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                    //    SearchTitAdditionalInfoAdd(obj);
                    //}
                    HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
                }
                else if (i != -1)
                {
                    var info = await GetInfoIDAsync(activeJson.Results[i].Id);
                    var obj = await UploadReleasesDates(info.Results[0], info.Results[0].Id);
                    SearchTitAdditionalInfoAdd(obj);
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
            OnlyNewCheckBox.Checked = false;

            listTitles_SelectedIndexChanged(sender, e);

            a.Hide();
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
                //savedJson.Results[indexO].Type = item.Type;
                //savedJson.Results[indexO].Year = item.Year;
                //savedJson.Results[indexO].ReleaseDate = item.ReleaseDate;
                //savedJson.Results[indexO].RuntimeStr = item.RuntimeStr;
                //savedJson.Results[indexO].Awards = item.Awards;
                //savedJson.Results[indexO].Directors = item.Directors;
                //savedJson.Results[indexO].Stars = item.Stars;
                //if (item.TitleOrigin  is not null) savedJson.Results[indexO].TitleOrigin = item.TitleOrigin;
                //savedJson.Results[indexO].TitleRus = item.TitleRus;

                //savedJson.Results[indexO].Companies = item.Companies;
                //savedJson.Results[indexO].Languages = item.Languages;
                //savedJson.Results[indexO].ContentRating = item.ContentRating;

                //savedJson.Results[indexO].Countries = item.Countries;

                //savedJson.Results[indexO].IMDbRating = item.IMDbRating;
                //savedJson.Results[indexO].Plot = item.Plot;

                savedJson.Results[indexO].CounrtyReleaseAll = item.CounrtyReleaseAll;

                PropertyInfo[] properties = savedJson.Results[indexO].GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object sourceValue = property.GetValue(item);
                    object targetValue = property.GetValue(savedJson.Results[indexO]);

                    //if (targetValue == null && sourceValue != null)
                    //    property.SetValue(savedJson.Results[indexO], sourceValue);
                    if (sourceValue != null)
                        property.SetValue(savedJson.Results[indexO], sourceValue);
                }
            }
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");
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

        /// <summary>
        /// ��������� ��������� ��� ����� �������
        /// </summary>
        /// <param name="path"></param>
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

        /// <summary>
        /// �������� ��������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAchive_Click(object sender, EventArgs e)
        {
            var a = MessageBox.Show("����� ������������ ������� �����?", "���������", MessageBoxButtons.OKCancel);
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
            WaitForm a = new WaitForm();
            a.Show();

            if (String.IsNullOrEmpty(cmbCountry.Text) || cmbCountry.Text.Length < 2)
            {
                a.Hide();
                MessageBox.Show("������ ������");
                return;
            }

            UpdateSameJson(false);
            string country = cmbCountry.SelectedValue.ToString();
            CalendarData cdD = await CalendarIMDB.GetIMDBCalendarAsync(country, "Movie");
            if (cdD is not null)
            {
                FilmData newJ = Converter.CalendarToData(cdD, Structure.Language.countryCodeDictionary[country]);
                UpdateListOfMeta(newJ);
            }

            a.Hide();
        }

        /// <summary>
        /// ��������� ���� ������� � ������ �������
        /// </summary>
        /// <param name="date"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private async Task<JsonData> UploadReleasesDates(JsonData date, string title)
        {
            if (title is null) return new JsonData();
            CalendarD initital = await ReleasesDates.GetReleasesToData(title);
            if (initital.data is null) return date;
            date = Converter.EnterReleasesDates(initital, date);
            return date;
        }

        /// <summary>
        /// �������� ���������� ��� ���� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brtRemovettl_Click(object sender, EventArgs e)
        {
            if (AllAddCheckBox.Checked && sameJson)
            {
                savedJson.Results.Clear();

            }
            else if (sameJson)
            {
                //��������� ������ ���������� � excel � ���� json
                //savedJson.Results.Remove(activeJson.Results[listTitles.SelectedIndex]);
                savedJson.Results.RemoveAt(listTitles.SelectedIndex);
            }
            else
            {
                MessageBox.Show("�� �� � ��������� �����", "����� � �����");
                AllAddCheckBox.Checked = false;
                return;
            }

            HardTool.SaveJson(savedJson, "imdb");
            btnFromArchive_Click(sender, e);
        }
    }
}