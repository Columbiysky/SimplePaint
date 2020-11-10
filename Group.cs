using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace MyPaint
{
    class Group : Figure
    {
        List<Figure> figures { get; }
        public int figuresCount { get => figures.Count; }
        public int Corner { get => corner; }
        int corner = -1;
        public Group()
        {
            figures = new List<Figure>();
        }

        public override void Draw(Graphics gr)
        {
            if (figures.Count == 0) return;

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2)); //Main

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)X - 5, (int)Y - 5, 7, 7)); //left-Top - 1
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)(X + W - 2), (int)Y - 5, 7, 7)); //right-Top - 2
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)X - 5, (int)(Y + H - 2), 7, 7)); //left-bot - 3
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)(X + W - 2), (int)(Y + H - 2), 7, 7)); //right-bot - 4
        }

        public override void Move(int dx, int dy)
        {
            foreach (var f in figures)
                f.Move(dx, dy);
            X += dx;
            Y += dy;
            Update();
        }

        public override void Resize(int c, int dx, int dy)
        {
            int kw = dx / W, kh = dy / H;
            foreach (var f in figures)
            {
                f.Resize(c, f.W * kw, f.H * kh);
                f.Move(kw * (f.X - X), kh * (f.Y - Y));
            }
            //W += dx;
            //H += dy;
            //Update();
            if (c == 1)
            {
                X += dx;
                Y += dy;
                W -= dx;
                H -= dy;
            }
            else if (c == 2)
            {
                Y += dy;
                W += dx;
                H -= dy;
            }
            else if (c == 3)
            {
                X += dx;
                W -= dx;
                H += dy;
            }
            else if (c == 4)
            {
                W += dx;
                H += dy;
            }
        }

        public override bool Touch(Graphics gr, int x, int y)
        {
            if (x >= X - 2 && x <= X + 2 &&
                y >= Y - 2 && y <= Y + 2)
            {
                corner = 1;
                return true;
            }

            else if (x >= X + W - 2 && x <= X + W + 2 &&
                 y >= Y - 2 && y <= Y + 2)
                 {
                     corner = 2;
                     return true;
                 }

            else if (x >= X - 2 && x <= X + 2 &&
                 y >= Y + H - 2 && y <= Y + H + 2)
                 {
                     corner = 3;
                     return true;
                 }

            else if (x >= X + W - 2 && x <= X + W + 2 &&
                 y >= Y + H - 2 && y <= Y + H + 2)
                 {
                     corner = 4;
                     return true;
                 }

            else if (x >= X && x <= X + W &&
                 y >= Y && y <= Y + H)
                 {
                     corner = -1;
                     Draw(gr);
                     return true;
                 }
            else
            {
                X = Y = W = H = 0;
                corner = -1;
                gr.DrawRectangle(new Pen(Color.White), new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2));
                return false;
            }
        }

        public void Add(Figure f)
        {
            figures.Add(f);
            Update();
        }

        public void Clear(Graphics gr)
        {
            figures.Clear();
            gr.DrawRectangle(new Pen(Color.White), new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2));
            X = Y = W = H = 0;
        }

        public void Update()
        {
            X = figures.Min(f => f.X);
            Y = figures.Min(f => f.Y);
            W = figures.Max(f => f.X + f.W) - X;
            H = figures.Max(f => f.Y + f.H) - Y;
        }
    }
}
