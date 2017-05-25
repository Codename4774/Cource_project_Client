using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    public class GameCore
    {
        private Graphics graphicControl;
        private BufferedGraphicsContext currentContext = new BufferedGraphicsContext();
        public BufferedGraphics buffer1, buffer2, currBuffer;
        private Color buffColor = new Color();

        private Client client;

        public int delay;
        private Timer timer;

        const char KEY_UP = (char)Keys.W;
        const char KEY_DOWN = (char)Keys.S;
        const char KEY_LEFT = (char)Keys.A;
        const char KEY_RIGHT = (char)Keys.D;
        const char KEY_PLACE_BOMB = (char)Keys.Space;
        const char KEY_SPAWN = (char)Keys.Enter;
        private bool isUpPressed = false;
        private bool isDownPressed = false;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;

        public int id;

        public void RedrawFunc()
        {
            lock (currBuffer)
            {
                currBuffer.Render();
                currBuffer.Graphics.Clear(buffColor);
            }
            ChangeBuffer();
            
        }
        public void ChangeBuffer()
        {
            if (currBuffer == buffer1)
            {
                currBuffer = buffer2;
            }
            else
            {
                currBuffer = buffer1;
            }
        }

        public void TimerEvent(object sender, EventArgs e)
        {
            RedrawFunc();
        }
        public void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            switch ( Char.ToUpper(e.KeyChar) )
            {
                case KEY_UP:
                    {
                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.UP);
                        isUpPressed = true;
                    }
                    break;
                case KEY_DOWN:
                    {

                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.DOWN);
                        isDownPressed = true; 
                    }
                    break;
                case KEY_LEFT:
                    {

                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.LEFT);

                        isLeftPressed = true;
                        
                    }
                    break;
                case KEY_RIGHT:
                    {

                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.RIGHT);

                        isRightPressed = true;


                    }
                    break;
                case KEY_PLACE_BOMB:
                    {
                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.PlaceBomb);
                    }
                    break;
                case KEY_SPAWN:
                    {
                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.Spawn);
                    }
                    break;
            }
        }

        void GetDirection()
        {
            if (isUpPressed)
            {
                client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.UP);
            }
            else
                if (isDownPressed)
                {
                    client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.DOWN);
                }
                else
                    if (isLeftPressed)
                    {
                        client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.LEFT);
                    }
                    else
                        if (isRightPressed)
                        {
                            client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.NewDirection, (int)KindMessages.Direction.RIGHT);

                        }
                        else
                        {
                            client.SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.StopWalking);
                        }
        }
        public void KeyUpEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case KEY_UP:
                    {
                        isUpPressed = false;
                        GetDirection();
                    }
                    break;
                case KEY_DOWN:
                    {
                        isDownPressed = false;
                        GetDirection();
                    }
                    break;
                case KEY_LEFT:
                    {
                        isLeftPressed = false;
                        GetDirection();
                    }
                    break;
                case KEY_RIGHT:
                    {
                        isRightPressed = false;
                        GetDirection();     
                    }
                    break;
            }
            
        }

        public void startCore()
        {
            timer.Start();
        }
        public GameCore
            (
                int width, int height, Graphics graphicControl, string playerName, Client client, int id
            )
        {
            this.graphicControl = graphicControl;

            timer = new Timer();
            delay = 60;
            timer.Interval = delay;
            timer.Tick += TimerEvent;
            this.id = id;
            this.client = client;

            buffer1 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            buffer2 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            currBuffer = buffer1;
            buffColor = Color.ForestGreen;
        }
    }
}
