using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitoyPrj.Tools
{
    public class Player
    {
        public int id;
        public string name;
        public int rank;
        public List<RummyTile> playerTiles;

        public Player(int p_id, string p_name)
        {
            id = p_id;
            name = p_name;
            playerTiles = new List<RummyTile>();
        }

       
       
    }
}
