using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    public class BombFactory
    {
        private Image bombSprite;
        private Size bombSize;
        public Bomb.DeleteBomb deleteBombFunc;
        public Map map;
        public Bomb GetBomb(Player.BombLevel bombLevel, Point location)
        {
            return new Bomb(location, bombSprite, (int)bombLevel, map, deleteBombFunc );
        }
        public BombFactory(Map map, Image bombSprite, Size bombSize, Bomb.DeleteBomb deleteFunc)
        {
            this.map = map;
            this.bombSprite = bombSprite;
            this.bombSize = bombSize;
            deleteBombFunc = deleteFunc;
        }
    }
}
