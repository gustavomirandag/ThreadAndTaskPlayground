using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleApp.Services
{
    public class StudentGradeService
    {
        public static readonly List<StudentGrade> studentsGrades
            = new List<StudentGrade>();

        public double AverageGradeSync()
        {
            if (studentsGrades.Count == 0)
                return 0;

            double result = 0;
            foreach (var studentGrade in studentsGrades)
                result += studentGrade.Grade;
            return result / studentsGrades.Count;
        }

        public async Task<double> AverageGradeAsync()
        {
            var grade = await Task.Run(AverageGradeSync);
            return grade;
        }

        public void AddGradeSync()
        {
            foreach (var studentGrade in studentsGrades)
                studentGrade.Grade += 1;
        }

        public async Task AddGradeAsync()
        {
            await Task.Run(AddGradeSync);
        }

        public async Task AddGradeParallelAsync()
        {
            await Task.Run(() =>
            {
                Parallel.For(0, studentsGrades.Count,
                    (i) => studentsGrades[i].Grade++);
            });
        }

        public async Task AddGradeLinqAsParallelAsync()
        {
            await Task.Run(() =>
            {
                var incrementedStudentGrades = studentsGrades
                    .AsParallel().Select(sg =>
                        new StudentGrade
                        {
                            Student = sg.Student,
                            Grade = sg.Grade + 1
                        }
                );
            });
        }


    }
}
