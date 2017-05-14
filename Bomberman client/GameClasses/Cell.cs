using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    public class Cell
    {
        private Point location;
        public int X
        {
            set
            {
                location.X = value;
            }
            get
            {
                return location.X;
            }
        }
        public int Y
        {
            set
            {
                location.Y = value;
            }
            get
            {
                return location.Y;
            }
        }

        public Image texture { get; set; }
        public Cell(Point location, Image texture)
        {
            this.texture = texture;

            this.location = location;
        }
    }
}
