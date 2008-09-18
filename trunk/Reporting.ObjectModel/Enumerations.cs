using System;
using System.Drawing;

namespace Reporting.ObjectModel
{
	public enum ChartElementOutput
	{
		Output,
		NoOutput
	}

	public enum ChartType
	{
		Column,
		Bar,
		Line,
		Pie,
		Scatter,
		Bubble,
		Area,
		Doughnut,
		Radar,
		Stock,
		Polar
	}

	public enum ChartSubType
	{
		Plain,
		Stacked,
		PercentStacked,
		Smooth,
		SmoothStacked,
		SmoothPercentStacked,
		Exploded,
		Line,
		SmoothLine,
		HighLowClose,
		OpenHighLowClose,
		Candlestick
	}

	public enum CommandType
	{
		Text,
		StoredProcedure,
		TableDirect
	}

	public enum DataElementOutput
	{
		Auto,
		Output,
		NoOutput,
		ContentsOnly		
	}

	public enum DataElementStyle
	{
		Auto,
		AttributeNormal,
		ElementNormal
	}

	public enum DataLabelPosition
	{
		Auto,
		Top,
		TopLeft,
		TopRight,
		Left,
		Center,
		Right,
		BottomRight,
		Bottom,
		BottomLeft
	}

	public enum DataType
	{
		Boolean,
		DateTime,
		Integer,
		Float,
		String
	}

	public enum DatapointDataElementOutput
	{
		Output,
		NoOutput
	}

	public enum DetailDataElementOutput
	{
		Output,
		NoOutput
	}

	public enum DataInstanceElementOutput
	{
		Output,
		NoOutput
	}

	public enum Direction
	{
		Ascending,
		Descending
	}

	public enum DrawingStyle
	{
		Cube,
		Cylinder
	}

	public enum CellDataElementOutput
	{
		Output,
		NoOutput
	}

	public enum GroupingDataElementOutput
	{
		Output,
		NoOutput
	}

	public enum LayoutDirection
	{
		LTR,
		RTL
	}

	public enum LegendLayout
	{
		Column,
		Row,
		Table
	}

	public enum LegendPosition
	{
		RightTop,
		TopLeft,
		TopCenter,
		TopRight,
		LeftTop,
		LeftCenter,
		LeftBottom,
		RightCenter,
		RightBottom,
		BottomRight,
		BottomCenter,
		BottomLeft
	}

	public enum MajorTickMarks
	{
		None,
		Inside,
		Outside,
		Cross
	}

	public enum MarkerType
	{
		None,
		Square,
		Circle,
		Diamond,
		Triangle,
		Cross,
		Auto
	}

	public enum MinorTickMarks
	{
		None,
		Inside,
		Outside,
		Cross
	}

	public enum MIMEType
	{
		Bitmap,
		JPEG,
		GIF,
		PNG,
		XPNG
	}

	public enum Palette
	{
		Default,
		EarthTones,
		Excel,
		GrayScale,
		Light,
		Pastel,
		SemiTransparent
	}

	public enum PlotType
	{
		Auto,
		Line
	}

	public enum Position
	{
		After,
		Before
	}

	public enum ProjectionMode
	{
		Perspective,
		Orthographic
	}

	public enum ReportDataElementStyle
	{
		AttributeNormal,
		ElementNormal
	}

	public enum Sensitivity
	{
		Auto,
		True,
		False,
	}

	public enum Shading
	{
		None, 
		Simple,
		Real
	}

	public enum Sizing
	{
		AutoSize,
		Fit,
		FitProportional,
		Clip
	}

	public enum Source
	{
		External,
		Embedded,
		Database
	}

	public enum TitlePosition
	{
		Center,
		Near,
		Far
	}

	public enum UsedInQuery
	{
		Auto,
		True,
		False,
	}

	public enum Operator
	{
		Equal,
		Like,
		NotEqual,
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual,
		TopN,
		BottomN,
		TopPercent,
		BottomPercent,
		In,
		Between
	}

	public enum UnitType
	{
		Point = 2,
		Pica = 3,
		Inch = 4,
		Mm = 5,		
		Cm = 6
	}

	public struct Size
	{
		private Unit	_unit;

		public Size(string size)
		{
			_unit = Unit.Parse(size);
        }

        public Size(Unit unit)
        {
            _unit = unit;
        }
        public Size(double inches)
        {
            _unit = Unit.Parse(inches.ToString()+"in");
        }

		#region Public Properties

		public double Length
		{
			get { return _unit.Value; }
			set { _unit.FPixels = value; }
		}

		public Unit Unit
		{
			get { return _unit; }
			set { _unit = value; }
		}

		public bool IsEmpty
		{
			get { return _unit.IsEmpty; }
		}

		#endregion

		public override string ToString()
		{
			return _unit.ToString();
		}

		public static Size Parse(string value)
		{
			Size size = new Size();

			size.Unit = Unit.Parse(value);

			return size;
		}

        #region Operator Overloads

		public static Size operator +(Size left, Size right)
		{
            return new Size(left.Unit.Value+right.Unit.Value);
		}

		public static Size operator -(Size left, Size right)
		{
            return new Size(left.Unit.Value-right.Unit.Value);
		}
        public static bool operator ==(Size left, Size right)
        {
            return (left.Unit.Value == right.Unit.Value);
        }
        public override bool Equals(object obj)
        {
            if (obj is Size)
                return this == (Size)obj;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator !=(Size left, Size right)
        {
            return (left.Unit.Value != right.Unit.Value);
        }
        public static bool operator >(Size left, Size right)
        {
            return (left.Unit.Value > right.Unit.Value);
        }
        public static bool operator <(Size left, Size right)
        {
            return (left.Unit.Value < right.Unit.Value);
        }
		#endregion
	}
}
