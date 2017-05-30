using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Bomberman_server;

namespace Bomberman_client.GameClasses
{
    public class GameCore
    {
        private Graphics graphicControl;
        private BufferedGraphicsContext currentContext = new BufferedGraphicsContext();
        public BufferedGraphics buffer1, buffer2, currBuffer;
        private Color buffColor = new Color();

        private Client client;
        public ObjectsLists objectsList;

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

        public Bitmap bombTexture;
        public Bitmap bombExplosionTexture;
        public Bitmap playerTexture;
        public Bitmap playerDieTexture;
        public Bitmap staticWallTexture;
        public Bitmap dynamicWallTexture;
        public Bitmap dynamicWallDestroyTexture;
        public List<Bitmap> explosionsTexture;

        public int id;

        public void CalcBuff()
        {

            foreach (Player player in objectsList.players)
            {
                if (!player.IsDead)
                {
                    lock (player)
                    {
                        if (!player.IsDying)
                        {
                            currBuffer.Graphics.DrawImage(player.GetAnimState(playerTexture), player.X, player.Y);
                        }
                        else
                        {
                            lock (playerDieTexture)
                            {

                                currBuffer.Graphics.DrawImage(playerDieTexture, player.X, player.Y, new Rectangle(new Point(player.currSpriteOffset, 0), player.size), GraphicsUnit.Pixel);
                            }
                        }
                    }
                }
            }
            lock (objectsList.bombs)
            {
                for (int i = 0; i < objectsList.bombs.Count; i++)
                {
                    lock (objectsList.bombs[i])
                    {
                        lock (bombTexture)
                        {
                            if (objectsList.bombs[i].isBlowedUp)
                            {
                                currBuffer.Graphics.DrawImage(bombExplosionTexture, objectsList.bombs[i].X, objectsList.bombs[i].Y, new Rectangle(new Point(objectsList.bombs[i].currSpriteOffset, 0), objectsList.bombs[i].size), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                currBuffer.Graphics.DrawImage(bombTexture, objectsList.bombs[i].X, objectsList.bombs[i].Y);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < objectsList.explosions.Count; i++)
            {
                lock (objectsList.explosions[i])
                {
                    objectsList.explosions[i].DrawExplosion(currBuffer, explosionsTexture);
                }
            }
            for (int i = 0; i < objectsList.dynamicWalls.Count; i++)
            {
                lock (objectsList.dynamicWalls[i])
                {
                    if (!objectsList.dynamicWalls[i].isBlowedUpNow)
                    {
                        lock (dynamicWallTexture)
                        {
                            currBuffer.Graphics.DrawImage(dynamicWallTexture, objectsList.dynamicWalls[i].X, objectsList.dynamicWalls[i].Y);
                        }
                    }
                    else
                    {
                        lock (dynamicWallDestroyTexture)
                        {
                            currBuffer.Graphics.DrawImage(dynamicWallDestroyTexture, objectsList.dynamicWalls[i].X, objectsList.dynamicWalls[i].Y, new Rectangle(new Point(objectsList.dynamicWalls[i].currSpriteOffset, 0), objectsList.dynamicWalls[i].size), GraphicsUnit.Pixel);
                        }
                    }
                }
            }
            for (int i = 0; i < objectsList.staticWalls.Count; i++)
            {
                lock (objectsList.staticWalls[i])
                {
                    lock (staticWallTexture)
                    {
                        currBuffer.Graphics.DrawImage(staticWallTexture, objectsList.staticWalls[i].X, objectsList.staticWalls[i].Y);
                    }
                }
            }
        }


        public void RedrawFunc()
        {
            lock (currBuffer)
            {
                currBuffer.Render();
                currBuffer.Graphics.Clear(buffColor);
            }
            ChangeBuffer();
            CalcBuff();
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

        private void LoadImages(string resDir)
        {
            this.bombTexture = new Bitmap(resDir + "Bomb\\bomb.png");
            this.bombExplosionTexture = new Bitmap(resDir + "Bomb\\BombExplosion.png");
            this.playerTexture = new Bitmap(resDir + "Player\\bomberman_new.png");
            this.playerDieTexture = new Bitmap(resDir + "Player\\bomberman_death.png");
            //public enum KindExplosionTexture { explosionTextureHorizontalMiddle, explosionTextureLeftEdge, explosionTextureRightEdge, explosionTextureVerticalMiddle, explosionTextureUpEdge, explosionTextureBottomEdge, explosionTextureCenter };

            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionHorizontalMiddle.png"));
            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionLeftEdge.png"));
            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionRightEdge.png"));
            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionVerticalMiddle.png"));
            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionUpEdge.png"));
            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionBottomEdge.png"));
            this.explosionsTexture.Add(new Bitmap(resDir + "Explosion\\ExplosionCenter.png"));
            this.staticWallTexture = new Bitmap(resDir + "Walls\\StaticWall.png");
            this.dynamicWallTexture = new Bitmap(resDir + "Walls\\DynamicWall.png");
            this.dynamicWallDestroyTexture = new Bitmap(resDir + "Walls\\DynamicWallDestroy.png");
        }
        public void startCore()
        {
            timer.Start();
        }
        public GameCore
            (
                int width, int height, Graphics graphicControl, string playerName, Client client, int id, string dirResources
            )
        {
            this.graphicControl = graphicControl;

            timer = new Timer();
            delay = 60;
            timer.Interval = delay;
            timer.Tick += TimerEvent;
            this.id = id;
            this.client = client;
            this.objectsList = new ObjectsLists();
            this.explosionsTexture = new List<Bitmap>();

            buffer1 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            buffer2 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            currBuffer = buffer1;
            buffColor = Color.ForestGreen;
            LoadImages(dirResources);
        }
    }
}
