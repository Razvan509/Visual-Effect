using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisualEffect
{
    class RobotPlayer : GameObject
    {

        private Handler handler;
        private int HP = 100;
        private int c = 0;
        private float xB = 0;
        private float yB = 0;
        private Color color;

        public RobotPlayer(Handler handler, float x, float y, int width, int height, ID id, Color color)
        {
            this.handler = handler;
            this.id = id;
            this.HP = 100;
            this.color = color;
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }


        public override void tick()
        {
            VelX = 0;
            VelY = 0;
            if (handler.bars.Count != 0) // se calculeaza punctul unde trebuie sa ajunga robotul
            {
                xB = handler.bars[0].Width + Game.LARGIME/2 - 25;
                yB = handler.bars[0].Y;
            }

            if ((this.X < xB - 25 || this.X > xB + 25) && xB != 0) // daca 
            {
                if (c == 5)
                {
                    VelY = 10;
                    c = 0;
                }
                c++;
                if (this.X < xB - 25) VelX = 10;
                else if (this.X > xB + 25) VelX = -10;
            }
            else
            {
                if (this.Y > yB - 25 && yB != 0)
                {
                    VelY = -10;
                }
                else
                {
                    if (handler.bars.Count != 0)
                    {
                        handler.deleteBar(handler.bars[0]); 
                    }
                }
            }

            X += VelX;
            Y += VelY;
            
            X = Game.cleste(X, 0, Game.WIDTH - 50);
            Y = Game.cleste(Y, 0, Game.HEIGHT - 60);
            for (int i = 0; i < handler.objects.Count; ++i)
            {
                if (handler.objects[i].id == ID.BAR)
                {
                    if (this.getRectangle().IntersectsWith(handler.objects[i].getRectangle()))
                    {
                        if (this.HP > 0)
                        {
                            this.HP--;
                            Game.botHp.Text = "Bot: " + this.HP;
                        }
                        else
                        {
                            Game.pierde("BOT");
                            this.HP = 100;
                            Game.botHp.Text = "Bot: " + this.HP;
                            break;
                        }

                        if (this.getRectangle().Bottom <= handler.objects[i].getRectangle().Bottom + 5) // player sus
                        {
                            Y = handler.objects[i].getRectangle().Top - Height + 4;
                            continue;
                        }

                        if (((Bar)handler.objects[i]).bi == BAR_ID.LEFT) // bara stanga
                        {
                            if (this.getRectangle().Left >= handler.objects[i].getRectangle().Left &&
                                getRectangle().Left < handler.objects[i].getRectangle().Right - 10) // sub bara
                            {
                                this.Y = handler.objects[i].getRectangle().Bottom;
                                if (this.Y + this.Height >= Game.HEIGHT - 20)
                                    Game.pierde("BOT");
                            }
                            else if (this.getRectangle().Left >= handler.objects[i].getRectangle().Right - 10) // langa bara
                            {
                                this.X = handler.objects[i].getRectangle().Right;
                            }
                        }
                        else // bara dreapta
                        {
                            if (this.getRectangle().Right > handler.objects[i].getRectangle().Left + 10 &&
                                this.getRectangle().Right <= handler.objects[i].getRectangle().Right) // sub bara
                            {
                                this.Y = handler.objects[i].getRectangle().Bottom;
                                if (this.Y + this.Height >= Game.HEIGHT - 20)
                                    Game.pierde("BOT");
                            }
                            else if (this.getRectangle().Right <= handler.objects[i].getRectangle().Right + 10) // langa bara
                            {
                                this.X = handler.objects[i].getRectangle().Left - Width;
                            }
                        }
                    }
                    // intersectia a doi playeri
                }
                else if ((handler.objects[i].id == ID.PLAYER2 || handler.objects[i].id == ID.PLAYER1) &&
                          this.id != handler.objects[i].id)
                {
                    Rectangle p1 = this.getRectangle();
                    Rectangle p2 = handler.objects[i].getRectangle();
                    Player player2 = (Player)handler.objects[i];
                    int powerDif = p1.Width * p1.Height - p2.Width * p2.Height;

                    if (p1.IntersectsWith(p2))
                    {
                        if (p1.Bottom >= p2.Top && p1.Bottom <= p2.Bottom) // deasupra
                        {
                            if (p1.Right >= p2.Left && p1.Right <= p2.Right) // stanga
                            {
                                if (this.VelY >= 0 && p1.Bottom - p2.Top <= VelY) this.Y = handler.objects[i].Y - handler.objects[i].Height;
                                else if (this.VelX > 0) this.X = handler.objects[i].X - handler.objects[i].Width;
                            }
                            else
                            {
                                if (p1.Left <= p2.Right && p1.Left >= p2.Left) // dreapta
                                {
                                    if (this.VelY >= 0 && p1.Bottom - p2.Top <= VelY) this.Y = handler.objects[i].Y - handler.objects[i].Height;
                                    else if (this.VelX < 0) this.X = handler.objects[i].X + handler.objects[i].Width;
                                }
                            }
                        }
                        else
                        {
                            if (p1.Top <= p2.Bottom && p1.Top >= p2.Top) //sub
                            {
                                if (p1.Right >= p2.Left && p1.Right <= p2.Right) // stanga
                                {
                                    if (this.VelY <= 0 && p2.Bottom - p1.Top <= -VelY) this.Y = handler.objects[i].Y + handler.objects[i].Height;
                                    else if (this.VelX > 0) this.X = handler.objects[i].X - handler.objects[i].Width;
                                    else if (this.VelY <= 0) this.Y = handler.objects[i].Y + handler.objects[i].Height;
                                }
                                else
                                {
                                    if (p1.Left <= p2.Right && p1.Left >= p2.Left) // dreapta
                                    {
                                        if (this.VelY <= 0 && p2.Bottom - p1.Top <= -VelY) this.Y = handler.objects[i].Y + handler.objects[i].Height;
                                        else if (this.VelX < 0) this.X = handler.objects[i].X + handler.objects[i].Width;
                                        else if (this.VelY <= 0) this.Y = handler.objects[i].Y + handler.objects[i].Height;
                                    }
                                }
                            }
                        }
                    }
                }

            }


        }

        public override void render(System.Drawing.Graphics g)
        {
            if (Game.start)
                g.FillRectangle(new SolidBrush(color), X, Y, Width, Height);
        }
    }
}
