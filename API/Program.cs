using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)

                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseKestrel(options =>
                       {
                        //    options.ListenAnyIP(6245, opts =>
                        //        {
                        //            opts.UseHttps(FindMatchingCertificateBySubject("*.ncts.com.tr"));
                        //        }
                        //    );
                       });
                       webBuilder.UseStartup<Startup>();
                   });
        }

        private static X509Certificate2 FindMatchingCertificateBySubject(string subjectCommonName)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
                var certCollection = store.Certificates;
                var matchingCerts = new X509Certificate2Collection();

                foreach (var enumeratedCert in certCollection)
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals(subjectCommonName, enumeratedCert.GetNameInfo(X509NameType.SimpleName, forIssuer: false))
                        && DateTime.Now < enumeratedCert.NotAfter
                        && DateTime.Now >= enumeratedCert.NotBefore)
                    {
                        matchingCerts.Add(enumeratedCert);
                    }
                }

                if (matchingCerts.Count == 0)
                {
                    throw new Exception($"Could not find a match for a certificate with subject 'CN={subjectCommonName}'.");
                }

                return matchingCerts[0];
            }
        }
    }


}
