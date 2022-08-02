using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWithSerilogAndDI
{
    public class DataRepository : IDataRepository
    {
        private readonly ILogger<DataRepository> _log;
        private readonly IConfiguration _config;

        public DataRepository(ILogger<DataRepository> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Connect()
        {
            _log.LogInformation($"Connection string {_config.GetConnectionString("DefaultConnection")}");
        }
    }
}
