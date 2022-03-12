using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitoyPrj.Tools
{
    public class RummyTile
    {
        public int id;
        public Colors color;
        public int value;

        public bool isOkey = false;
        public bool isFakeOkey = false;
        public bool isUsed = false;

        public RummyTile(int p_id, Colors p_color, int p_value)
        {
            id = p_id;
            color = p_color;
            value = p_value;
        }
    }
}
