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
    //Non-commercial WinForms to get new releases and other information using IMDB APi
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
            //_apiTMDB = new TMDbLib.Client.TMDbClient(KeysAccess.GetKeyTMDB());
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tipsForm = new System.Windows.Forms.ToolTip();
            tipsForm.SetToolTip(button2, "����� ���� � ��������� ��������� ��� �� ���������� ������");
            tipsForm.SetToolTip(button3, "���������� ����� ������ ��� ������ �����(�� ��� ������) ���������� � excel");
            tipsForm.SetToolTip(button4, "���������� ���������� ���� �������� ������� � ����� ���� ���� � �����");
            tipsForm.SetToolTip(button5, "����������� ���� ���������� ������������ � ������");
            tipsForm.SetToolTip(listBox1, "���� ��� ����������� ���������� ������ ��� �������� �������");
            tipsForm.SetToolTip(pictureBox1, "������");
            tipsForm.SetToolTip(OnlyNewCheckBox, "���������� ��� ��������� excel ����� � ���� ������� ������� ���� ������");
            tipsForm.SetToolTip(AllAddCheckBox, "���������� �������� ����� �������� ���� ������ ��������� ������� � �����");
            tipsForm.SetToolTip(textBox1, "������������ ��� ������ �� IMDB");
            tipsForm.SetToolTip(comboBox1, "����� ������ ��� ������ ����� �������");
            tipsForm.SetToolTip(ArchiveSearchCheckBox, "��������� ��� ������ ������ � ������");
            tipsForm.SetToolTip(SearchTit, "��������� ���, ���, ���� ������, �����, ��������� � ������ ��� ���������� ������");
            tipsForm.SetToolTip(SearchButton, "������ ��� ������ �� IMDB ��� ������");
            tipsForm.SetToolTip(dateFrom, "���� ������ ���������");
            tipsForm.SetToolTip(dateTo, "���� �������� ���������");
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


        /// <summary>
        /// ���������� ������ �����
        /// </summary>
        private async void FillInCountry()
        {

            var country = Language.MakeCountry();
            comboBox1.DataSource = country.MyList;
            comboBox1.DisplayMember = "Country";
            comboBox1.ValueMember = "Symbol";
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

            listBox1.DataSource = activeJson.Results;
            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "Id";
        }

        /// <summary>
        /// ������������ ����� �� ������� � �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
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
        /// ��������� ���������� ���� ������ ��� ���� � ���� EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson));
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
                //��������� ������ ���������� � excel � ���� json
                var y = activeJson.Results[listBox1.SelectedIndex];
                sameFilms += AddNewItemToJson(y);
            }
            else
            {
                MessageBox.Show("�� ������ �������� ��� �����������?", "�����?");
                return;
            }

            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson));
            if (!string.IsNullOrEmpty(sameFilms))
            {
                //MessageBox.Show("������ ������� ��� ���������� �����:\n" + sameFilms.Substring(0,sameFilms.Length - 1));
                var answer = FlexibleMessageBox.Show("������ ������� ��� ���� ���������� �����: " + sameFilms.Substring(0, sameFilms.Length - 1),
                                        "�������� ����������?",
                                        buttons: MessageBoxButtons.YesNo);
                //TODO : �������� ���������� ��� ��������� �� ����� �������
            }
            MessageBox.Show("���������");
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
        /// ���� ����������� ���������� �� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //��������� ������ ����������� ���� ����� ����
            sameJson = true;
            savedJson = HardTool.GetSavedJson();
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

        /// <summary>
        /// ����� �� ������ �������
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
        /// ���������� ���� � ��������� ���������� ������
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
                    MessageBox.Show("������� ����� �� ������");
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
            Forms.Form2 fr2 = new Forms.Form2();
            fr2.Show();
            fr2.Location = new Point(fr2.Left = this.Right + SystemInformation.BorderSize.Width, this.Location.Y);
        }

    }
}