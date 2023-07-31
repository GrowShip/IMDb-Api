using Button = System.Windows.Forms.Button;
using MediaApi.Forms;
using MediaApi.Structure;

namespace MediaApi
{
    //Non-commercial WinForms to get new releases and other information using IMDB APi
    //https://imdb-api.com/API/AdvancedSearch/
    //title_type=tv_movie,tv_series,tv_episode,documentary,video&
    //release_date=2023-04-10,2023-06-10&
    //countries=af&
    //languages=eu,ca,zh,eo,fr,de,hi,ko,kvk,ru,rsl,es,ssp,ta,te,tr
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы
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

        private void btnIMDb_Click(object sender, EventArgs e)
        {
            if (IMDbSearchForm.instance != null && IMDbSearchForm.instance.activeForm2 != null)
                IMDbSearchForm.instance.activeForm2.Close();
            if (activeForm != null)
                activeForm.Close();
            Dictionary<string, int> data = new Dictionary<string, int>
            {
                { "x", this.Location.X },
                { "y", this.Location.Y },
                { "width", this.Width },
                { "height", this.Height }
            };
            OpenChildForm(new Forms.IMDbSearchForm(data), sender);
        }

        private void btnKinopoisk_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new Forms.KPSearchForm(), sender);
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
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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

        /// <summary>
        /// Устаглвка фона при наведение на поиск IMDB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTabSearch_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                deskPanel.BackgroundImage = Image.FromFile(@"media\imdb\IMDbFull.png");
            }
            catch (Exception)
            {

                throw;
            }

            //"C:\Users\Shipitsyn\Documents\Visual Studio 2022\IMDbApi\Add\IMDbFull.png"
        }

        /// <summary>
        /// Clean backGroundImage when cursor leave from imdb search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTabSearch_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                deskPanel.BackgroundImage = null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnKinopoisk_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                deskPanel.BackgroundImage = Image.FromFile(@"media\kinopoisk\KinopoiskFull.png");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnKinopoisk_MouseLeave(object sender, EventArgs e)
        {
            btnTabSearch_MouseLeave(sender, e);
        }

    }
}