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

        public void TimerEvent(object sender, EventArgs e)
        {
            deleteObjectFunc(this);            
        }

        public Bomb(Point location, Image texture, int areaOfExplosion, DeleteObjectFunc deleteBombFunc)
            : base(location, texture)
        {
            timerExplosion = new Timer();
            timerExplosion.Interval = 2000;
            this.deleteObjectFunc = deleteBombFunc;
            timerExplosion.Tick += TimerEvent;
            this.areaOfExplosion = areaOfExplosion;
            timerExplosion.Enabled = true;
        }
    }
}
