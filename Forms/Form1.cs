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
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            random = new Random();
        }

        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        private void btnTabSearch_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.SearchForm(), sender);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        #region Color Change Btn

        //Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColour.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColour.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColour.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelMenu.BackColor = color;
                    //panelLogo.BackColor = ThemeColour.ChangeColorBrightness(color, -0.3);
                    ThemeColour.PrimaryColor = color;
                    ThemeColour.SecondaryColor = ThemeColour.ChangeColorBrightness(color, -0.3);
                    //btnCloseChildForm.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(12, 12, 12);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        #endregion

        #region Forms

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();

            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.deskPanel.Controls.Add(childForm);
            this.deskPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //lblTitle.Text = childForm.Text;
        }

        private void Reset()
        {
            DisableButton();
            //lblTitle.Text = "HOME";
            panelMenu.BackColor = Color.FromArgb(12, 12, 12);
            //panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            //btnCloseChildForm.Visible = false;
        }

        #endregion
    }
}