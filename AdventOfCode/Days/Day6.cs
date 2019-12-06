using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day6 : Day<Orbit[], int>
    {
        public override int Puzzle1()
        {
            var orbitingBodies = GetOrbitingBodies();

            var parentCount = 0;

            foreach (var orbitingBody in orbitingBodies)
            {
                parentCount++;
                
                var nextParentBody = new Body(orbitingBody.Parent);
                while (orbitingBodies.TryGetValue(nextParentBody, out var actualParent))
                {
                    parentCount++;
                    nextParentBody = new Body(actualParent.Parent);
                }
            }

            return parentCount;
        }

        public override int Puzzle2()
        {
            var orbitingBodies = GetOrbitingBodies();

            const string santaName = "SAN";
            var santaID = Body.NameToUniqueInt(santaName, 0, santaName.Length);

            const string youName = "YOU";
            var myID = Body.NameToUniqueInt(youName, 0, youName.Length);

            if (!orbitingBodies.TryGetValue(santaID, out var santaBody))
            {
                Console.WriteLine("Couldn't find Santa! Christmas is ruined...");
                return 0;
            }

            if (!orbitingBodies.TryGetValue(myID, out var myBody))
            {
                Console.WriteLine("Couldn't find myself!");
                return 0;
            }
            
            var myParents = new List<Body>();

            var myNextParent = new Body(myBody.Parent);
            while (orbitingBodies.TryGetValue(myNextParent, out var myActualParent))
            {
                myParents.Add(myActualParent);
                myNextParent = new Body(myActualParent.Parent);
            }

            var santaParentCount = 0;
            
            var nextSantaParent = new Body(santaBody.Parent);
            while (orbitingBodies.TryGetValue(nextSantaParent, out var actualSantaParent))
            {

                if (myParents.Contains(actualSantaParent))
                {
                    // Found a common ancestor!
                    santaParentCount += myParents.IndexOf(actualSantaParent);
                    break;
                }
                
                santaParentCount++;
                nextSantaParent = new Body(actualSantaParent.Parent);
            }

            return santaParentCount;
        }

        private HashSet<Body> GetOrbitingBodies()
        {
            var orbitingBodies = new HashSet<Body>(Input.Length);
            
            foreach (var orbit in Input)
            {
                var orbitingBody = new Body(orbit.Child, orbit.Parent);

                if (!orbitingBodies.Add(orbitingBody))
                {
                    Console.WriteLine("Found conflicting orbiting body!");

                    orbitingBodies.Remove(orbitingBody);
                    orbitingBodies.Add(orbitingBody);
                }
            }

            return orbitingBodies;
        }

        protected override Orbit[] ReadInput(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            var orbits = new Orbit[lines.Length];

            for (var i = 0; i < orbits.Length; i++)
            {
                var orbit = new Orbit(lines[i]);
                orbits[i] = orbit;
            }

            return orbits;
        }
    }

    public struct Body
    {
        private static readonly byte[] _nameBuffer = new byte[4];
        private readonly int _identifier;

        public readonly int Parent;

        public Body(int id)
        {
            _identifier = id;
            Parent = 0;
        }

        public Body(int id, int parent)
        {
            _identifier = id;
            Parent = parent;
        }

        public override int GetHashCode()
        {
            return _identifier;
        }

        public override bool Equals(object body)
        {
            if (body == null) return false;
            
            return GetHashCode() == body.GetHashCode();
        }
        
        public static implicit operator int(Body body)
        {
            return body._identifier;
        }

        public static implicit operator Body(int integer)
        {
            return new Body(integer);
        }
        
        public static int NameToUniqueInt(string name, int startIndex, int length)
        {
            for (var i = 0; i < _nameBuffer.Length; i++)
            {
                var stringIndex = startIndex + i;
                
                if (i >= length || name.Length <= stringIndex)
                {
                    _nameBuffer[i] = 0;
                    continue;
                }

                _nameBuffer[i] = (byte) name[stringIndex];
            }

            return BitConverter.ToInt32(_nameBuffer, 0);
        }
    }

    public struct Orbit
    {
        private const int NAME_LENGTH = 3;

        public readonly int Parent;
        public readonly int Child;

        public Orbit(string orbitString)
        {
            // First 4 bytes of the ASCII names of the bodies are converted into an int to get a unique id.
            Parent = Body.NameToUniqueInt(orbitString, 0, NAME_LENGTH);
            Child = Body.NameToUniqueInt(orbitString, 4, NAME_LENGTH);
        }
    }
}