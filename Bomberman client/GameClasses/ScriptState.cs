using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;

namespace Bomberman_client.GameClasses
{
    class ScriptState
    {
        private int countStates;
        private int currState;
        private PhysicalObject obj;
        private ScriptEngine.OnEndFunc onEnd;
        private Bitmap sprite;
        private Timer currTimer;
        private Bitmap temp;

        public void OnTimerEvent(object sender, EventArgs e)
        {
            currState--;
            if (currState == 0)
            {
                temp.Dispose();
                sprite.Dispose();
                onEnd(obj);
                currTimer.Enabled = false;
            }
            else
            {
                SetImage();
            }
        }
        private void SetImage()
        {
            temp = new Bitmap(sprite.Clone(new Rectangle(new Point(((countStates - 1) - (currState - 1)) * obj.size.Width, 0), obj.size), sprite.PixelFormat));
            obj.texture = temp;
        }
        public ScriptState(PhysicalObject obj, Image sprite, ScriptEngine.OnEndFunc onEndFunc, int countStates, int delay )
        {
            currTimer = new Timer();

            currTimer.Interval = delay;

            this.obj = obj;

            this.onEnd = onEndFunc;
            this.countStates = countStates;
            this.currState = this.countStates;
            this.sprite = new Bitmap (sprite as Bitmap);
            currTimer.Elapsed += OnTimerEvent;
            currTimer.Enabled = true;
        }
    }
}
