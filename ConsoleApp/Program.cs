using ConsoleApp.Models;
using ConsoleApp.Services;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentGradeService = new StudentGradeService();

            //Seed StudentsGrades
            Random rand = new Random();
            for(UInt64 i=0; i<9999999; i++)
            {
                StudentGradeService.studentsGrades.Add(
                    new StudentGrade
                    {
                        Student = new Student { Name="Student " + i.ToString() },
                        Grade = rand.Next(0, 10)
                    }
                );
            }
            Console.WriteLine("Terminou de criar os Students and Grades"); ;
            Thread.Sleep(5000);
            Console.WriteLine("Começa Primeiro Incremento"); ;
            //Incremento de Nota Async SEM Parallel
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            studentGradeService.AddGradeAsync().Wait();
            stopWatch.Stop();
            Console.WriteLine("Incremento de Nota Async SEM Parallel " + stopWatch.ElapsedMilliseconds);

            Thread.Sleep(5000);

            //Incremento de Nota Async COM Parallel
            stopWatch.Reset();
            stopWatch.Start();
            studentGradeService.AddGradeParallelAsync().Wait();
            stopWatch.Stop();
            Console.WriteLine("Incremento de Nota Async COM Parallel " + stopWatch.ElapsedMilliseconds);


            //var myThread = new Thread(DoSomething);
            //myThread.Start();
            Console.WriteLine("Hello World!");
        }

        //private static void DoSomething()
        //{
        //    Thread.Sleep(5000);
        //}
    }
}
