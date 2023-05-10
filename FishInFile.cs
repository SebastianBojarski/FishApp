﻿

using System.ComponentModel.Design;

namespace FishApp
{
    public class FishInFile : FishBase
    {
        private const string fileName = "gradesFish.txt";
        public override event GradeAddedDelegate GradeAdded;
        public FishInFile(string name, string weight, string time)
             : base(name, weight, time)
        {

        }
        public override void AddGrade(float grade)
        {
            if (grade >= 0 && grade <= 10)
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(grade);
                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                }
            }
            else
            {
                throw new Exception("Za duza ocena");
            }
        }
        public override void AddGrade(int grade)
        {
            this.AddGrade((float)grade);
        }
        public override void AddGrade(string grade)
        {
            if (float.TryParse(grade, out float result))
            {
                this.AddGrade(result);
            }
            else
            {
                throw new Exception("Prosze podac cyfre");
            }
        }
        public override Statistics GetStatistics()
        {
            var gradesFromFile = this.ReadGradesFromFile();
            var result = this.CountStatistics(gradesFromFile);
            return (Statistics)result;
        }
        private List<float> ReadGradesFromFile()
        {
            var grades = new List<float>();
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText($"{fileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var number = float.Parse(line);
                        grades.Add(number);
                        line = reader.ReadLine();
                    }
                }
            }
            return grades;
        }
        private Statistics CountStatistics(List<float> grades)
        {
            {
                var statistics = new Statistics();

                foreach (var grade in grades)
                {
                    statistics.AddGrade(grade);
                }

                return statistics;
            }
        }
    }
}







