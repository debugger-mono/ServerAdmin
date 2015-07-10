using System;
using System.Configuration;

namespace Tbl.Framework.Configuration
{
    /// <summary>
    /// The application configuration manager
    /// </summary>
    public sealed class AppConfigManager : IConfigManager
    {
        /// <summary>
        /// Gets the typed app setting value
        /// </summary>
        /// <typeparam name="T">Type of setting value</typeparam>
        /// <param name="key">Setting key name</param>
        /// <returns>Typed setting value</returns>
        public T GetAppSetting<T>(string key)
        {
            string appSetting = ConfigurationManager.AppSettings[key];

            if (appSetting == null)
            {
                throw new ApplicationException(string.Format("Key '{0}' did not exist in the config file.", key));
            }

            try
            {
                return (T)Convert.ChangeType(appSetting, typeof(T));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    string.Format(
                        "Key '{0}' with the value of '{1}' could not be converted into the type '{2}'.",
                        key,
                        appSetting,
                        typeof(T)),
                    ex);
            }
        }
    }
}
