using System.Collections.Generic;

namespace XMLParser
{
	public class DataAboutPattern
	{
		public string EnglishName { get; set; }
		
		public string RussianName { get; set; }
		
		public List<string> EnglishAuthors { get; set; }
		
		public List<string> RussianAuthors { get; set; }
		
		public List<string> DescriptionsSentences { get; set; }
		
		public List<string> ClaimsSentences { get; set; }
		
		public List<string> RussianAbstractsSentences { get; set; }
	}
}