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
        public void ClearPrevPlace(PhysicalMap PhysicalMap)
        {
            for (int i = Y; i < Y + size.Height; i++)
            {
                for (int j = X; j < X + size.Width; j++)
                {
                    PhysicalMap.MapMatrix[i][j] = 0;
                }
            }
        }
        public void ChangeMapMatrix(PhysicalMap PhysicalMap)
        {
            for (int i = Y; i < Y + size.Height; i++)
            {
                for (int j = X; j < X + size.Width; j++)
                {
                    PhysicalMap.MapMatrix[i][j] = 1;
                }
            }
        }

        public delegate void DeleteObjectFunc(PhysicalObject obj);
        protected DeleteObjectFunc deleteObjectFunc;

        public PhysicalObject(Point location, Image texture)
            : base(location, texture)
        {
            size = texture.Size;
        }
        public PhysicalObject(Point location, Image sprite, Size spriteSize, DeleteObjectFunc deleteObjectFunc )
            : base(location, sprite)
        {
            this.size = spriteSize;
            this.deleteObjectFunc = deleteObjectFunc;
        }
    }
}
