using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualEffect
{
    class KeyInput
    {
        private bool[] keysPressed;
        private bool[] keysPressed2;
        private Handler handler;
        private static int defaultVel = 10;

        public KeyInput (Handler handler)
        {
            this.handler = handler;
            keysPressed = new bool[4];
            keysPressed2 = new bool[4];

            keysPressed[0] = false; // W
            keysPressed[1] = false; // A
            keysPressed[2] = false; // S
            keysPressed[3] = false; // D

            keysPressed2[0] = false; // Sus
            keysPressed2[1] = false; // Stanga
            keysPressed2[2] = false; // Jos
            keysPressed2[3] = false; // Dreapta
        }

        public void keyDown (object sender, KeyEventArgs evt)
        {
            Keys key = evt.KeyCode;

            for (int i = 0; i < handler.objects.Count; ++i)
            {
                if (handler.objects[i].id == ID.PLAYER2)
                {
                    if (key == Keys.W) { keysPressed[0] = true; handler.objects[i].VelY = -defaultVel; }
                    if (key == Keys.A) { keysPressed[1] = true; handler.objects[i].VelX = -defaultVel; }
                    if (key == Keys.S) { keysPressed[2] = true; handler.objects[i].VelY = defaultVel; }
                    if (key == Keys.D) { keysPressed[3] = true; handler.objects[i].VelX = defaultVel; }

                }
                else if (handler.objects[i].id == ID.PLAYER1)
                {
                    if (key == Keys.Up) { keysPressed2[0] = true; handler.objects[i].VelY = -defaultVel; }
                    if (key == Keys.Left) { keysPressed2[1] = true; handler.objects[i].VelX = -defaultVel; }
                    if (key == Keys.Down) { keysPressed2[2] = true; handler.objects[i].VelY = defaultVel; }
                    if (key == Keys.Right) { keysPressed2[3] = true; handler.objects[i].VelX = defaultVel; }
                }
            }
        }

        public void keyUp (object sender, KeyEventArgs evt)
        {
            Keys key = evt.KeyCode;

            for (int i = 0; i < handler.objects.Count; ++i)
            {
                if (handler.objects[i].id == ID.PLAYER2)
                {
                    if (key == Keys.W) { keysPressed[0] = false; }
                    if (key == Keys.A) { keysPressed[1] = false; }
                    if (key == Keys.S) { keysPressed[2] = false; }
                    if (key == Keys.D) { keysPressed[3] = false; }

                    if (!keysPressed[0] && !keysPressed[2]) handler.objects[i].VelY = 0;
                    if (!keysPressed[1] && !keysPressed[3]) handler.objects[i].VelX = 0;
                }
                else if (handler.objects[i].id == ID.PLAYER1)
                {
                    if (key == Keys.Up) { keysPressed2[0] = false; }
                    if (key == Keys.Left) { keysPressed2[1] = false; }
                    if (key == Keys.Down) { keysPressed2[2] = false; }
                    if (key == Keys.Right) { keysPressed2[3] = false; }

                    if (!keysPressed2[0] && !keysPressed2[2]) handler.objects[i].VelY = 0;
                    if (!keysPressed2[1] && !keysPressed2[3]) handler.objects[i].VelX = 0;
                }
            }            
        }

        public void exitGame (object sender, KeyEventArgs evt)
        {
            Keys key = evt.KeyCode;
            if (key == Keys.Escape) Application.Exit();
        }
    }
}
