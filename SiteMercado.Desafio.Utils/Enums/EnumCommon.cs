using System;
using System.ComponentModel;

namespace SiteMercado.Desafio.Utils.Enums
{
    public static class EnumCommon
    {
        public enum SystemMessageTypeEnum
        {
            Success,
            Info,
            Error,
        };

        public enum MensagemTipo
        {
            Success = 1,
            Danger,
            Info,
            Warning
        }

        public enum PessoaTipo
        {
            [Description("Pessoa Física")]
            PessoaFísica = 1,
            [Description("Pessoa Jurídica")]
            PessoaJurídica
        }



    }
}
