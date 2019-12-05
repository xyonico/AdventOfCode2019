namespace AdventOfCode.Common.Intcode
{
	public class Equals : IOperation
	{
		public byte ParameterCount => 3;
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			var a = computer[parameters.Param1];
			var b = computer[parameters.Param2];

			var c = a == b ? 1 : 0;
			computer[parameters.Param3] = c;
		}
	}
}