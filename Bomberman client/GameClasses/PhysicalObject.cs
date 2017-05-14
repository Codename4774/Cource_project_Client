using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    public class PhysicalObject : Cell
    {
        public readonly Size size;
        public void ClearPrevPlace(Map map)
        {
            for (int i = Y; i < X + size.Height; i++)
            {
                for (int j = Y; j < X + size.Width; j++)
                {
                    map.MapMatrix[i][j] = 0;
                }
            }
        }
        public void ChangeMapMatrix(Map map)
        {
            for (int i = X; i < X + size.Height; i++)
            {
                for (int j = Y; j < X + size.Width; j++)
                {
                    map.MapMatrix[i][j] = 1;
                }
            }
        }
        public PhysicalObject(Point location, Image texture)
            : base(location, texture)
        {
            size = texture.Size;
        }
        public PhysicalObject(Point location, Image sprite, Size spriteSize)
            : base(location, sprite)
        {
            this.size = spriteSize;
        }
    }
}
