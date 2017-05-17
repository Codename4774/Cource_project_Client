using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    public class Explosion
    {
        private int radius;
        public List<PartExplosion> partsExplosionLeft;
        public List<PartExplosion> partsExplosionRight;
        public List<PartExplosion> partsExplosionBottom;
        public List<PartExplosion> partsExplosionUp;
        public PartExplosion partExplosionCenter;
        private Point explosionLocation;
        private int countParts;
        public delegate void OnEndAllExplosionFunc(object sender);
        private OnEndAllExplosionFunc onEndAllExplosionFunc;
        Size size;
        public void ChangeState()
        {
            for (int i = 0; i < partsExplosionBottom.Count; i++)
            {
                partsExplosionBottom[i].ChangeState();
            }
            for (int i = 0; i < partsExplosionUp.Count; i++)
            {
                partsExplosionUp[i].ChangeState();
            }
            for (int i = 0; i < partsExplosionLeft.Count; i++)
            {
                partsExplosionLeft[i].ChangeState();
            }
            for (int i = 0; i < partsExplosionRight.Count; i++)
            {
                partsExplosionRight[i].ChangeState();
            }
            partExplosionCenter.ChangeState();
        }
        public void onEndExplosionFunc(object sender)
        {
            countParts--;
            if (countParts == 0)
            {
                onEndAllExplosionFunc(this);
            }
        }
        private void InitExplosion
            (
                Image explosionTextureCenter, Image explosionTextureUpEdge,
                Image explosionTextureBottomEdge, Image explosionTextureLeftEdge,
                 Image explosionTextureRightEdge, Image explosionTextureVerticalMiddle,
                Image explosionTextureHorizontalMiddle, Size size, Point location, int radius,
                PhysicalMap map
            )
        {
            const int countStates = 7;
            partsExplosionBottom = new List<PartExplosion>();
            partsExplosionLeft = new List<PartExplosion>();
            partsExplosionRight = new List<PartExplosion>();
            partsExplosionUp = new List<PartExplosion>();
            countParts = 0;
            for (int i = 0; i < radius; i++)
            {
                if ((location.X - size.Width * (i + 1)) > 0)
                {
                    if (i < radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureHorizontalMiddle, size, new Point((location.X - size.Width * (i + 1)), location.Y), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureLeftEdge, size, new Point((location.X - size.Width * (i + 1)), location.Y), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                }
                else
                {
                    if (i < radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureHorizontalMiddle, size, new Point(0, location.Y), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureLeftEdge, size, new Point(0, location.Y), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                    break;
                }
            }
            for (int i = 0; i < radius; i++)
            {
                if ((location.X + size.Width * (i + 1) ) < map.Width)
                {
                    if (i < radius - 1)
                    {
                        partsExplosionRight.Add(new PartExplosion(explosionTextureHorizontalMiddle, size, new Point((location.X + size.Width * (i + 1)), location.Y), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionRight.Add(new PartExplosion(explosionTextureRightEdge, size, new Point((location.X + size.Width * (i + 1)), location.Y), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                }
                else
                {
                    if (i < radius - 1)
                    {
                        partsExplosionRight.Add(new PartExplosion(explosionTextureHorizontalMiddle, size, new Point((map.Width - size.Width), location.Y), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionRight.Add(new PartExplosion(explosionTextureRightEdge, size, new Point(map.Width - size.Width, location.Y), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                    break;
                }
            }
            for (int i = 0; i < radius; i++)
            {
                if ((location.Y - size.Height * (i + 1)) > 0)
                {
                    if (i < radius - 1)
                    {
                        partsExplosionUp.Add(new PartExplosion(explosionTextureVerticalMiddle, size, new Point(location.X, location.Y - size.Height * (i + 1)), countStates, onEndExplosionFunc));
                                            
                    }
                    else
                    {
                        partsExplosionUp.Add(new PartExplosion(explosionTextureUpEdge, size, new Point(location.X, location.Y - size.Height * (i + 1)), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                }
                else
                {
                    if (i < radius - 1)
                    {
                        partsExplosionUp.Add(new PartExplosion(explosionTextureVerticalMiddle, size, new Point(location.X, 0), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionUp.Add(new PartExplosion(explosionTextureUpEdge, size, new Point(location.X, 0), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                    break;
                }
            }
            for (int i = 0; i < radius; i++)
            {
                if ((location.Y + size.Height * (i + 1)) < map.Height)
                {
                    if (i < radius - 1)
                    {
                        partsExplosionBottom.Add(new PartExplosion(explosionTextureVerticalMiddle, size, new Point(location.X, location.Y + size.Height * (i + 1)), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionBottom.Add(new PartExplosion(explosionTextureBottomEdge, size, new Point(location.X, location.Y + size.Height * (i + 1)), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                }
                else
                {
                    if (i < radius - 1)
                    {
                        partsExplosionBottom.Add(new PartExplosion(explosionTextureVerticalMiddle, size, new Point(location.X, map.Height - size.Height), countStates, onEndExplosionFunc));
                    }
                    else
                    {
                        partsExplosionBottom.Add(new PartExplosion(explosionTextureBottomEdge, size, new Point(location.X, map.Height - size.Height), countStates, onEndExplosionFunc));
                    }
                    countParts++;
                    break;
                }
            }

            partExplosionCenter = new PartExplosion(explosionTextureCenter, size, location, countStates, onEndExplosionFunc);
            countParts++;
        }
        public void ChangePhysicalMap(PhysicalMap map)
        {
            for (int i = 0; i < partsExplosionBottom.Count; i++)
            {
                partsExplosionBottom[i].ChangePhysicalMap(map);
            }
            for (int i = 0; i < partsExplosionUp.Count; i++)
            {
                partsExplosionUp[i].ChangePhysicalMap(map);
            }
            for (int i = 0; i < partsExplosionLeft.Count; i++)
            {
                partsExplosionLeft[i].ChangePhysicalMap(map);
            }
            for (int i = 0; i < partsExplosionRight.Count; i++)
            {
                partsExplosionRight[i].ChangePhysicalMap(map);
            }
            partExplosionCenter.ChangePhysicalMap(map);
        }
        public void ClearSprites()
        {
            for (int i = 0; i < partsExplosionBottom.Count; i++)
            {
                partsExplosionBottom[i].texture.Dispose();
            }
            for (int i = 0; i < partsExplosionUp.Count; i++)
            {
                partsExplosionUp[i].texture.Dispose();
            }
            for (int i = 0; i < partsExplosionLeft.Count; i++)
            {
                partsExplosionLeft[i].texture.Dispose();
            }
            for (int i = 0; i < partsExplosionRight.Count; i++)
            {
                partsExplosionRight[i].texture.Dispose();
            }
            partExplosionCenter.texture.Dispose();
        }
        public void DrawExplosion(BufferedGraphics currBuffer)
        {
            for (int j = 0; j < partsExplosionBottom.Count; j++)
            {
                currBuffer.Graphics.DrawImage(partsExplosionBottom[j].currTexture, partsExplosionBottom[j].location);
            }
            for (int j = 0; j < partsExplosionBottom.Count; j++)
            {
                currBuffer.Graphics.DrawImage(partsExplosionBottom[j].currTexture, partsExplosionBottom[j].location);
            }
            for (int j = 0; j < partsExplosionUp.Count; j++)
            {
                currBuffer.Graphics.DrawImage(partsExplosionUp[j].currTexture, partsExplosionUp[j].location);
            }
            for (int j = 0; j < partsExplosionLeft.Count; j++)
            {
                currBuffer.Graphics.DrawImage(partsExplosionLeft[j].currTexture, partsExplosionLeft[j].location);
            }
            for (int j = 0; j < partsExplosionRight.Count; j++)
            {
                currBuffer.Graphics.DrawImage(partsExplosionRight[j].currTexture, partsExplosionRight[j].location);
            }
            currBuffer.Graphics.DrawImage(partExplosionCenter.currTexture, partExplosionCenter.location);
        }
        ~Explosion()
        {
            for (int i = 0; i < partsExplosionBottom.Count; i++)
            {
                partsExplosionBottom[i].texture.Dispose();
            }
            for (int i = 0; i < partsExplosionUp.Count; i++)
            {
                partsExplosionUp[i].texture.Dispose();
            }
            for (int i = 0; i < partsExplosionLeft.Count; i++)
            {
                partsExplosionLeft[i].texture.Dispose();
            }
            for (int i = 0; i < partsExplosionRight.Count; i++)
            {
                partsExplosionRight[i].texture.Dispose();
            }
            partExplosionCenter.texture.Dispose();
        }
        public Explosion
            (
                Image explosionTextureCenter, Image explosionTextureUpEdge,
                Image explosionTextureBottomEdge, Image explosionTextureLeftEdge,
                 Image explosionTextureRightEdge, Image explosionTextureVerticalMiddle,
                Image explosionTextureHorizontalMiddle, Size size, Point location, int radius,
                PhysicalMap map, OnEndAllExplosionFunc funcEnd
            )
        {
            onEndAllExplosionFunc = funcEnd;
            explosionLocation = location;
            this.size = size;
            this.radius = radius;
            InitExplosion(explosionTextureCenter, explosionTextureUpEdge, explosionTextureBottomEdge,  explosionTextureLeftEdge,
                          explosionTextureRightEdge,  explosionTextureVerticalMiddle,  explosionTextureHorizontalMiddle,
                          size, location, radius, map);
            ChangeState();
        }
    }
}
