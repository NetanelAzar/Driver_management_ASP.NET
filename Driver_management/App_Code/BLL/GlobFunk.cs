using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Driver_management
{
	public class GlobFunk
	{
		public static string GetRandStr(int length)
		{
			string str = "abcdefjhijklmnopqrstuvwsyz0123456789";

			string RetStr = "";
			int index;
			Random rand = new Random();
			for (int i = 0; i < length; i++)
			{
				index = rand.Next(str.Length);
				RetStr += str[index];
			}
			return RetStr;
		}
	}
}