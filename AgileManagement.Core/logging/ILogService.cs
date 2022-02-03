using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    public static class LogLevels
    {
        public const string Error = "Error";
        public const string Information = "Info";
        public const string Warning = "Warning";
        public const string Success = "Success";
    }

    /// <summary>
    /// Yapılan işlemlere ait kayıtları izleyebilmek için bu servisin Log methodunu kullanacağız. Mesaj ve logLevel loglama kritiklik seviyesi
    /// </summary>
    public interface ILogService
    {
        //asasd
        void Log(string message, string logLevel);
    }
}
