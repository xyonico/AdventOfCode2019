using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Common;
using AdventOfCode.Common.Intcode;

namespace AdventOfCode.Days
{
	public class Day7 : Day<int[], int>
	{
		private int _lastOutputThrust;
		private int _currentPhaseSetting;
		private bool _hasDoneFirstInput;

		public override int Puzzle1()
		{
			const int amplifierCount = 5;

			var combination = new List<int>(amplifierCount);
			var computers = new IntcodeComputer[amplifierCount];

			int InputHandler(AutoInputOp sender)
			{
				if (!_hasDoneFirstInput)
				{
					_hasDoneFirstInput = true;
					return _currentPhaseSetting;
				}

				_hasDoneFirstInput = false;
				return _lastOutputThrust;
			}

			void OutputHandler(AutoOutputOp sender, int output)
			{
				_lastOutputThrust = output;
			}

			for (var i = 0; i < amplifierCount; i++)
			{
				combination.Add(i);

				computers[i] = CreateNewComputer(InputHandler, OutputHandler);
			}

			var maxOutputThrust = 0;

			foreach (var permutation in combination.Permute())
			{
				var index = 0;

				foreach (var phaseSetting in permutation)
				{
					Console.Write($"{phaseSetting}, ");

					_currentPhaseSetting = phaseSetting;
					_hasDoneFirstInput = false;

					var computer = computers[index];

					while (computer.Tick())
					{
					}

					// Reset computer.
					ProgramComputerWithInput(computer);
					computer.SetCurrentAddress(0);

					index++;
				}

				if (_lastOutputThrust > maxOutputThrust)
				{
					maxOutputThrust = _lastOutputThrust;
				}

				Console.Write($"= {_lastOutputThrust}{Environment.NewLine}");

				_lastOutputThrust = 0;
			}

			return maxOutputThrust;
		}

		private int _currentComputerIndex;
		private int _computersReceivedPhase = -1;
		private IEnumerator<int> _currentPermutation;

		public override int Puzzle2()
		{
			const int amplifierCount = 5;

			var combination = new List<int>(amplifierCount);
			var computers = new IntcodeComputer[amplifierCount];

			int InputHandler(AutoInputOp sender)
			{
				if (_computersReceivedPhase < _currentComputerIndex)
				{
					_computersReceivedPhase = _currentComputerIndex;
					_currentPermutation.MoveNext();
					var currentPermutation = _currentPermutation.Current;

					Console.Write($"{currentPermutation}, ");
					
					return currentPermutation;
				}

				return _lastOutputThrust;
			}

			void OutputHandler(AutoOutputOp sender, int output)
			{
				_lastOutputThrust = output;
				_currentComputerIndex++;

				if (_currentComputerIndex >= amplifierCount)
				{
					_currentComputerIndex = 0;
				}
			}

			for (var i = 0; i < amplifierCount; i++)
			{
				combination.Add(i + 5);

				computers[i] = CreateNewComputer(InputHandler, OutputHandler);
			}

			var maxOutputThrust = 0;

			foreach (var permutation in combination.Permute())
			{
				_currentComputerIndex = 0;

				_currentPermutation = permutation.GetEnumerator();

				while (computers[_currentComputerIndex].Tick())
				{
				}

				if (_lastOutputThrust > maxOutputThrust)
				{
					maxOutputThrust = _lastOutputThrust;
				}

				Console.Write($"= {_lastOutputThrust}{Environment.NewLine}");
				
				_currentPermutation.Dispose();
				
				foreach (var computer in computers)
				{
					ProgramComputerWithInput(computer);
					computer.SetCurrentAddress(0);
				}
				
				_lastOutputThrust = 0;
				_currentComputerIndex = 0;
				_computersReceivedPhase = -1;
			}

			return maxOutputThrust;
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

		private IntcodeComputer CreateNewComputer(AutoInputOp.InputHandler inputHandler, AutoOutputOp.OutputHandler outputHandler)
		{
			var operations = new Dictionary<int, IOperation>
			{
				{1, new AdditionOp()},
				{2, new MultiplyOp()},
				{3, new AutoInputOp(inputHandler)},
				{4, new AutoOutputOp(outputHandler)},
				{5, new JumpIfTrue()},
				{6, new JumpIfFalse()},
				{7, new LessThan()},
				{8, new Equals()}
			};

			var computer = new IntcodeComputer(Input.Length, operations);

			ProgramComputerWithInput(computer);

			return computer;
		}

		private void ProgramComputerWithInput(IntcodeComputer computer)
		{
			for (var i = 0; i < Input.Length; i++)
			{
				computer[i] = Input[i];
			}
		}
	}
}