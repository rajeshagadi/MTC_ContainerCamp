using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureReadingList.Models
{
    public class Settings
    {
        public static string DatabaseId = "ReadingList2";
        public static string CollectionId = "Recommendations";
        public static string readerName = "richross";
        public static string EndPoint = Environment.GetEnvironmentVariable("EndPoint");
        public static string ReadWriteAuthKey = Environment.GetEnvironmentVariable("ReadWriteAuthKey"); //read-write
        public static string ReadOnlyAuthKey = Environment.GetEnvironmentVariable("ReadOnlyAuthKey"); //read only
    }
}
