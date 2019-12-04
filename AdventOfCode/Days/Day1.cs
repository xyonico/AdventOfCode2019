using System;
using System.IO;

namespace AdventOfCode.Days
{
	public class Day1 : Day<int[], int>
	{
		public override int Puzzle1()
		{
			var totalFuel = 0;
			
			for (var i = 0; i < Input.Length; i++)
			{
				var mass = Input[i];
				var fuel = FuelForMass(mass);

				totalFuel += fuel;
			}

			return totalFuel;
		}

		public override int Puzzle2()
		{
			var totalFuel = 0;
			
			for (var i = 0; i < Input.Length; i++)
			{
				var mass = Input[i];
				
				var fuel = FuelForMass(mass);
				totalFuel += fuel;
				var fuelRequired = FuelForMass(fuel);
				
				while (fuelRequired > 0)
				{
					totalFuel += fuelRequired;
					fuelRequired = FuelForMass(fuelRequired);
				}
			}

			return totalFuel;
		}

		protected override int[] ReadInput(string inputPath)
		{
			var stringArray = File.ReadAllLines(inputPath);
					
			var input = new int[stringArray.Length];
					
			for (var i = 0; i < input.Length; i++)
			{
				input[i] = Convert.ToInt32(stringArray[i]);
			}

			return input;
		}


		private static int FuelForMass(int mass)
		{
			return mass / 3 - 2;
		}
	}
}