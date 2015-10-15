using System;

namespace ConnectUTS
{
	public class HelpingFunction
	{
		public const string strSeparator = "__,__";

		public static string convertArrayToString(string[] strArray)
		{
			string str = "";
			for (int i = 0; i < strArray.Length; i++) 
			{
				str += strArray [i];
				if (i < strArray.Length - 1) 
				{
					str += strSeparator;
				}
			}
			return str;
		}

		public static string[] convertStringToArray(string str)
		{
			string[] arr = str.Split (strSeparator.ToCharArray());
			return arr;
		}
	}
}
