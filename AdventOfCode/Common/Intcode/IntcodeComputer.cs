using System;
using System.Collections.Generic;

namespace AdventOfCode.Common.Intcode
{
	public class IntcodeComputer
	{
		private readonly long[] _memory;
		private readonly long[] _op = new long[5];
		private readonly Dictionary<int, IOperation> _operations;

		private int _currentAddress;
		private bool _didJump;

		public IntcodeComputer(int memoryCapacity, Dictionary<int, IOperation> operations)
		{
			_memory = new long[memoryCapacity];
			_operations = operations;
		}

		public long this[int address]
		{
			get { return _memory[address]; }
			set { _memory[address] = value; }
		}

		public int MemoryCapacity => _memory.Length;
		public int RelativeBaseAddress { get; set; }

		public void SetCurrentAddress(int address)
		{
			_currentAddress = address;
			_didJump = true;
		}

		public bool Tick()
		{
			var op = _memory[_currentAddress];

			SplitToArray(op, _op);

			var opCode = GetOpCode();

			if (opCode == 99) return false;

			if (!_operations.TryGetValue((int) opCode, out var operation))
			{
				throw new NotImplementedException();
			}

			var parameters = new Parameters(operation.ParameterCount);
			
			for (var i = 0; i < parameters.Count; i++)
			{
				var mode = _op[i + 2];

				var parameter = _memory[_currentAddress + i + 1];
				var parameterAddress = mode switch
				{
					// Position mode.
					0 => parameter,
					// Immediate mode.
					1 => _currentAddress + i + 1,
					// Relative mode.
					2 => RelativeBaseAddress + parameter,
					_ => throw new NotImplementedException()
				};

				parameters[i] = (int) parameterAddress;
			}

			_didJump = false;
			
			operation.Run(parameters, this);

			if (!_didJump)
			{
				_currentAddress += parameters.Count + 1;
			}

			return true;
		}

		private void SplitToArray(long num, long[] array)
		{
			for (var i = 0; i < array.Length; i++)
			{
				array[i] = num % 10;
				num /= 10;
			}
		}

		private long GetOpCode()
		{
			return _op[0] + _op[1] * 10;
		}
	}
}