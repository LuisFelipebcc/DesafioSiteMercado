using System.Text;

namespace SiteMercado.Desafio.Utils.Services
{
    public class Setting
    {
        public static string ConnectionStringGerenciamento { get; set; }
        public static string ConnectionStringPortalSistemas { get; set; }
        public static string ConnectionStringRenavamOleDB { get; set; }
        public static string RenavamWebserviceUserCode { get; set; }
        public static string RenavamWebserviceSenha { get; set; }
        public static string SefazWebserviceUrl { get; set; }
        public static byte[] SecretKey { get; set; } = Encoding.ASCII.GetBytes("7xxQJiCdl7e8CvTNmpqI");
        public static bool IsProducao { get; set; }
    }
}
