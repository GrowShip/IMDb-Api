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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMDbSearchForm));
            btnSearch = new Button();
            txtBoxSearch = new TextBox();
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
            prgProccess = new ProgressBar();
            lblTotalTitles = new Label();
            chBoxUpdDown = new CheckBox();
            dGVtitles = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            titleDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            titleRusDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            imageDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            runtimeStrDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            genresDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            contentRatingDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            iMDbRatingDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            plotDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            starsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            locationSearchDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            typeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            yearDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            releaseDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            awardsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            directorsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            companiesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            countriesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            languagesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            grossWorldDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            counrtyReleaseAllDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jsonDataBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)pictPoster).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dGVtitles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)jsonDataBindingSource).BeginInit();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.BackColor = SystemColors.ActiveCaption;
            resources.ApplyResources(btnSearch, "btnSearch");
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
            // pictPoster
            // 
            resources.ApplyResources(pictPoster, "pictPoster");
            pictPoster.BackColor = SystemColors.ButtonFace;
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
            cmbCountry.FormattingEnabled = true;
            resources.ApplyResources(cmbCountry, "cmbCountry");
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
            // prgProccess
            // 
            resources.ApplyResources(prgProccess, "prgProccess");
            prgProccess.Name = "prgProccess";
            prgProccess.Step = 1;
            prgProccess.VisibleChanged += prgProccess_VisibleChanged;
            // 
            // lblTotalTitles
            // 
            resources.ApplyResources(lblTotalTitles, "lblTotalTitles");
            lblTotalTitles.Name = "lblTotalTitles";
            // 
            // chBoxUpdDown
            // 
            resources.ApplyResources(chBoxUpdDown, "chBoxUpdDown");
            chBoxUpdDown.Name = "chBoxUpdDown";
            chBoxUpdDown.UseVisualStyleBackColor = true;
            // 
            // dGVtitles
            // 
            dGVtitles.AllowUserToAddRows = false;
            dGVtitles.AllowUserToDeleteRows = false;
            resources.ApplyResources(dGVtitles, "dGVtitles");
            dGVtitles.AutoGenerateColumns = false;
            dGVtitles.BackgroundColor = SystemColors.ButtonFace;
            dGVtitles.BorderStyle = BorderStyle.None;
            dGVtitles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVtitles.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, titleDataGridViewTextBoxColumn, titleRusDataGridViewTextBoxColumn, imageDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, runtimeStrDataGridViewTextBoxColumn, genresDataGridViewTextBoxColumn, contentRatingDataGridViewTextBoxColumn, iMDbRatingDataGridViewTextBoxColumn, plotDataGridViewTextBoxColumn, starsDataGridViewTextBoxColumn, locationSearchDataGridViewTextBoxColumn, typeDataGridViewTextBoxColumn, yearDataGridViewTextBoxColumn, releaseDateDataGridViewTextBoxColumn, awardsDataGridViewTextBoxColumn, directorsDataGridViewTextBoxColumn, companiesDataGridViewTextBoxColumn, countriesDataGridViewTextBoxColumn, languagesDataGridViewTextBoxColumn, grossWorldDataGridViewTextBoxColumn, counrtyReleaseAllDataGridViewTextBoxColumn });
            dGVtitles.Cursor = Cursors.Hand;
            dGVtitles.DataSource = jsonDataBindingSource;
            dGVtitles.GridColor = SystemColors.ButtonFace;
            dGVtitles.Name = "dGVtitles";
            dGVtitles.ReadOnly = true;
            dGVtitles.RowTemplate.Height = 25;
            dGVtitles.SelectionChanged += dGVtitles_SelectionChanged;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            titleDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            resources.ApplyResources(titleDataGridViewTextBoxColumn, "titleDataGridViewTextBoxColumn");
            titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleRusDataGridViewTextBoxColumn
            // 
            titleRusDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            titleRusDataGridViewTextBoxColumn.DataPropertyName = "TitleRus";
            resources.ApplyResources(titleRusDataGridViewTextBoxColumn, "titleRusDataGridViewTextBoxColumn");
            titleRusDataGridViewTextBoxColumn.Name = "titleRusDataGridViewTextBoxColumn";
            titleRusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // imageDataGridViewTextBoxColumn
            // 
            imageDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            imageDataGridViewTextBoxColumn.DataPropertyName = "Image";
            imageDataGridViewTextBoxColumn.FillWeight = 50F;
            resources.ApplyResources(imageDataGridViewTextBoxColumn, "imageDataGridViewTextBoxColumn");
            imageDataGridViewTextBoxColumn.Name = "imageDataGridViewTextBoxColumn";
            imageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.FillWeight = 50F;
            resources.ApplyResources(descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // runtimeStrDataGridViewTextBoxColumn
            // 
            runtimeStrDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            runtimeStrDataGridViewTextBoxColumn.DataPropertyName = "RuntimeStr";
            resources.ApplyResources(runtimeStrDataGridViewTextBoxColumn, "runtimeStrDataGridViewTextBoxColumn");
            runtimeStrDataGridViewTextBoxColumn.Name = "runtimeStrDataGridViewTextBoxColumn";
            runtimeStrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // genresDataGridViewTextBoxColumn
            // 
            genresDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            genresDataGridViewTextBoxColumn.DataPropertyName = "Genres";
            resources.ApplyResources(genresDataGridViewTextBoxColumn, "genresDataGridViewTextBoxColumn");
            genresDataGridViewTextBoxColumn.Name = "genresDataGridViewTextBoxColumn";
            genresDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // contentRatingDataGridViewTextBoxColumn
            // 
            contentRatingDataGridViewTextBoxColumn.DataPropertyName = "ContentRating";
            resources.ApplyResources(contentRatingDataGridViewTextBoxColumn, "contentRatingDataGridViewTextBoxColumn");
            contentRatingDataGridViewTextBoxColumn.Name = "contentRatingDataGridViewTextBoxColumn";
            contentRatingDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iMDbRatingDataGridViewTextBoxColumn
            // 
            iMDbRatingDataGridViewTextBoxColumn.DataPropertyName = "IMDbRating";
            resources.ApplyResources(iMDbRatingDataGridViewTextBoxColumn, "iMDbRatingDataGridViewTextBoxColumn");
            iMDbRatingDataGridViewTextBoxColumn.Name = "iMDbRatingDataGridViewTextBoxColumn";
            iMDbRatingDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // plotDataGridViewTextBoxColumn
            // 
            plotDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            plotDataGridViewTextBoxColumn.DataPropertyName = "Plot";
            plotDataGridViewTextBoxColumn.FillWeight = 50F;
            resources.ApplyResources(plotDataGridViewTextBoxColumn, "plotDataGridViewTextBoxColumn");
            plotDataGridViewTextBoxColumn.Name = "plotDataGridViewTextBoxColumn";
            plotDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // starsDataGridViewTextBoxColumn
            // 
            starsDataGridViewTextBoxColumn.DataPropertyName = "Stars";
            resources.ApplyResources(starsDataGridViewTextBoxColumn, "starsDataGridViewTextBoxColumn");
            starsDataGridViewTextBoxColumn.Name = "starsDataGridViewTextBoxColumn";
            starsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // locationSearchDataGridViewTextBoxColumn
            // 
            locationSearchDataGridViewTextBoxColumn.DataPropertyName = "LocationSearch";
            resources.ApplyResources(locationSearchDataGridViewTextBoxColumn, "locationSearchDataGridViewTextBoxColumn");
            locationSearchDataGridViewTextBoxColumn.Name = "locationSearchDataGridViewTextBoxColumn";
            locationSearchDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            typeDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            resources.ApplyResources(typeDataGridViewTextBoxColumn, "typeDataGridViewTextBoxColumn");
            typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // yearDataGridViewTextBoxColumn
            // 
            yearDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            yearDataGridViewTextBoxColumn.DataPropertyName = "Year";
            resources.ApplyResources(yearDataGridViewTextBoxColumn, "yearDataGridViewTextBoxColumn");
            yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
            yearDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // releaseDateDataGridViewTextBoxColumn
            // 
            releaseDateDataGridViewTextBoxColumn.DataPropertyName = "ReleaseDate";
            resources.ApplyResources(releaseDateDataGridViewTextBoxColumn, "releaseDateDataGridViewTextBoxColumn");
            releaseDateDataGridViewTextBoxColumn.Name = "releaseDateDataGridViewTextBoxColumn";
            releaseDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // awardsDataGridViewTextBoxColumn
            // 
            awardsDataGridViewTextBoxColumn.DataPropertyName = "Awards";
            resources.ApplyResources(awardsDataGridViewTextBoxColumn, "awardsDataGridViewTextBoxColumn");
            awardsDataGridViewTextBoxColumn.Name = "awardsDataGridViewTextBoxColumn";
            awardsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // directorsDataGridViewTextBoxColumn
            // 
            directorsDataGridViewTextBoxColumn.DataPropertyName = "Directors";
            resources.ApplyResources(directorsDataGridViewTextBoxColumn, "directorsDataGridViewTextBoxColumn");
            directorsDataGridViewTextBoxColumn.Name = "directorsDataGridViewTextBoxColumn";
            directorsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // companiesDataGridViewTextBoxColumn
            // 
            companiesDataGridViewTextBoxColumn.DataPropertyName = "Companies";
            resources.ApplyResources(companiesDataGridViewTextBoxColumn, "companiesDataGridViewTextBoxColumn");
            companiesDataGridViewTextBoxColumn.Name = "companiesDataGridViewTextBoxColumn";
            companiesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countriesDataGridViewTextBoxColumn
            // 
            countriesDataGridViewTextBoxColumn.DataPropertyName = "Countries";
            resources.ApplyResources(countriesDataGridViewTextBoxColumn, "countriesDataGridViewTextBoxColumn");
            countriesDataGridViewTextBoxColumn.Name = "countriesDataGridViewTextBoxColumn";
            countriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // languagesDataGridViewTextBoxColumn
            // 
            languagesDataGridViewTextBoxColumn.DataPropertyName = "Languages";
            resources.ApplyResources(languagesDataGridViewTextBoxColumn, "languagesDataGridViewTextBoxColumn");
            languagesDataGridViewTextBoxColumn.Name = "languagesDataGridViewTextBoxColumn";
            languagesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // grossWorldDataGridViewTextBoxColumn
            // 
            grossWorldDataGridViewTextBoxColumn.DataPropertyName = "GrossWorld";
            resources.ApplyResources(grossWorldDataGridViewTextBoxColumn, "grossWorldDataGridViewTextBoxColumn");
            grossWorldDataGridViewTextBoxColumn.Name = "grossWorldDataGridViewTextBoxColumn";
            grossWorldDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counrtyReleaseAllDataGridViewTextBoxColumn
            // 
            counrtyReleaseAllDataGridViewTextBoxColumn.DataPropertyName = "CounrtyReleaseAll";
            resources.ApplyResources(counrtyReleaseAllDataGridViewTextBoxColumn, "counrtyReleaseAllDataGridViewTextBoxColumn");
            counrtyReleaseAllDataGridViewTextBoxColumn.Name = "counrtyReleaseAllDataGridViewTextBoxColumn";
            counrtyReleaseAllDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // jsonDataBindingSource
            // 
            jsonDataBindingSource.DataSource = typeof(Structure.JsonData);
            // 
            // IMDbSearchForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(dGVtitles);
            Controls.Add(chBoxUpdDown);
            Controls.Add(lblTotalTitles);
            Controls.Add(prgProccess);
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
            Controls.Add(txtBoxSearch);
            Controls.Add(btnSearch);
            Name = "IMDbSearchForm";
            Load += SearchForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictPoster).EndInit();
            ((System.ComponentModel.ISupportInitialize)dGVtitles).EndInit();
            ((System.ComponentModel.ISupportInitialize)jsonDataBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSearch;
        private TextBox txtBoxSearch;
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
        private ProgressBar prgProccess;
        private Label lblTotalTitles;
        private CheckBox chBoxUpdDown;
        private DataGridView dGVtitles;
        private BindingSource jsonDataBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn titleRusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn imageDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn runtimeStrDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn genresDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn contentRatingDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iMDbRatingDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn plotDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn starsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn locationSearchDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn releaseDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn awardsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn directorsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn companiesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn countriesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn languagesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn grossWorldDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn counrtyReleaseAllDataGridViewTextBoxColumn;
    }
}