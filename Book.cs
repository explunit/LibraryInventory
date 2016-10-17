using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration;

namespace LibraryInventory
{
	public class Book
	{
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string AuthorLastName { get; set; }
		public string AuthorFirstName { get; set; }
		public string CoAuthorLastName { get; set; }
		public string CoAuthorFirstName { get; set; }
		public string Publisher { get; set; }
		public List<string> Categories { get; set; }
		public string GetAuthorDisplayName( )
		{
			string displayName = AuthorLastName;
			if (!string.IsNullOrWhiteSpace(AuthorFirstName))
			{
				displayName += $", {AuthorFirstName}";
			}
			if ( !string.IsNullOrWhiteSpace( CoAuthorLastName ) )
			{
				displayName += $"; {CoAuthorLastName}";
			}
			if ( !string.IsNullOrWhiteSpace( CoAuthorFirstName ) )
			{
				displayName += $", {CoAuthorFirstName}";
			}
			return displayName;
		}

		public string GetBookDisplayName()
		{
			string displayName = Title;
			if ( !string.IsNullOrWhiteSpace( SubTitle ) )
			{
				displayName += $" ({SubTitle})";
			}
			return displayName;
		}
	}

	public sealed class BookMap : CsvClassMap<Book>
	{
		private List<string> categoryColumns =
			new List<string> { "Cat0", "Cat1", "Cat2", "Cat3", "Cat4", "Cat5", "Cat6", "Cat7", "Cat8", "Cat9", "Cat10" };

		public override void CreateMap()
		{
			Map( m => m.Title ).Name( "Title" );
			Map( m => m.SubTitle ).Name( "OriginalTitle" );
			Map( m => m.AuthorLastName ).Name( "Surname" );
			Map( m => m.AuthorFirstName ).Name( "FirstNames" );
			Map( m => m.CoAuthorLastName ).Name( "2ndSurname" );
			Map( m => m.CoAuthorFirstName ).Name( "2ndFirstNames" );
			Map( m => m.Publisher ).Name( "Publisher" );
			Map( m => m.Categories ).ConvertUsing( row =>
				categoryColumns
					.Select( column => row.GetField<string>( column ) )
					.Where( value => String.IsNullOrWhiteSpace( value ) == false )
					.ToList()
				);
		}
	}
}
