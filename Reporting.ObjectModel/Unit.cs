using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;

namespace Reporting.ObjectModel
{
	public struct Unit
	{
		#region Constants

		public const float CentimetersPerInch = 2.54f;
		public const float MillimetersPerInch = 25.4f;
		public const float PicasPerInch = 6f;
		public const float PointsPerInch = 72f;

		public static readonly Unit Empty;

		#endregion

		private static float _dotsPerInch;
		private static UnitType _defaultType;

		private UnitType _type;
		private double _value;

		#region Constructors

		static Unit()
		{
			Unit.Empty = new Unit();
			_defaultType = UnitType.Inch;
		}

		public Unit(double pixels)
		{
			double units = Unit.ConvertToUnits(pixels, Unit.DefaultType);
			_value = units;
			_type = Unit.DefaultType;
		}

		public Unit(int pixels)
		{
			double units = Unit.ConvertToUnits((double)pixels, Unit.DefaultType);
			_value = units;
			_type = Unit.DefaultType;
		}

        public Unit(double value, UnitType type)
        {
            _type = type;
            _value = Unit.ConvertToUnits((double)value, type);
        }


		public Unit(string value) : this(value, CultureInfo.CurrentCulture, Unit.DefaultType) { }

		public Unit(string value, CultureInfo culture) : this(value, culture, Unit.DefaultType){}

		public Unit(string value, CultureInfo culture, UnitType defaultType)
		{
			_value = 0;
			_type = defaultType;
			if ((value != null) && (value.Length != 0))
				Init(value, culture, defaultType);
		}

		#endregion

		#region Public Properties

		public bool IsEmpty
		{
			get { return (_type == ((UnitType)0)); }
		}

		public UnitType Type
		{
			get
			{
				if (!IsEmpty)
					return _type;

				return Unit.DefaultType;
			}
		}

		public double Value
		{
			get { return _value; }
            set { _value = value; }
		}

		public int Pixels
		{
			get { return Convert.ToInt32(Unit.ConvertToPixels(_value, _type)); }
		}

		public double FPixels
		{
			get { return Unit.ConvertToPixels(_value, _type); }
			set
			{
				if (IsEmpty)
					_type = Unit.DefaultType;

				_value = Unit.ConvertToUnits(value, _type);
			}
		}

		public static UnitType DefaultType
		{
			get { return Unit._defaultType; }
			set { Unit._defaultType = value; }
		}

		public static float DotsPerInch
		{
			get
			{
				if (Unit._dotsPerInch == 0f)
				{
					using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
					{
						Unit._dotsPerInch = graphics.DpiX;
					}
				}
				return Unit._dotsPerInch;
			}
		}

		#endregion

		#region Public Methods

		public string ToString(CultureInfo culture)
		{
			if (IsEmpty)
				return string.Empty;

			string text = _value.ToString("0.#####", culture);
			return (text + Unit.GetStringFromType(_type));
		}

		public Unit ChangeType(UnitType type)
		{
			if (type == _type)
				return this;

			return new Unit(Unit.ConvertToUnits(Unit.ConvertToPixels(_value, _type), type), type);
		}

		public static double ConvertToPixels(double value, UnitType type)
		{
			switch (type)
			{
				case UnitType.Point:
					{
						value *= (Unit.DotsPerInch / 72f);
						return value;
					}
				case UnitType.Pica:
					{
						value *= (Unit.DotsPerInch / 6f);
						return value;
					}
				case UnitType.Inch:
					{
						value *= Unit.DotsPerInch;
						return value;
					}
				case UnitType.Mm:
					{
						value *= (Unit.DotsPerInch / 25.4f);
						return value;
					}
				case UnitType.Cm:
					{
						value *= (Unit.DotsPerInch / 2.54f);
						return value;
					}
			}
			return value;
		}
        /// <summary>
        /// Converts pixes to the specified unit type
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="type"></param>
        /// <returns></returns>
		public static double ConvertToUnits(double pixels, UnitType type)
		{
			double tempPixels = pixels;
			switch (type)
			{
				case UnitType.Point:
					{
						return (tempPixels / ((double)(Unit.DotsPerInch / 72f)));
					}
				case UnitType.Pica:
					{
						return (tempPixels / ((double)(Unit.DotsPerInch / 6f)));
					}
				case UnitType.Inch:
					{
						return (tempPixels / ((double)Unit.DotsPerInch));
					}
				case UnitType.Mm:
					{
						return (tempPixels / ((double)(Unit.DotsPerInch / 25.4f)));
					}
				case UnitType.Cm:
					{
						return (tempPixels / ((double)(Unit.DotsPerInch / 2.54f)));
					}
			}
			return tempPixels;
		}

