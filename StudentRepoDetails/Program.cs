using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.IO;


namespace StudentRepoDetails
{
    class Program
    {
        static void Main()
        {
  
            string localFolderPath = "E:\\FYP\\OOP Lab\\TestRepo\\";
            string fileName = "E:\\FYP\\OOP Lab\\TestRepo\\Result.xlsx";
            string excelSheet = "E:\\FYP\\OOP Lab\\TestRepo\\GUI.xlsx";
           
            //ExcelReport(fileName, student);
            Students students = ReadXLS(excelSheet);
           
            Console.WriteLine("Feteching Repositories");
            foreach (Student student in students.GetStudents()) {
                student.LocalRepo = CreateLocalRepository(student.Repository, localFolderPath + student.RollNumber);
            }
            Console.WriteLine("Feteched all Repositories");



            Console.WriteLine("Feteching Commits' Details");
            foreach (Student student in students.GetStudents())
            {
                CommitDetails details = GetCommitsDetailsFromLocalRepo(student.LocalRepo);
                student.Details = details;
            }
            Console.WriteLine("Feteched Commits' Details");

            Console.WriteLine("Creating report");
            StudentsReport(students, fileName);
            Console.WriteLine("Successfully created report");

            Console.ReadKey();
        }


        static Students ReadXLS(string FilePath)
        {
            /**
             * Read student details from excel sheet*
             */
            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;     //get row count

                Students students = new Students();

                for (int row = 2; row <= rowCount; row++)
                {
                    Student s = new Student
                    {
                        Section = worksheet.Cells[row, 1].Value?.ToString(),
                        RollNumber = worksheet.Cells[row, 2].Value?.ToString(),
                        Email = worksheet.Cells[row, 3].Value?.ToString(),
                        Repository = worksheet.Cells[row, 4].Value?.ToString()
                    };
                    students.AddStudent(s);
                    
                }
            
                return students;
            }
        }
        static string CreateLocalRepository(string url, string localFolderPath)
        {
            /**
             * Get the url of the github repository and create local repo
             * **/
            try {
                string repoPath = Repository.Clone(url, @localFolderPath);
                return repoPath;

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            
            }
            
        }

        static CommitDetails GetCommitsDetailsFromLocalRepo(string localRepo) {

            if (localRepo == null) {
                return null;
            }
            CommitDetails details = new CommitDetails();
            Repository repo = new Repository(localRepo);

            foreach (var commit in repo.Commits)
            {
                Commit c = new Commit
                {
                    Author = commit.Author.Name,
                    Date = commit.Author.When.Date.ToString("d"),
                    Time = commit.Author.When.DateTime.ToString("h:mm tt")
                };
                details.SetCommits(c);
            }
            return details;
        }
        

       static void StudentsReport(Students students, string fileName)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Worksheet1");

            var excelWorksheet = excel.Workbook.Worksheets["Worksheet1"];

            List<string[]> headerRow = new List<string[]>() {
              new string[] { "Roll Number","Email","Section" , "Repository", "No. of commits", "Date and time" }
            };

            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
            excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

            int row = 2;

            foreach( Student student in students.GetStudents())
            {
                excelWorksheet.Cells[row, 1].Value = student.RollNumber;
                excelWorksheet.Cells[row, 2].Value = student.Email;
                excelWorksheet.Cells[row, 3].Value = student.Section;
                excelWorksheet.Cells[row, 4].Value = student.Repository;
                if (student.Details!=null)
                {
                    excelWorksheet.Cells[row, 5].Value = student.Details.GetCommits().Count();
                    int column = 6;
                    foreach (Commit c in student.Details.GetCommits())
                    {
                        excelWorksheet.Cells[row, column].Value = c.Date + " " + c.Time;
                        column++;

                    }
                }
                row++;
            }
            FileInfo excelFile = new FileInfo(@fileName);
            excel.SaveAs(excelFile);


        }

       
    }
}
