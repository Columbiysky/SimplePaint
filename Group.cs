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

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 1, Y - 1, W + 2, H + 2)); //Main

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 5, Y - 5, 7, 7)); //left-Top - 1
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X + W - 2, Y - 5, 7, 7)); //right-Top - 2
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 5, Y + H - 2, 7, 7)); //left-bot - 3
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X + W - 2, Y + H - 2, 7, 7)); //right-bot - 4
        }

        public override void Move(int dx, int dy)
        {
            foreach (var f in figures)
                f.Move(dx, dy);
            Update();
        }

        public override void Resize(int c, int dx, int dy)
        {
            foreach (var f in figures)
                f.Resize(c, dx, dy);
        }

        public override bool Touch(Graphics gr, int x, int y)
        {
            if (x >= X - 7 && x <= X + 3 &&
            y >= Y - 7 && y <= Y + 3)
            {
                corner = 1;
                return true;
            }

            else if (x >= X + W - 3 && x <= X + W + 7 &&
                 y >= Y - 7 && y <= Y + 3)
                 {
                     corner = 2;
                     return true;
                 }

            else if (x >= X - 7 && x <= X + 3 &&
                 y >= Y + H - 3 && y <= Y + H + 7)
                 {
                     corner = 3;
                     return true;
                 }

            else if (x >= X + W - 3 && x <= X + W + 7 &&
                 y >= Y + H - 3 && y <= Y + H + 7)
                 {
                     corner = 4;
                     return true;
                 }

            else if (x >= X && x <= X + W &&
                 y >= Y && y <= Y + H)
                 {
                     corner = -1;
                     return true;
                 }
            else
                return false;
        }

        public void Add(Figure f)
        {
            figures.Add(f);
            Update();
        }

        public void Clear(Graphics gr)
        {
            figures.Clear();
            gr.DrawRectangle(new Pen(Color.White), new Rectangle(X - 1, Y - 1, W + 2, H + 2));
            X = Y = W = H = 0;
        }

        public void Update()
        {
            foreach(var f in figures)
            {
                if (f.X < X || X == 0)
                    X = f.X;
                if (f.Y < Y || Y == 0)
                    Y = f.Y;
                if (f.X + f.W > X + W)
                    W = (f.X + f.W) - X;
                if (f.Y + f.H > Y + H)
                    H = (f.Y + f.H) - Y;
            }
        }
    }
}
