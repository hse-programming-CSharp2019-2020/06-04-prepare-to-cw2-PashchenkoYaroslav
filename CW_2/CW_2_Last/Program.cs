using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serialization_Library;
using System.IO;
using CW_2_Lib;

namespace CW_2_Last
{
    class Program
    {
        static char sep = Path.DirectorySeparatorChar;
        static string path = $"..{sep}..{sep}..{sep}CW_2{sep}bin{sep}debug{sep}out.ser";
        static void Main(string[] args)
        {
            List<Street> streets=null;
           try
            {
                streets = Serializations.XMLDeserialize<List<Street>>(path, new Type[] { typeof(List<Street>) });
            }
            catch(IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var magicHouses = (from str in streets
                            where -str&&~str%2!=0
                            select str).ToArray();
            if (magicHouses.Length == 0)
            {
                Console.WriteLine("Волшебных домов не обнаружено");
            }
            else
            {
                foreach (var str in magicHouses)
                    Console.WriteLine(str);
            }
        }
    }
}
