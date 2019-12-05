namespace AdventOfCode.Common.Intcode
{
	public class MultiplyOp : IOperation
	{
		public byte ParameterCount => 3;

		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			var a = computer[parameters.Param1];
			var b = computer[parameters.Param2];

			computer[parameters.Param3] = a * b;
		}
	}
}