/*
Некоторое количество человек то наливают воду в бочку, то черпают из бочки. Если человек
пытается налить больше воды, чем есть свободного объема – это ошибка, при этом объем воды в
бочке не меняется. Так же если человек пытается зачерпнуть больше воды, чем есть в бочке –
ошибка, объем воды также при этом не меняется. В остальных случаях – успех.
Вам дан лог файл. Напишите программу, которая ответит на следующие вопросы:
- какое количество попыток налить воду в бочку было за указанный период?
- какой процент ошибок был допущен за указанный период?
- какой объем воды был налит в бочку за указанный период?
- какой объем воды был не налит в бочку за указанный период?
- … тоже самое для забора воды из бочки …
- какой объем воды был в бочке в начале указанного периода? Какой в конце указанного
периода?
Путь к логу, желаемый период – подаются в качестве аргументов командной строки. Результат
записывается в csv файл (с наименованием столбцов).
Пример строки запуска: java –jar App ./log.log 2020-01-01T12:00:00 2020-01-01T13:00:00
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace task3
{
    class Program 
    {
        static void Main(string[] args)
        {
            string path;
            Report report = new Report();
            Program program = new Program();
            if (args.Length == 3)
            {
                try
                {
                    path = args[0]; //Присваиваем путь из 1го аргумента
                    report.dateStart = DateTime.Parse(args[1].Replace("Z", "").Replace("T", " ")); //Дату из 2го аргумента
                    report.dateEnd = DateTime.Parse(args[2].Replace("Z", "").Replace("T", " ")); //Дату из 3го аргумента

                    if (!File.Exists(path))
                    {
                        try
                        {
                            program.GenerateLogFile(path);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    using (StreamReader sr = new StreamReader(path, Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("username"))//понимаем что это строка с логом
                            {
                                try
                                {
                                    line = line.Replace("top up", "topup"); //для удобства
                                    string[] str = line.Split(' ');
                                    DateTime parsedDate = DateTime.Parse(str[0].Replace("Z", "").Replace("T", " "));//Получаем дату из строки лога
                                    if ((report.dateStart <= parsedDate) && (parsedDate <= report.dateEnd)) //Сравниваем дату
                                    {
                                        int liter = Convert.ToInt32(str[6].Replace("l", "").Replace("L", ""));
                                        if (line.Contains("scoop"))
                                        {
                                            if (line.Contains("успех"))
                                            {
                                                report.countSuccesfullStatusDrop++;
                                                report.literSuccesfullDrop += liter;
                                            }
                                            else
                                            {
                                                report.countFailedStatusDrop++;
                                                report.literFailedDrop += liter;
                                            }
                                        }
                                        else
                                        {
                                            if (line.Contains("успех"))
                                            {
                                                report.countSuccesfullStatusTopUp++;
                                                report.literSuccesfullTopUp += liter;
                                            }

                                            else
                                            {
                                                report.countFailedStatusTopUp++;
                                                report.literFailedTopUp += liter;
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Не удалось прочитать лог");
                                    Console.WriteLine(ex.Message);
                                    break;
                                }
                            }
                        }

                        Console.WriteLine();
                        if (!report.SaveInCSV(report))
                            Console.WriteLine("Error - CSV not saved");
                    }
                }
                catch
                {
                    Console.WriteLine("не верно задан формат аргументов: path date date");
                }
            }
            else
            {
                Console.WriteLine("Не верное количество аргументов, ожидается: path date date");
            }
        }


        public void GenerateLogFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path));
            Barrel barrel = new Barrel();
            var random = new Random();
            string value;
            int randomMaxLiter = random.Next(20, 201);
            barrel.MaxLiter = randomMaxLiter;
            barrel.CurLiter = random.Next(0, randomMaxLiter + 1); ;
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default)) //Head log:
            {
                sw.WriteLine("META DATA:");
                sw.WriteLine("{0} (объем бочки)",barrel.MaxLiter);
                sw.WriteLine("{0} (текущий объем воды в бочке)", barrel.CurLiter);
            }
            for (int i = 0; i <= 18000; i++)
            {
                int randomUserName = random.Next(1, 11);//от 1 до 10 номера юзеров
                int randomValue = random.Next(1, randomMaxLiter / 2);//литраж изменения
                if (random.Next(0, 2) == 0) // возвращает 1 из 2 значений <0> или <1>
                {
                    if (barrel.TopUp(randomValue))
                    {
                        value = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "Z - [username" + randomUserName + "] - wanna top up " + randomValue + "L (успех)";
                    }
                    else
                    {
                        value = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "Z - [username" + randomUserName + "] - wanna top up " + randomValue + "L (фейл)";
                    }
                }
                else
                {
                    if (barrel.Drop(randomValue))
                    {
                        value = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "Z - [username" + randomUserName + "] - wanna scoop " + randomValue + "L (успех)"; 
                    }
                    else
                    {
                        value = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "Z - [username" + randomUserName + "] - wanna scoop " + randomValue + "L (фейл)"; 
                    }
                }
                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                {
                    sw.WriteLine(value);
                }
            
            }

        }
        public void RandomDateGenerate()
        {
            DateTime dateTime = DateTime.MinValue;
        }
    }
}
