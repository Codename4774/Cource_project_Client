using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bomberman_client.GameClasses
{
    public class Map
    {
        private int[][] mapMatrix;
        public int[][] MapMatrix
        {
            get
            {
                return mapMatrix;
            }
            private set
            { }
        }
        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            private set
            {
            }
        }
        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            private set
            {
            }

        }
        public Map(int[][] mapMatrix)
        {
            this.mapMatrix = mapMatrix;
        }

        public Map(int width, int height)
        {
            mapMatrix = new int[height][];
            for (int i = 0; i < height; i++)
            {
                mapMatrix[i] = new int[width];
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mapMatrix[i][j] = 0;
                }
            }
            this.width = width;
            this.height = height;
        }
    }
}
