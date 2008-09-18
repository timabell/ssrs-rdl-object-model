using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Formatters;
using System.Security.Permissions;

namespace Reporting.Extensibility
{
    public class Util
    {

        private static string XML_SETTING_PATH = "//{0}";
        private static string SETTING_CULTURE_INFO      = "CultureInfo";

        public enum FormatTypeEnum
        {
            None,
            Number,
            Currency,
            Percentage,
            PercentageNoSign,
            Date
        }
        /// <summary>
        /// Returns a setting value by its name
        /// </summary>
        /// <param name="settings">The Settings XML parameter</param>
        /// <param name="setting">The Setting Name</param>
        /// <returns></returns>
        public static string GetSetting(string settings, string setting)
        {
            if (string.IsNullOrEmpty(settings)) return null;

            XmlNode node;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(settings);
            
            node = doc.SelectSingleNode(string.Format(XML_SETTING_PATH, setting));
            return node==null?null:node.InnerText;
        }

  
        /// <summary>
        /// Returns a setting value by its name by looking it up in the Settings and ReportPackageSettings
        /// in that order.
        /// </summary>
        /// <param name="settings">The Settings XML parameter</param>
        /// <param name="reportPackageSettings">The ReportPackageSettings XML parameter</param>
        /// <param name="setting">The Setting Name</param>
        /// <returns></returns>
        public static string GetSetting(string settings, string reportPackageSettings, string setting)
        {
            if (string.IsNullOrEmpty(settings) || string.IsNullOrEmpty(reportPackageSettings)) return null;

            string settingValue = GetSetting(settings, setting);
            return settingValue==null?GetSetting(reportPackageSettings, setting):settingValue;
        }
 
        /// <summary>
        /// Return the format specifier to format currencies
        /// </summary>
        /// <param name="settings">The Settings XML parameter</param>
        /// <returns></returns>
       public static string GetCurrencyFormat(string settings, bool currencySymbol, int decimalPlaces)
        {

            string format = null;
            string cultureInfo = GetSetting(settings, SETTING_CULTURE_INFO);

            if (cultureInfo == null) return null;

            CultureInfo ci = DesializeCulture(cultureInfo);
 
            // get generic format for positive numbers
            string pf = GetGenericCurrencyFormatString(ci, true);

            // get generic format for negative numbers
            string nf = GetGenericCurrencyFormatString(ci, false);

            // construct the generic format specifier for both positive and negative numbers
            format = pf + ";" + nf;

            // replace currency symbol
            format = pf.Replace("$", currencySymbol ? ci.NumberFormat.CurrencySymbol : String.Empty);

            // get decimal places
            string decimalPlacesExpanded = GetDecimalPlaces(ci, decimalPlaces, FormatTypeEnum.Currency);

            // replace the numeric portion
            format = format.Replace("n", "#" + ci.NumberFormat.CurrencyGroupSeparator + "##0" + decimalPlacesExpanded);

            return format;
        }

        private static string GetGenericCurrencyFormatString(CultureInfo ci, bool positive)
        {
            string format = null;

            if (positive)
            {
                switch (ci.NumberFormat.CurrencyPositivePattern)
                {
                    case 0: format = "$n"; break;
                    case 1: format = "n$"; break;
                    case 2: format = "$ n"; break;
                    case 3: format = "n $"; break;
                }
            }
            else
            {
                switch (ci.NumberFormat.CurrencyNegativePattern)
                {
                    case 0: format = "($n)"; break;
                    case 1: format = "-$n"; break;
                    case 2: format = "$-n"; break;
                    case 3: format = "$n-"; break;
                    case 4: format = "(n$)"; break;
                    case 5: format = "-n$"; break;
                    case 6: format = "n-$"; break;
                    case 7: format = "n$-"; break;
                    case 8: format = "-n $"; break;
                    case 9: format = "-$ n"; break;
                    case 10: format = "n $-"; break;
                    case 11: format = "$ n-"; break;
                    case 12: format = "$ -n"; break;
                    case 13: format = "n- $"; break;
                    case 14: format = "($ n)"; break;
                    case 15: format = "(n $)"; break;
                }
            }
            return format;
        }
 
        private static string GetDecimalPlaces(CultureInfo ci, int decimalDigits, FormatTypeEnum numberFormatType)
        {
            string separator = null;
            if (decimalDigits <= 0) return string.Empty;

            switch (numberFormatType)
            {
                case FormatTypeEnum.Currency: separator = ci.NumberFormat.CurrencyDecimalSeparator; break;
                case FormatTypeEnum.Number: separator = ci.NumberFormat.NumberDecimalSeparator; break;
                case FormatTypeEnum.Percentage: separator = ci.NumberFormat.PercentDecimalSeparator; break;
            }

            return separator + String.Empty.PadLeft(decimalDigits, '0');

        }

        private static CultureInfo DesializeCulture(string culture)
        {
            SecurityPermission sp = new SecurityPermission(PermissionState.Unrestricted);
            sp.Assert();
            CultureInfo ci = (CultureInfo)StringToObject(culture);

            return ci;
        }


        private static string StringFromObject(object ObjectToSerialize)
        {
            MemoryStream ms = new MemoryStream();
            string theSerializedObject;


            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter
                = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            try
            {
                formatter.Serialize(ms, ObjectToSerialize);
                byte[] thisByteArray = ms.ToArray();
                theSerializedObject = Convert.ToBase64String(thisByteArray);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {

                throw;
            }
            finally
            {
                ms.Close();
            }


            return theSerializedObject;
        }


        private static object StringToObject(string StringToDeserialize)
        {
            object thisDeserializedObject = null;
            byte[] thisByteArray = Convert.FromBase64String(StringToDeserialize);
            MemoryStream ms = new MemoryStream(thisByteArray);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                thisDeserializedObject = formatter.Deserialize(ms);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {

                throw;
            }
            finally
            {
                ms.Close();
            }


            return thisDeserializedObject;
        }


    }
}
