namespace LibraryInventory
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.txtLastUpdated = new System.Windows.Forms.Label();
			this.txtBookListPath = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Exported Booklist path";
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(170, 76);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 1;
			this.btnImport.Text = "Generate";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// txtLastUpdated
			// 
			this.txtLastUpdated.AutoSize = true;
			this.txtLastUpdated.Location = new System.Drawing.Point(170, 57);
			this.txtLastUpdated.Name = "txtLastUpdated";
			this.txtLastUpdated.Size = new System.Drawing.Size(0, 13);
			this.txtLastUpdated.TabIndex = 2;
			// 
			// txtBookListPath
			// 
			this.txtBookListPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LibraryInventory.Properties.Settings.Default, "BookListPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtBookListPath.Location = new System.Drawing.Point(170, 31);
			this.txtBookListPath.Name = "txtBookListPath";
			this.txtBookListPath.Size = new System.Drawing.Size(529, 20);
			this.txtBookListPath.TabIndex = 0;
			this.txtBookListPath.Text = global::LibraryInventory.Properties.Settings.Default.BookListPath;
			this.txtBookListPath.TextChanged += new System.EventHandler(this.txtBookListPath_TextChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(829, 359);
			this.Controls.Add(this.txtLastUpdated);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtBookListPath);
			this.Name = "Form1";
			this.Text = "Library Inventory";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtBookListPath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Label txtLastUpdated;
	}
}

