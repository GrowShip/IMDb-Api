using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaApi.Structure;

namespace MediaApi.Forms
{
    public partial class IMDbArchiveForm : Form
    {
        /// <summary>
        /// Массив с файлове в корневой папке
        /// </summary>
        private string[] dirs;

        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private extern static void ReleaseCapture();
        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Необходимо для управления формой из вне
        /// </summary>
        public static IMDbArchiveForm instance;

        public IMDbArchiveForm()
        {
            InitializeComponent();
            instance = this;
            LoadTheme();
            LoadForm();
            this.ControlBox = false;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void LoadForm()
        {
            GetFilesInArchives();
            UpdateArrayFilenames();
        }

        /// <summary>
        /// Обновляет информацию о файлах для листа просмотра
        /// </summary>
        private void UpdateArrayFilenames()
        {
            List<string> filenames = new List<string>();
            foreach (string dir in dirs)
            {
                filenames.Add(Path.GetFileName(dir));
            }
            listBoxJson.DataSource = filenames;
        }

        /// <summary>
        /// Перезагружает форму
        /// </summary>
        public void UpdateList()
        {
            LoadForm();
        }

        /// <summary>
        /// Обнуляет наличие формы в родительской
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            IMDbSearchForm.instance.activeForm2 = null;
            this.Close();
        }

        /// <summary>
        /// Дает использовать JSON который выбрали в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUseJson_Click(object sender, EventArgs e)
        {
            var indexList = listBoxJson.SelectedIndex;
            if (listBoxJson.Items[indexList].ToString() == "file.json")
                IMDbSearchForm.instance.UpdateSameJson(true);
            else IMDbSearchForm.instance.UpdateSameJson(false);
            IMDbSearchForm.instance.ChangeSourceList(dirs[indexList]);

        }

        private void LoadTheme()
        {
            foreach (var btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColour.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColour.SecondaryColor;
                }
                pnlArchive.BackColor = ThemeColour.PrimaryColor;
                lblArchive.ForeColor = Color.White;
            }
        }

        private void GetFilesInArchives()
        {
            dirs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\JSON\imdb",
                                               "*.json",
                                               SearchOption.TopDirectoryOnly);
        }

        private void btnRemoveJson_Click(object sender, EventArgs e)
        {
            var indexList = listBoxJson.SelectedIndex;
            if (listBoxJson.Items[indexList].ToString() == "file.json")
            {
                MessageBox.Show("Этот файл удалять нельзя!", "Attention");
                return;
            }
            HardTool.RemoveAnArchive(dirs[indexList]);
            UpdateList();
        }

    }
}
