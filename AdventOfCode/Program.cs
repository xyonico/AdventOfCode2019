using System;

namespace AdventOfCode
{
	public class Program
	{
		private static void Main(string[] args)
		{
			string dayName;
			byte part = 2;

			if (args.Length > 0)
			{
				dayName = args[0];

				if (args.Length > 1)
				{
					part = Convert.ToByte(args[1]);
				}
			}
			else
			{
				dayName = "5";
			}
			
			var dayType = typeof(Program).Assembly.GetType($"AdventOfCode.Days.Day{dayName}", false);

			if (dayType == null)
			{
				Console.WriteLine($"Day {dayName} is not defined.");
				return;
			}

			dynamic day = Activator.CreateInstance(dayType);

			string result;
			
			if (part == 1)
			{
				result = day.Puzzle1().ToString();
			}
			else
			{
				result = day.Puzzle2().ToString();
			}
			
			Console.WriteLine($"Day {dayName} (Part {part}) result: {result}");
		}
	}
}