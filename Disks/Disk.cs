using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disks
{
    class Disk
    {
        public int id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string company { get; set; }
        public int releaseYear { get; set; }
        public string type { get; set; }
        public Disk(int id, string code, string title, string company, int releaseYear, string type)
        {
            this.id = id;
            this.code = code;
            this.title = title;
            this.company = company;
            this.releaseYear = releaseYear;
            this.type = type;
        }
    }
}
