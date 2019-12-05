using System;
using System.IO;

namespace AdventOfCode
{
	public abstract class Day<TInput, TOutput>
	{
		private TInput _input;

		protected TInput Input
		{
			get
			{
				if (default(TInput) == null && _input == null || default(TInput) != null && _input.Equals(default(TInput)))
				{
					var typeName = GetType().Name;
					var number = typeName.Substring(3);

					_input = ReadInput($"input/{number}.txt");
				}

				return _input;
			}
		}

		public abstract TOutput Puzzle1();
		public abstract TOutput Puzzle2();

		protected abstract TInput ReadInput(string inputPath);
	}
}