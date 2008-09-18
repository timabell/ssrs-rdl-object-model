//using Microsoft.ReportingServices.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;

namespace Reporting.ObjectModel
{
    [TypeConverter(typeof(FontWeightConverter))]
    public sealed class ReportFontWeight
	{
		#region Constants

		private static readonly string[] fontWeightStrings = new string[] { "Lighter", "Thin", "Extra Light", "Light", "Normal", "Medium", "Semi-bold", "Bold", "Extra Bold", "Heavy", "Bolder" };
		private static readonly string[] fontWeightValues = new string[] { "Lighter", "100", "200", "300", "400", "500", "600", "700", "800", "900", "Bolder" };

		#endregion

		#region Private Variables

		private static Hashtable fontWeightHash;
		private string _value;
        private string _text;

		#endregion

		#region Constructors

        static ReportFontWeight()
        {
			ReportFontWeight.fontWeightHash = new Hashtable();
			for (int i = 0; i < fontWeightValues.Length; i++)
				ReportFontWeight.fontWeightHash.Add(fontWeightValues[i], fontWeightStrings[i]);
        }

        public ReportFontWeight()
        {
            _value = "";
        }

        public ReportFontWeight(string value)
        {
			_value = value;
		}
        public ReportFontWeight(string value, string text)
        {
			_value = value;
            _text = text;
		}

		#endregion

		internal string Text
		{
			get
			{
				if (ValuesHash.ContainsKey(_value))
					return (string)ValuesHash[_value];

				return _value;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					_value = string.Empty;
				else if (ValuesHash.ContainsValue(value))
					_value = fontWeightValues[Array.IndexOf<string>(fontWeightStrings, value)];
				else
					throw new ArgumentException("Invalud value: " + value);
			}
		}

        internal string Value
        {
            get { return _value; }
            set
            {
                if ((value == "Bold") || (value == "Normal"))
                    value = fontWeightValues[Array.IndexOf<string>(fontWeightStrings, value)];

				if (!ValuesHash.ContainsKey(value))
					throw new ArgumentException("Invalid value: " + value);
				
                _value = value;
                _text = GetStringFromValue(value);
            }

        }

        internal Hashtable ValuesHash
        {
            get { return ReportFontWeight.fontWeightHash; }
        }
        internal static string GetValueFromString(string s)
        {

            for (int i = 0; i < fontWeightStrings.Length; i++)
            {
                if (String.Compare(fontWeightStrings[i], s, true) == 0) return fontWeightValues[i];
            }
            return null;
        }
        internal static string GetStringFromValue(string v)
        {

            for (int i = 0; i < fontWeightValues.Length; i++)
            {
                if (String.Compare(fontWeightValues[i], v, true) == 0) return fontWeightStrings[i];
            }
            return null;
        }
        public override string  ToString()
        {
 	         return _text;
        }

        internal sealed class FontWeightConverter : TypeConverter
        {
            // Methods
			public FontWeightConverter() { }

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
        //                private static readonly string[] fontWeightStrings = new string[] { "Lighter", "Thin", "Extra Light", "Light", "Normal", "Medium", "Semi-bold", "Bold", "Extra Bold", "Heavy", "Bolder" };
        //private static readonly string[] fontWeightValues = new string[] { "Lighter", "100", "200", "300", "400", "500", "600", "700", "800", "900", "Bolder" };

                Collection<ReportFontWeight> values = new Collection<ReportFontWeight>();
                values.Add(new ReportFontWeight("Lighter", "Lighter"));
                values.Add(new ReportFontWeight("100", "Thin"));
                values.Add(new ReportFontWeight("200", "Extra Light"));
                values.Add(new ReportFontWeight("300", "Light"));
                values.Add(new ReportFontWeight("400", "Normal"));
                values.Add(new ReportFontWeight("500", "Medium"));
                values.Add(new ReportFontWeight("600", "Semi-bold"));
                values.Add(new ReportFontWeight("700", "Bold"));
                values.Add(new ReportFontWeight("800", "Extra Bold"));
                values.Add(new ReportFontWeight("900", "Heavy"));
                values.Add(new ReportFontWeight("Bolder", "Bolder"));

                return new StandardValuesCollection(values);
				//return new StandardValuesCollection(ReportFontWeight.fontWeightStrings);
			}

            //public override string ToString()
            //{
            //    return _value;
            //}

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

            //public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        //    {
        //        if (sourceType == typeof(string))
        //            return true;

        //        return base.CanConvertFrom(context, sourceType);
        //    }

        //    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        //    {
        //        return true;
        //        if (destinationType == typeof(ReportFontWeight))
        //            return true;

        //        return base.CanConvertTo(context, destinationType);
        //    }

        //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        //    {
        //        if (!(value is string))
        //            return base.ConvertFrom(context, culture, value);

        //        ReportFont font = (ReportFont)context.Instance;
        //        ReportFontWeight weigth = font.FontWeight;
        //        return weigth.GetValueFromString((string)value);
        //        //return new ReportFontWeight(weigth.Value);


        //    }

        //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        //    {
        //        System.Diagnostics.Trace.WriteLine(destinationType.ToString());

        //        if (value is ReportFontWeight)
        //            return value;
        //        else
        //            return ReportFontWeight.GetValueFromString((string)value);
        //        if (destinationType == null)
        //            throw new ArgumentNullException("destinationType");

        //        if (destinationType == typeof(string))
        //        {
        //            if (value is ReportFontWeight)
        //            {
                        
        //            }
        //        }
        //        return base.ConvertTo(context, culture, value, destinationType);
        //    }
        
        }


    }
}

