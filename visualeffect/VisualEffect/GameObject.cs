using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisualEffect
{
    abstract class GameObject
    {
        private float x;
        private float y;
        private float velX;
        private float velY;
        private int width;
        private int height;

        public ID id;
        public float X { get { return this.x;} set{this.x = value;} }
        public float Y { get { return this.y; } set { this.y = value; } }
        public float VelX { get { return this.velX; } set { this.velX = value; } }
        public float VelY { get { return this.velY; } set { this.velY = value; } }
        public int Width { get { return this.width; } set { this.width = value; } }
        public int Height { get { return this.height; } set { this.height = value; } }

        public abstract void tick();
        public abstract void render(Graphics g);

        public Rectangle getRectangle ()
        {
            return new Rectangle((int) X, (int) Y, Width, Height);
        }

    }
}
