using System;
using System.Collections.Generic;

namespace AdventOfCode.Common.Intcode
{
	public class IntcodeComputer
	{
		private readonly int[] _memory;
		private readonly int[] _op = new int[5];
		private readonly Dictionary<int, IOperation> _operations;

		private int _currentAddress;

		public IntcodeComputer(int memoryCapacity, Dictionary<int, IOperation> operations)
		{
			_memory = new int[memoryCapacity];
			_operations = operations;
		}

		public int this[int address]
		{
			get { return _memory[address]; }
			set { _memory[address] = value; }
		}

		public int MemoryCapacity => _memory.Length;

		public void SetCurrentAddress(int address)
		{
			_currentAddress = address;
		}

		public bool Tick()
		{
			var op = _memory[_currentAddress];

			SplitToArray(op, _op);

			var opCode = GetOpCode();

			if (opCode == 99) return false;

			if (!_operations.TryGetValue(opCode, out var operation))
			{
				throw new NotImplementedException();
			}

			var parameters = new Parameters(operation.ParameterCount);
			
			for (var i = 0; i < parameters.Count; i++)
			{
				var mode = _op[i + 2];

				var parameter = _memory[_currentAddress + i + 1];
				var parameterValue = mode switch
				{
					// Position mode.
					0 => parameter,
					// Immediate mode.
					1 => _currentAddress + i + 1,
					_ => throw new NotImplementedException()
				};

				parameters[i] = parameterValue;
			}

			var prevAddress = _currentAddress;
			
			operation.Run(parameters, this);

			if (prevAddress == _currentAddress)
			{
				_currentAddress += parameters.Count + 1;
			}

			return true;
		}

		private void SplitToArray(int num, int[] array)
		{
			for (var i = 0; i < array.Length; i++)
			{
				array[i] = num % 10;
				num /= 10;
			}
		}

		private int GetOpCode()
		{
			return _op[0] + _op[1] * 10;
		}
	}
}