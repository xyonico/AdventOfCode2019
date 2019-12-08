using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
	public class Day8 : Day<byte[], int>
	{
		private const int IMAGE_WIDTH = 25;
		private const int IMAGE_HEIGHT = 6;
		
		public override int Puzzle1()
		{
			var layers = GetLayers();

			var minZeroCount = int.MaxValue;
			var minOneCount = int.MaxValue;
			var minTwoCount = int.MaxValue;

			foreach (var layer in layers)
			{
				var zeroCount = 0;
				var oneCount = 0;
				var twoCount = 0;

				foreach (var b in layer)
				{
					if (b == 0)
					{
						zeroCount++;
					}
					else if (b == 1)
					{
						oneCount++;
					}
					else if (b == 2)
					{
						twoCount++;
					}
				}

				if (zeroCount < minZeroCount)
				{
					minZeroCount = zeroCount;
					minOneCount = oneCount;
					minTwoCount = twoCount;
				}
			}

			return minOneCount * minTwoCount;
		}

		public override int Puzzle2()
		{
			var image = new bool[IMAGE_WIDTH, IMAGE_HEIGHT];
			var opaquePixels = new bool[IMAGE_WIDTH, IMAGE_HEIGHT];
			
			var layers = GetLayers();

			foreach (var layer in layers)
			{
				for (var x = 0; x < IMAGE_WIDTH; x++)
				{
					for (var y = 0; y < IMAGE_HEIGHT; y++)
					{
						var index = x + IMAGE_WIDTH * y;
						var pixel = layer[index];

						if (pixel > 1)
						{
							continue;
						}

						if (opaquePixels[x, y])
						{
							continue;
						}

						opaquePixels[x, y] = true;
						image[x, y] = pixel == 1;
					}
				}
			}

			DrawPixels(image);
			
			return 0;
		}

		protected override byte[] ReadInput(string inputPath)
		{
			var text = File.ReadAllText(inputPath);

			var digitArray = new byte[text.Length];

			for (var i = 0; i < text.Length; i++)
			{
				digitArray[i] = (byte) char.GetNumericValue(text[i]);
			}

			return digitArray;
		}

		private IEnumerable<ArraySegment<byte>> GetLayers()
		{
			const int pixelsPerLayer = IMAGE_WIDTH * IMAGE_HEIGHT;

			var layerCount = Input.Length / pixelsPerLayer;

			for (var i = 0; i < layerCount; i++)
			{
				var layer = new ArraySegment<byte>(Input, i * pixelsPerLayer, pixelsPerLayer);
				yield return layer;
			}
		}

		private static void DrawPixels(bool[,] pixels)
		{
			var width = pixels.GetLength(0);
			var height = pixels.GetLength(1);
			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					var pixel = pixels[x, y];
					var color = pixel ? ConsoleColor.White : ConsoleColor.Black;
					Console.BackgroundColor = color;
					Console.Write(" ");
				}
				
				Console.Write(Environment.NewLine);
			}
		}
	}
}