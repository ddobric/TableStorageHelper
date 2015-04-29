using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorageHelper
{
    class Program
    {
        private static CloudStorageAccount m_StorageAccount;
        private static string m_AccKey;
        private static string m_AccName;
         

        /// <summary>
        /// accountname key command commandparam
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                goHelp();
                return;
            }

            readArgs(args);

            if (args.Length == 4)
            {
                switch (args[2].ToLower())
                {
                    case "deletetables":
                    case "dt":
                        deleteTables(args[3]);
                        break;
                    case "listtables":
                    case "lt":
                        listTables(args[3]);
                        break;
                    case "deleterecords":
                    case "dr":
                        deleteTableRecords(args[3]);
                        break;
                    default:
                        break;
                }
            }
            m_StorageAccount = getAccount();
        }





        private static void goHelp()
        {
            Console.WriteLine("TableStorageHelper StorageAccountName StorageAccountKey Command CommandParams");
            Console.WriteLine("Supported commands:");
            Console.WriteLine("deletetables (dt)");
            Console.WriteLine("listtables (lt)");
            Console.WriteLine("\r\nExamples:");
            Console.WriteLine("delete all tables which start witn name 'abc'");
            Console.WriteLine("TableStorageHelper StorageAccountName StorageAccountKey delete abc");
        }

        private static void readArgs(string[] args)
        {
            m_AccName = args[0];
            m_AccKey = args[1];

            m_StorageAccount = getAccount();
        }

        private static CloudStorageAccount getAccount()
        {

            CloudStorageAccount storageAccount =
                new CloudStorageAccount(new StorageCredentials(m_AccName, m_AccKey), true);


            //CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            //var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            return storageAccount;
        }

        private static void deleteTables(string strtsWith)
        {
            Console.WriteLine("Delete tables which start with '{0}'", strtsWith);

            CloudTableClient tableClient = new CloudTableClient(m_StorageAccount.TableEndpoint,
                 m_StorageAccount.Credentials);

            var tables = tableClient.ListTables(strtsWith);
            foreach (var tbl in tables)
            {
                Console.WriteLine("Deleting table '{0}'", tbl.Name);
                tbl.Delete();
            }
        }

        private static void listTables(string strtsWith)
        {
            Console.WriteLine("List tables which start with '{0}'", strtsWith);

            CloudTableClient tableClient = new CloudTableClient(m_StorageAccount.TableEndpoint,
               m_StorageAccount.Credentials);

            var tables = tableClient.ListTables(strtsWith);
            foreach (var tbl in tables)
            {
                Console.WriteLine(tbl.Name);
            }
        }

        private static void deleteTableRecords(string p)
        {
            throw new NotImplementedException();
        }

    }
}
