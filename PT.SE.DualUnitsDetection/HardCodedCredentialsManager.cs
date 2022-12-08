using Teamcenter.Schemas.Soa._2006_03.Exceptions;
using Teamcenter.Soa.Client;

namespace PT.SE.DualUnitsDetection
{
    internal class HardCodedCredentialsManager : CredentialManager
    {
        public string username = "";
        public string password = "";
        public string group = "";
        public string role = "";
        public string discriminator = "";

        public int CredentialType => 2;

        public string[] GetCredentials(InvalidCredentialsException invalidCredentials)
        {
            return new string[] { username, password, group, role, discriminator };
        }

        public string[] GetCredentials(InvalidUserException invalidUser)
        {
            return new string[] { username, password, group, role, discriminator };
        }

        public void SetGroupRole(string group, string role)
        {
            this.group = group;
            this.role = role;
        }

        public void SetUserPassword(string user, string password, string discriminator)
        {
            this.username = user;
            this.password = password;
            this.discriminator = discriminator;
        }
    }
}
