using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_2_Lib
{
    public class Street
    {
        string _name;
        int[] _houses;

        public Street(string name, int[] houses)
        {
            this.name = name;
            this.houses = houses;
        }

        public Street()
        {
        }

        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        public int[] houses { get => _houses; set => _houses = value; }
        public static int operator ~(Street street)
        {
            return street.houses.Length;
        }
        public static bool operator -(Street street)
        {
            return (from h in street._houses
                    select h).Any(h => h.ToString().Contains("7"));
        }

        public override string ToString()
        {
            return $"{_name}\n {string.Join(" ", _houses)}";
        }
    }
}
