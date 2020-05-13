using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketIO.Contracts.V1
{
    public static class APIRoutes
    {
        public const string Version = "V1/";
        public const string Base = Version +"api/";
        public static class Account
        {
            public const string Base =  APIRoutes.Base +"account/";
            public const string Login = Base + "Login/";
            public const string CheckLogin = Base + "CheckLogin/";


        }

    }
}
