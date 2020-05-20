using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_2_Lib;
using Serialization_Library;
using System.IO;

namespace CW_2
{
    class Program
    {
        static Random rnd = new Random();
        static string path_in = $"data.txt";
        static string path_out = $"out.ser";
        static void Main(string[] args)
        {
            int N = GetNumber<int>("Введите количество улиц", x => x > 0);
            List<Street> streets = new List<Street>();
            if (CheckStreetFile(path_in))
            {
                string[] streetsData = File.ReadAllLines(path_in, Encoding.GetEncoding("Windows-1251"));
                foreach (var street in streetsData)
                {
                    string[] str = street.Split(new char[] { ' ' });
                    int number = -1;
                    var nums = (from t in str
                                where int.TryParse(t, out number)
                                select number).ToList();
                    try
                    {
                        streets.Add(new Street(str[0], nums.ToArray()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }
            }
            else
                FillStreetData(streets, N);
            while (N < streets.Count)
            {
                streets.RemoveAt(streets.Count-1);
            }
            foreach (var street in streets)
                Console.WriteLine(street);
            Serializations.XMLSerialize(streets, path_out, new Type[] { typeof(List<Street>) });
        }


        private static void FillStreetData(List<Street> streets, int n)
        {
            for (int i = 0; i < n; i++)
                streets.Add(new Street(GenerateName(), GenerateIntArr()));
        }
        /// <summary>
        /// Метод генерирует имя.
        /// </summary>
        /// <returns></returns>
        static string GenerateName()
        {
            int len = rnd.Next(3, 8);
            var sb = new StringBuilder();
            sb.Append((char)rnd.Next('A', 'Z' + 1));
            for (int i = 0; i < len; i++)
                sb.Append((char)rnd.Next('a', 'z' + 1));

            return sb.ToString();
        }
        /// <summary>
        /// Метод генерирует массив интов.
        /// </summary>
        /// <returns></returns>
        static int[] GenerateIntArr()
        {
            var nums = new List<int>();
            int len = rnd.Next(1, 11);

            for (int i = 0; i < len; i++)
                nums.Add(rnd.Next(1, 101));

            return nums.ToArray();
        }

        /// <summary>
        /// Метод возвращает введенное 
        /// число если оно удовлетворяет условиям.
        /// </summary>
        /// <returns></returns>
        public static T GetNumber<T>(string str, Predicate<T> conditions)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(str);
            do
            {
                try
                {
                    var result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    if (conditions(result))
                        return result;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенная строка не является командой");
                }
                catch (InvalidCastException icex)
                {
                    Console.WriteLine(icex.Message);
                }
                catch (OverflowException oex)
                {
                    Console.WriteLine(oex.Message);
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
                catch (ArgumentNullException anex)
                {
                    Console.WriteLine(anex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ResetColor();
            } while (true);
        }
        static bool CheckStreetFile(string path)
        {
            bool checker = true;
            string[] streetsData = File.ReadAllLines(path);
            foreach (var street in streetsData)
            {
                string[] str = street.Split(new char[] { ' ' });
                if (str.Length < 2)
                    return false;
                int temp = 0;
                var nums = (from t in str
                            where int.TryParse(t, out temp)
                            select t).ToList();

                if (nums.Count != str.Length - 1)
                {
                    checker = false;
                    break;
                }
            }

            return checker;
        }
    }
}
