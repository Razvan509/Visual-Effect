using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisualEffect
{
    class Bar : GameObject
    {
        private Handler handler;
        public BAR_ID bi;
        public int r;
        public static int vely = 4;

        public Bar (Handler handler, int x, int y, int w, int h, ID id, BAR_ID bi, int r)
        {
            this.handler = handler;
            this.id = id;
            this.bi = bi;
            this.r = r;
            X = x;
            Y = y;
            Width = w;
            Height = h;
            VelY = vely;
        }

        public override void render(Graphics g)
        {   
            g.FillRectangle(new SolidBrush(Color.Green), getRectangle());
        }

        public override void tick()
        {
            if (this.Y == Game.HEIGHT) handler.delete(this);
            this.Y += VelY;
           
        }
    }
}
