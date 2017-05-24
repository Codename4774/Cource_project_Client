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
        public PhysicalMap map;
        public Player player;
        public List<Bomb> bombs;
        public List<Explosion> explosions;
        public List<PhysicalObject> staticWalls;
        public List<DynamicWall> dynamicWalls;

        public List<Player> otherPlayers;

        private Graphics graphicControl;
        private BufferedGraphicsContext currentContext = new BufferedGraphicsContext();
        private BufferedGraphics buffer1, buffer2, currBuffer;
        private Color buffColor = new Color();

        public int delay;
        private Timer timer;

        private ScriptEngine scriptEngine;

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


        private Size playerSize;
        private Size playerOnDeathSize;
        private Size bombSize;
        private Size explosionSize;
        private Size wallSize;

        public Image bombTexture;
        public Image bombExplosionTexture;
        public Image playerTexture;
        public Image playerDieTexture;
        public Image staticWallTexture;
        public Image dynamicWallTexture;
        public Image dynamicWallDestroyTexture;
        public Image explosionCenterTexture;
        public Image explosionLeftEdgeTexture;
        public Image explosionRightEdgeTexture;
        public Image explosionUpEdgeTexture;
        public Image explosionBottomEdgeTexture;
        public Image explosionVerticalTexture;
        public Image explosionHorizontalTexture;

        public readonly int id;

        public void RedrawFunc()
        {
            currBuffer.Render();
            currBuffer.Graphics.Clear(buffColor);
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
        public void CalcBuff()
        {
            if (!player.IsDead)
            {
                lock (player)
                {
                    if (!player.IsDying)
                    {
                        currBuffer.Graphics.DrawImage(player.GetAnimState(), player.X, player.Y);
                    }
                    else
                    {
                        currBuffer.Graphics.DrawImage(player.texture, player.X, player.Y);
                    }
                }
            }
            lock (bombs)
            {
                for (int i = 0; i < bombs.Count; i++)
                {
                    lock (bombs[i])
                    {
                        currBuffer.Graphics.DrawImage(bombs[i].texture, bombs[i].X, bombs[i].Y);
                    }
                }
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                lock (explosions[i])
                {
                    explosions[i].DrawExplosion(currBuffer);
                }
            }
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                lock (dynamicWalls[i])
                {
                    currBuffer.Graphics.DrawImage(dynamicWalls[i].texture, dynamicWalls[i].X, dynamicWalls[i].Y);
                }
            }
            for (int i = 0; i < staticWalls.Count; i++)
            {
                lock (staticWalls[i])
                {
                    currBuffer.Graphics.DrawImage(staticWalls[i].texture, staticWalls[i].X, staticWalls[i].Y);
                }
            }

        }

        public void ChangePhysicalState()
        {
            if (!player.IsDead)
            {
                player.ChangeMapMatrix(map);
            }
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].ChangeMapMatrix(map);
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].ChangePhysicalMap(map);
            }
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                if ((dynamicWalls[i].isWallBlowedUp(map)) && (!dynamicWalls[i].isBlowedUpNow))
                {
                    dynamicWalls[i].isBlowedUpNow = true;
                    StartDestroingDynamicWall(dynamicWalls[i]);
                }
                dynamicWalls[i].ChangeMapMatrix(map);
            }
            for (int i = 0; i < staticWalls.Count; i++)
            {
                staticWalls[i].ChangeMapMatrix(map);
            }
            if (player.isPlayerBlowedUp(map))
            {
                onDeathPlayer(player);
            }
        }

        public void TimerEvent(object sender, EventArgs e)
        {
            player.OnMove(map);
            RedrawFunc();
            map.ClearCurrMatrix();
            map.SwitchMatrix();
            ChangePhysicalState();
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
                        if (player.CurrCountBombs != player.maxCountBombs)
                        {
                            bombs.Add(player.bombFactory.GetBomb(player.bombLevel, new Point(player.X, player.Y)));
                            player.CurrCountBombs++;
                        }
                    }
                    break;
                case KEY_SPAWN:
                    {
                        if (player.IsDead)
                        {
                            player.X = 20;
                            player.Y = 20;
                            player.texture = playerTexture;
                            player.IsDying = false;
                            player.IsDead = false;
                        }
                    }
                    break;
            }
        }

        public void onDeathPlayer(PhysicalObject player)
        {
            var temp = player as Player;

            lock (player.texture)
            {
                scriptEngine.StartSimpleScript(temp, playerDieTexture, playerOnDeathSize, DeletePlayerFromField, 200, 6);
            }
            temp.IsDying = true;
            temp.isMoved = false;
        }
        public void DeletePlayerFromField(object player)
        {
            var temp = player as Player;

            temp.IsDead = true;
        }


        public void ExplosionBomb(PhysicalObject bomb)
        {
            var temp = bomb as Bomb;

            scriptEngine.StartSimpleScript(temp, bombExplosionTexture, DeleteBombFromField, 100, 3);
        }


        public void DeleteBombFromField(object bomb)
        {
            var temp = bomb as Bomb;

            if (bombs.IndexOf(temp) >= 0)
            {
                temp.owner.CurrCountBombs--;
                bombs.Remove(temp);

                Explosion tempExpl = new Explosion(explosionCenterTexture, explosionUpEdgeTexture, explosionBottomEdgeTexture, explosionLeftEdgeTexture,
                   explosionRightEdgeTexture, explosionVerticalTexture, explosionHorizontalTexture, explosionSize,
                   new Point(temp.X, temp.Y), (int)player.bombLevel, map, DeleteExplosionFromField);

                explosions.Add(tempExpl);
                scriptEngine.StartExplosion(tempExpl, DeleteExplosionFromField, 100, 7);

            }
        }

        public void DeleteExplosionFromField(object explosion)
        {
            var temp = explosion as Explosion;

            explosions.Remove(temp);
        }

        public void StartDestroingDynamicWall(DynamicWall wall)
        {
            scriptEngine.StartSimpleScript(wall, dynamicWallDestroyTexture, DeleteDynamicWallFromField, 200, 6);
        }

        public void DeleteDynamicWallFromField(object wall)
        {
            var temp = wall as DynamicWall;

            if (dynamicWalls.IndexOf(temp) >= 0)
            {
                dynamicWalls.Remove(temp);
            }
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

        private void spawnWalls()
        {
            staticWalls.Add(new PhysicalObject(new Point(50, 50), staticWallTexture, wallSize));
            dynamicWalls.Add(new DynamicWall(new Point(100, 100), dynamicWallTexture, wallSize));
            dynamicWalls.Add(new DynamicWall(new Point(124, 100), dynamicWallTexture, wallSize));
            dynamicWalls.Add(new DynamicWall(new Point(148, 100), dynamicWallTexture, wallSize));
        }

        private void LoadImages(string resDir)
        {
            this.bombTexture = new Bitmap(resDir + "Bomb\\bomb.png");
            this.bombExplosionTexture = new Bitmap(resDir + "Bomb\\BombExplosion.png");
            this.playerTexture = new Bitmap(resDir + "Player\\bomberman_new.png");
            this.playerDieTexture = new Bitmap(resDir + "Player\\bomberman_death.png");
            this.explosionCenterTexture = new Bitmap(resDir + "Explosion\\ExplosionCenter.png");
            this.explosionUpEdgeTexture= new Bitmap(resDir + "Explosion\\ExplosionUpEdge.png");
            this.explosionBottomEdgeTexture = new Bitmap(resDir + "Explosion\\ExplosionBottomEdge.png");
            this.explosionLeftEdgeTexture = new Bitmap(resDir + "Explosion\\ExplosionLeftEdge.png");
            this.explosionRightEdgeTexture = new Bitmap(resDir + "Explosion\\ExplosionRightEdge.png");
            this.explosionVerticalTexture = new Bitmap(resDir + "Explosion\\ExplosionVerticalMiddle.png");
            this.explosionHorizontalTexture = new Bitmap(resDir + "Explosion\\ExplosionHorizontalMiddle.png");
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
                int width, int height, Graphics graphicControl, string playerName, Size playerSize, Size playerOnDeathSize,
                Size bombSize, Size explosionSize, Size wallSize, string dirResources, int id
            )
        {
            this.map = new PhysicalMap(width, height);
            this.graphicControl = graphicControl;
            bombs = new List<Bomb>();
            explosions = new List<Explosion>();
            staticWalls = new List<PhysicalObject>();
            dynamicWalls = new List<DynamicWall>();
            otherPlayers = new List<Player>();

            timer = new Timer();
            delay = 60;
            timer.Interval = delay;
            timer.Tick += TimerEvent;
            this.id = id;

            buffer1 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            buffer2 = currentContext.Allocate(graphicControl, new Rectangle(0, 0, width, height));
            currBuffer = buffer1;
            buffColor = Color.ForestGreen;

            scriptEngine = new ScriptEngine();

            this.playerSize = playerSize;
            this.bombSize = bombSize;
            this.explosionSize = explosionSize;
            this.wallSize = wallSize;
            this.playerOnDeathSize = playerOnDeathSize;

            LoadImages(dirResources);
            this.player = new Player(new Point(20, 20), playerTexture, playerSize, playerName, DeletePlayerFromField, bombTexture, bombSize, ExplosionBomb, 0);
            spawnWalls();
        }
    }
}
