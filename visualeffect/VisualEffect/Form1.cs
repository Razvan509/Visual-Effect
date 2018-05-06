using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualEffect.Properties;

namespace VisualEffect
{
    enum ID {PLAYER1, BAR, PLAYER2, PLAYERROBOT};
    enum BAR_ID {LEFT, RIGHT};

    public partial class Game : Form
    {
        private static int PERIOADA = 2000;
        public static int LARGIME = 200;
        public static int WIDTH = 950;
        public static int HEIGHT = 500;
        public static int WIDTHB = 250;
        public static int HEIGHTB = 100;

        private static Handler handler;
        private static Graphics g;
        private Random r;
        private KeyInput keyEvents;

        public static Timer timer1;
        public static Timer timer2;
        public static Timer timer3;

        private static Button b1 = new Button();
        private static Button b2 = new Button();
        private static Button b3 = new Button();
        private static Button b4 = new Button();
        private static Button b5 = new Button();
        private static Button b6 = new Button();
        private static Button b7 = new Button();

        public static bool start = false;
        public static bool start2 = false;

        public static int val;
        public static int lastVal;
        public static int gaura;

        public static Label redHp;
        public static Label blueHp;
        public static Label botHp;
        public static Label titlu;

        public Game()
        {
            InitializeComponent();

            WIDTH = SystemInformation.VirtualScreen.Width;
            HEIGHT = SystemInformation.VirtualScreen.Height;
            this.Width = WIDTH;
            this.Height = HEIGHT;
                            
            handler = new Handler();
            this.r = new Random();
            this.keyEvents = new KeyInput(handler);
            timer1 = new Timer();
            timer2 = new Timer();
            timer3 = new Timer();

            this.KeyPreview = true; //! activeaza ascultatorii tastaturii !            
            this.DoubleBuffered = true; // previne flickeritul
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // fixeaza o dimensiune fixa a ferestrei
            this.ClientSize = new Size(WIDTH, HEIGHT);
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer2.Tick += new System.EventHandler(timer2_Tick);
            timer3.Tick += new System.EventHandler(timer3_Tick);

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(keyEvents.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(keyEvents.keyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(keyEvents.exitGame);
            
            timer1.Interval = 20;
            timer2.Interval = PERIOADA;
            timer3.Interval = PERIOADA * 10;

            gaura = LARGIME;
            lastVal = WIDTH / 2 - LARGIME / 2;

            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;

            Point p1 = new Point(WIDTH / 2 - WIDTHB / 2, HEIGHT / 4 - HEIGHTB + 120);
            Point p2 = new Point(WIDTH / 2 - WIDTHB / 2, HEIGHT / 4 - HEIGHTB + 240);
            Point p3 = new Point(WIDTH / 2 - WIDTHB / 2, HEIGHT / 4 - HEIGHTB + 360);
            Point p4 = new Point(WIDTH / 2 - WIDTHB / 2, HEIGHT / 4 - HEIGHTB + 480);
            

            b1.Text = "Play";
            b2.Text = "Help";
            b3.Text = "Credits";
            b4.Text = "Exit";
            b5.Text = "P vs P";
            b6.Text = "P vs AI";
            b7.Text = "Back";

            b1.Height = HEIGHTB;
            b1.Width = WIDTHB;
            b2.Height = HEIGHTB;
            b2.Width = WIDTHB;
            b3.Height = HEIGHTB;
            b3.Width = WIDTHB;
            b4.Height = HEIGHTB;
            b4.Width = WIDTHB;
            b5.Height = HEIGHTB;
            b5.Width = WIDTHB;
            b6.Height = HEIGHTB;
            b6.Width = WIDTHB;
            b7.Height = HEIGHTB;
            b7.Width = WIDTHB;

            b1.Location = p1;
            b2.Location = p2;
            b3.Location = p3;
            b4.Location = p4;
            b5.Location = p1;
            b6.Location = p2;
            b7.Location = p3;

            b1.Font = new Font("Castellar", 30f, FontStyle.Bold);
            b2.Font = new Font("Castellar", 30f, FontStyle.Bold);
            b3.Font = new Font("Castellar", 30f, FontStyle.Bold);
            b4.Font = new Font("Castellar", 30f, FontStyle.Bold);
            b5.Font = new Font("Castellar", 30f, FontStyle.Bold);
            b6.Font = new Font("Castellar", 30f, FontStyle.Bold);
            b7.Font = new Font("Castellar", 30f, FontStyle.Bold);

            b1.ForeColor = Color.DarkBlue;
            b2.ForeColor = Color.DarkBlue;
            b3.ForeColor = Color.DarkBlue;
            b4.ForeColor = Color.DarkBlue;
            b5.ForeColor = Color.DarkBlue;
            b6.ForeColor = Color.DarkBlue;
            b7.ForeColor = Color.DarkBlue;


            b1.Click += new EventHandler(b1_click);
            b2.Click += new EventHandler(b2_click);
            b3.Click += new EventHandler(b3_click);
            b4.Click += new EventHandler(b4_click);
            b5.Click += new EventHandler(b5_click);
            b6.Click += new EventHandler(b6_click);
            b7.Click += new EventHandler(b7_click);

            this.Controls.Add(b1);
            this.Controls.Add(b2);
            this.Controls.Add(b3);
            this.Controls.Add(b4);

            redHp = new Label();
            blueHp = new Label();
            botHp = new Label();
            titlu = new Label();

            Point pr = new Point(20, 100);
            Point pb = new Point(WIDTH - 150, 100);
            Point pt = new Point(WIDTH / 2 - WIDTHB / 2 - 140, HEIGHT / 4 - HEIGHTB);

            redHp.Height = 35;
            redHp.Width = 120;
            blueHp.Height = 35;
            blueHp.Width = 130;
            botHp.Height = 35;
            botHp.Width = 120;
            titlu.Height = 75;
            titlu.Width = 580;

            redHp.Location = pr;
            blueHp.Location = pb;
            botHp.Location = pr;
            titlu.Location = pt;

            redHp.Font = new Font("Castellar",16f);
            blueHp.Font = new Font("Castellar", 16f);
            botHp.Font = new Font("Castellar", 16f);
            titlu.Font = new Font("Castellar", 50f);

            

            redHp.ForeColor = Color.DarkRed;
            blueHp.ForeColor = Color.Blue;
            botHp.ForeColor = Color.DarkOrange;
            titlu.ForeColor = Color.DarkRed;

            titlu.Text = "Visual Effect";


            this.Controls.Add(titlu);

            timer1.Start();
            timer2.Start();

            g = this.CreateGraphics();


        }

        private void timer1_Tick(object sender, EventArgs e) // face ca toate obiectele sa se miste si sa se incarce pe ecran
        {
            
            g.Clear(this.BackColor);
            handler.tick();
            handler.render(g);
            
        }

        private void timer2_Tick(object sender, EventArgs evt) // genereaza barele
        {
            val = r.Next(WIDTH) - gaura; 
            if (val < 0) val = WIDTH / 2;
            if (Math.Abs(val - lastVal) > WIDTH / 2) val = WIDTH / 2;
            handler.add(new Bar(handler, 0, 0, val, 10, ID.BAR, BAR_ID.LEFT, val));
            handler.add(new Bar(handler, val + gaura, 0, WIDTH - val - gaura, 10, ID.BAR, BAR_ID.RIGHT, val));

            lastVal = val;

            if (start2)
            {
                handler.addBar(new Bar(handler, 0, 0, val, 10, ID.BAR, BAR_ID.LEFT, val));
            }
        }

        private void timer3_Tick(object sender, EventArgs evt) // face jocul mai interesant 
        {
            if (start)
            {
                if (gaura <= 70) { gaura = 70; timer3.Enabled = false; }
                else gaura -= 20; 
                handler.raiseUp(1); // pentru a nu avea bare care se misca mai repede si altele care se misca mai incet
                Bar.vely += 1; // cresc viteza standard a barelor
                timer2.Interval -= 250; // barele se genereaza mai repede

            }
        }

        public static int cleste  (int val, int min, int max)
        {
            if (val < min) return min;
            else if (val > max) return max;
            return val;
        }

        public static float cleste (float val, float min, float max)
        {
            if (val < min) return min;
            else if (val > max) return max;
            return val;
        }

        private void b1_click(object sender, EventArgs e)
        {
            b1.Visible = false;
            b2.Visible = false;
            b3.Visible = false;
            b4.Visible = false;

            b1.Enabled = false;
            b2.Enabled = false;
            b3.Enabled = false;
            b4.Enabled = false;

            this.Controls.Add(b5);
            this.Controls.Add(b6);
            this.Controls.Add(b7);

            b5.Enabled = true;
            b6.Enabled = true;
            b7.Enabled = true;

            b5.Visible = true;
            b6.Visible = true;
            b7.Visible = true;

        }

        private void b2_click(object sender, EventArgs e)
        {
            new Help().Visible = true;
        }

        private void b3_click(object sender, EventArgs e)
        {
            new Credits().Visible = true;
        }

        private void b4_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void b5_click(object sender, EventArgs e)
        {
            b5.Visible = false;
            b6.Visible = false;
            b7.Visible = false;

            b5.Enabled = false;
            b6.Enabled = false;
            b7.Enabled = false;

            titlu.Visible = false;

           

            start = true;
            handler.deleteAll();

            timer3.Start();

            handler.add(new Player(handler, WIDTH / 2 + 100, HEIGHT / 2 + 200, 50, 50, ID.PLAYER1, Color.Blue));
            handler.add(new Player(handler, WIDTH / 2 - 100, HEIGHT / 2 + 200, 50, 50, ID.PLAYER2, Color.Red));

            blueHp.Visible = true;
            redHp.Visible = true;

            redHp.Text = "RED: 100";
            blueHp.Text = "BLUE: 100";

            this.Controls.Add(blueHp);
            this.Controls.Add(redHp);
        }

        public void b6_click(object sender, EventArgs e)
        {
            b5.Visible = false;
            b6.Visible = false;
            b7.Visible = false;

            b5.Enabled = false;
            b6.Enabled = false;
            b7.Enabled = false;

            titlu.Visible = false;

            start = true;
            start2 = true;
            handler.deleteAll();

            timer3.Start();

            handler.add(new Player(handler, WIDTH / 2 + 100, HEIGHT / 2 + 200, 50, 50, ID.PLAYER1, Color.Blue));
            handler.add(new RobotPlayer(handler, WIDTH / 2 - 100, HEIGHT / 2 + 200, 50, 50, ID.PLAYERROBOT, Color.Orange));

            blueHp.Text = "BLUE: 100";
            botHp.Text = "BOT: 100";

            this.Controls.Add(blueHp);
            this.Controls.Add(botHp);

            blueHp.Visible = true;
            botHp.Visible = true;
        }

        public void b7_click(object sender, EventArgs e)
        {
            b1.Visible = true;
            b2.Visible = true;
            b3.Visible = true;
            b4.Visible = true;

            b1.Enabled = true;
            b2.Enabled = true;
            b3.Enabled = true;
            b4.Enabled = true;

            b5.Visible = false;
            b6.Visible = false;
            b7.Visible = false;

            b5.Enabled = false;
            b6.Enabled = false;
            b7.Enabled = false;
        }

        public static void pierde(String o)
        {
            start = false;
            start2 = false;

            timer2.Interval = PERIOADA;
            gaura = LARGIME;
            Bar.vely = 4;


            handler.deleteAll();
            handler.bars.Clear();

            MessageBox.Show(o+ " a pierdut!");
            

            b1.Visible = true;
            b2.Visible = true;
            b3.Visible = true;
            b4.Visible = true;
            b1.Enabled = true;
            b2.Enabled = true;
            b3.Enabled = true;
            b4.Enabled = true;

            timer3.Stop();

            redHp.Visible = false;
            blueHp.Visible = false;
            botHp.Visible = false;

            titlu.Visible = true;

        }
    }
}
