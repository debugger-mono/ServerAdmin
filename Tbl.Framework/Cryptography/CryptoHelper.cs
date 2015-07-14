using System;
using System.Security.Cryptography;
using System.Text;

namespace Tbl.Framework.Cryptography
{
    internal static class CryptoHelper
    {
        private const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Encrypts plainText string
        /// </summary>
        /// <param name="plainText">The plain text string to encrypt</param>
        /// <param name="secretKey">The secret key</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string plainText, string secretKey)
        {
            using (SymmetricAlgorithm algorithm = EncryptAlgorithm(secretKey))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);

                using (ICryptoTransform cryptoTransform = algorithm.CreateEncryptor())
                {
                    byte[] tranform = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                    return Base64Encode(tranform);
                }
            }
        }

        /// <summary>
        /// Decrypts encrypted text to plain text
        /// </summary>
        /// <param name="encryptedText">Encrypted Text</param>
        /// <param name="secretKey">The secret key</param>
        /// <returns>The decrypted text</returns>
        public static string Decrypt(string encryptedText, string secretKey)
        {
            using (SymmetricAlgorithm algo = EncryptAlgorithm(secretKey))
            {
                byte[] bytes;
                Base64Decode(encryptedText, out bytes);

                using (ICryptoTransform cyptoTransform = algo.CreateDecryptor())
                {
                    byte[] transform = cyptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                    return Encoding.UTF8.GetString(transform);
                }
            }
        }

        /// <summary>
        /// Encodes text to base 64 string
        /// </summary>
        /// <param name="text">The text string</param>
        /// <returns>The base 64 encoded string</returns>
        public static string Base64Encode(string text)
        {
            return Base64Encode(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Encodes byte array to base 64 string
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <returns>The base 64 encoded string</returns>
        public static string Base64Encode(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 8)
            {
                int remainingLength = bytes.Length - i;
                ulong longNumber;

                if (remainingLength < 8)
                {
                    byte[] array = new byte[8];
                    for (int j = 0; i < remainingLength; j++)
                    {
                        array[j] = bytes[i + j];
                    }

                    longNumber = BitConverter.ToUInt64(array, 0);
                }
                else
                {
                    longNumber = BitConverter.ToUInt64(bytes, i);
                }

                sb.Append(LongToText(longNumber));
                sb.Append("-");
            }

            return sb.ToString().TrimEnd(new char[] { '-' });
        }

        /// <summary>
        /// Decodes base64 encoded string
        /// </summary>
        /// <param name="base64Text">The base64 encoded string</param>
        /// <returns>The decoded string</returns>
        public static string Base64Decode(string base64Text)
        {
            byte[] bytes;
            Base64Decode(base64Text, out bytes);
            string str = Encoding.UTF8.GetString(bytes);
            return str.TrimEnd(new char[1]);
        }

        /// <summary>
        /// Decodes base64 encoded string to byte array
        /// </summary>
        /// <param name="base64Text">The base 64 encoded text</param>
        /// <param name="bytes">Decoded byte array</param>
        public static void Base64Decode(string base64Text, out byte[] bytes)
        {
            string[] temp = base64Text.Split(new char[] { '-' });
            int wordSize = 8;
            bytes = new byte[wordSize * temp.Length];
            int num = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                string str = temp[i];
                ulong value = TextToLong(str);
                byte[] tempBytes = BitConverter.GetBytes(value);
                tempBytes.CopyTo(bytes, num);
                num += wordSize;
            }
        }

        /// <summary>
        /// Gets Symmetric Algorithm for encryption/decryption
        /// </summary>
        /// <param name="secretKey">The secret key</param>
        /// <returns>The symmetric algorithm</returns>
        private static SymmetricAlgorithm EncryptAlgorithm(string secretKey)
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            Rfc2898DeriveBytes rfc2898bytes = new Rfc2898DeriveBytes(secretKey, new byte[]{
                99,  // c
                53,  // 5
                121, // y
                57,  // 9
                55,  // 7
                48,  // 0
                64,  // @
                55,  // 7
                104, // h
                33,  // !
                110, // n
                75,  // K
                56,  // 8
                89,  // Y 
                55,  // 7
                36,  // $
                76,  // L
                55,  // 7
                68   // D
            });

            rijndael.Key = rfc2898bytes.GetBytes(32);
            rijndael.IV = rfc2898bytes.GetBytes(16);

            return rijndael;
        }

        /// <summary>
        /// Converts unsigned long number to text
        /// </summary>
        /// <param name="longNumber">The unsigned long to be converted</param>
        /// <returns>The text string</returns>
        private static string LongToText(ulong longNumber)
        {
            uint length = 12;
            uint @base = 36;
            char[] array = new char[length + 1];
            char[] allowedChars = AllowedChars.ToCharArray();

            while (longNumber > 0)
            {
                ulong temp = longNumber % @base;
                array[length] = allowedChars[temp];
                longNumber = (longNumber - temp) / @base;
                length--;
            }

            char[] trimChar = new char[1];

            return string.Concat(array).Trim(trimChar);
        }

        /// <summary>
        /// Converts string to unsigned long number
        /// </summary>
        /// <param name="text">The text string</param>
        /// <returns>The unsigned long number</returns>
        private static ulong TextToLong(string text)
        {
            ulong result = 0;
            uint @base = 36;

            text = text.ToUpperInvariant().Trim();

            foreach (char c in text.ToCharArray())
            {
                int index = AllowedChars.IndexOf(c);
                result = result * @base + (ulong)index;
            }

            return result;
        }
    }
}