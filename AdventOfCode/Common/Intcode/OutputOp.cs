using System;

namespace AdventOfCode.Common.Intcode
{
	public class OutputOp : IOperation
	{
		public byte ParameterCount => 1;
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			Console.WriteLine($"Output: {computer[parameters.Param1]}");
		}
	}
}