namespace MediaApi
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panelMenu = new Panel();
            btnKinopoisk = new Button();
            btnClose = new Button();
            btnTabSearch = new Button();
            deskPanel = new Panel();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(12, 12, 12);
            panelMenu.Controls.Add(btnKinopoisk);
            panelMenu.Controls.Add(btnClose);
            panelMenu.Controls.Add(btnTabSearch);
            resources.ApplyResources(panelMenu, "panelMenu");
            panelMenu.Name = "panelMenu";
            // 
            // btnKinopoisk
            // 
            btnKinopoisk.FlatAppearance.BorderColor = Color.FromArgb(18, 18, 18);
            btnKinopoisk.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 48, 48);
            btnKinopoisk.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 25, 25);
            resources.ApplyResources(btnKinopoisk, "btnKinopoisk");
            btnKinopoisk.ForeColor = SystemColors.ControlLightLight;
            btnKinopoisk.Name = "btnKinopoisk";
            btnKinopoisk.UseVisualStyleBackColor = true;
            btnKinopoisk.MouseEnter += btnKinopoisk_MouseEnter;
            btnKinopoisk.MouseLeave += btnKinopoisk_MouseLeave;
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            btnClose.ForeColor = Color.FromArgb(25, 25, 25);
            btnClose.Image = Properties.Resources.cross_out__2_;
            btnClose.Name = "btnClose";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnTabSearch
            // 
            btnTabSearch.FlatAppearance.BorderColor = Color.FromArgb(18, 18, 18);
            btnTabSearch.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 48, 48);
            btnTabSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 25, 25);
            resources.ApplyResources(btnTabSearch, "btnTabSearch");
            btnTabSearch.ForeColor = SystemColors.ControlLightLight;
            btnTabSearch.Name = "btnTabSearch";
            btnTabSearch.UseVisualStyleBackColor = true;
            btnTabSearch.Click += btnTabSearch_Click;
            btnTabSearch.MouseEnter += btnTabSearch_MouseEnter;
            btnTabSearch.MouseLeave += btnTabSearch_MouseLeave;
            // 
            // deskPanel
            // 
            deskPanel.BackColor = Color.FromArgb(12, 12, 12);
            resources.ApplyResources(deskPanel, "deskPanel");
            deskPanel.Name = "deskPanel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(deskPanel);
            Controls.Add(panelMenu);
            Name = "MainForm";
            Load += Form1_Load;
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnTabSearch;
        private Panel panelMenu;
        private Button btnClose;
        private Panel deskPanel;
        private Button btnKinopoisk;
    }
}