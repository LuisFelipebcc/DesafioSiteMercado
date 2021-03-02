using System;

namespace SiteMercado.Desafio.Utils.Interfaces
{
    public interface ILog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(Exception exception);
    }
}
