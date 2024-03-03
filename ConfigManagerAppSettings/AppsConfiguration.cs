using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConfigManagerAppSettings
{
    public class AppsConfiguration : IConfigManager
    {
        private readonly IConfiguration _configuration;
        public AppsConfiguration(IConfiguration configuration) { 
        this._configuration = configuration;
        }

        public string APICONSTRING
        {
            get
            {
                return this._configuration["ConnectionStrings:BookDeliverySystem"];
            }
        }
    }
}
