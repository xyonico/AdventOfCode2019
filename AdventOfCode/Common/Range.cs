namespace AdventOfCode.Common
{
	public struct Range<T> where T : struct
	{
		public T Min;
		public T Max;

		public Range(T min, T max)
		{
			Min = min;
			Max = max;
		}

		public bool Equals(Range<T> other)
		{
			return Min.Equals(other.Min) && Max.Equals(other.Max);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Min.GetHashCode() * 397) ^ Max.GetHashCode();
			}
		}
	}
}