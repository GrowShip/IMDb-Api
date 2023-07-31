using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaApi.Structure;
using static System.Net.WebRequestMethods;

namespace MediaApi.Forms
{
    public partial class KPSearchForm : Form
    {
        public KPSearchForm()
        {
            InitializeComponent();

        }

        private void KPSearchForm_Load(object sender, EventArgs e)
        {
            LoadForm();
            textSearch.PlaceholderText = "КиноПоиск";
        }

        private void LoadForm()
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

            checkBoxArchiveSearch.ForeColor = ThemeColour.SecondaryColor;
            //label5.ForeColor = ThemeColour.PrimaryColor;

        }

        /// <summary>
        /// Копирует текст описания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSynopsis_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSynopsis.Text))
            {
                Clipboard.SetText(lblSynopsis.Text);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textSearch.Text) ||
                textSearch.Text.Equals("КиноПоиск", StringComparison.CurrentCultureIgnoreCase))
                MessageBox.Show("Введите что-то в поиске");
        }

    }

    internal partial class UrlMaker
    {
        public string CreateUrlv1(string typeSearch)
        {
            /// https://api.kinopoisk.dev
            /// 
            ///

            return "";
        }
        public string CreateUrlv2(string typeSearch)
        {
            ///
            ///
            ///

            return "";
        }

        public string GetNewReleasesUri(string year, string month)
        {
            return string.Format("https://kinopoiskapiunofficial.tech/api/v2.2/films/premieres?year={0}&month={2}", year, month);
        }

        public string GetSearchUri(string keyWord)
        {
            return string.Format("https://kinopoiskapiunofficial.tech/api/v2.1/films/search-by-keyword?keyword={0}&page=1", keyWord);
        }
    }

}
