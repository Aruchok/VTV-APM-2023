using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace XMLParser
{
	class Program
	{
		static void Main(string[] args)
		{
			// Variable for getting all directory contains .xml files.
			var allfiles = Directory.GetFiles("Resources", "*.xml", SearchOption.AllDirectories);

			// Adds records in list.
			foreach (var file in allfiles)
			{
				var doc = new XmlDocument();
				doc.Load(file);

				_listDataAboutPattern.Add(GetDataAboutPattern(doc));
			}

			Console.WriteLine("Hello World!");
		}

		/// <summary>
		/// Provides list with data about pattern.
		/// </summary>
		private static List<DataAboutPattern> _listDataAboutPattern = new List<DataAboutPattern>();

		/// <summary>
		/// Provides list indexes for clearing.
		/// </summary>
		private static List<int> _indexes = new List<int>();

		/// <summary>
		/// Provides getting DataAboutPattern object.
		/// </summary>
		/// <param name="doc">XmlDocument document.</param>
		/// <returns>New DataAboutPattern object.</returns>
		private static DataAboutPattern GetDataAboutPattern(XmlDocument doc)
		{
			try
			{
				var allXmlElementsInDoc =
					doc.ChildNodes.OfType<XmlElement>().First().ChildNodes.OfType<XmlElement>().ToList();

				var ruName = allXmlElementsInDoc
					.Where(i => i.Name == "SDOBI").ToList()
					.First().ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "B500")
					.ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "ru-b540")
					.ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "ru-b542")
					.InnerText;

				var euName = allXmlElementsInDoc
					.Where(i => i.Name == "SDOBI").ToList()
					.Last().ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "B500")
					.ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "ru-b540")
					.ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "ru-b542")
					.InnerText;

				var ruAuthors = allXmlElementsInDoc
					.Where(i => i.Name == "SDOBI").ToList()
					.First().ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "B700")
					.ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "B720")
					.ChildNodes.OfType<XmlElement>()
					.Where(i => i.Name == "B721")
					.Select(i => i.InnerText).ToList();

				var euAuthors = allXmlElementsInDoc
					.Where(i => i.Name == "SDOBI").ToList()
					.Last().ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "B700")
					.ChildNodes.OfType<XmlElement>()
					.First(i => i.Name == "B720")
					.ChildNodes.OfType<XmlElement>()
					.Where(i => i.Name == "B721")
					.Select(i => i.InnerText).ToList();

				var descriptions = allXmlElementsInDoc
					.First(i => i.Name == "description")
					.ChildNodes.OfType<XmlElement>()
					.Select(i => i.InnerText).ToList();

				var claims = allXmlElementsInDoc
					.First(i => i.Name == "claims")
					.ChildNodes.OfType<XmlElement>()
					.Where(i => i.Name == "claim").ToList()
					.Select(i => i.InnerText).ToList();

				var ruAbstracts = allXmlElementsInDoc
					.First(i => i.Name == "abstract")
					.ChildNodes.OfType<XmlElement>()
					.Where(i => i.Name == "p").ToList()
					.Select(i => i.InnerText).ToList();

				ClearListString(descriptions);
				ClearListString(claims);
				ClearListString(ruAbstracts);

				return new DataAboutPattern
				{
					EnglishName = euName,
					RussianName = ruName,
					EnglishAuthors = euAuthors,
					RussianAuthors = ruAuthors,
					DescriptionsSentences = descriptions,
					ClaimsSentences = claims,
					RussianAbstractsSentences = ruAbstracts
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}

		/// <summary>
		/// Provides clearing list for string list.
		/// </summary>
		/// <param name="list">String list.</param>
		private static void ClearListString(IList<string> list)
		{
			for (var i = 0; i < list.Count; i++)
				if (list[i].Length < 15)
					_indexes.Add(i);

			foreach (var index in _indexes.OrderByDescending(v => v))
			{
				list.RemoveAt(index);
			}

			if (_indexes.Count != 0)
				_indexes.RemoveRange(0, _indexes.Count);
		}
	}
}