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
        private string[] dirs;
        public static Form2 instance;

        public Form2()
        {
            InitializeComponent();
            instance = this;
            LoadTheme();
            LoadForm();
        }

        private void LoadForm()
        {
            GetFilesInArchives();
            List<string> filenames = new List<string>();
            foreach (string dir in dirs)
            {
                filenames.Add(Path.GetFileName(dir));
            }
            listBoxJson.DataSource = filenames;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUseJson_Click(object sender, EventArgs e)
        {
            var indexList = listBoxJson.SelectedIndex;
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
            if (listBoxJson.Items[indexList] == "file.json")
            {
                MessageBox.Show("Этот файл удалять нельзя!", "Attention");
                return;
            }
        }
    }
}
