using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
	public class Day4 : Day<Range<int>, int>
	{
		public override int Puzzle1()
		{
			var range = Input;

			var currentNumberArray = new int[6];
			
			var bruteValidNumbers = new HashSet<int>();
			
			for (var i = range.Min; i <= range.Max; i++)
			{
				SplitToArray(i, currentNumberArray);

				var hasAdjacentDigits = false;
				var isValid = true;
				var prevDigit = currentNumberArray[0];
				for (var j = 1; j < currentNumberArray.Length; j++)
				{
					var digit = currentNumberArray[j];
					if (digit < prevDigit)
					{
						isValid = false;
						break;
					}

					if (digit == prevDigit)
					{
						hasAdjacentDigits = true;
					}

					prevDigit = digit;
				}

				if (isValid && hasAdjacentDigits)
				{
					bruteValidNumbers.Add(i);
				}
			}

			return bruteValidNumbers.Count;
		}

		public override int Puzzle2()
		{
			var range = Input;

			var currentNumberArray = new int[6];

			var bruteValidNumbers = new HashSet<int>();
			
			for (var i = range.Min; i <= range.Max; i++)
			{
				SplitToArray(i, currentNumberArray);

				var adjacentCount = 0;
				var hasGottenAdjacent = false;
				var isValid = true;
				var prevDigit = currentNumberArray[0];
				for (var j = 1; j < currentNumberArray.Length; j++)
				{
					var digit = currentNumberArray[j];
					if (digit < prevDigit)
					{
						isValid = false;
						break;
					}

					if (!hasGottenAdjacent)
					{
						if (digit == prevDigit)
						{
							adjacentCount++;
						}
						else if (adjacentCount == 1)
						{
							hasGottenAdjacent = true;
						}
						else
						{
							adjacentCount = 0;
						}
					}

					prevDigit = digit;
				}

				if (isValid)
				{
					if (hasGottenAdjacent || adjacentCount == 1)
					{
						bruteValidNumbers.Add(i);
					}
				}
			}

			return bruteValidNumbers.Count;
		}

		protected override Range<int> ReadInput(string inputPath)
		{
			var text = File.ReadAllText(inputPath);
			var numbers = text.Split('-');

			var min = Convert.ToInt32(numbers[0]);
			var max = Convert.ToInt32(numbers[1]);

			return new Range<int>(min, max);
		}

		private void SplitToArray(int num, int[] array)
		{
			var listOfInts = new List<int>();
			while (num > 0)
			{
				listOfInts.Add(num % 10);
				num = num / 10;
			}

			for (var i = 0; i < listOfInts.Count; i++)
			{
				var reverseIndex = array.Length - 1 - i;
				array[reverseIndex] = listOfInts[i];
			}
		}

		private int ArrayToInt(int[] array)
		{
			return array.Select((t, i) => t * Convert.ToInt32(Math.Pow(10, array.Length - i - 1))).Sum();
		}
	}
}