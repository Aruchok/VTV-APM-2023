using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace XMLParser
{
	public class XmlParser
	{
		private static void Main(string[] args)
		{
			// Variable for getting all directory contains .xml files.
			var allFiles = Directory.GetFiles("Resources", "*.xml", SearchOption.AllDirectories);
			var xmlParser = new XmlParser();

			// Adds records in list.
			foreach (var file in allFiles)
			{
				var doc = new XmlDocument();
				doc.Load(file);

				ListDataAboutPattern.Add(xmlParser.GetDataAboutPattern(doc));
			}

			foreach (var dataAboutPattern in ListDataAboutPattern)
			{
				var filePath = Path.Combine("Resources", "Result", $"{dataAboutPattern.RussianName}.txt");
				if (File.Exists(filePath))
					continue;

				File.Create(filePath).Dispose();
				using var sw = new StreamWriter(filePath);
				dataAboutPattern.DescriptionsSentences.ForEach(item => sw.WriteLine(item));

				sw.Close();
			}

			// var resultList = new List<string>();
			// foreach (var dataAboutPattern in ListDataAboutPattern)
			// {
			// 	var result = JsonConvert.SerializeObject(dataAboutPattern, (Newtonsoft.Json.Formatting)Formatting.Indented);
			// 	resultList.Add(result);
			// }
			//
			// // {
			// // 	Console.WriteLine("");
			// // }
			Console.WriteLine("");
		}

		/// <summary>
		/// Provides list with data about pattern.
		/// </summary>
		private static readonly List<DataAboutPattern> ListDataAboutPattern = new List<DataAboutPattern>();

		/// <summary>
		/// Provides list indexes for clearing.
		/// </summary>
		private static readonly List<int> Indexes = new List<int>();

		/// <summary>
		/// Provides getting DataAboutPattern object.
		/// </summary>
		/// <param name="doc">XmlDocument document.</param>
		/// <returns>New DataAboutPattern object.</returns>
		public DataAboutPattern GetDataAboutPattern(XmlDocument doc)
		{
			try
			{
				var allXmlElementsInDoc =
					doc.ChildNodes.OfType<XmlElement>().First().ChildNodes.OfType<XmlElement>().ToList();

				var patentNum =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B100")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B190")
						.InnerText + allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B100")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B110")
						.InnerText + allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B100")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B130")
						.InnerText;

				var ruName =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B500")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "ru-b540")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "ru-b542")
						.InnerText;

				var euName =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.Last().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B500")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "ru-b540")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "ru-b542")
						.InnerText;

				var classIndexXml =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B500")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B510EP")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "classification-ipcr")
						.ChildNodes.OfType<XmlElement>();

				var classIndex =
					classIndexXml.First(i => i.Name == "section").InnerText +
					classIndexXml.First(i => i.Name == "class").InnerText +
					classIndexXml.First(i => i.Name == "subclass").InnerText +
					classIndexXml.First(i => i.Name == "main-group").InnerText + "/" +
					classIndexXml.First(i => i.Name == "subgroup").InnerText;


				var ruAuthors =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B700")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B720")
						.ChildNodes.OfType<XmlElement>()
						.Where(i => i.Name == "B721")
						.Select(i => i.InnerText).ToList();

				var euAuthors =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.Last().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B700")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B720")
						.ChildNodes.OfType<XmlElement>()
						.Where(i => i.Name == "B721")
						.Select(i => i.InnerText).ToList();

				var publishDateString =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.Last().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B100")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B140")
						.InnerText;


				var publishDate = DateTime.ParseExact(publishDateString, "yyyyMMdd",
					System.Globalization.CultureInfo.InvariantCulture);

				var citation =
					allXmlElementsInDoc
						.Where(i => i.Name == "SDOBI").ToList()
						.First().ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B500")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "B560")
						.ChildNodes.OfType<XmlElement>()
						.First(i => i.Name == "ru-b560")
						.InnerText;

				var descriptions =
					allXmlElementsInDoc
						.First(i => i.Name == "description")
						.ChildNodes.OfType<XmlElement>()
						.Select(i => i.InnerText).ToList();

				var claims =
					allXmlElementsInDoc
						.First(i => i.Name == "claims")
						.ChildNodes.OfType<XmlElement>()
						.Where(i => i.Name == "claim").ToList()
						.Select(i => i.InnerText).ToList();

				var ruAbstracts =
					allXmlElementsInDoc
						.First(i => i.Name == "abstract")
						.ChildNodes.OfType<XmlElement>()
						.Where(i => i.Name == "p").ToList()
						.Select(i => i.InnerText).ToList();

				ClearListString(descriptions);
				ClearListString(claims);
				ClearListString(ruAbstracts);

				return new DataAboutPattern
				{
					PatentNumber = patentNum,
					EnglishName = euName,
					RussianName = ruName,
					ClassIndex = classIndex,
					EnglishAuthors = euAuthors,
					RussianAuthors = ruAuthors,
					RegistrationData = publishDate,
					Citation = citation,
					DescriptionsSentences = descriptions,
					ClaimsSentences = claims,
					RussianAbstractsSentences = ruAbstracts
				};
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Provides clearing list for string list.
		/// </summary>
		/// <param name="list">String list.</param>
		public void ClearListString(IList<string> list)
		{
			for (var i = 0; i < list.Count; i++)
				if (list[i].Length < 15)
					Indexes.Add(i);

			foreach (var index in Indexes.OrderByDescending(v => v))
			{
				list.RemoveAt(index);
			}

			if (Indexes.Count != 0)
				Indexes.RemoveRange(0, Indexes.Count);
		}
	}
}