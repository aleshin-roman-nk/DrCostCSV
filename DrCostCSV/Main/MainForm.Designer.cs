namespace DrCostCSV
{
	partial class MainForm
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
			dataGridView1 = new DataGridView();
			label1 = new Label();
			label2 = new Label();
			dataGridView2 = new DataGridView();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
			SuspendLayout();
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.BackgroundColor = Color.AntiqueWhite;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Location = new Point(12, 50);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersWidth = 62;
			dataGridView1.Size = new Size(513, 571);
			dataGridView1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 14F);
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(217, 38);
			label1.TabIndex = 1;
			label1.Text = "Расход по дням";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 14F);
			label2.Location = new Point(546, 9);
			label2.Name = "label2";
			label2.Size = new Size(299, 38);
			label2.TabIndex = 3;
			label2.Text = "Расход по категориям";
			// 
			// dataGridView2
			// 
			dataGridView2.AllowUserToAddRows = false;
			dataGridView2.AllowUserToDeleteRows = false;
			dataGridView2.BackgroundColor = Color.AntiqueWhite;
			dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView2.Location = new Point(546, 50);
			dataGridView2.Name = "dataGridView2";
			dataGridView2.ReadOnly = true;
			dataGridView2.RowHeadersWidth = 62;
			dataGridView2.Size = new Size(513, 571);
			dataGridView2.TabIndex = 2;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1124, 633);
			Controls.Add(label2);
			Controls.Add(dataGridView2);
			Controls.Add(label1);
			Controls.Add(dataGridView1);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridView1;
		private DataGridViewTextBoxColumn dayDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn sumDataGridViewTextBoxColumn;
		private Label label1;
		private Label label2;
		private DataGridView dataGridView2;
	}
}
