using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace XMLParser
{
	public class DataAboutPattern
	{
		public ObjectId Id { get; set; }
		/// <summary>
		/// Номер патента.
		/// </summary>
		public string PatentNumber { get; set; }

		/// <summary>
		/// Имя патента на русском.
		/// </summary>
		public string EnglishName { get; set; }

		/// <summary>
		/// Имя патента на английском.
		/// </summary>
		public string RussianName { get; set; }

		/// <summary>
		/// Классификационный индекс.
		/// </summary>
		public string ClassIndex { get; set; }

		/// <summary>
		/// Список авторов на русском языке.
		/// </summary>
		public List<string> RussianAuthors { get; set; }

		/// <summary>
		/// Список авторов на английском языке.
		/// </summary>
		public List<string> EnglishAuthors { get; set; }
		
		/// <summary>
		/// Дата регистрации патента.
		/// </summary>
		public DateTime RegistrationData { get; set; }

		/// <summary>
		/// Список цитирования.
		/// </summary>
		public string Citation { get; set; }

		/// <summary>
		/// Список предложений с описанием патента.
		/// </summary>
		public List<string> DescriptionsSentences { get; set; }

		/// <summary>
		/// Список предложений с требованиями патента.
		/// </summary>
		public List<string> ClaimsSentences { get; set; }

		/// <summary>
		/// Список предложений с авторефератами на русском языке.
		/// </summary>
		public List<string> RussianAbstractsSentences { get; set; }
		
		/// <summary>
		/// АОС структуры.
		/// </summary>
		public List<AOC> AOC { get; set; }
		
		/// <summary>
		/// Инженерные признаки.
		/// </summary>
		public List<string> EngineeringParameters { get; set; }
	}
}