﻿using System;
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
            foreach (var fig in figures)
                fig.Draw(gr);
        }

        public override void Move(float dx, float dy)
        {
            foreach (var f in figures)
                f.Move(dx, dy);
            X += dx;
            Y += dy;
            Update();
        }

        public override void Resize(int c, float dx, float dy)
        {
            float kw = dx / W, kh = dy / H;
            foreach (var f in figures)
            {
                f.Resize(c, f.W * kw, f.H * kh);
                f.Move(kw * (f.X - X), kh * (f.Y - Y));
            }
            W += dx;
            H += dy;
        }

        public override bool Touch(Graphics gr, float x, float y) //same as manipulator's touch method
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
                return false;
        }

        public void Add(Figure f)
        {
            //If wanted figure is already in group: skip it
            foreach (var fig in figures)
                if (fig == f)
                    return;
            figures.Add(f);
            Update();
        }

        public void Clear()
        {
            figures.Clear();
            X = Y = W = H = 0;
        }

        public void Update()
        {
            X = figures.Min(f => f.X);
            Y = figures.Min(f => f.Y);
            W = figures.Max(f => f.X + f.W) - X;
            H = figures.Max(f => f.Y + f.H) - Y;
        }

        public override Figure Clone()
        {
            Group gr = new Group();
            foreach (var f in figures)
                gr.Add(f.Clone());
            return gr;
        }
    }
}
