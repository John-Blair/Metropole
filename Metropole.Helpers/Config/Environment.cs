using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Metropole.Helpers
{
    public sealed class Environment
    {
        private static readonly NameValueCollection settingCollection =
               (NameValueCollection)ConfigurationManager.GetSection("Environment");
        private static readonly Environment instance = new Environment();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Environment()
        {
        }

       

        private bool _DebugMenuInit;
        private bool _DebugMenu;
        public bool DebugMenu
        {
            get
            {
                if (!_DebugMenuInit)
                {
                    _DebugMenuInit = true;
                    _DebugMenu = settingCollection["DebugMenu"].ToBoolean();

                }

                return _DebugMenu;
            }
        }


        private Environment()
        {

        }

        public static Environment Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
