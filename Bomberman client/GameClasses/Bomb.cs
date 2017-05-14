using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bomberman_client.GameClasses;
using System.Drawing;
using System.Windows.Forms;

namespace Bomberman_client.GameClasses
{
    public class Bomb : PhysicalObject
    {
        private int areaOfExplosion;
        private Timer timerExplosion;
        public delegate void DeleteBomb(Bomb bomb);
        public DeleteBomb deleteBombFunc;

        public void TimerEvent(object sender, EventArgs e)
        {
            deleteBombFunc(this);            
        }

        public Bomb(Point location, Image texture, int areaOfExplosion, Map map, DeleteBomb deleteBombFunc)
            : base(location, texture)
        {
            timerExplosion = new Timer();
            timerExplosion.Interval = 3000;
            this.deleteBombFunc = deleteBombFunc;
            timerExplosion.Tick += TimerEvent;
            this.areaOfExplosion = areaOfExplosion;
            timerExplosion.Enabled = true;
        }
    }
}
