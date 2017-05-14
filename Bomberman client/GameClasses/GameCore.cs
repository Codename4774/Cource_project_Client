﻿using System;
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
        public Map map;
        public Player player;
        public List<Bomb> bombs;

        private Graphics graphicControl;
        private BufferedGraphicsContext currentContext = new BufferedGraphicsContext();
        private BufferedGraphics buffer1, buffer2, currBuffer;
        private Color buffColor = new Color();

        public int delay;
        private Timer timer;

        const char KEY_UP = (char)Keys.W;
        const char KEY_DOWN = (char)Keys.S;
        const char KEY_LEFT = (char)Keys.A;
        const char KEY_RIGHT = (char)Keys.D;
        const char KEY_PLACE_BOMB = (char)Keys.Space;
        private bool isUpPressed = false;
        private bool isDownPressed = false;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private int countPressed;

        private Image bombTexture;
        private Image playerSprite;
        private Image wallSprite;


        public void RedrawFunc()
        {
            currBuffer.Render();
            currBuffer.Graphics.Clear(buffColor);
            if (currBuffer == buffer1)
            {
                currBuffer = buffer2;
            }
            else
            {
                currBuffer = buffer1;
            }
            CalcBuff();
        }

        public void CalcBuff()
        {
            currBuffer.Graphics.DrawImage(player.GetAnimState(), player.X, player.Y);
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].ChangeMapMatrix(map);
                currBuffer.Graphics.DrawImage(bombs[i].texture, bombs[i].X, bombs[i].Y);
            }
        }

        public void TimerEvent(object sender, EventArgs e)
        {
            player.OnMove(map);
            RedrawFunc();
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].ChangeMapMatrix(map);
            }
        }
        public void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            switch ( Char.ToUpper(e.KeyChar) )
            {
                case KEY_UP:
                    {
                            player.isMoved = true;
                            player.prevDirection = player.direction;
                            player.direction = Player.Direction.UP;
                            isUpPressed = true;
                    }
                    break;
                case KEY_DOWN:
                    {

                            player.isMoved = true;
                            player.prevDirection = player.direction;
                            player.direction = Player.Direction.DOWN;
                            isDownPressed = true; 
                    }
                    break;
                case KEY_LEFT:
                    {

                            player.isMoved = true;
                            player.prevDirection = player.direction;
                            player.direction = Player.Direction.LEFT;
                            isLeftPressed = true;
                        
                    }
                    break;
                case KEY_RIGHT:
                    {

                            player.isMoved = true;
                            player.prevDirection = player.direction;
                            player.direction = Player.Direction.RIGHT;
                            isRightPressed = true;

                        
                    }
                    break;
                case KEY_PLACE_BOMB:
                    {
                        player.isMoved = true;
                        bombs.Add(player.bombFactory.GetBomb(player.bombLevel, new Point(player.X, player.Y)));
                    }
                    break;
            }
        }

        public void DeletePlayerFromField(Player player)
        {
            player.ClearPrevPlace(map);
            this.player = null;

        }

        public void DeleteBombFromField(Bomb bomb)
        {
            bomb.ClearPrevPlace(map);
            bombs.Remove(bomb);
        }

        void GetDirection()
        {
            if (isUpPressed)
            {
                player.direction = Player.Direction.UP;
            }
            else
                if (isDownPressed)
                {
                    player.direction = Player.Direction.DOWN;
                }
                else
                    if (isLeftPressed)
                    {
                        player.direction = Player.Direction.LEFT;
                    }
                    else
                        if (isRightPressed)
                        {
                            player.direction = Player.Direction.RIGHT;
                        }
                        else
                        {
                            player.isMoved = false;                      
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
        public GameCore(int width, int height, Graphics graphicControl, string playerName, Size playerSize, Size bombSize, Image playerSprite, Image bombSprite, Image wallImage)
        {
            this.map = new Map(width, height);
            this.graphicControl = graphicControl;
            bombs = new List<Bomb>();

            timer = new Timer();
            delay = 60;
            timer.Interval = delay;
            timer.Tick += TimerEvent;

            this.playerSprite = playerSprite;
            this.wallSprite = wallImage;

            buffer1 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            buffer2 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            currBuffer = buffer1;
            buffColor = Color.ForestGreen;

            this.player = new Player(new Point(20, 20), playerSprite, playerSize, playerName, bombSprite, bombSize, DeleteBombFromField, map);
        }
    }
}