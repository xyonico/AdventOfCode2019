namespace AdventOfCode.Common.Intcode
{
	public class AdjustRelativeBase : IOperation
	{
		public byte ParameterCount => 1;
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			var offset = computer[parameters.Param1];
			computer.RelativeBaseAddress += (int) offset;
		}
	}
}