		public static UnitType GetTypeFromString(string value)
		{
			if ((value == null) || (value.Length <= 0))
				return Unit.DefaultType;

			if (value.Equals("pt"))
				return UnitType.Point;

			if (value.Equals("pc"))
				return UnitType.Pica;

			if (value.Equals("in"))
				return UnitType.Inch;

			if (value.Equals("mm"))
				return UnitType.Mm;

			if (!value.Equals("cm"))
				throw new ArgumentException(string.Format("{0} is not a valid unit designator. Valid unit designators are in, mm, cm, pt, pc.", value));

			return UnitType.Cm;
		}

		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture, Unit._defaultType);
		}

		public static Unit Parse(string s, CultureInfo culture, UnitType unitType)
		{
			return new Unit(s, culture, unitType);
		}

		public static void ValidateRange(string paramName, Unit value, Unit min, Unit max)
		{
            //if (min == null)
            //    throw new ArgumentNullException("min");

            //if(max == null)
            //    throw new ArgumentNullException("max");

			double fPixels = value.FPixels;
			if (fPixels > ((Unit)min).FPixels && fPixels < ((Unit)max).FPixels)
				return;
			else
				throw new ArgumentOutOfRangeException(paramName);
		}

		#endregion

		#region Internal Methods

		public static Unit Parse(string s)
		{
			return new Unit(s);
		}

        public static Unit FromPixels(double pixels, UnitType type)
		{
			return new Unit(Unit.ConvertToUnits(pixels, type), type);
		}
        public static Unit FromInches(double inches)
        {
            Unit u = new Unit();
            u._type = UnitType.Inch;
            u.Value = inches;
            return u;
        }
        public static Unit FromPoints(double points)
        {
            Unit u = new Unit();
            u._type = UnitType.Point;
            u.Value = points;
            return u;
        }

		#endregion

		#region Private Methods

		private void Init(string value, CultureInfo culture, UnitType defaultType)
		{
			if (culture == null)
				culture = CultureInfo.CurrentCulture;

			string valueString = value.Trim().ToLower();
			int valueLength = valueString.Length;
			int num2 = -1;

			for (int i = 0; i < valueLength; i++)
			{
				char currentChar = valueString[i];
				if (((currentChar < '0') || (currentChar > '9')) && (((currentChar != '-') && (currentChar != '.')) && (currentChar != ',')))
					break;

				num2 = i;
			}
			if (num2 == -1)
				throw new FormatException(string.Format("{0} cannot be parsed as a unit because it does not contain numeric values. Examples of valid unit strings are \"1pt\" and \".5in\".", value));

			if (num2 < (valueLength - 1))
			{
				_type = Unit.GetTypeFromString(valueString.Substring(num2 + 1).Trim());
			}
			else
			{
				if (defaultType == ((UnitType)0))
					throw new FormatException(string.Format("{0} does not contain a unit specification. Examples of valid unit strings are \"1pt\" and \".5in\".", value));

				_type = defaultType;
			}

			string valueSubstring = valueString.Substring(0, num2 + 1);

			try
			{
				TypeConverter converter = new SingleConverter();	
				_value = (float)converter.ConvertFromString(null, CultureInfo.InvariantCulture, valueSubstring);
			}
			catch
			{
				throw new FormatException(string.Format("The numeric portion of {0} cannot be parsed as a unit of type {2}.", value, _type.ToString("G")));
			}
		}

		private static string GetStringFromType(UnitType type)
		{
			switch (type)
			{
				case UnitType.Point:
					{
						return "pt";
					}
				case UnitType.Pica:
					{
						return "pc";
					}
				case UnitType.Inch:
					{
						return "in";
					}
				case UnitType.Mm:
					{
						return "mm";
					}
				case UnitType.Cm:
					{
						return "cm";
					}
			}
			return string.Empty;
		}

		#endregion

		#region Object Overrides

		public override int GetHashCode()
		{
			return ((_type.GetHashCode() << 2) ^ _value.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			if ((obj != null) && (obj is Unit))
			{
				Unit unit = (Unit)obj;
				if ((unit._type == _type) && (unit._value == _value))
					return true;
			}

			return false;
		}

		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}

		#endregion

		#region Operator Overloads

		public static bool operator ==(Unit left, Unit right)
		{
			if (left._type == right._type)
			{
				return (left._value == right._value);
			}

			return false;
		}

		public static bool operator !=(Unit left, Unit right)
		{
			if (left._type == right._type)
			{
				return (left._value != right._value);
			}

			return true;
		}
        public static bool operator >(Unit left, Unit right)
        {
            if (left._type == right._type)
            {
                return (left._value > right._value);
            }

            return false;
        }
        public static bool operator <(Unit left, Unit right)
        {
            if (left._type == right._type)
            {
                return (left._value < right._value);
            }

            return false;
        }
		#endregion
	}
}
