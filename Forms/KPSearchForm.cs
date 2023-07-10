using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaApi.Forms
{
    public partial class KPSearchForm : Form
    {
        public KPSearchForm()
        {
            InitializeComponent();
            LoadForm();
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
    }
}
