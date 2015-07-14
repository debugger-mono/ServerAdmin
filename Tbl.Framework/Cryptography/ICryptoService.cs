
namespace Tbl.Framework.Cryptography
{
    public interface ICryptoService
    {
        /// <summary>
        /// Encrypts plainText string
        /// </summary>
        /// <param name="plainText">The plain text string to encrypt</param>
        /// <param name="secretKey">The secret key</param>
        /// <returns>The encrypted string</returns>
        string Encrypt(string plainText, string secretKey);

        /// <summary>
        /// Decrypts encrypted text to plain text
        /// </summary>
        /// <param name="encryptedText">Encrypted Text</param>
        /// <param name="secretKey">The secret key</param>
        /// <returns>The decrypted text</returns>
        string Decrypt(string encryptedText, string secretKey);

        /// <summary>
        /// Encodes text to base 64 string
        /// </summary>
        /// <param name="text">The text string</param>
        /// <returns>The base 64 encoded string</returns>
        string Base64Encode(string text);

        /// <summary>
        /// Encodes byte array to base 64 string
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <returns>The base 64 encoded string</returns>
        string Base64Encode(byte[] bytes);

        /// <summary>
        /// Decodes base64 encoded string
        /// </summary>
        /// <param name="base64Text">The base64 encoded string</param>
        /// <returns>The decoded string</returns>
        string Base64Decode(string base64Text);

        /// <summary>
        /// Decodes base64 encoded string to byte array
        /// </summary>
        /// <param name="base64Text">The base 64 encoded text</param>
        /// <param name="bytes">Decoded byte array</param>
        void Base64Decode(string base64Text, out byte[] bytes);
    }
}