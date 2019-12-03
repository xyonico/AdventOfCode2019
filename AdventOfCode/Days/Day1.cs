using System;

namespace AdventOfCode.Days
{
	public class Day1 : Day<int, int>
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

		private static int FuelForMass(int mass)
		{
			return mass / 3 - 2;
		}
	}
}