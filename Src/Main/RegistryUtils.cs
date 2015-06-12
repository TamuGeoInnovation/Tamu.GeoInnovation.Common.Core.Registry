using System;
using Microsoft.Win32;

namespace USC.GISResearchLab.Common.Utils.Registries
{
	/// <summary>
	/// Summary description for RegistryUtils.
	/// </summary>
	public class RegistryUtils
	{
		public static bool showError = false;

		public RegistryUtils()
		{
		}

		public static string getDefaultDataPath(RegistryKey baseRegistryKey, string SubKey, string dataPathKey)
		{
			string ret = Read(baseRegistryKey, SubKey, dataPathKey);
			if (ret == null)
			{
				Write(baseRegistryKey, SubKey, dataPathKey, "C:\\");
			}
			ret = Read(baseRegistryKey, SubKey, dataPathKey);
			return ret;
		}

		public static void setDefaultDataPath(RegistryKey baseRegistryKey, string SubKey, string dataPathKey, string dataKeyValue)
		{
			Write(baseRegistryKey, SubKey, dataPathKey, dataKeyValue);
		}

		public static string Read(RegistryKey baseRegistryKey, string subKey, string KeyName)
		{
            string ret = null;
			RegistryKey rk = baseRegistryKey ;
			RegistryKey sk1 = rk.OpenSubKey(subKey);
			if ( sk1 != null )
			{
				
				try 
				{
					ret = (string)sk1.GetValue(KeyName.ToUpper());
				}
				catch (Exception e)
				{
					throw new Exception("Error Reading registry " + KeyName.ToUpper(), e);
				}
			}

            return ret;
		}

		public static bool Write(RegistryKey baseRegistryKey, string subKey,string KeyName, object Value)
		{
            bool ret;
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				sk1.SetValue(KeyName.ToUpper(), Value);

				ret = true;
			}
			catch (Exception e)
			{
                throw new Exception("Error writing registry " + KeyName.ToUpper(), e);
			}
            return ret;
		}

		public static bool DeleteKey(RegistryKey baseRegistryKey, string subKey, string KeyName)
		{
            bool ret;
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
                if (sk1 != null)
                {
                    sk1.DeleteValue(KeyName);
                }
				ret = true;
			}
			catch (Exception e)
			{
                throw new Exception("Error Deleting subKey " + subKey, e);
			}
            return ret;
		}


		public static bool DeletesubKeyTree(RegistryKey baseRegistryKey, string subKey)
		{
            bool ret;
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                {
                    rk.DeleteSubKeyTree(subKey);
                }
				ret = true;
			}
			catch (Exception e)
			{
                throw new Exception("Error Deleting subKeyTree " + subKey, e);
			}
            return ret;
		}

		public static int subKeyCount(RegistryKey baseRegistryKey, string subKey)
		{
            int ret = 0;
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                {
                    ret = sk1.SubKeyCount;
                }
			}
			catch (Exception e)
			{
                throw new Exception("Error Retriving subKeys count of " + subKey, e);
			}
            return ret;
		}

		public static int ValueCount(RegistryKey baseRegistryKey, string subKey)
		{
            int ret = 0;
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                {
                    ret = sk1.ValueCount;
                }
			}
			catch (Exception e)
			{
                throw new Exception("Error Retriving subKeys value count of " + subKey, e);
			}
            return ret;
		}
	}
}
