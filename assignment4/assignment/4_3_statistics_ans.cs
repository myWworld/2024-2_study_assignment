
using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------


            ScoreSystem sc = new ScoreSystem();
            sc.SetStdCnt(stdCount);
            sc.SetInformation(data);
            sc.PrintAverage();
            sc.PrintMaxAndMin();
            sc.PrintPlace();
            // --------------------
        }




    }


    public class ScoreSystem
    {

        int stdCnt;
        string[,] infomation = new string[6,6];

        public void SetInformation(string[,] data) { infomation = data; }

        public void SetStdCnt(int stdcnt) { stdCnt = stdcnt; }

        public void PrintAverage()
        {
            double mathAverage = 0;
            double scienceAverage = 0;
            double englishAverage = 0;

            for (int i = 1; i <= stdCnt; i++)
            {
                mathAverage += double.Parse(infomation[i, 2]);
                scienceAverage += double.Parse(infomation[i, 3]);
                englishAverage += double.Parse(infomation[i, 4]);

            }

            mathAverage /= stdCnt;
            scienceAverage /= stdCnt;
            englishAverage /= stdCnt;


            Console.WriteLine("Average Scores:");
            Console.WriteLine("Math: {0}", mathAverage);
            Console.WriteLine("Science: {0}", scienceAverage);
            Console.WriteLine("English: {0}\n", englishAverage);

        }

        public void PrintMaxAndMin()
        {
  

            Console.WriteLine("Max and min Scores:");

    

          for (int sub = 2;  sub <stdCnt;  sub++)
          {
              double maxVal = -999;
              double minVal = 999;

              for (int j = 1; j <= stdCnt; j++)
              {
                  maxVal = (maxVal < double.Parse(infomation[j,sub])) ? double.Parse(infomation[j, sub]) : maxVal;
                  minVal = (minVal > double.Parse(infomation[j, sub])) ? double.Parse(infomation[j, sub]) : minVal;
              }

              Console.WriteLine("{0}: ({1}, {2})", infomation[0,sub], maxVal,minVal);
          }

        
        

            Console.WriteLine('\n');

        }

        public void PrintPlace()
        {
            Console.WriteLine("Student rank by total score");

            double[] totalScore = new double[stdCnt + 1];
            int[] place = new int[stdCnt + 1];
            

            for (int i = 1; i <= stdCnt; i++)
            {
     
                for (int sub = 2; sub < stdCnt; sub++)
                {
                    totalScore[i] += double.Parse(infomation[i, sub]);
                }
            }

            for (int i = 1; i <= stdCnt; i++)
            {
                place[i] = 1;

                for (int j = 1; j <= stdCnt ; j++)
                {

                    if(i != j)
                    {
                        if (totalScore[i] < totalScore[j])
                        {
                            place[i]++;
                        }
                    }

                }
            }

            for (int i = 1; i <= stdCnt; i++)
            {
                switch(place[i])
                {
                    case 1:
                        Console.WriteLine("{0}: {1}st", infomation[i, 1], place[i]);
                        break;
                    case 2:
                        Console.WriteLine("{0}: {1}nd", infomation[i, 1], place[i]);
                        break;
                    case 3:
                        Console.WriteLine("{0}: {1}rd", infomation[i, 1], place[i]);
                        break;
                    default:
                        Console.WriteLine("{0}: {1}th", infomation[i, 1], place[i]);
                        break;
                }

                
            }


        }

     
    }
}