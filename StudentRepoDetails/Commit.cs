using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepoDetails
{
    public class Commit
    {
        private string author;
        private string time;
        private string date;

        public string Author { get => author; set => author = value; }
        public string Time { get => time; set => time = value; }
        public string Date { get => date; set => date = value; }
    }
}
