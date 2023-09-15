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
            btnSearch = new Button();
            txtBoxSearch = new TextBox();
            listTitles = new ListBox();
            pictPoster = new PictureBox();
            lblInfo = new Label();
            btnAddTitle = new Button();
            linkLabel1 = new LinkLabel();
            btnFromArchive = new Button();
            lblSynopsis = new Label();
            btnExcelSave = new Button();
            ArchiveSearchCheckBox = new CheckBox();
            AllAddCheckBox = new CheckBox();
            OnlyNewCheckBox = new CheckBox();
            btnSearchExtension = new Button();
            btnOpenArchive = new Button();
            CmngSoonBtn = new Button();
            btnClearAchive = new Button();
            btnSrchCountryDate = new Button();
            cmbCountry = new ComboBox();
            lblCountry = new Label();
            lblReleaseDate = new Label();
            label6 = new Label();
            dateFrom = new MaskedTextBox();
            dateTo = new MaskedTextBox();
            btnInCountryRls = new Button();
            lblDates = new Label();
            brtRemovettl = new Button();
            ((System.ComponentModel.ISupportInitialize)pictPoster).BeginInit();
            SuspendLayout();
            // 
            // btnSearch
            // 
            resources.ApplyResources(btnSearch, "btnSearch");
            btnSearch.BackColor = SystemColors.ActiveCaption;
            btnSearch.ForeColor = SystemColors.ButtonHighlight;
            btnSearch.Name = "btnSearch";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtBoxSearch
            // 
            resources.ApplyResources(txtBoxSearch, "txtBoxSearch");
            txtBoxSearch.Name = "txtBoxSearch";
            // 
            // listTitles
            // 
            resources.ApplyResources(listTitles, "listTitles");
            listTitles.BackColor = SystemColors.MenuBar;
            listTitles.BorderStyle = BorderStyle.None;
            listTitles.FormattingEnabled = true;
            listTitles.Name = "listTitles";
            listTitles.SelectedIndexChanged += listTitles_SelectedIndexChanged;
            // 
            // pictPoster
            // 
            pictPoster.BackColor = SystemColors.ButtonFace;
            resources.ApplyResources(pictPoster, "pictPoster");
            pictPoster.Name = "pictPoster";
            pictPoster.TabStop = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(lblInfo, "lblInfo");
            lblInfo.BackColor = SystemColors.ButtonFace;
            lblInfo.Name = "lblInfo";
            // 
            // btnAddTitle
            // 
            resources.ApplyResources(btnAddTitle, "btnAddTitle");
            btnAddTitle.BackColor = SystemColors.ActiveCaption;
            btnAddTitle.ForeColor = SystemColors.ButtonHighlight;
            btnAddTitle.Name = "btnAddTitle";
            btnAddTitle.UseVisualStyleBackColor = false;
            btnAddTitle.Click += btnAddTitle_Click;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.BackColor = SystemColors.ControlLight;
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // btnFromArchive
            // 
            resources.ApplyResources(btnFromArchive, "btnFromArchive");
            btnFromArchive.BackColor = SystemColors.ActiveCaption;
            btnFromArchive.ForeColor = SystemColors.ButtonHighlight;
            btnFromArchive.Name = "btnFromArchive";
            btnFromArchive.UseVisualStyleBackColor = false;
            btnFromArchive.Click += btnFromArchive_Click;
            // 
            // lblSynopsis
            // 
            resources.ApplyResources(lblSynopsis, "lblSynopsis");
            lblSynopsis.BackColor = SystemColors.ButtonFace;
            lblSynopsis.CausesValidation = false;
            lblSynopsis.Name = "lblSynopsis";
            // 
            // btnExcelSave
            // 
            resources.ApplyResources(btnExcelSave, "btnExcelSave");
            btnExcelSave.BackColor = SystemColors.ActiveCaption;
            btnExcelSave.ForeColor = SystemColors.ButtonHighlight;
            btnExcelSave.Name = "btnExcelSave";
            btnExcelSave.UseVisualStyleBackColor = false;
            btnExcelSave.Click += btnExcelSave_Click;
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
            // btnSearchExtension
            // 
            resources.ApplyResources(btnSearchExtension, "btnSearchExtension");
            btnSearchExtension.BackColor = SystemColors.ActiveCaption;
            btnSearchExtension.ForeColor = SystemColors.ButtonHighlight;
            btnSearchExtension.Name = "btnSearchExtension";
            btnSearchExtension.UseVisualStyleBackColor = false;
            btnSearchExtension.Click += btnSearchExtension_Click;
            // 
            // btnOpenArchive
            // 
            resources.ApplyResources(btnOpenArchive, "btnOpenArchive");
            btnOpenArchive.BackColor = SystemColors.ActiveCaption;
            btnOpenArchive.ForeColor = SystemColors.ButtonHighlight;
            btnOpenArchive.Name = "btnOpenArchive";
            btnOpenArchive.UseVisualStyleBackColor = false;
            btnOpenArchive.Click += btnOpenArchive_Click;
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
            // btnSrchCountryDate
            // 
            resources.ApplyResources(btnSrchCountryDate, "btnSrchCountryDate");
            btnSrchCountryDate.BackColor = SystemColors.ActiveCaption;
            btnSrchCountryDate.ForeColor = SystemColors.ButtonHighlight;
            btnSrchCountryDate.Name = "btnSrchCountryDate";
            btnSrchCountryDate.UseVisualStyleBackColor = false;
            btnSrchCountryDate.Click += btnSrchCountryDate_Click;
            // 
            // cmbCountry
            // 
            resources.ApplyResources(cmbCountry, "cmbCountry");
            cmbCountry.FormattingEnabled = true;
            cmbCountry.Name = "cmbCountry";
            // 
            // lblCountry
            // 
            resources.ApplyResources(lblCountry, "lblCountry");
            lblCountry.Name = "lblCountry";
            // 
            // lblReleaseDate
            // 
            resources.ApplyResources(lblReleaseDate, "lblReleaseDate");
            lblReleaseDate.Name = "lblReleaseDate";
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
            // lblDates
            // 
            resources.ApplyResources(lblDates, "lblDates");
            lblDates.BackColor = SystemColors.ButtonFace;
            lblDates.Name = "lblDates";
            // 
            // brtRemovettl
            // 
            resources.ApplyResources(brtRemovettl, "brtRemovettl");
            brtRemovettl.BackColor = SystemColors.ActiveCaption;
            brtRemovettl.ForeColor = SystemColors.ButtonHighlight;
            brtRemovettl.Name = "brtRemovettl";
            brtRemovettl.UseVisualStyleBackColor = false;
            brtRemovettl.Click += brtRemovettl_Click;
            // 
            // IMDbSearchForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(brtRemovettl);
            Controls.Add(lblDates);
            Controls.Add(btnInCountryRls);
            Controls.Add(dateTo);
            Controls.Add(btnClearAchive);
            Controls.Add(dateFrom);
            Controls.Add(label6);
            Controls.Add(CmngSoonBtn);
            Controls.Add(lblReleaseDate);
            Controls.Add(lblCountry);
            Controls.Add(btnOpenArchive);
            Controls.Add(cmbCountry);
            Controls.Add(btnSearchExtension);
            Controls.Add(btnSrchCountryDate);
            Controls.Add(OnlyNewCheckBox);
            Controls.Add(AllAddCheckBox);
            Controls.Add(ArchiveSearchCheckBox);
            Controls.Add(btnExcelSave);
            Controls.Add(lblSynopsis);
            Controls.Add(btnFromArchive);
            Controls.Add(linkLabel1);
            Controls.Add(btnAddTitle);
            Controls.Add(lblInfo);
            Controls.Add(pictPoster);
            Controls.Add(listTitles);
            Controls.Add(txtBoxSearch);
            Controls.Add(btnSearch);
            Name = "IMDbSearchForm";
            Load += SearchForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictPoster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSearch;
        private TextBox txtBoxSearch;
        private ListBox listTitles;
        private PictureBox pictPoster;
        private Label lblInfo;
        private Button btnAddTitle;
        private LinkLabel linkLabel1;
        private Button btnFromArchive;
        private Label lblSynopsis;
        private Button btnExcelSave;
        private CheckBox ArchiveSearchCheckBox;
        private CheckBox AllAddCheckBox;
        private CheckBox OnlyNewCheckBox;
        private Button btnSearchExtension;
        private Button btnOpenArchive;
        private Button CmngSoonBtn;
        private Button btnClearAchive;
        private Button btnSrchCountryDate;
        private ComboBox cmbCountry;
        private Label lblCountry;
        private Label lblReleaseDate;
        private Label label6;
        private MaskedTextBox dateFrom;
        private MaskedTextBox dateTo;
        private Button btnInCountryRls;
        private Label lblDates;
        private Button brtRemovettl;
    }
}