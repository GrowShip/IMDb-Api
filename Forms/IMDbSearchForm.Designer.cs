namespace MediaApi.Forms
{
    partial class IMDbSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMDbSearchForm));
            SearchButton = new Button();
            textBox1 = new TextBox();
            listBox1 = new ListBox();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            button4 = new Button();
            linkLabel1 = new LinkLabel();
            button5 = new Button();
            label3 = new Label();
            button3 = new Button();
            ArchiveSearchCheckBox = new CheckBox();
            AllAddCheckBox = new CheckBox();
            OnlyNewCheckBox = new CheckBox();
            SearchTit = new Button();
            scndForm = new Button();
            CmngSoonBtn = new Button();
            btnClearAchive = new Button();
            button2 = new Button();
            cmbCountry = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            dateFrom = new MaskedTextBox();
            dateTo = new MaskedTextBox();
            btnInCountryRls = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // SearchButton
            // 
            resources.ApplyResources(SearchButton, "SearchButton");
            SearchButton.BackColor = SystemColors.ActiveCaption;
            SearchButton.ForeColor = SystemColors.ButtonHighlight;
            SearchButton.Name = "SearchButton";
            SearchButton.UseVisualStyleBackColor = false;
            SearchButton.Click += button1_Click;
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // listBox1
            // 
            resources.ApplyResources(listBox1, "listBox1");
            listBox1.BackColor = SystemColors.MenuBar;
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.FormattingEnabled = true;
            listBox1.Name = "listBox1";
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonFace;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.BackColor = SystemColors.ButtonFace;
            label2.Name = "label2";
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.BackColor = SystemColors.ActiveCaption;
            button4.ForeColor = SystemColors.ButtonHighlight;
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.BackColor = SystemColors.ControlLight;
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // button5
            // 
            resources.ApplyResources(button5, "button5");
            button5.BackColor = SystemColors.ActiveCaption;
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.Name = "button5";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.BackColor = SystemColors.ButtonFace;
            label3.Name = "label3";
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.BackColor = SystemColors.ActiveCaption;
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // ArchiveSearchCheckBox
            // 
            resources.ApplyResources(ArchiveSearchCheckBox, "ArchiveSearchCheckBox");
            ArchiveSearchCheckBox.Name = "ArchiveSearchCheckBox";
            ArchiveSearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllAddCheckBox
            // 
            resources.ApplyResources(AllAddCheckBox, "AllAddCheckBox");
            AllAddCheckBox.Name = "AllAddCheckBox";
            AllAddCheckBox.UseVisualStyleBackColor = true;
            // 
            // OnlyNewCheckBox
            // 
            resources.ApplyResources(OnlyNewCheckBox, "OnlyNewCheckBox");
            OnlyNewCheckBox.Name = "OnlyNewCheckBox";
            OnlyNewCheckBox.UseVisualStyleBackColor = true;
            // 
            // SearchTit
            // 
            resources.ApplyResources(SearchTit, "SearchTit");
            SearchTit.BackColor = SystemColors.ActiveCaption;
            SearchTit.ForeColor = SystemColors.ButtonHighlight;
            SearchTit.Name = "SearchTit";
            SearchTit.UseVisualStyleBackColor = false;
            SearchTit.Click += SearchTit_Click;
            // 
            // scndForm
            // 
            resources.ApplyResources(scndForm, "scndForm");
            scndForm.BackColor = SystemColors.ActiveCaption;
            scndForm.ForeColor = SystemColors.ButtonHighlight;
            scndForm.Name = "scndForm";
            scndForm.UseVisualStyleBackColor = false;
            scndForm.Click += scndForm_Click;
            // 
            // CmngSoonBtn
            // 
            resources.ApplyResources(CmngSoonBtn, "CmngSoonBtn");
            CmngSoonBtn.BackColor = SystemColors.ActiveCaption;
            CmngSoonBtn.ForeColor = SystemColors.ButtonHighlight;
            CmngSoonBtn.Name = "CmngSoonBtn";
            CmngSoonBtn.UseVisualStyleBackColor = false;
            CmngSoonBtn.Click += CmngSoonBtn_Click;
            // 
            // btnClearAchive
            // 
            resources.ApplyResources(btnClearAchive, "btnClearAchive");
            btnClearAchive.BackColor = SystemColors.ActiveCaption;
            btnClearAchive.ForeColor = SystemColors.ControlLightLight;
            btnClearAchive.Name = "btnClearAchive";
            btnClearAchive.UseVisualStyleBackColor = false;
            btnClearAchive.Click += btnClearAchive_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.BackColor = SystemColors.ActiveCaption;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // cmbCountry
            // 
            resources.ApplyResources(cmbCountry, "cmbCountry");
            cmbCountry.FormattingEnabled = true;
            cmbCountry.Name = "cmbCountry";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // dateFrom
            // 
            resources.ApplyResources(dateFrom, "dateFrom");
            dateFrom.Name = "dateFrom";
            dateFrom.ValidatingType = typeof(DateTime);
            // 
            // dateTo
            // 
            resources.ApplyResources(dateTo, "dateTo");
            dateTo.Name = "dateTo";
            dateTo.ValidatingType = typeof(DateTime);
            // 
            // btnInCountryRls
            // 
            resources.ApplyResources(btnInCountryRls, "btnInCountryRls");
            btnInCountryRls.BackColor = SystemColors.ActiveCaption;
            btnInCountryRls.ForeColor = SystemColors.ButtonHighlight;
            btnInCountryRls.Name = "btnInCountryRls";
            btnInCountryRls.UseVisualStyleBackColor = false;
            btnInCountryRls.Click += btnInCountryRls_Click;
            // 
            // IMDbSearchForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(btnInCountryRls);
            Controls.Add(dateTo);
            Controls.Add(btnClearAchive);
            Controls.Add(dateFrom);
            Controls.Add(label6);
            Controls.Add(CmngSoonBtn);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(scndForm);
            Controls.Add(cmbCountry);
            Controls.Add(SearchTit);
            Controls.Add(button2);
            Controls.Add(OnlyNewCheckBox);
            Controls.Add(AllAddCheckBox);
            Controls.Add(ArchiveSearchCheckBox);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(button5);
            Controls.Add(linkLabel1);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(listBox1);
            Controls.Add(textBox1);
            Controls.Add(SearchButton);
            Name = "IMDbSearchForm";
            Load += SearchForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button SearchButton;
        private TextBox textBox1;
        private ListBox listBox1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button button4;
        private LinkLabel linkLabel1;
        private Button button5;
        private Label label3;
        private Button button3;
        private CheckBox ArchiveSearchCheckBox;
        private CheckBox AllAddCheckBox;
        private CheckBox OnlyNewCheckBox;
        private Button SearchTit;
        private Button scndForm;
        private Button CmngSoonBtn;
        private Button btnClearAchive;
        private Button button2;
        private ComboBox cmbCountry;
        private Label label4;
        private Label label5;
        private Label label6;
        private MaskedTextBox dateFrom;
        private MaskedTextBox dateTo;
        private Button btnInCountryRls;
    }
}