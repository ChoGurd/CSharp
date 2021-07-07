using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task3
{
    class Report
    {
        public DateTime dateStart = new DateTime();
        public DateTime dateEnd = new DateTime(2099, 1, 2, 3, 10, 20, 30);
        public int countSuccesfullStatusTopUp = 0;//Количество удачных статусов добавления воды
        public int countFailedStatusTopUp = 0;//Количество не улачных статусов добавления воды
        public int literSuccesfullTopUp = 0;//Количество литров налито воды;
        public int literFailedTopUp = 0;//Количество литров не налито воды;
        public int countSuccesfullStatusDrop = 0;//сколько удачных статусов убавления воды
        public int countFailedStatusDrop = 0;//Сколько не улачных статусов убавления воды
        public int literSuccesfullDrop = 0;//сколько было литров слито воды
        public int literFailedDrop = 0;//сколько было литров не слито воды


        public string PercentFailed(int a, int b, int c)
        {
            Double percent = ((Convert.ToDouble(a)) * 100 / Convert.ToDouble(b));
            return Math.Round(percent, c) + "";
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }

        public bool SaveInCSV(Report report)
        {
            string path = "resulte.csv";
            try
            {
                if (!File.Exists(path))
                {
                    using (File.Create(path));
                }
                using (StreamWriter sr = new StreamWriter(path, false, Encoding.UTF8))
                {
                    
                    
                    sr.WriteLine("количество попыток налить воду в бочку было за указанный период;" + report.Sum(report.countFailedStatusTopUp , report.countSuccesfullStatusTopUp));
                    sr.WriteLine("Процент ошибок наливания воды за указанный период;" + report.PercentFailed(countFailedStatusTopUp, countFailedStatusTopUp + countSuccesfullStatusTopUp,2));//2
                    sr.WriteLine("Количество налитых литров воды;" + report.literSuccesfullTopUp);//3
                    sr.WriteLine("Количество не налитых литров воды;" + report.literFailedTopUp);//4
                    sr.WriteLine("количество попыток вылить воду в бочку было за указанный период;" + report.Sum(report.countFailedStatusDrop , report.countSuccesfullStatusDrop));//5
                    sr.WriteLine("Процент ошибок выливания воды;" + report.PercentFailed(countFailedStatusDrop, countFailedStatusDrop + countSuccesfullStatusDrop, 2));//6
                    sr.WriteLine("Количество вылитых литров воды;" + report.literSuccesfullDrop);//7
                    sr.WriteLine("Количество не вылитых литров воды;" + report.literFailedDrop);//8
                    
                    sr.Close();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}