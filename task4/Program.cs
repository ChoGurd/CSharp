using System;
using System.Text.RegularExpressions;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Ожидаемые аргументы: <word> <word>");
            }
            else 
            {
                Program program = new Program();
                //start version 1
                if (program.Like(args[0],args[1]))
                    Console.WriteLine("Ok");
                else
                    Console.WriteLine("KO");
                //end version 1
                //-------------------------------------------------------
                //start version 2
                //Console.WriteLine(program.Compare(args[0], args[1]));
                //end version 2
            }
        }
        public bool Like(string text,string liketext) //Version 1 Регулярные выражения
        {
            Regex reg = new Regex(liketext.Replace("*", ".*") + "$");
            return reg.IsMatch(text);

        }
        public string Compare(string arg0, string arg1) //Ver 2 Функция сравнения
        {
            char symb = '*';
            string str = DeleteRepeatingCharacter(arg1, symb);
            if (str.Replace("*","").Trim().Length > arg0.Length) //Если количество символов второго аргумента без учета пробелов и * больше количества символов первого аргумента то КО
                return "KO";
            string[] words = str.Split(symb); //Разделяем строку на слова/символы
            for (int i = 0; i < words.Length; i++)  //Цикл для обрезания первого аргумента по найденным соответствиям
            {
                Console.WriteLine("index i = {0} ; arg = {1}",i,arg0);
                if (i == 0)
                {
                    if (words[i] == "")
                        continue;
                    else if (arg0.StartsWith(words[i]))
                        arg0 = Cut(arg0,words[i]); //Обрезаем строку первого аргумента по первому найденному совпадению
                    else
                        return "KO";
                }
                else if (i != words.Length-1) //если не последний в 
                {
                    if (arg0.Contains(words[i]))
                        arg0 = Cut(arg0, words[i]);//Обрезаем строку первого аргумента по первому найденному совпадению
                    else
                        return "KO";
                }
                else 
                {
                    if (words[i] == "")
                        continue;
                    else if (arg0.EndsWith(words[i]))
                        arg0 = Cut(arg0, words[i]); //Обрезаем строку первого аргумента по первому найденному совпадению
                    else
                        return "KO";
                }
            }
            return "OK";
        }
        public string DeleteRepeatingCharacter(string str, char symb) //Ver 2 Удляем повторяющиеся символы * (только мешают)
        {
            char[] arr = str.ToCharArray();
            string resulte = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != arr.Length - 1)//Если еще есть символы в массиве
                {
                    if (arr[i] == symb) //сравниваем на соответсвие с символом, который исключаем из повторений
                    {
                        if (arr[i] != arr[i + 1])
                            resulte += arr[i];
                    }
                    else
                    {
                        resulte += arr[i];
                    }
                }
                else
                {
                    resulte += arr[i];
                }
            }
            return resulte;
        }
        public string Cut(string text, string liketext)//Ver 2
        {
            return text.Substring(text.IndexOf(liketext) + liketext.Length);
        }


    }
}
