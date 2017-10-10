using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureReadingList.Models
{
    public class Recommendation
    {
        public string id;
        public string type = "recommendation";
        public String isbn;
        public String title;
        public String author;
        public String description;
        public String imageURL;
    }
}
