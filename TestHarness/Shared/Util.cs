using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestHarness.Shared
{
    class Util
    {
        internal static string StringFromObject(object ObjectToSerialize)
        {
            MemoryStream ms = new MemoryStream();
            string theSerializedObject;
            
            BinaryFormatter formatter = new BinaryFormatter();
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
    }
}
