using System;
using System.Text;

namespace Program
{
	
	public static class BinaryFilePatchExtensions
	{
		
		public static string BytesToString(this byte[] bytes, string addBetween = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte value in bytes)
			{
				stringBuilder.Append(value).Append(addBetween);
			}
			StringBuilder stringBuilder2 = stringBuilder;
			int i = stringBuilder2.Length;
			stringBuilder2.Length = i - 1;
			return stringBuilder.ToString();
		}

		
		public static byte[] HexStringToBytes(this string hex)
		{
			try
			{
				hex = hex.CleanHexString();
				if (hex.Length % 2 == 1)
				{
					return new byte[0];
				}
				byte[] array = new byte[hex.Length >> 1];
				for (int i = 0; i < hex.Length >> 1; i++)
				{
					array[i] = (byte)((hex[i << 1].GetHexVal() << 4) + hex[(i << 1) + 1].GetHexVal());
				}
				return array;
			}
			catch (Exception)
			{
			}
			return new byte[0];
		}

		
		public static int GetHexVal(this char hex)
		{
			return (int)(hex - ((hex < ':') ? '0' : '7'));
		}

		
		public static string CleanHexString(this string hexString)
		{
			foreach (string oldValue in BinaryFilePatchExtensions.hexSeparators)
			{
				hexString = hexString.Replace(oldValue, string.Empty);
			}
			return hexString.ToUpper();
		}

		
		public static string FormatHexString(this string hexString, string placeBetweenEachHex)
		{
			string text = hexString.CleanHexString();
			for (int i = text.Length - 2; i > 1; i -= 2)
			{
				text = text.Insert(i, placeBetweenEachHex);
			}
			return text;
		}

		
		public static readonly string[] hexSeparators = new string[]
		{
			" ",
			"0x",
			"x",
			":",
			"-"
		};
	}
}
