using System;
using System.Security.Cryptography.X509Certificates;

namespace WebApi.Identity.Server
{
    static class Certificate
    {
        public static X509Certificate2 Get() 
            => new X509Certificate2($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Identity\Server\Certs\idsrv3test.pfx", "idsrv3test");
    }
}