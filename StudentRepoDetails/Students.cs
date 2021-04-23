using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepoDetails
{
    class Students
    {
        readonly List<Student> students = new List<Student>();

        public List<Student> GetStudents() {
            return students;
        }
        public void AddStudent(Student value) {
            students.Add(value);
        }

    }
}
