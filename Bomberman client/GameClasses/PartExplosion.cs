using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    class PartExplosion
    {
        private int countStates;
        private int currState;
        private int currSpriteOffset;
        public Bitmap sprite;
        public Bitmap currTexture;
        Size size;
        private Point location;
        //public enum KindPartOfExplosion { CENTER, EDGE, MIDDLE };
        public void ChangeState()
        {
            currTexture = sprite.Clone(new Rectangle(new Point(currSpriteOffset, 0), size), sprite.PixelFormat);
            currSpriteOffset += size.Width;
            currState++;
        }
        public void ChangePhysicalMap(PhysicalMap map)
        {
            for (int i = location.Y; i < location.Y + size.Height; i++)
            {
                for (int j = location.X; j < location.X + size.Width; j++)
                {
                    map.MapMatrix[i][j] = 2;
                }
            }
        }
        public PartExplosion(Image sprite, Size size, Point location, int countStates)
        {
            this.sprite = sprite as Bitmap;
            this.size = size;
            this.countStates = countStates;
            currState = 0;
            currSpriteOffset = 0;
        }
    }
}
