using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepoDetails
{
    class CommitDetails
    {
     
        private readonly List<Commit> commits = new List<Commit>();

        public List<Commit> GetCommits() {
            return commits;
        }
        
        public void SetCommits(Commit value){
            commits.Add(value);
        }
      
    }
}
