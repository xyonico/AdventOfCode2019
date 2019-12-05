using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Common.Intcode;

namespace AdventOfCode.Days
{
	public class Day5 : Day<int[], int>
	{
		public override int Puzzle1()
		{
			var operations = new Dictionary<int, IOperation>
			{
				{1, new AdditionOp()},
				{2, new MultiplyOp()},
				{3, new InputOp()},
				{4, new OutputOp()}
			};
			
			var computer = new IntcodeComputer(Input.Length, operations);

			for (var i = 0; i < Input.Length; i++)
			{
				computer[i] = Input[i];
			}

			while (computer.Tick())
			{
			}

			return 0;
		}

		public override int Puzzle2()
		{
			var operations = new Dictionary<int, IOperation>
			{
				{1, new AdditionOp()},
				{2, new MultiplyOp()},
				{3, new InputOp()},
				{4, new OutputOp()},
				{5, new JumpIfTrue()},
				{6, new JumpIfFalse()},
				{7, new LessThan()},
				{8, new Equals()}
			};
			
			var computer = new IntcodeComputer(Input.Length, operations);

			for (var i = 0; i < Input.Length; i++)
			{
				computer[i] = Input[i];
			}

			while (computer.Tick())
			{
			}

			return 0;
		}

		protected override int[] ReadInput(string inputPath)
		{
			var text = File.ReadAllText(inputPath);
			var numbers = text.Split(',');
			var input = new int[numbers.Length];

			for (var i = 0; i < numbers.Length; i++)
			{
				input[i] = Convert.ToInt32(numbers[i]);
			}

			return input;
		}
	}
}