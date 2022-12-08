using SolidEdge.SDK;
using SolidEdgeConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Teamcenter.Services.Strong.Core;
using Teamcenter.Services.Strong.Core._2010_09.DataManagement;
using Teamcenter.Services.Strong.Query;
using Teamcenter.Services.Strong.Query._2007_06.SavedQuery;
using Teamcenter.Services.Strong.Query._2010_04.SavedQuery;
using Teamcenter.Soa.Client;
using Teamcenter.Soa.Client.Model;
using Teamcenter.Soa.Client.Model.Strong;
using Teamcenter.Soa.Common;

namespace PT.SE.DualUnitsDetection
{

    public class Options
    {
        [CommandLine.Option('u', "uii", Required = true, HelpText = "")]
        public string Uii { get; set; }

        [CommandLine.Option('w', "weight", Required = true, HelpText = "")]
        public string Weight { get; set; }

        [CommandLine.Option('d', "dimension", Required = false, HelpText = "")]
        public string Dimension { get; set; }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var options = CommandLine.Parser.Default.ParseArguments<Options>(args).Value;

            Console.WriteLine("Connection to TC");
            CredentialManager credentialManager = new HardCodedCredentialsManager();
            credentialManager.SetUserPassword("test_user", "plmtest", "");
            var connection = new Teamcenter.Soa.Client.Connection("http://tc13dv.premiertech.com/tc", new System.Net.CookieCollection(), credentialManager, "REST", "HTTP", false);
            SavedQueryService sqService = SavedQueryService.getService(connection);
            DataManagementService dmService = DataManagementService.getService(connection);


        }


    }
}
