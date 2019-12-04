using System;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day2 : Day<int[], int>
    {
        protected override int[] ReadInput(string inputPath)
        {
            var text = File.ReadAllText(inputPath);
            var numbers = text.Split(',');
            var input = new int[numbers.Length];

            for (var i = 0; i < numbers.Length; i++)
            {
                input[i] = Convert.ToInt32(numbers[i]);
            }

            return input;
        }

        public override int Puzzle1()
        {
            Input[1] = 1;
            Input[2] = 2;
            
            var index = 0;

            while (RunOperation(index))
            {
                index += 4;
            }
            
            //PrintState();

            return Input[0];
        }

        public override int Puzzle2()
        {
            var maxValue = Input.Length - 1;

            var originalInput = new int[Input.Length];
            Input.CopyTo(originalInput, 0);

            void ResetInput()
            {
                originalInput.CopyTo(Input, 0);
            }
            
            for (var noun = 0; noun < maxValue; noun++)
            {
                for (var verb = 0; verb < maxValue; verb++)
                {
                    Input[1] = noun;
                    Input[2] = verb;

                    var index = 0;

                    while (RunOperation(index))
                    {
                        index += 4;
                    }

                    if (Input[0] == 19690720)
                    {
                        return 100 * noun + verb;
                    }
                    
                    ResetInput();
                }
            }

            return -1;
        }
        
        private bool RunOperation(int index)
        {
            var opCode = Input[index];

            if (opCode == 99) return false;

            var aIndex = Input[index + 1];
            var bIndex = Input[index + 2];
            var outIndex = Input[index + 3];

            var a = Input[aIndex];
            var b = Input[bIndex];

            int outValue;

            switch (opCode)
            {
                case 1:
                    outValue = a + b;
                    break;
                case 2:
                    outValue = a * b;
                    break;
                default:
                    throw new NotImplementedException($"Opcode {opCode} is not recognized.");
            }

            Input[outIndex] = outValue;

            return true;
        }

        private void PrintState()
        {
            Console.WriteLine(string.Join(",", Input));
        }
    }
}