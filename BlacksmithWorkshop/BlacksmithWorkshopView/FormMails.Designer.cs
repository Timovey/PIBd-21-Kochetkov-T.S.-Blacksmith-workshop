namespace BlacksmithWorkshopView
{
    partial class FormMails
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.textBoxPage = new System.Windows.Forms.TextBox();
            this.buttonPage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(111, 89);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(396, 214);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(436, 330);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(171, 44);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Следующая";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(12, 330);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(167, 46);
            this.buttonPrev.TabIndex = 2;
            this.buttonPrev.Text = "Предыдущая";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // textBoxPage
            // 
            this.textBoxPage.Location = new System.Drawing.Point(83, 17);
            this.textBoxPage.Name = "textBoxPage";
            this.textBoxPage.Size = new System.Drawing.Size(137, 20);
            this.textBoxPage.TabIndex = 3;
            // 
            // buttonPage
            // 
            this.buttonPage.Location = new System.Drawing.Point(263, 14);
            this.buttonPage.Name = "buttonPage";
            this.buttonPage.Size = new System.Drawing.Size(150, 33);
            this.buttonPage.TabIndex = 4;
            this.buttonPage.Text = "Перейти";
            this.buttonPage.UseVisualStyleBackColor = true;
            this.buttonPage.Click += new System.EventHandler(this.buttonPage_Click);
            // 
            // FormMails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 378);
            this.Controls.Add(this.buttonPage);
            this.Controls.Add(this.textBoxPage);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormMails";
            this.Text = "Почта";
            this.Load += new System.EventHandler(this.FormMails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.TextBox textBoxPage;
        private System.Windows.Forms.Button buttonPage;
    }
}