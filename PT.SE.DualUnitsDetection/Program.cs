using CommandLine;
using Teamcenter.Services.Strong.Core;
using Teamcenter.Soa.Client;
using static System.Console;


namespace TcSOAMasterForm
{
    public class Options
    {
        [Option('d', "drawingNumber", Required = true, HelpText = "")]
        public string DrawingNumber { get; set; }

        [Option('r', "revision", Required = true, HelpText = "")]
        public string Revision { get; set; }

        [Option('w', "weight", Required = true, HelpText = "")]
        public string Weight { get; set; }

        [Option('x', "dimX", Required = true, HelpText = "")]
        public string DimX { get; set; }

        [Option('y', "dimY", Required = true, HelpText = "")]
        public string DimY { get; set; }

        [Option('z', "dimZ", Required = true, HelpText = "")]
        public string DimZ { get; set; }

        [Option('u', "user", Required = true, HelpText = "")]
        public string User { get; set; }

        [Option('p', "password", Required = true, HelpText = "")]
        public string Password { get; set; }

        [Option('l', "url", Required = true, HelpText = "")]
        public string Url { get; set; }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var o = Parser.Default.ParseArguments<Options>(args).Value;

            // Credentials
            CredentialManager credentialManager = new XCredentialsManager();
            credentialManager.SetUserPassword(o.User, o.Password, "");
            var connection = new Connection(o.Url, new System.Net.CookieCollection(), credentialManager, "REST", "HTTP", false);

            // Data Management Service
            var dmService = DataManagementService.getService(connection);

            dmService
                .GetMasterItemRevById(connection, o.DrawingNumber, o.Revision)
                .UpdateFormProperty("p9Weight", o.Weight) // hide the service
                .UpdateFormProperty("p9DimensionalX", o.DimX)
                .UpdateFormProperty("p9DimensionalY", o.DimY)
                .UpdateFormProperty("p9DimensionalZ", o.DimZ);


            WriteLine("Press key to exit...");
            ReadKey();
        }
    }
}


