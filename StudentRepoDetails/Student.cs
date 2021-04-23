using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepoDetails
{
    class Student
    {
        string rollNumber;
        string email;
        string repository;
        string localRepo;
        string section;
        CommitDetails commits;

        public string RollNumber { get => rollNumber; set => rollNumber = value; }
        public string Email { get => email; set => email = value; }
        public string Repository { get => repository; set => repository = value; }
        

        public string Section { get => section; set => section = value; }
        public string LocalRepo { get => localRepo; set => localRepo = value; }
        internal CommitDetails Details { get => commits; set => commits = value; }
    }
}
