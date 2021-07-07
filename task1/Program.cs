using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            uint nb;
            Program program = new Program();
            try
            {
                nb = Convert.ToUInt32(args[0]);
                if (args.Length == 2)
                {
                        Console.WriteLine(program.itoBase(nb, args[1]));

                }
                else if (args.Length == 3)
                {
                    if (program.testNb(nb, args[1])) //проверяем корректность системы исчисления с заданным числом
                        Console.WriteLine(program.itoBase(nb, args[1], args[2]));
                    else
                    {
                        Console.WriteLine("Число не соответствует системе исчисления");
                        Console.WriteLine("Вариант1 - ожидаются аргументы: <входящее число> <система исчислений> ");
                        Console.WriteLine("Вариант2 - ожидаются аргументы: <входящее число> <система исчислений> <система исчислений>");
                    }
                }
                else
                {
                    Console.WriteLine("Введено не верное количество аргументов");
                    Console.WriteLine("Вариант1 - ожидаются аргументы: <входящее число> <система исчислений> ");
                    Console.WriteLine("Вариант2 - ожидаются аргументы: <входящее число> <система исчислений> <система исчислений>");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Вариант1 - ожидаются аргументы: <входящее число> <система исчислений> ");
                Console.WriteLine("Вариант2 - ожидаются аргументы: <входящее число> <система исчислений> <система исчислений>");
            }
        }
        
        public string itoBase ( uint nb, string baseDst) //Переводит, если грамотно расписана система исчисления.
        {
            string resulte = "";
            int len = baseDst.Length;
            char[] arr = baseDst.ToCharArray(); //переписываем в массив CHAR нашу строку
            uint x =  nb;
            
            while (x > 0)
            {
                resulte = arr[x % len] + resulte; //записываем значения из массива ориентируясь на индексацию символов.
                x = x / (uint)len;
            }
            return resulte; 
        }
        public string itoBase (uint nb, String baseSrc, String baseDst)
        {
            uint res = 0;
            int lenSrc = baseSrc.Length;
            char[] arrNb = (""+nb).ToCharArray(); //Переписываем входное число в массив символов;
            int j = arrNb.Length -1;
            for (int i = 0; i < arrNb.Length; i++)
            {
                res += (uint)Math.Pow(lenSrc, j)*(uint)(arrNb[i]-'0');
                j--;
            }
            return itoBase(res, baseDst); 
        }
        public bool testNb(uint nb, string baseDst) //через регулярные лучше было сделать но не было времени
        {
            char[] arrNb = Convert.ToString(nb).ToCharArray();
            foreach (char anb in arrNb)
            {
                if (!baseDst.Contains(anb))
                    return false;
            }
            return true;
        }
    }
}
