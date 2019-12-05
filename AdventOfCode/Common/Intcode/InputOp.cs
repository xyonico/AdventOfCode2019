using System;

namespace AdventOfCode.Common.Intcode
{
	public class InputOp : IOperation
	{
		public byte ParameterCount => 1;
		
		public void Run(Parameters parameters, IntcodeComputer computer)
		{
			var validInput = false;
			var input = 0;
			
			Console.Write("Input: ");
			
			while (!validInput)
			{
				var inputString = Console.ReadLine();
				validInput = int.TryParse(inputString, out input);

				if (!validInput)
				{
					Console.Write("\nInvalid input. Please input an integer: ");
				}
			}

			computer[parameters.Param1] = input;
		}
	}
}