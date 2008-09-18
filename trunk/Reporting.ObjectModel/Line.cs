using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Represents a line report item on a report.
	/// </summary>
	public class Line : ReportItem
	{
		/// <summary>
		/// Creates a new instance of the Line class.
		/// </summary>
		public Line(){ }

		#region Hidden Properties

		/// <summary>
		/// Does not apply to a line.
		/// </summary>
		private new Expression ToolTip
		{
			get {return base.ToolTip;}
			set{base.ToolTip = value;}
		}

		/// <summary>
		/// Does not apply to a line.
		/// </summary>
		private new string LinkToChild
		{
			get {return base.LinkToChild;}
			set{base.LinkToChild = value;}
		}

		#endregion

		#region Protected Methods

		protected override string GetRootElement()
		{
			return Rdl.LINE;
		}

		protected override void WriteRDL(XmlWriter writer)
		{
			//Do Nothing
		}

		protected override void ReadRDL(XmlReader reader)
		{
			//Do Nothing
		}

		#endregion
	}
}
