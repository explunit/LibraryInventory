using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;

namespace LibraryInventory
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnImport_Click( object sender, EventArgs e )
		{
			Cursor.Current = Cursors.WaitCursor;
			Application.DoEvents();

			try
			{
				List<Book> books;
				using ( var reader = new CsvReader( new StreamReader( txtBookListPath.Text ) ) )
				{
					reader.Configuration.RegisterClassMap<BookMap>();
					books = reader.GetRecords<Book>().ToList();
				}
				var categories = books.SelectMany( b => b.Categories ).Distinct().ToList();

				var excludeCategories = categories.Where( c => c.Contains( "Rejected" ) || c.Contains( "pulled" ) ).ToList();
				var ignoreCategories = categories.Where( c => c.StartsWith( "zzz" ) ).ToList();

				new InventoryExporter( books, excludeCategories, ignoreCategories ).Export();
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void txtBookListPath_TextChanged( object sender, EventArgs e )
		{
			CheckFileInfo();
		}

		private void CheckFileInfo()
		{
			var fileInfo = new FileInfo( txtBookListPath.Text );
			if ( fileInfo.Exists )
			{
				btnImport.Enabled = true;
				txtLastUpdated.Text = $"Last updated {fileInfo.LastWriteTime}.";
				if ( ( DateTime.Now - fileInfo.LastWriteTime ).TotalMinutes > 20 )
				{
					txtLastUpdated.BackColor = System.Drawing.Color.Yellow;
					txtLastUpdated.Text += " Did you remember to export from BookDB?";
				}
			}
			else
			{
				btnImport.Enabled = false;
				txtLastUpdated.Text = "File not found.";
				txtLastUpdated.BackColor = System.Drawing.Color.Red;
			}
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			CheckFileInfo();
		}
	}
}
