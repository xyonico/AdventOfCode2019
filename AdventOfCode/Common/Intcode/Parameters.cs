using System;

namespace AdventOfCode.Common.Intcode
{
	public struct Parameters
	{
		public readonly byte Count;

		private int _param1;
		private int _param2;
		private int _param3;

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => Param1,
					1 => Param2,
					2 => Param3,
					_ => throw new IndexOutOfRangeException()
				};
			}
			set
			{
				switch (index)
				{
					case 0:
						Param1 = value;
						break;
					case 1:
						Param2 = value;
						break;
					case 2:
						Param3 = value;
						break;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}

		public int Param1
		{
			get
			{
				if (Count < 1)
				{
					throw new IndexOutOfRangeException();
				}

				return _param1;
			}
			set
			{
				if (Count < 1)
				{
					throw new IndexOutOfRangeException();
				}

				_param1 = value;
			}
		}

		public int Param2
		{
			get
			{
				if (Count < 2)
				{
					throw new IndexOutOfRangeException();
				}

				return _param2;
			}
			set
			{
				if (Count < 2)
				{
					throw new IndexOutOfRangeException();
				}

				_param2 = value;
			}
		}
		
		public int Param3
		{
			get
			{
				if (Count < 3)
				{
					throw new IndexOutOfRangeException();
				}

				return _param3;
			}
			set
			{
				if (Count < 3)
				{
					throw new IndexOutOfRangeException();
				}

				_param3 = value;
			}
		}

		public Parameters(byte count)
		{
			Count = count;
			
			_param1 = 0;
			_param2 = 0;
			_param3 = 0;
		}
	}
}