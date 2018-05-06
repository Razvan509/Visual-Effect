using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace VisualEffect
{
    class Player : GameObject
    {
        private Pen pen;
        private Handler handler;
        private Color color;
        private int HP;

        public Player(Handler handler, float x, float y, int width, int height, ID id, Color color)
        {
            this.handler = handler;
            this.id = id;
            this.color = color;
            this.HP = 100;
            Width = width;
            Height = height;
            X = x;
            Y = y;
            VelX = 0;
            VelY = 0;
            pen = new Pen(color);            
            Thread.Sleep(5);
        }

        public override void tick()
        {            
            X += VelX;
            Y += VelY;
            X = Game.cleste(X, 0, Game.WIDTH - 50);
            Y = Game.cleste(Y, 0, Game.HEIGHT - 50);
            for (int i = 0; i < handler.objects.Count; ++i)
            {
                if (handler.objects[i].id == ID.BAR)
                {
                    if (this.getRectangle().IntersectsWith(handler.objects[i].getRectangle()))
                    {
                        if (this.id == ID.PLAYER2)
                        {
                            if (HP > 0)
                            {
                                HP--;
                                Game.redHp.Text = "Red: " + HP;
                            }
                            else
                            {
                                Game.pierde(this.color.Name);
                                HP = 100;
                                Game.redHp.Text = "Red: " + HP;
                                break;
                            }
                        }
                        else
                        {
                            if (HP > 0)
                            {
                                HP--;
                                Game.blueHp.Text = "Blue: " + HP;
                            }
                            else
                            {
                                Game.pierde(this.color.Name);
                                HP = 100;
                                Game.blueHp.Text = "Blue: " + HP;
                                break;
                            }
                        }

                        if (this.getRectangle().Bottom <= handler.objects[i].getRectangle().Bottom + 5) // player sus
                        {
                            Y = handler.objects[i].getRectangle().Top - Height + 4;
                            continue;
                        }

                        if (((Bar) handler.objects[i]).bi == BAR_ID.LEFT) // bara stanga
                        {
                            if (this.getRectangle().Left >= handler.objects[i].getRectangle().Left && 
                                getRectangle().Left < handler.objects[i].getRectangle().Right - 10) // sub bara
                            {
                                this.Y = handler.objects[i].getRectangle().Bottom;
                                if (this.Y + this.Height >= Game.HEIGHT - 20)
                                    Game.pierde(this.color.Name);
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
                                    Game.pierde(this.color.Name);
                            }
                            else if (this.getRectangle().Right <= handler.objects[i].getRectangle().Right + 10) // langa bara
                            {
                                this.X = handler.objects[i].getRectangle().Left - Width;
                            }
                        }
                    }
                  // intersectia a doi playeri
                } else if ((handler.objects[i].id == ID.PLAYER2 || handler.objects[i].id == ID.PLAYER1 || handler.objects[i].id == ID.PLAYERROBOT) &&
                            this.id != handler.objects[i].id)
                {
                    Rectangle p1 = this.getRectangle();
                    Rectangle p2 = handler.objects[i].getRectangle();
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