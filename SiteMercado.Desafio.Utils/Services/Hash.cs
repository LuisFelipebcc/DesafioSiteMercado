using System;
using System.Text;

namespace SiteMercado.Desafio.Utils.Services
{
    public static class Hash
    {
        #region MD5

        /// <summary>
        /// Hash MD5 de um Conteúdo em string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string MD5(string input)
        {
            return MD5(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash MD5 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string MD5(byte[] input)
        {
            using System.Security.Cryptography.MD5 hash =
                System.Security.Cryptography.MD5.Create();
            return BitConverter.ToString(hash.ComputeHash(input))
.Replace("-", "").ToLower();
        }

        #endregion

        #region SHA-1

        /// <summary>
        /// Hash SHA-1 de um Conteúdo em string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string SHA1(string input)
        {
            return SHA1(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash SHA-1 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string SHA1(byte[] input)
        {
            using System.Security.Cryptography.SHA1 hash =
                System.Security.Cryptography.SHA1.Create();
            return BitConverter.ToString(hash.ComputeHash(input))
.Replace("-", "").ToLower();
        }

        #endregion

        #region SHA-256

        /// <summary>
        /// Hash SHA-256 de um Conteúdo em string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string SHA256(this string input)
        {
            return SHA256(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash SHA-256 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string SHA256(byte[] input)
        {
            using System.Security.Cryptography.SHA256 hash =
                System.Security.Cryptography.SHA256.Create();
            return BitConverter.ToString(hash.ComputeHash(input)).Replace("-", "").ToLower();
        }

        #endregion

        #region SHA-512

        /// <summary>
        /// Hash SHA-512 de um Conteúdo em string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string SHA512(string input)
        {
            return SHA512(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash SHA-512 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em string Hexadecimal</returns>
        public static string SHA512(byte[] input)
        {
            using System.Security.Cryptography.SHA512 hash =
                System.Security.Cryptography.SHA512.Create();
            return BitConverter.ToString(hash.ComputeHash(input))
.Replace("-", "").ToLower();
        }

        #endregion
    }
}
