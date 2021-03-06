﻿namespace BlacksmithWorkshopView
{
    partial class FormCreateOrder
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
            this.labelProduct = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelSum = new System.Windows.Forms.Label();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelProduct
            // 
            this.labelProduct.AutoSize = true;
            this.labelProduct.Location = new System.Drawing.Point(17, 17);
            this.labelProduct.Name = "labelProduct";
            this.labelProduct.Size = new System.Drawing.Size(54, 13);
            this.labelProduct.TabIndex = 0;
            this.labelProduct.Text = "Изделие:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(16, 52);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "Количество:";
            // 
            // labelSum
            // 
            this.labelSum.AutoSize = true;
            this.labelSum.Location = new System.Drawing.Point(17, 84);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(44, 13);
            this.labelSum.TabIndex = 2;
            this.labelSum.Text = "Сумма:";
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.Location = new System.Drawing.Point(91, 14);
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(148, 21);
            this.comboBoxProduct.TabIndex = 3;
            this.comboBoxProduct.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProduct_SelectedIndexChanged);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(91, 49);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(148, 20);
            this.textBoxCount.TabIndex = 4;
            this.textBoxCount.TextChanged += new System.EventHandler(this.TextBoxCount_TextChanged);
            // 
            // textBoxSum
            // 
            this.textBoxSum.Enabled = false;
            this.textBoxSum.Location = new System.Drawing.Point(91, 84);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.Size = new System.Drawing.Size(148, 20);
            this.textBoxSum.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(91, 120);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(99, 28);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(209, 120);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(111, 28);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // FormCreateOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 173);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxProduct);
            this.Controls.Add(this.labelSum);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelProduct);
            this.Name = "FormCreateOrder";
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormCreateOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProduct;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.ComboBox comboBoxProduct;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}