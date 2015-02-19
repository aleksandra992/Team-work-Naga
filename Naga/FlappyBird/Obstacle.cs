using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class Obstacle
    {
        public int height;
        public string[] upperPart = { " __________ ", "|          |", "|          |", " __________ " };
        public int X, Y;


        public Obstacle(int height, int X, int Y)
        {
            this.height = height;
            this.X = X;
            this.Y = Y;
        }
    }
}
