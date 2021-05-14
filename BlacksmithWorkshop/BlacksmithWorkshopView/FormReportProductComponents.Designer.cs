namespace BlacksmithWorkshopView
{
	partial class FormReportProductComponents
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
			this.buttonSaveToExel = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.ColumnComponent = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnManufacture = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSaveToExel
			// 
			this.buttonSaveToExel.Location = new System.Drawing.Point(28, 10);
			this.buttonSaveToExel.Name = "buttonSaveToExel";
			this.buttonSaveToExel.Size = new System.Drawing.Size(152, 33);
			this.buttonSaveToExel.TabIndex = 0;
			this.buttonSaveToExel.Text = "Сохранить в Exsel";
			this.buttonSaveToExel.UseVisualStyleBackColor = true;
			this.buttonSaveToExel.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnComponent,
            this.ColumnManufacture,
            this.ColumnCount});
			this.dataGridView.Location = new System.Drawing.Point(15, 67);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Size = new System.Drawing.Size(738, 361);
			this.dataGridView.TabIndex = 1;
			// 
			// ColumnComponent
			// 
			this.ColumnComponent.HeaderText = "Компонент";
			this.ColumnComponent.Name = "ColumnComponent";
			// 
			// ColumnManufacture
			// 
			this.ColumnManufacture.HeaderText = "Изделие";
			this.ColumnManufacture.Name = "ColumnManufacture";
			// 
			// ColumnCount
			// 
			this.ColumnCount.HeaderText = "Количество";
			this.ColumnCount.Name = "ColumnCount";
			// 
			// FormReportProductComponents
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.buttonSaveToExel);
			this.Name = "FormReportProductComponents";
			this.Text = "Компоненты по изделиям";
			this.Load += new System.EventHandler(this.FormReportProductComponents_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSaveToExel;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComponent;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnManufacture;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
	}
}