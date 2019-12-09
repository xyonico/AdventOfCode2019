namespace AdventOfCode.Common.Intcode
{
	public class JumpIfFalse : IOperation
	{
		public byte ParameterCount => 2;
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			if (computer[parameters.Param1] == 0)
			{
				computer.SetCurrentAddress((int) computer[parameters.Param2]);
			}
		}
	}
}