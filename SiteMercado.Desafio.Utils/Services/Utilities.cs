using SiteMercado.Desafio.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteMercado.Desafio.Utils.Services
{
    public static class Utilities
    {
        public static string AdicionarPrefixo(int count, bool tela = false)
        {
            var espace = count * (tela ? 12 : 8);
            return AdicionarEspaco(espace == 0 && tela ? 8 : espace) + " |--";
        }

        private static string AdicionarEspaco(int count)
        {
            var ret = string.Empty;

            for (var i = 0; i < count; i++)
            {
                ret += "-";
            }

            return ret;
        }

        public static string AnaliseException(Exception ex)
        {
            //if (ex is DbUpdateException exd)
            //{
            //    switch (Setting.Tipo)
            //    {
            //        case Enumerators.ConexaoTipo.MySql:
            //            {
            //                MySqlException exm = (MySqlException)ex;
            //                switch (exm.SqlState)
            //                {
            //                    case "2300":
            //                        return "Existem chaves associadas a este registro";
            //                    default:
            //                        return exd.InnerException.Message;
            //                }
            //            }
            //        default:
            //            return exd.InnerException.Message;
            //    }
            //}
            //else
            return ex.Message;
        }

        public static string AccentInsensitive(string texto)
        {
            texto ??= string.Empty;
            var normalizedString = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            str = AccentInsensitive(str);
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '-' || c == '_' || c == ' ')
                    sb.Append(c);
            }

            return sb.ToString();
        }

        public static string GetEnumDescription<T>(string value)
        {
            var type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static IEnumerable<SelectListItem> EnumToSelectList<T>(string tipoCase = null)
        {
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                e => new SelectListItem()
                {
                    Text = (tipoCase == null ? GetEnumDescription<T>(e.ToString()) : (tipoCase.ToUpper() == "U" ? GetEnumDescription<T>(e.ToString()).ToUpper() : GetEnumDescription<T>(e.ToString()).ToLower())),
                    Value = e.ToString()
                })).ToList();
        }

        public static T GetClaim<T>(ClaimsPrincipal user, string claimType)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var claims = identity.Claims.Where(x => x.Type == claimType).FirstOrDefault();
            if (claims != null)
                return (T)Convert.ChangeType(claims.Value, typeof(T));
            else
                throw new Exception($"{claimType} não encontrado");
        }

        public static T Parse<T>(string row, int indice, char caractere = '*')
        {
            var data = row.Split(caractere);

            try
            {
                var dado = data[indice];
                return (T)Convert.ChangeType(dado, typeof(T));
            }
            catch
            {
                return (T)Convert.ChangeType("0", typeof(T));
            }
        }

        public static string FormataDocumento(EnumCommon.PessoaTipo tipo, long documento)
        {
            var ret = tipo switch
            {
                EnumCommon.PessoaTipo.PessoaFísica => documento.ToString(@"000\.000\.000\-00"),
                EnumCommon.PessoaTipo.PessoaJurídica => documento.ToString(@"00\.000\.000\/0000\-00"),
                _ => string.Empty
            };

            return ret;
        }
    }
}
