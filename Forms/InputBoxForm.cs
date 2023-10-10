using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaApi.Forms
{
    public partial class InputBoxForm : Form
    {
        public string filename { get; set; }

        public InputBoxForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            filename = inputText.Text;
            this.Hide();
        }
    }
}
