using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ShadowClassLibrary
{
    public class ActiveDirectory
    {
        private const string DOMAINNAME = "DOMAINNAME HERE";
        // set up domain context
        static private PrincipalContext ctx = new PrincipalContext(ContextType.Domain, DOMAINNAME);
        private static List<string> ou = new List<string> { "BACK CALLROOM WIN10", "FRONT CALLROOM WIN10", "MAIN CALLROOM WIN10", "VDI MACHINES" };
        private string userName, password;
        public ActiveDirectory(string workStation)
        {
            Computer = ComputerPrincipal.FindByIdentity(ctx, workStation);
            //isMemberOf(Environment.UserName, "Domain Admins");
        }
        public ActiveDirectory(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public bool IsValid
        {
            get
            {
                // validate the credentials
                return ctx.ValidateCredentials(userName, password);
            }
        }
        public List<string> GetOu
        {
            get
            {
                if (Computer == null) { return null; }
                List<string> orgUnits = new List<string>();
                // Getting the DirectoryEntry
                DirectoryEntry directoryEntry = Computer.GetUnderlyingObject() as DirectoryEntry;
                //if the directoryEntry is not null
                if (directoryEntry != null)
                {
                    string s = directoryEntry.Path.Substring(directoryEntry.Path.IndexOf("OU"));
                    s = s.Remove(s.IndexOf(","));
                    orgUnits.Add(s);
                }
                return orgUnits;
            }

        }



        public bool IsManagment
        {
            get
            {
                if (Computer != null)
                {
                    foreach (string ou in GetOu)
                    {
                        if (ou.Contains("MANAGMENT"))
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
        }
        public static bool IsRestricted(string workStation)
        {
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, workStation);
            bool isRestricted = true;
            // Getting the DirectoryEntry
            DirectoryEntry directoryEntry = computer.GetUnderlyingObject() as DirectoryEntry;
            //if the directoryEntry is not null
            if (directoryEntry != null)
            {
                foreach (string s in ou)
                {
                    if (directoryEntry.Path.ToUpper().Contains(s))
                    {
                        isRestricted = false;
                        continue;
                    }
                }
            }
            return isRestricted;

        }
        public static bool ComputerExists(string workStation)
        {
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, workStation);
            if (computer != null)
            {
                return true;
            }
            return false;
        }
        public bool isMemberOf(string userName, string groupName = "Domain Admins", string domain = DOMAINNAME)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain);

            // find a user
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);

            // find the group in question
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, groupName);

            if (user == null)
                return false;
            //throw new ApplicationException(string.Format("User {0} not found.", userName));

            if (group == null)
                return false;
            //throw new ApplicationException(string.Format("Group {0} not found.", groupName));


            Principal foundUsers = group.GetMembers(true).Where(p => p.SamAccountName.Equals(user.SamAccountName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            // check if user is member of that group
            if (foundUsers != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ComputerPrincipal Computer { get; set; }
    }

}

