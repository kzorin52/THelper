using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace THelper
{
    public class Crypto
    {
        /// <summary>
        ///     Шифрует строку с помощью пароля и некоторых алгоритмов шифрования. Возвращает строку в кодировке Base64
        /// </summary>
        /// <param name="Text">Входной текст</param>
        /// <param name="Pass">Пароль для шифровки</param>
        /// <returns>Зашифрованный текст</returns>
        public static string Encode(string Text, string Pass)
        {
            return Helper2.Encrypt(Helper1.Encrypt(Text), Pass);
        }

        /// <summary>
        ///     Шифрует файл с помощью пароля и некоторых алгоритмов шифрования.
        /// </summary>
        /// <param name="PathToFile">Путь к файлу</param>
        /// <param name="pass">Пароль</param>
        public static void EncodeFile(string PathToFile, string pass)
        {
            var FileData = File.ReadAllText(PathToFile);
            var FileDataEncoded = Encode(FileData, pass);
            File.WriteAllText(PathToFile, FileDataEncoded);
        }

        /// <summary>
        ///     Дешифрует файл с помощью пароля, заранее зашифрованный способом THelper.Crypto.EncryptFile.
        /// </summary>
        /// <param name="PathToFile">Путь к файлу</param>
        /// <param name="pass">Пароль</param>
        public static void DecodeFile(string PathToFile, string pass)
        {
            var FileData = File.ReadAllText(PathToFile);
            var FileDataDecoded = Decode(FileData, pass);
            File.WriteAllText(PathToFile, FileDataDecoded);
        }

        /// <summary>
        ///     Расшифровывает строку в кодировке Base64, возвращает string
        /// </summary>
        /// <param name="EncodedText">Зашифрованный текст в формате Base64</param>
        /// <param name="Pass">Пароль для дешифровки</param>
        /// <returns>Расшифрованный текст</returns>
        public static string Decode(string EncodedText, string Pass)
        {
            return Helper2.Decrypt(Helper1.Decrypt(EncodedText), Pass);
        }
    }

    internal class Helper2
    {
        #region Fields

        private static readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Расшифровывает строку в кодировке Base64, возвращает string
        /// </summary>
        /// <param name="cipherText">Зашифрованная строка в кодировке base64</param>
        /// <param name="passPhrase">Пароль для раскодировки</param>
        /// <returns>Расшифрованный текст</returns>
        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                var ciphertextS = DecodeFrom64(cipherText);
                var ciphersplit = Regex.Split(ciphertextS, "-");
                var passsalt = Convert.FromBase64String(ciphersplit[1]);
                var initVectorBytes = Convert.FromBase64String(ciphersplit[0]);
                var cipherTextBytes = Convert.FromBase64String(ciphersplit[2]);
                var password = new PasswordDeriveBytes(passPhrase, passsalt, "SHA512", 100);
                var keyBytes = password.GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                var plainTextBytes = new byte[cipherTextBytes.Length];
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        ///     Шифрует строку с помощью пароля. Возвращает строку в кодировке base64
        /// </summary>
        /// <param name="plainText">Входной текст</param>
        /// <param name="passPhrase">Пароль для шифровки</param>
        /// <returns>Шифрованный текст</returns>
        public static string Encrypt(string plainText, string passPhrase)
        {
            var initvector = new byte[16]; //MUST BE 16 Bytes for AES 256
            var passsalt = new byte[16]; //For Salt
            rng.GetBytes(initvector);
            rng.GetBytes(passsalt);
            var initVectorBytes = initvector;
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, passsalt, "SHA512", 100);
            var keyBytes = password.GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return
                EncodeTo64(Convert.ToBase64String(initVectorBytes) + "-" + Convert.ToBase64String(passsalt) + "-" +
                           Convert.ToBase64String(cipherTextBytes));
        }

        private static string DecodeFrom64(string encodedData)
        {
            var encodedDataAsBytes = Convert.FromBase64String(encodedData);
            var returnValue = Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }

        private static string EncodeTo64(string toEncode)
        {
            var toEncodeAsBytes = Encoding.UTF8.GetBytes(toEncode);
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        #endregion Methods
    }

    internal class Helper1
    {
        public static string Pass = "P@@Sw0rd";
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        /// <summary>
        ///     Шифрует строку. Возвращает строку в кодировке base64
        /// </summary>
        /// <param name="plainText">Входной текст</param>
        /// <returns>Зашифрованный текст</returns>
        public static string Encrypt(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            var keyBytes = new Rfc2898DeriveBytes(Pass, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC, Padding = PaddingMode.Zeros};
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }

                memoryStream.Close();
            }

            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        ///     Расшифровывает строку в кодировке Base64, возвращает string
        /// </summary>
        /// <param name="encryptedText">Шифрованный текст в формате Base64</param>
        /// <returns>Расшифрованный текст</returns>
        public static string Decrypt(string encryptedText)
        {
            var cipherTextBytes = Convert.FromBase64String(encryptedText);
            var keyBytes = new Rfc2898DeriveBytes(Pass, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC, Padding = PaddingMode.None};

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];

            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}