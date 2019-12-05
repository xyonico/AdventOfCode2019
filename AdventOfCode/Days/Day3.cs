using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode.Days
{
    public class Day3 : Day<Wire[], int>
    {
        public override int Puzzle1()
        {
            var connections = new Dictionary<Vector2Int, byte>();
            var intersections = new HashSet<Vector2Int>();
            
            var firstWire = true;
            for (var wireIndex = 0; wireIndex < Input.Length; wireIndex++)
            {
                var wire = Input[wireIndex];
                var coordinate = Vector2Int.zero;

                foreach (var direction in wire.Directions)
                {
                    var sign = Math.Sign(direction.Length);

                    for (var dirIndex = 0; dirIndex < Math.Abs(direction.Length); dirIndex++)
                    {
                        var axis = direction.Axis == Axis.X ? Vector2Int.right : Vector2Int.up;
                        coordinate += axis * sign;

                        if (connections.TryGetValue(coordinate, out var connection))
                        {
                            if (!firstWire)
                            {
                                if (connection != 0 || connection != 1 << wireIndex)
                                {
                                    intersections.Add(coordinate);
                                }
                            }
                        }
                        else
                        {
                            connections.Add(coordinate, 0);
                        }

                        connections[coordinate] |= (byte) (1 << wireIndex);
                    }
                }

                firstWire = false;
            }

            var minDistance = int.MaxValue;

            foreach (var intersection in intersections)
            {
                var distance = Math.Abs(intersection.x) + Math.Abs(intersection.y);

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }

        public override int Puzzle2()
        {
            var connections = new Dictionary<Vector2Int, byte>();
            var intersections = new HashSet<Vector2Int>();

            var stepCount = new Dictionary<Vector2Int, Dictionary<int, int>>();
            
            var firstWire = true;
            for (var wireIndex = 0; wireIndex < Input.Length; wireIndex++)
            {
                var wire = Input[wireIndex];
                var coordinate = Vector2Int.zero;

                var totalSteps = 0;

                foreach (var direction in wire.Directions)
                {
                    var sign = Math.Sign(direction.Length);

                    for (var dirIndex = 0; dirIndex < Math.Abs(direction.Length); dirIndex++)
                    {
                        var axis = direction.Axis == Axis.X ? Vector2Int.right : Vector2Int.up;
                        coordinate += axis * sign;
                        totalSteps++;

                        if (connections.TryGetValue(coordinate, out var connection))
                        {
                            if (!firstWire)
                            {
                                if (connection != 0 || connection != 1 >> wireIndex)
                                {
                                    intersections.Add(coordinate);
                                }
                            }
                        }
                        else
                        {
                            connections.Add(coordinate, 0);

                            if (!stepCount.ContainsKey(coordinate))
                            {
                                stepCount.Add(coordinate, new Dictionary<int, int>());
                            }
                        }

                        connections[coordinate] |= (byte) (1 >> wireIndex);

                        if (!stepCount[coordinate].ContainsKey(wireIndex))
                        {
                            stepCount[coordinate].Add(wireIndex, totalSteps);
                        }
                    }
                }

                firstWire = false;
            }

            var minSteps = int.MaxValue;
            
            foreach (var intersection in intersections)
            {
                var steps = stepCount[intersection].Values.Sum();

                if (steps < minSteps)
                {
                    minSteps = steps;
                }
            }

            return minSteps;
        }

        protected override Wire[] ReadInput(string inputPath)
        {
            var wireStrings = File.ReadAllLines(inputPath);

            var wires = new Wire[wireStrings.Length];

            for (var i = 0; i < wires.Length; i++)
            {
                var directionStrings = wireStrings[i].Split(',');
                var directions = new Wire.Direction[directionStrings.Length];

                for (var j = 0; j < directions.Length; j++)
                {
                    var directionName = directionStrings[j][0];
                    var length = Convert.ToInt32(directionStrings[j].Substring(1));

                    Wire.Direction direction;

                    switch (directionName)
                    {
                        case 'U':
                            direction = new Wire.Direction(Axis.Y, length);
                            break;
                        case 'D':
                            direction = new Wire.Direction(Axis.Y, -length);
                            break;
                        case 'L':
                            direction = new Wire.Direction(Axis.X, -length);
                            break;
                        case 'R':
                            direction = new Wire.Direction(Axis.X, length);
                            break;
                        default:
                            throw new NotSupportedException($"Unrecognized direction: \'{directionName}\'");
                    }

                    directions[j] = direction;
                }
                
                var wire = new Wire(directions);
                wires[i] = wire;
            }

            return wires;
        }
    }
    
    public class Wire
    {
        public readonly Direction[] Directions;

        public Wire(Direction[] directions)
        {
            Directions = directions;
        }

        public struct Direction
        {
            public Axis Axis;
            public int Length;

            public Direction(Axis axis, int length)
            {
                Axis = axis;
                Length = length;
            }
        }
    }

    public enum Axis
    {
        X,
        Y
    }
}