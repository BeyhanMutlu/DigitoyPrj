using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitoyPrj.Tools
{
    public enum Colors { Yellow,Blue,Black,Red,FakeOkey }

    public class HelperFunctions
    {
        public List<Player> PrepareGame(int RummyTilesSet,List<Player> players)
        {
            List<RummyTile> rummyTiles = GenerateRummyTiles(RummyTilesSet);
            rummyTiles = ShuffleRummyTiles(rummyTiles);
            DistributeRummyTiles(players,rummyTiles);
            GenerateOkeyFakeOkey(players,rummyTiles);
            ClosestToWin(players);
            return players.OrderByDescending(i => i.rank).ToList();
        }

        private List<RummyTile> GenerateRummyTiles(int RummyTilesSet)
        {
            List<RummyTile> rummyTiles = new List<RummyTile>();
            int countId = 0;
            int setCount = 0;

            while (setCount < RummyTilesSet)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 14; j++)
                    {
                        rummyTiles.Add(new RummyTile(countId = countId + 1, (Colors)i, j));
                    }
                }
                rummyTiles.Add(new RummyTile(countId = countId + 1, Colors.FakeOkey, -1));

                setCount++;
            }
            return rummyTiles;
        }

        private List<RummyTile> ShuffleRummyTiles(List<RummyTile> rummyTiles)
        {
            Random rnd = new Random();
            rummyTiles =  rummyTiles.OrderBy(a => rnd.Next()).ToList();           
            return rummyTiles;
        }

        private void DistributeRummyTiles(List<Player> players, List<RummyTile> rummyTiles)
        {
            int distributedCount = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    players[i].playerTiles.Add(rummyTiles[distributedCount]);
                    rummyTiles[distributedCount].isUsed = true;
                    distributedCount++;
                }
            }
            Random rnd = new Random();
            players[rnd.Next(0, 3)].playerTiles.Add(rummyTiles[distributedCount]);
            rummyTiles[distributedCount].isUsed = true;
        }

        private void GenerateOkeyFakeOkey(List<Player> players, List<RummyTile> rummyTiles)
        {
            Random rnd = new Random();
            var rummyTilesWithoutFake = rummyTiles.Where(i => i.color != Colors.FakeOkey).ToList();
            var openedRummyTile = rummyTilesWithoutFake[rnd.Next(0, rummyTilesWithoutFake.Count)];
            var okeys = new List<RummyTile>();
            if (openedRummyTile.value == 13)
            {
                okeys = rummyTiles.Where(i => i.value == 1 && i.color == openedRummyTile.color).ToList();
                foreach (var item in okeys)
                {
                    item.isOkey = true;
                }
            }
            else
            {
                okeys = rummyTiles.Where(i => i.value == openedRummyTile.value && i.color == openedRummyTile.color).ToList();
                foreach (var item in okeys)
                {
                    item.isOkey = true;
                }
            }

            var fakeOkeys = rummyTiles.Where(i => i.value == -1).ToList();
            var okeyTiles = rummyTiles.Where(i => i.isOkey == true).First();
            foreach (var item in fakeOkeys)
            {
                item.color = okeyTiles.color;
                item.value = okeyTiles.value;
                item.isFakeOkey = true;
            }

            foreach (var player in players)
            {
                if (player.playerTiles.Where(i => i.id == okeys[0].id || i.id == okeys[1].id).Count() > 0)
                    player.playerTiles.Where(i => i.id == okeys[0].id || i.id == okeys[1].id).First().isOkey = true;

                var playerFakeOkeys = player.playerTiles.Where(i => i.value == -1).ToList();
                if (playerFakeOkeys.Count() > 0)
                {                    
                    foreach (var item in playerFakeOkeys)
                    {
                        item.color = okeyTiles.color;
                        item.value = okeyTiles.value;
                        item.isFakeOkey = true;
                    }
                }
                  
            }
        }

        public void ClosestToWin(List<Player> players)
        {
            foreach (var player in players)
            {
                int okeyCount = 0;
                foreach (var item in player.playerTiles)
                {
                    if (item.isOkey)
                    {
                        okeyCount++;
                    }
                }
                player.rank += okeyCount * 130;

                List<RummyTile> orderingNumber = player.playerTiles.OrderBy(i=>i.value).ToList();
                for (int i = 0; i < orderingNumber.Count(); i++)
                {
                    for (int j = 0; j < orderingNumber.Count(); j++)
                    {
                        if (orderingNumber[i].color == orderingNumber[j].color && orderingNumber[i].value == (orderingNumber[j].value+1))
                        {
                            player.rank += 15;
                        }
                        else if(orderingNumber[i].color == orderingNumber[j].color && orderingNumber[i].value == 13)
                        {
                            if (orderingNumber[j].value == 1)
                            {
                                player.rank += 15;
                            }
                        }
                    }
                   
                }
                var orderingColor = player.playerTiles.OrderBy(i => i.color).ToList();
                for (int i = 0; i < orderingColor.Count(); i++)
                {
                    for (int j = 0; j < orderingColor.Count(); j++)
                    {
                        if (orderingColor[i].color != orderingColor[j].color && orderingColor[i].value == orderingColor[j].value)
                        {
                            player.rank += 15;
                        }                       
                    }

                }
            }
        }

    }
}
