using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Common.Intcode;

namespace AdventOfCode.Days
{
	public class Day9 : Day<long[], int>
	{
		public override int Puzzle1()
		{
			var computer = CreateComputer(2048);
			ProgramComputer(computer);

			while (computer.Tick())
			{
			}

			return 0;
		}

		public override int Puzzle2()
		{
			return Puzzle1();
		}

		protected override long[] ReadInput(string inputPath)
		{
			var text = File.ReadAllText(inputPath);
			var numbers = text.Split(',');
			var input = new long[numbers.Length];

			for (var i = 0; i < numbers.Length; i++)
			{
				input[i] = Convert.ToInt64(numbers[i]);
			}

			return input;
		}

		private IntcodeComputer CreateComputer(int memoryCapacity)
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
				{8, new Equals()},
				{9, new AdjustRelativeBase()}
			};

			return new IntcodeComputer(memoryCapacity, operations);
		}

		private void ProgramComputer(IntcodeComputer computer)
		{
			for (var i = 0; i < Input.Length; i++)
			{
				computer[i] = Input[i];
			}
		}
	}
}