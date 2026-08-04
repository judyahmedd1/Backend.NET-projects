using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace projects_1
{
    internal class Program
    {
        static double GetAverage(List<double> grades)
        {
            //ensure no divison by zero 
            if (grades.Count == 0)
                return 0.0;

            double sum = 0.0;
            for (int i = 0; i < grades.Count; i++)
            {
                sum += grades[i];
            }
            return sum / grades.Count;
        }
        static void Main(string[] args)
        {
            //Store student names in a list of grades.
            //            Calculate the average grade for each student.
            //Assign a random grade level to each student
            //            Freshman, Sophomore, Junior, Senior.
            //            Generate and display a formatted report of all studen
            //            Project - Student Grades Tracker
            //            Rule for Level Assignment
            //            Average 0.0 – 20 → Freshman
            //            Average 20 – 40 → Sophomore
            //            Average 40 – 60 → Junior
            //            Average 60 – 100 → Senior


            List<string> students = new List<string>();
            students.Add("judy");
            students.Add("hana");
            students.Add("menna");

            //assigning values to grades' lists
            List<double> judygrades = new List<double> { 15, 18, 20 };
            List<double> hanagrades = new List<double> { 35, 40, 38 };
            List<double> mennagrades = new List<double> { 55, 60, 58 };
            List<List<double>> allGrades = new List<List<double>> {
               judygrades, hanagrades, mennagrades
            };

            string randomlevel;
            for (int i = 0; i < students.Count; i++)
            {
                double average = GetAverage(allGrades[i]);
                //assigning level based on average calculated
                if (average >= 0.0 && average <= 20.0)
                {
                    randomlevel = "Freshman";
                }
                else if (average > 20.0 && average <= 40.0)
                {
                    randomlevel = "Sophomore";
                }
                else if (average > 40.0 && average <= 60.0)
                {
                    randomlevel = "Junior";
                }
                else
                    randomlevel = "Senior";

                //printing results for each student
                Console.WriteLine($"student: {students[i]}");
                Console.WriteLine($"average is: {average}");
                Console.WriteLine($"level is: {randomlevel}");
            }

        }
    }
}
