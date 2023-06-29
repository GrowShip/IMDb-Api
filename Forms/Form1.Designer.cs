namespace IMDbApi
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            SearchButton = new Button();
            textBox1 = new TextBox();
            listBox1 = new ListBox();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            button2 = new Button();
            button4 = new Button();
            linkLabel1 = new LinkLabel();
            button5 = new Button();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label4 = new Label();
            button3 = new Button();
            ArchiveSearchCheckBox = new CheckBox();
            AllAddCheckBox = new CheckBox();
            progressBar1 = new ProgressBar();
            OnlyNewCheckBox = new CheckBox();
            SearchTit = new Button();
            label5 = new Label();
            label6 = new Label();
            scndForm = new Button();
            CmngSoonBtn = new Button();
            dateFrom = new MaskedTextBox();
            dateTo = new MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // SearchButton
            // 
            resources.ApplyResources(SearchButton, "SearchButton");
            SearchButton.Name = "SearchButton";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += button1_Click;
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            resources.ApplyResources(listBox1, "listBox1");
            listBox1.Name = "listBox1";
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ControlLight;
            resources.ApplyResources(button5, "button5");
            button5.Name = "button5";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            resources.ApplyResources(comboBox1, "comboBox1");
            comboBox1.Name = "comboBox1";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
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
            // progressBar1
            // 
            progressBar1.BackColor = SystemColors.ActiveCaption;
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
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
            SearchTit.Name = "SearchTit";
            SearchTit.UseVisualStyleBackColor = true;
            SearchTit.Click += SearchTit_Click;
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
            // scndForm
            // 
            resources.ApplyResources(scndForm, "scndForm");
            scndForm.Name = "scndForm";
            scndForm.UseVisualStyleBackColor = true;
            scndForm.Click += scndForm_Click;
            // 
            // CmngSoonBtn
            // 
            resources.ApplyResources(CmngSoonBtn, "CmngSoonBtn");
            CmngSoonBtn.Name = "CmngSoonBtn";
            CmngSoonBtn.UseVisualStyleBackColor = true;
            CmngSoonBtn.Click += CmngSoonBtn_Click;
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
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(dateTo);
            Controls.Add(dateFrom);
            Controls.Add(CmngSoonBtn);
            Controls.Add(scndForm);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(SearchTit);
            Controls.Add(OnlyNewCheckBox);
            Controls.Add(progressBar1);
            Controls.Add(AllAddCheckBox);
            Controls.Add(ArchiveSearchCheckBox);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(button5);
            Controls.Add(linkLabel1);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(listBox1);
            Controls.Add(textBox1);
            Controls.Add(SearchButton);
            Controls.Add(label1);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button SearchButton;
        private TextBox textBox1;
        private ListBox listBox1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button button2;
        private Button button4;
        private LinkLabel linkLabel1;
        private Button button5;
        private Label label3;
        private ComboBox comboBox1;
        private Label label4;
        private Button button3;
        private CheckBox ArchiveSearchCheckBox;
        private CheckBox AllAddCheckBox;
        private ProgressBar progressBar1;
        private CheckBox OnlyNewCheckBox;
        private Button SearchTit;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button scndForm;
        private Button CmngSoonBtn;
        private MaskedTextBox dateFrom;
        private MaskedTextBox dateTo;
    }
}