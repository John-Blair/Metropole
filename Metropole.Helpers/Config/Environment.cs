using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Metropole.Helpers
{
    public sealed class EnvironmentSingleton
    {
        private static readonly NameValueCollection settingCollection =
               (NameValueCollection)ConfigurationManager.GetSection("Environment");
        private static readonly EnvironmentSingleton instance = new EnvironmentSingleton();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static EnvironmentSingleton()
        {
        }

        private string _EmailApiKey;
        public string EmailApiKey {
            get {
                return _EmailApiKey ?? 
                    (_EmailApiKey = settingCollection["EmailApiKey"] ?? "");
            }
        }


        private EnvironmentSingleton()
        {

        }

        public static EnvironmentSingleton Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
