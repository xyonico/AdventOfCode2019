namespace AdventOfCode.Common.Intcode
{
	public class AutoInputOp : IOperation
	{
		public delegate int InputHandler(AutoInputOp sender);

		private readonly InputHandler _inputHandler;
		
		public byte ParameterCount => 1;

		public AutoInputOp(InputHandler inputHandler)
		{
			_inputHandler = inputHandler;
		}
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			var input = _inputHandler(this);
			computer[parameters.Param1] = input;
		}
	}
}