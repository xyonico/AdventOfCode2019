using System;

namespace AdventOfCode.Common.Intcode
{
	public interface IOperation
	{
		byte ParameterCount { get; }

		void Run(Parameters parameters, IntcodeComputer computer);
	}
}