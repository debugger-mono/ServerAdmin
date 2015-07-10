
namespace Tbl.Framework.Configuration
{
    /// <summary>
    /// IConfigManager interface - Defines contract for implementing Config Manager
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// The get app setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <typeparam name="T">The type of value</typeparam>
        /// <returns>The key value of type</returns>
        T GetAppSetting<T>(string key);
    }
}