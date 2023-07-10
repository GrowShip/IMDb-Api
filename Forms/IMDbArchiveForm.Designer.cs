namespace MediaApi.Forms
{
    partial class IMDbArchiveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMDbArchiveForm));
            btnReturn = new Button();
            pnlArchive = new Panel();
            lblArchive = new Label();
            listBoxJson = new ListBox();
            btnUseJson = new Button();
            btnRemoveJson = new Button();
            infoLbl = new Label();
            pnlArchive.SuspendLayout();
            SuspendLayout();
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(273, 518);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(108, 31);
            btnReturn.TabIndex = 0;
            btnReturn.Text = "Go Back";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += btnReturn_Click;
            // 
            // pnlArchive
            // 
            pnlArchive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlArchive.BackColor = Color.FromArgb(12, 12, 12);
            pnlArchive.Controls.Add(lblArchive);
            pnlArchive.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            pnlArchive.Location = new Point(-1, -2);
            pnlArchive.Name = "pnlArchive";
            pnlArchive.Size = new Size(395, 57);
            pnlArchive.TabIndex = 1;
            // 
            // lblArchive
            // 
            lblArchive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblArchive.AutoSize = true;
            lblArchive.FlatStyle = FlatStyle.Flat;
            lblArchive.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblArchive.ForeColor = SystemColors.ControlLightLight;
            lblArchive.Location = new Point(103, 20);
            lblArchive.Name = "lblArchive";
            lblArchive.Size = new Size(188, 24);
            lblArchive.TabIndex = 0;
            lblArchive.Text = "Архив мета-данных";
            // 
            // listBoxJson
            // 
            listBoxJson.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxJson.FormattingEnabled = true;
            listBoxJson.ItemHeight = 15;
            listBoxJson.Location = new Point(34, 141);
            listBoxJson.Name = "listBoxJson";
            listBoxJson.Size = new Size(333, 244);
            listBoxJson.TabIndex = 2;
            // 
            // btnUseJson
            // 
            btnUseJson.FlatStyle = FlatStyle.Flat;
            btnUseJson.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnUseJson.Location = new Point(199, 443);
            btnUseJson.Name = "btnUseJson";
            btnUseJson.Size = new Size(168, 35);
            btnUseJson.TabIndex = 3;
            btnUseJson.Text = "Использовать";
            btnUseJson.UseVisualStyleBackColor = true;
            btnUseJson.Click += btnUseJson_Click;
            // 
            // btnRemoveJson
            // 
            btnRemoveJson.FlatStyle = FlatStyle.Flat;
            btnRemoveJson.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnRemoveJson.Location = new Point(34, 443);
            btnRemoveJson.Name = "btnRemoveJson";
            btnRemoveJson.Size = new Size(159, 35);
            btnRemoveJson.TabIndex = 4;
            btnRemoveJson.Text = "Удалить";
            btnRemoveJson.UseVisualStyleBackColor = true;
            btnRemoveJson.Click += btnRemoveJson_Click;
            // 
            // infoLbl
            // 
            infoLbl.AutoSize = true;
            infoLbl.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            infoLbl.Location = new Point(39, 108);
            infoLbl.Name = "infoLbl";
            infoLbl.Size = new Size(323, 16);
            infoLbl.TabIndex = 5;
            infoLbl.Text = "file.json - основной файл для работы приложения";
            // 
            // IMDbArchiveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(393, 561);
            Controls.Add(infoLbl);
            Controls.Add(btnRemoveJson);
            Controls.Add(btnUseJson);
            Controls.Add(listBoxJson);
            Controls.Add(pnlArchive);
            Controls.Add(btnReturn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(409, 600);
            MinimumSize = new Size(409, 600);
            Name = "IMDbArchiveForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form2";
            pnlArchive.ResumeLayout(false);
            pnlArchive.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReturn;
        private Panel pnlArchive;
        private ListBox listBoxJson;
        private Button btnUseJson;
        private Label lblArchive;
        private Button btnRemoveJson;
        private Label infoLbl;
    }
}