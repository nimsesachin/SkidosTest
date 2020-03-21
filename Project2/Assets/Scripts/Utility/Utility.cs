using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Utility
{
	public static object GetDefault(Type type)
	{
#if UNITY_WSA && !UNITY_EDITOR
			return null;
#else
		// If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
		if (type == null || !type.IsValueType || type == typeof(void))
			return null;

		return Activator.CreateInstance(type);
#endif
	}


	public static object DeserializeFromXml(string inData, Type inType)
		{
			if (string.IsNullOrEmpty(inData))
			{
				Debug.LogError("Deserialize error inData is null!"+ "\n Data Type : "+inType);
				return GetDefault(inType);
			}

			//make object from Deserializer
			using (StringReader sr = new StringReader(inData))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(inType);
				try
				{
					return xmlSerializer.Deserialize(sr);
				}
				catch (System.Exception inException)
				{
					return GetDefault(inType);
				}
			}
		}
}
