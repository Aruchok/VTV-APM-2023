using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using NUnit.Framework;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace XMLParser.Test
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
			_dataAboutPattern.Add(JsonConvert.DeserializeObject<DataAboutPattern>(File.ReadAllText(Path.Combine("Resources", "Data.json"))));
			var xmlParser = new XmlParser();
			var allFiles = Directory.GetFiles("Resources", "*.xml", SearchOption.AllDirectories);
			var  listDataAboutPattern = new List<DataAboutPattern>();
			
			foreach (var file in allFiles)
			{
				var doc = new XmlDocument();
				doc.Load(file);

				listDataAboutPattern.Add(xmlParser.GetDataAboutPattern(doc));
			}

			_dataAboutPattern.AddRange(listDataAboutPattern);
		}

		private List<DataAboutPattern> _dataAboutPattern = new List<DataAboutPattern>();
		
		
		[Test, Description("Testing that files directory return properly.")]
		public void TestMethodShouldHasReturnPathProperly()
		{
			var firstPath =  Directory.GetFiles(Path.Combine("Resources", "001"), ".xml", SearchOption.AllDirectories);
			var secondPath =  Directory.GetFiles(Path.Combine("Resources", "002"), ".xml", SearchOption.AllDirectories);
			var thirdPath =  Directory.GetFiles(Path.Combine("Resources", "003", "0031"), ".xml", SearchOption.AllDirectories);
			
			Assert.That(firstPath, Is.Not.Null);
			Assert.That(secondPath, Is.Not.Null);
			Assert.That(thirdPath, Is.Not.Null);
		}
		
		[Test, Description("Testing that number of records return properly.")]
		public void TestGetDataAboutPatternMethodShouldHasReturnRecordsProperly()
		{
			var xmlParser = new XmlParser();
			var allFiles = Directory.GetFiles("Resources", "*.xml", SearchOption.AllDirectories);
			var  listDataAboutPattern = new List<DataAboutPattern>();
			
			foreach (var file in allFiles)
			{
				var doc = new XmlDocument();
				doc.Load(file);

				listDataAboutPattern.Add(xmlParser.GetDataAboutPattern(doc));
			}
			
			Assert.That(listDataAboutPattern, Is.Not.Null);
			Assert.That(listDataAboutPattern.Count, Is.EqualTo(5));
		}
		
		[Test, Description("Testing that amount of sentences return properly.")]
		public void TestClearListStringMethodShouldHasClearProperly()
		{
			var xmlParser = new XmlParser();
			var testData = new List<string>
			{
				"....................",
				"....................",
				"....................",
				"....................",
				"..........",
				"....................",
				"....................",
				"..........",
				"..........",
				"....................",
				"....................",
				"....................",
				"....................",
				".........."
			};
			
			xmlParser.ClearListString(testData);
			
			Assert.That(testData, Is.Not.Null);
			Assert.That(testData.Count, Is.EqualTo(10));
		}
		
		[Test, Description("Testing that all data return properly.")]
		public void TestGetDataAboutPatternMethodShouldHasReturnAllDataProperly()
		{
			var xmlParser = new XmlParser();
			var allFiles = Directory.GetFiles("Resources", "*.xml", SearchOption.AllDirectories);
			var  listDataAboutPattern = new List<DataAboutPattern>();
			
			foreach (var file in allFiles)
			{
				var doc = new XmlDocument();
				doc.Load(file);

				listDataAboutPattern.Add(xmlParser.GetDataAboutPattern(doc));
			}
			Assert.That(listDataAboutPattern.All(item => item.PatentNumber != null), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.ClassIndex != null), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.EnglishName != null), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.RussianName != null), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.Citation != null), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.RegistrationData != null), Is.True);
			
			Assert.That(listDataAboutPattern.All(item => item.RussianAuthors.All(author => author != null)), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.EnglishAuthors.All(author => author != null)), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.ClaimsSentences.All(claim => claim != null)), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.DescriptionsSentences.All(description => description != null)), Is.True);
			Assert.That(listDataAboutPattern.All(item => item.RussianAbstractsSentences.All(abstractItem => abstractItem != null)), Is.True);
		}
		
		[Test, Description("Testing that AOC structure find properly.")]
		public void TestAOCStructureHasFindProperly()
		{
			var patent = _dataAboutPattern.Find(item => item.PatentNumber == "RU02438283C1");

			Assert.That(patent, Is.Not.Null);
			Assert.That(patent.AOC.Any(item => item.Action.Contains("защита")), Is.True);
			Assert.That(patent.AOC.Any(item => item.Action.Contains("повышение")), Is.True);
			Assert.That(patent.AOC.Any(item => item.Action.Contains("увеличение")), Is.True);
			Assert.That(patent.AOC.Count, Is.EqualTo(6));
		}
	}
}