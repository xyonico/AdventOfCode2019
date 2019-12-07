namespace AdventOfCode.Common.Intcode
{
	public class AutoOutputOp : IOperation
	{
		public delegate void OutputHandler(AutoOutputOp sender, int output);

		private readonly OutputHandler _outputHandler;
		
		public byte ParameterCount => 1;

		public AutoOutputOp(OutputHandler outputHandler)
		{
			_outputHandler = outputHandler;
		}
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			var output = computer[parameters.Param1];
			_outputHandler(this, output);
		}
	}
}