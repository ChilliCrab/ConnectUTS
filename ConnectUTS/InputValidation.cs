using System;

namespace ConnectUTS
{
	public class InputValidation
	{
		public static bool isFilled(String[] input)
		{
			foreach (string str in input) 
			{
				if (String.IsNullOrWhiteSpace (str))
					return false;
			}
			return true;
		}


	}
}

