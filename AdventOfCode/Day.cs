using System;
using System.IO;

namespace AdventOfCode
{
	public abstract class Day<TInput, TOutput>
	{
		private TInput[] _input;

		protected TInput[] Input
		{
			get
			{
				if (_input == null)
				{
					var typeName = GetType().Name;
					var number = typeName.Substring(3);

					var stringArray = File.ReadAllLines($"input/{number}.txt");
					
					_input = new TInput[stringArray.Length];
					
					for (var i = 0; i < _input.Length; i++)
					{
						_input[i] = ConvertFromString(stringArray[i]);
					}
				}

				return _input;
			}
		}

		public abstract TOutput Puzzle1();
		public abstract TOutput Puzzle2();

		protected virtual TInput ConvertFromString(string inputString)
		{
			return (TInput) Convert.ChangeType(inputString, typeof(TInput));
		}
	}
}