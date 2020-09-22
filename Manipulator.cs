using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class Manipulator : Figure
    {
        public Figure fig { get; private set; }
        public override void Draw(Graphics gr) //нарисовать рамку
        {
            if (fig == null) return;

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 1, Y - 1, W + 2, H + 2)); //Main

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 3, Y - 3, 5, 5)); //left-Top
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X + W - 2, Y - 3, 5, 5)); //right-Top
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 3, Y + H - 2, 5, 5)); //left-bot
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X + W - 2, Y + H - 2, 5, 5)); //right-bot
        }

        public override void Move(int dx, int dy)
        {
            throw new NotImplementedException();
        }

        public override void Resize(Graphics gr, int dw, int dh)
        {
            throw new NotImplementedException();
        }

        public override bool Touch(Graphics gr, int x, int y)
        {
            throw new NotImplementedException();
        }

        public virtual void Attach( Figure G ) //присоеденить манипулятор к одной фигуре, хз как по-русски это сказать
        {
            fig = G;
            Update();
        }

        public virtual void Drag(int dx, int dy) //move-resize one method
        {
            X += dx;
            Y += dy;
            fig.Move(dx, dy);
            Update();
        }

        public virtual void Clear(Graphics gr) //убрать ссылку на фигуру
        {
            fig = null;
            gr.DrawRectangle(new Pen(Color.White), new Rectangle(X - 1, Y - 1, W + 2, H + 2));
        }

        public virtual void Update() //обновить параметры манипулятора под параметры фигуры
        {
            X = fig.X;
            Y = fig.Y;
            W = fig.W;
            H = fig.H;
        }
    }
}