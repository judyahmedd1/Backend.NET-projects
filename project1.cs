using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projects_1
{
    internal class Program
    {
        enum Randomlevel
        {
            Freshman = 1,
            Sophomore,
            Junior,
            Senior
        }
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


            Dictionary<string, List<double>> students =  new Dictionary<string, List<double>>();

            students.Add("judy", new List<double> { 15, 18, 20 });
            students.Add("hana", new List<double> { 35, 40, 38 });
            students.Add("menna", new List<double> { 55, 60, 58 });

            //list to get count of all names to be used in loop
            List<string> studentnames = new List<string>(students.Keys);


            Randomlevel level;

            for (int i = 0; i < studentnames.Count; i++)
            {
                string currentstudent = studentnames[i];

                double average = GetAverage(students[currentstudent]);

                if (average <= 20)
                    level = Randomlevel.Freshman;
                else if (average <= 40)
                    level = Randomlevel.Sophomore;
                else if (average <= 60)
                    level = Randomlevel.Junior;
                else
                    level = Randomlevel.Senior;

                Console.WriteLine($"student: {currentstudent}\naverage is: {average:F2}\nrandom level is: {level}\n");
            }
            
        }
    }
}
