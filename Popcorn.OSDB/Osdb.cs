﻿using System.Globalization;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using Popcorn.OSDB.Backend;

namespace Popcorn.OSDB
{
    public class Osdb
    {
        private IOsdb ProxyInstance { get; set; }

        private IOsdb Proxy => ProxyInstance ?? (ProxyInstance = XmlRpcProxyGen.Create<IOsdb>());

        public Task<IAnonymousClient> Login(string userAgent)
        {
            var systemLanguage = GetSystemLanguage();
            return Login(systemLanguage, userAgent);
        }

        private async Task<IAnonymousClient> Login(string language, string userAgent)
        {
            var client = new AnonymousClient(Proxy);
            await client.Login(string.Empty, string.Empty, language, userAgent);
            return client;
        }

        private string GetSystemLanguage()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            return currentCulture.TwoLetterISOLanguageName.ToLower(CultureInfo.InvariantCulture);
        }
    }
}