namespace MediaApi.Forms
{
    partial class KPSearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pctPoster = new PictureBox();
            lblDescribtion = new Label();
            textSearch = new TextBox();
            button1 = new Button();
            checkBoxArchiveSearch = new CheckBox();
            listTitles = new ListBox();
            lblSynopsis = new Label();
            ((System.ComponentModel.ISupportInitialize)pctPoster).BeginInit();
            SuspendLayout();
            // 
            // pctPoster
            // 
            pctPoster.BackColor = SystemColors.ButtonFace;
            pctPoster.Location = new Point(12, 7);
            pctPoster.Name = "pctPoster";
            pctPoster.Size = new Size(202, 264);
            pctPoster.TabIndex = 0;
            pctPoster.TabStop = false;
            // 
            // lblDescribtion
            // 
            lblDescribtion.AutoSize = true;
            lblDescribtion.BackColor = SystemColors.ButtonFace;
            lblDescribtion.Location = new Point(217, 7);
            lblDescribtion.Margin = new Padding(0);
            lblDescribtion.MinimumSize = new Size(202, 264);
            lblDescribtion.Name = "lblDescribtion";
            lblDescribtion.Size = new Size(202, 264);
            lblDescribtion.TabIndex = 1;
            lblDescribtion.Text = "label1";
            // 
            // textSearch
            // 
            textSearch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textSearch.Location = new Point(428, 7);
            textSearch.Name = "textSearch";
            textSearch.Size = new Size(305, 29);
            textSearch.TabIndex = 2;
            textSearch.Text = "КиноПоиск";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(744, 7);
            button1.Name = "button1";
            button1.Size = new Size(95, 36);
            button1.TabIndex = 3;
            button1.Text = "Найти";
            button1.UseVisualStyleBackColor = false;
            // 
            // checkBoxArchiveSearch
            // 
            checkBoxArchiveSearch.AutoSize = true;
            checkBoxArchiveSearch.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxArchiveSearch.FlatStyle = FlatStyle.Flat;
            checkBoxArchiveSearch.Location = new Point(662, 36);
            checkBoxArchiveSearch.Name = "checkBoxArchiveSearch";
            checkBoxArchiveSearch.Size = new Size(80, 19);
            checkBoxArchiveSearch.TabIndex = 4;
            checkBoxArchiveSearch.Text = "По архиву";
            checkBoxArchiveSearch.TextAlign = ContentAlignment.MiddleRight;
            checkBoxArchiveSearch.UseVisualStyleBackColor = true;
            // 
            // listTitles
            // 
            listTitles.BackColor = SystemColors.ButtonFace;
            listTitles.BorderStyle = BorderStyle.None;
            listTitles.FormattingEnabled = true;
            listTitles.ItemHeight = 15;
            listTitles.Location = new Point(430, 56);
            listTitles.Name = "listTitles";
            listTitles.Size = new Size(409, 315);
            listTitles.TabIndex = 5;
            // 
            // lblSynopsis
            // 
            lblSynopsis.AutoSize = true;
            lblSynopsis.BackColor = SystemColors.ButtonFace;
            lblSynopsis.Location = new Point(15, 276);
            lblSynopsis.MinimumSize = new Size(404, 95);
            lblSynopsis.Name = "lblSynopsis";
            lblSynopsis.Size = new Size(404, 95);
            lblSynopsis.TabIndex = 6;
            lblSynopsis.Text = "label1";
            lblSynopsis.TextAlign = ContentAlignment.MiddleCenter;
            lblSynopsis.Click += lblSynopsis_Click;
            // 
            // KPSearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(847, 498);
            Controls.Add(lblSynopsis);
            Controls.Add(listTitles);
            Controls.Add(checkBoxArchiveSearch);
            Controls.Add(button1);
            Controls.Add(textSearch);
            Controls.Add(lblDescribtion);
            Controls.Add(pctPoster);
            MinimumSize = new Size(863, 537);
            Name = "KPSearchForm";
            Text = "KPSearchForm";
            ((System.ComponentModel.ISupportInitialize)pctPoster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pctPoster;
        private Label lblDescribtion;
        private TextBox textSearch;
        private Button button1;
        private CheckBox checkBoxArchiveSearch;
        private ListBox listTitles;
        private Label lblSynopsis;
    }
}