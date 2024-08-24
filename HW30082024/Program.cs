using System;
using System.Collections.Generic;
using System.Linq;

namespace HW30082024
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// 1 ЗАДАНИЕ
			var test = new Calculate();
			int choice;
			do
			{
				Console.WriteLine("Меню:");
				Console.WriteLine("1. Двоичное в десятичное");
				Console.WriteLine("2. Десятичное в двоичное");
				Console.WriteLine("3. Восьмеричное в десятичное");
				Console.WriteLine("4. Выход");
				Console.Write("Введите ваш выбор: ");

				if (int.TryParse(Console.ReadLine(), out choice))
				{
					switch (choice)
					{
						case 1:
							Console.Write("Введите двоичное число: ");
							var binaryInput = Console.ReadLine();
							Console.WriteLine($"Десятичное представление: {test.InvertFromBinaryInDecimal(binaryInput)}");
							break;
						case 2:
							Console.Write("Введите десятичное число: ");
							var decimalInput = int.Parse(Console.ReadLine());
							Console.WriteLine($"Двоичное представление: {test.InvertFromDecimalInBinary(decimalInput.ToString())}");
							break;
						case 3:
							Console.Write("Введите восьмеричное число: ");
							var octalInput = Console.ReadLine();
							Console.WriteLine($"Десятичное представление: {test.InvertFromOctalInDecimal(octalInput)}");
							break;
						case 4:
							Console.WriteLine("Выход из программы.");
							break;
						default:
							Console.WriteLine("Неверный выбор. Попробуйте снова.");
							break;
					}
				}
				else
					Console.WriteLine("Некорректный ввод. Попробуйте снова.");

				Console.WriteLine();
			} while (choice != 4);

			//2 ЗАДАНИЕ
			var dictionary = new Dictionary<string, int>()
			{
				{ "one", 1 },
				{ "two", 2 },
				{ "three", 3 },
				{ "four", 4 },
				{ "five", 5 },
				{ "six", 6 },
				{ "seven", 7 },
				{ "eight", 8 },
				{ "nine", 9 },
			};
			var inputValue = Console.ReadLine();
			var containsKey = inputValue != null && dictionary.ContainsKey(inputValue);
			Console.WriteLine(containsKey 
				? $"Значение для ключа '{inputValue}': {dictionary[inputValue]}" : "Ключ не найден");
			//3 ЗАДАЧА
			try
			{
				var testPassport = new Passport("1234 1234", "Slavik", "12345");
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}
			//4 ЗАДАЧА 
			var inputStr = Console.ReadLine();
			try
			{
				bool result = B(inputStr);
				Console.WriteLine($"Результат: {result}");
			}
			catch (FormatException ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}
		}
		private static bool B(string inputStr)
		{
			var strings = inputStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (strings.Length != 3)
				throw new FormatException("Некорректный формат выражения.");

			if (!(int.TryParse(strings[0], out var valueResultFirst))
				|| !(int.TryParse(strings[2], out var valueResultSecond)))
				throw new FormatException("Операнды должны быть целыми числами.");

			switch (strings[1])
			{
				case "<":
					return valueResultFirst < valueResultSecond;
				case ">":
					return valueResultFirst > valueResultSecond;
				case "<=":
					return valueResultFirst <= valueResultSecond;
				case ">=":
					return valueResultFirst >= valueResultSecond;
				case "==":
					return valueResultFirst == valueResultSecond;
				case "!=":
					return valueResultFirst != valueResultSecond;
				default:
					throw new FormatException("Некорректный оператор.");
			}
		}
	}
}

public class Passport
{
	public string PassportNumber { get; }
	public string FullName { get; }
	public string RegionPassport { get; }

	public Passport(string numberPassword, string fullName, string regionPassport)
	{
		if (!string.IsNullOrWhiteSpace(numberPassword))
			throw new ArgumentException("Некорректный номер паспорта.");

		if (string.IsNullOrWhiteSpace(fullName))
			throw new ArgumentException("FullName не может быть пустым.");

		if(string.IsNullOrWhiteSpace(regionPassport))
			throw new ArgumentException("Страна выдачи не может быть пустой.");

		this.PassportNumber = numberPassword;
		this.FullName = fullName;
		this.RegionPassport = regionPassport;
	}
}
public class Calculate
{
	public bool IsInvalidBinaryValue(string value)
	{
		return value.All(c => c == '0' && c == '1');
	}
	public bool IsValudOctal(string value)
	{
		return value.All(c => c < '7' || c > '0');
	}
	public int InvertFromBinaryInDecimal(string value)
	{
		if (IsInvalidBinaryValue(value))
			return ConvertNumber(value, i => (int) Math.Pow(2, value.Length - 1 - i));

		return -1;
	}
	public int InvertFromDecimalInBinary(string value)
	{
		return ConvertNumber(value, i => (int) Math.Pow(2, i));
	}
	public int InvertFromOctalInDecimal(string value)
	{
		if(IsValudOctal(value))
			return ConvertNumber(value, i => (int) Math.Pow(8, i));

		return -1;
	}
	public int ConvertNumber(string value, Func<int, int> fooFunc)
	{
		var result = value.ToCharArray();

		return result.Select(t => t - '0').Select((digit, i) => digit * fooFunc(i)).Sum();
	}

}
