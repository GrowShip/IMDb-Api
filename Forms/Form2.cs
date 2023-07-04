using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMDbApi.Forms
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// Массив с файлове в корневой папке
        /// </summary>
        private string[] dirs;
        
        /// <summary>
        /// Необходимо для управления формой из вне
        /// </summary>
        public static Form2 instance;

        public Form2()
        {
            InitializeComponent();
            instance = this;
            LoadTheme();
            LoadForm();
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
            SearchForm.instance.activeForm2 = null;
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
                                SearchForm.instance.UpdateSameJson(true);
            else SearchForm.instance.UpdateSameJson(false);
            SearchForm.instance.ChangeSourceList(dirs[indexList]);
            
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
            dirs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\JSON",
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
