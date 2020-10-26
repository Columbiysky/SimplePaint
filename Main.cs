﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyPaint
{
    public partial class Main : Form
    {
        List<Figure> Figures = new List<Figure>();
        Manipulator manipulator = new Manipulator(); //decorator
        Group g = new Group(); //che-to tam
        Dictionary<string, IFigureCreator> Tools = new Dictionary<string, IFigureCreator>();
        IFigureCreator FigureCreator;
        Figure CurrentFig;
        Point P;
        Graphics gr;

        bool isPressed = false;

        public Main()
        {
            Tools.Add("", null);
            Tools.Add("Rectangle", new MyRectangle.RectangleCreator());
            Tools.Add("Ellipse", new MyEllipse.EllipseCreator());

            this.ClientSize = new System.Drawing.Size(1500, 1000);

            InitializeComponent();

            RectangelButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                FigureCreator = Tools[RectangelButton.Text];
            };

            EllipseButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                FigureCreator = Tools[EllipseButton.Text];
            };

            SelectButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                FigureCreator = Tools[""];
            };

            ClearButton.Click += (s, a) =>
            {
                if (CurrentFig != null) CurrentFig = null;
                Figures.Clear();
                pictureBox1.Refresh();
            };

            gr = pictureBox1.CreateGraphics();
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        [DebuggerStepThrough]
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        { 
            foreach (Figure fig in Figures)
                fig.Draw(e.Graphics);

            manipulator.Draw(e.Graphics);
            g.Draw(e.Graphics);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (FigureCreator != null)
            {
                var figure = FigureCreator.Create(e.X, e.Y, 50, 50);
                figure.Draw(gr);
                Figures.Add(figure);
            }

            else
                foreach (var fig in Figures)
                    if (fig.Touch(gr, e.X, e.Y))
                    {
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            g.Add(fig);
                            g.Touch(gr, e.X, e.Y);
                            manipulator.Clear(gr);
                            P = e.Location;
                            isPressed = true;
                            break;
                        }

                        else
                        {
                            manipulator.Attach(fig); //decorator
                            manipulator.Touch(gr, e.X, e.Y);
                            P = e.Location;
                            isPressed = true;
                            break;
                        }
                    }
        }

        [DebuggerStepThrough]
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (manipulator.fig != null && isPressed)
            {
                if (g.figuresCount != 0 && g.Corner == -1)
                    g.Move(e.X - P.X, e.Y - P.Y);
                manipulator.Drag(e.X - P.X, e.Y - P.Y);
                P = e.Location;
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            if (e.X - P.X != 0 && e.Y - P.Y != 0)
            {
                manipulator.Clear(gr);
                g.Clear(gr);
            }
        }
    }
}
