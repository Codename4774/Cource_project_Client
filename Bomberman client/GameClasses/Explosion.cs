using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    class Explosion
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
                 Image explosionTextureRightEdge, Image explosionTextureUpMiddle,
                Image explosionTextureBottomMiddle, Image explosionTextureLeftMiddle,
                 Image explosionTextureRightMiddle, Size size, Point location, int radius,
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
                if ((location.X - size.Width) > 0)
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureLeftMiddle, size, new Point((location.X - size.Width), location.Y), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureLeftEdge, size, new Point((location.X - size.Width), location.Y), countStates));
                    }
                    countParts++;
                }
                else
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureLeftMiddle, size, new Point(0, location.Y), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureLeftEdge, size, new Point(0, location.Y), countStates));
                    }
                    countParts++;
                    break;
                }
            }
            for (int i = 0; i < radius; i++)
            {
                if ((location.X + size.Width) < map.Width)
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureRightMiddle, size, new Point((location.X + size.Width), location.Y), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureRightEdge, size, new Point((location.X + size.Width), location.Y), countStates));
                    }
                    countParts++;
                }
                else
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureRightMiddle, size, new Point((map.Width - size.Width), location.Y), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureRightEdge, size, new Point(map.Width - size.Width, location.Y), countStates));
                    }
                    countParts++;
                    break;
                }
            }
            for (int i = 0; i < radius; i++)
            {
                if ((location.Y - size.Height) > 0)
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureUpMiddle, size, new Point(location.X, location.Y - size.Height), countStates));
                                            
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureUpEdge, size, new Point(location.X, location.Y - size.Height), countStates));
                    }
                    countParts++;
                }
                else
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureUpMiddle, size, new Point(location.X, 0), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureUpEdge, size, new Point(location.X, 0), countStates));
                    }
                    countParts++;
                    break;
                }
            }
            for (int i = 0; i < radius; i++)
            {
                if ((location.Y + size.Height) < map.Height)
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureBottomMiddle, size, new Point(location.X, location.Y + size.Height), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureBottomEdge, size, new Point(location.X, location.Y + size.Height), countStates));
                    }
                    countParts++;
                }
                else
                {
                    if (i == radius - 1)
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureBottomMiddle, size, new Point(location.X, map.Height - location.Y), countStates));
                    }
                    else
                    {
                        partsExplosionLeft.Add(new PartExplosion(explosionTextureBottomEdge, size, new Point(location.X, map.Height - location.Y), countStates));
                    }
                    countParts++;
                    break;
                }
            }

            partExplosionCenter = new PartExplosion(explosionTextureCenter, size, location, countStates);
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
        public Explosion
            (
                Image explosionTextureCenter, Image explosionTextureUpEdge,
                Image explosionTextureBottomEdge, Image explosionTextureLeftEdge,
                 Image explosionTextureRightEdge, Image explosionTextureUpMiddle,
                Image explosionTextureBottomMiddle, Image explosionTextureLeftMiddle,
                 Image explosionTextureRightMiddle, Size size, Point location, int radius,
                PhysicalMap map, OnEndAllExplosionFunc funcEnd
            )
        {
            onEndAllExplosionFunc = funcEnd;
            explosionLocation = location;
            this.size = size;
            this.radius = radius;
            InitExplosion(explosionTextureCenter, explosionTextureUpEdge, explosionTextureBottomEdge,  explosionTextureLeftEdge,
                          explosionTextureRightEdge,  explosionTextureUpMiddle,  explosionTextureBottomMiddle,
                          explosionTextureLeftMiddle,  explosionTextureRightMiddle,  size, location, radius, map);

        }
    }
}
