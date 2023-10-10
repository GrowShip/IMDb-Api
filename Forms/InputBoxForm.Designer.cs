namespace MediaApi.Forms
{
    partial class InputBoxForm
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
            btnCancel = new Button();
            btnOK = new Button();
            inputText = new TextBox();
            lblText = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(12, 74);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(156, 36);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(246, 74);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(156, 38);
            btnOK.TabIndex = 1;
            btnOK.Text = "Ok";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOk_Click;
            // 
            // inputText
            // 
            inputText.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            inputText.Location = new Point(12, 41);
            inputText.Name = "inputText";
            inputText.PlaceholderText = "Only ENGLISH";
            inputText.Size = new Size(390, 27);
            inputText.TabIndex = 2;
            inputText.TextAlign = HorizontalAlignment.Center;
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblText.Location = new Point(28, 11);
            lblText.Name = "lblText";
            lblText.Size = new Size(358, 20);
            lblText.TabIndex = 3;
            lblText.Text = "Введите имя для архивного файла на английском";
            lblText.TextAlign = ContentAlignment.TopCenter;
            // 
            // InputBoxForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(414, 123);
            ControlBox = false;
            Controls.Add(lblText);
            Controls.Add(inputText);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Name = "InputBoxForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InputBoxForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private TextBox inputText;
        private Label lblText;
    }
}