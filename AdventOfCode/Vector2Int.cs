using System;
using System.Numerics;

namespace AdventOfCode
{
    public struct Vector2Int : IEquatable<Vector2Int>
    {
        private static readonly Vector2Int _zero = new Vector2Int(0, 0);
        private static readonly Vector2Int _one = new Vector2Int(1, 1);
        private static readonly Vector2Int _up = new Vector2Int(0, 1);
        private static readonly Vector2Int _down = new Vector2Int(0, -1);
        private static readonly Vector2Int _left = new Vector2Int(-1, 0);
        private static readonly Vector2Int _right = new Vector2Int(1, 0);
        private int _x;
        private int _y;

        public Vector2Int(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public int x
        {
            get { return this._x; }
            set { this._x = value; }
        }

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public int y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        /// <summary>
        ///   <para>Set x and y components of an existing Vector2Int.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Set(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return this.x;
                if (index == 1)
                    return this.y;
                throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!",
                    (object) index));
            }
            set
            {
                if (index != 0)
                {
                    if (index != 1)
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!",
                            (object) index));
                    this.y = value;
                }
                else
                    this.x = value;
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get { return (float) Math.Sqrt((this.x * this.x + this.y * this.y)); }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public int sqrMagnitude
        {
            get { return this.x * this.x + this.y * this.y; }
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector2Int a, Vector2Int b)
        {
            return (a - b).magnitude;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector2Int Scale(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector2Int scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        /// <summary>
        ///   <para>Clamps the Vector2Int to the bounds given by min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Clamp(Vector2Int min, Vector2Int max)
        {
            this.x = Math.Max(min.x, this.x);
            this.x = Math.Min(max.x, this.x);
            this.y = Math.Max(min.y, this.y);
            this.y = Math.Min(max.y, this.y);
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x + b.x, a.y + b.y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x - b.x, a.y - b.y);
        }

        public static Vector2Int operator *(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x * b.x, a.y * b.y);
        }

        public static Vector2Int operator *(Vector2Int a, int b)
        {
            return new Vector2Int(a.x * b, a.y * b);
        }

        public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        ///   <para>Returns true if the objects are equal.</para>
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            if (!(other is Vector2Int))
                return false;
            return this.Equals((Vector2Int) other);
        }

        public bool Equals(Vector2Int other)
        {
            return this.x.Equals(other.x) && this.y.Equals(other.y);
        }

        /// <summary>
        ///   <para>Gets the hash code for the Vector2Int.</para>
        /// </summary>
        /// <returns>
        ///   <para>The hash code of the Vector2Int.</para>
        /// </returns>
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        public override string ToString()
        {
            return $"({(object) this.x}, {(object) this.y})";
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (0, 0).</para>
        /// </summary>
        public static Vector2Int zero
        {
            get { return Vector2Int._zero; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (1, 1).</para>
        /// </summary>
        public static Vector2Int one
        {
            get { return Vector2Int._one; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (0, 1).</para>
        /// </summary>
        public static Vector2Int up
        {
            get { return Vector2Int._up; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (0, -1).</para>
        /// </summary>
        public static Vector2Int down
        {
            get { return Vector2Int._down; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (-1, 0).</para>
        /// </summary>
        public static Vector2Int left
        {
            get { return Vector2Int._left; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (1, 0).</para>
        /// </summary>
        public static Vector2Int right
        {
            get { return Vector2Int._right; }
        }
    }
}