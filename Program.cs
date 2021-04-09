using System;
using System.Globalization;
using System.Linq;

namespace ConsoleApp15
{
	class Program
	{
		static void Main(string[] args)
		{
			string date =  "08-2020";// принимает либо номер недели в году двумя форматами: ww-yyyy, ww-yy, либо дату dd/mm/yyyy; dd/mm/yy
			// Пример: 09.04.2021; 07.2020; 07.19; 08.12.65 // Допускается использования других разделительных знаков
			// Возвращает дату (если указан номер недели то возвращает дату пн этой недели)
			string source = "{Иван Иванов},{400 кабинет},{пн-пт},{10-22},{13-13.20}"; // Парсит строку формата: {Иван Иванов},{400 кабинет},{пн-пт},{10-22},{13-13.20}
			var rez = ParseData(source);
			var rez_ = ParseDater(date);
			Console.WriteLine($"1: {rez}\n"+$"2: {rez_}");
			Console.ReadLine();
		}

		#region // Информация о сотруднике
		static string ParseData(string source)
		{
			string[] separatingStrings = { "{", "}", "},{", ","};
			string[] words = source.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
			string[] words_ = { "Работник: {", "Кабинет: {", "Рабочие дни: {", "Время работы: {", "Обеденное время: {" };
			var numbersAndWords = words_.Zip(words, (first, second) => first + second +"}");
			string rez = null;
			foreach (var item in numbersAndWords)
				rez = rez + item + "\n";
			return rez;
		}
		#endregion

		#region // дата
		static string ParseDater(string date)
		{
			char[] separatingStrings = { '-','/',':','.' };
			string[] el = date.Split(separatingStrings);
			int count;
			var resultDate = new DateTime();
			if (el.Length==3)
			{
				count = el[2].Length;
			}
			else
			{
				count = el[1].Length;
			}
			if (el.Length ==2)
			{
				if (count == 4)
				{
					int weekNumber = Convert.ToInt32(el[0]);
					var startDate = new DateTime(Convert.ToInt32(el[1]), 1, 4);
					int offsetToFirstMonday = startDate.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)startDate.DayOfWeek - 1; 
					int offsetToDemandedMonday = -offsetToFirstMonday + 7 * (weekNumber - 1);
					var mondayOfTheGivenWeek = startDate + new TimeSpan(offsetToDemandedMonday, 0, 0, 0);
					resultDate = mondayOfTheGivenWeek;
				}
				if (count == 2)
				{
					int weekNumber = Convert.ToInt32(el[0]);
					if ((Convert.ToInt32(el[1]) >= 00) && (Convert.ToInt32(el[1]) <= 21))
					{
						el[1] = "20" + el[1];
					}
					else
					{
						el[1] = "19" + el[1];
					}
					var startDate = new DateTime(Convert.ToInt32(el[1]), 1, 4);
					int offsetToFirstMonday = startDate.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)startDate.DayOfWeek - 1;
					int offsetToDemandedMonday = -offsetToFirstMonday + 7 * (weekNumber - 1);
					var mondayOfTheGivenWeek = startDate + new TimeSpan(offsetToDemandedMonday, 0, 0, 0);
					resultDate = mondayOfTheGivenWeek;
				}
			}
			else
			{
				if (count == 4)
				{
					resultDate = new DateTime(Convert.ToInt32(el[2]), Convert.ToInt32(el[1]), Convert.ToInt32(el[0]));
				}
				if (count == 2)
				{
					if ((Convert.ToInt32(el[2]) >= 00) && (Convert.ToInt32(el[2]) <= 21))
					{
						el[2] = "20" + el[2];
						resultDate = new DateTime(Convert.ToInt32(el[2]), Convert.ToInt32(el[1]), Convert.ToInt32(el[0]));
					}
					else
					{
						el[2] = "19" + el[2];
						resultDate = new DateTime(Convert.ToInt32(el[2]), Convert.ToInt32(el[1]), Convert.ToInt32(el[0]));
					}
				}
			}
			return Convert.ToString(resultDate);
		}
		#endregion
	}
}
