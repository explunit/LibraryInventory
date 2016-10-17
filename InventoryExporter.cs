using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace LibraryInventory
{
	public class InventoryExporter
	{

		public InventoryExporter( List<Book> books, List<string> excludeCategories, List<string> ignoreCategories )
		{
			_books = books;
			_excludeCategories = excludeCategories;
			_ignoreCategories = ignoreCategories;

			_document = new Document();
			_document.UseCmykColor = true;
		}

		public void Export()
		{
			var categories = _books.SelectMany( b => b.Categories ).Distinct().ToList();

			var printCategories = categories.Where( c => !_excludeCategories.Contains( c ) && !_ignoreCategories.Contains( c ) ).ToList();
			var goodBooks = _books.Where( b => b.Categories.Intersect( _excludeCategories ).Count() == 0 ).ToList();

			foreach ( var category in printCategories )
			{
				var categoryBooks = goodBooks
					.Where( b => b.Categories.Contains( category ) )
					.OrderBy( b => b.AuthorLastName )
					.ThenBy( b => b.AuthorFirstName )
					.ThenBy( b => b.Title )
					.ThenBy( b => b.SubTitle )
					.ToList();

				AddSection( category, categoryBooks );
			}

			var maybeMistakeBooks = goodBooks.Where( b => b.Categories.Intersect( printCategories ).Count() == 0 ).ToList();
			AddSection( "Books not in valid category", maybeMistakeBooks );

			foreach ( var category in _excludeCategories )
			{
				var categoryBooks = _books
					.Where( b => b.Categories.Contains( category ) )
					.OrderBy( b => b.AuthorLastName )
					.ThenBy( b => b.AuthorFirstName )
					.ThenBy( b => b.Title )
					.ThenBy( b => b.SubTitle )
					.ToList();

				AddSection( category, categoryBooks );
			}

			var unicode = false;
			var pdfRenderer = new PdfDocumentRenderer( unicode );
			pdfRenderer.Document = _document;
			pdfRenderer.RenderDocument();

			var filename = "inventory.pdf";
			pdfRenderer.PdfDocument.Save( filename );
			Process.Start( filename );
		}

		private void AddSection( string sectionTitle, List<Book> sectionBooks )
		{
			var section = _document.AddSection();
			var pageSetup = _document.DefaultPageSetup.Clone();
			pageSetup.Orientation = Orientation.Landscape;
			pageSetup.StartingNumber = 1;
			pageSetup.TopMargin = Unit.FromInch( .4 );
			pageSetup.BottomMargin = Unit.FromInch( .5 );
			pageSetup.LeftMargin = Unit.FromInch( .7 );
			pageSetup.RightMargin = Unit.FromInch( .4 );
			pageSetup.FooterDistance = Unit.FromInch( .3 );
			section.PageSetup = pageSetup;

			var paragraph = section.AddParagraph( sectionTitle );
			paragraph.Format.Font.Bold = true;
			paragraph.Format.Font.Size = Unit.FromPoint( 16 );

			var pageNumberParagraph = new Paragraph();
			pageNumberParagraph.AddText( $"{sectionTitle} - Page " );
			pageNumberParagraph.AddPageField();
			pageNumberParagraph.AddText( " of " );
			pageNumberParagraph.AddSectionPagesField();
			pageNumberParagraph.AddTab();
			pageNumberParagraph.AddTab();
			pageNumberParagraph.AddTab();
			pageNumberParagraph.AddTab();
			pageNumberParagraph.AddDateField();
			section.Footers.Primary.Add( pageNumberParagraph );

			var table = new Table();
			table.Borders.Width = 0.75;

			table.AddColumn( Unit.FromInch( .5 ) );
			table.AddColumn( Unit.FromInch( 3 ) );
			table.AddColumn( Unit.FromInch( 5.5 ) );
			table.AddColumn( Unit.FromInch( .5 ) );
			table.AddColumn( Unit.FromInch( 1 ) );

			var row = table.AddRow();
			row.HeadingFormat = true;
			row.Shading.Color = Colors.AntiqueWhite;
			var cell = row.Cells[ 0 ];
			cell.AddParagraph( "OK" );
			cell = row.Cells[ 1 ];
			cell.AddParagraph( "Author(s)" );
			cell = row.Cells[ 2 ];
			cell.AddParagraph( "Title(s)" );
			cell = row.Cells[ 3 ];
			cell.AddParagraph( "Mssng" );
			cell = row.Cells[ 4 ];
			cell.AddParagraph( "New Cat." );

			foreach ( var book in sectionBooks )
			{
				row = table.AddRow();
				row.Format.Font.Color = Colors.Black;
				row.Format.Font.Size = Unit.FromPoint( 11 );
				cell = row.Cells[ 1 ];
				var par = cell.AddParagraph( book.GetAuthorDisplayName() );
				cell = row.Cells[ 2 ];
				cell.AddParagraph( book.GetBookDisplayName() );
			}

			section.Add( table );
		}

		private readonly List<Book> _books;
		private readonly List<string> _excludeCategories;
		private readonly List<string> _ignoreCategories;
		private Document _document;
	}
}
