using MyPaint.Figures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Main : Form
    {
        List<Figure> Figures = new List<Figure>();
        List<IFigureCreator> Tools = new List<IFigureCreator>();
        IFigureCreator FigureCreator;
        Figure CurrentFig;
        Point P;
        Graphics gr;
        bool resize = false;

        public Main()
        {
            Tools.Add(null);
            Tools.Add(new MyRectangle.RectangleCreator());
            Tools.Add(new MyEllipse.EllipseCreator());

            this.ClientSize = new System.Drawing.Size(1500, 1000);

            InitializeComponent();

            RectangelButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                resize = false;
                FigureCreator = Tools[1];
            };
            EllipseButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                resize = false;
                FigureCreator = Tools[2];
            };
            MoveButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                resize = false;
                FigureCreator = Tools[0];
            };
            ResizeButton.Click += (s, a) => 
            {
                FigureCreator = Tools[0];
                resize = true;
            };

            ClearButton.Click += (s, a) =>
            {
                if (CurrentFig != null) CurrentFig = null;
                resize = false;
                Figures.Clear();
                pictureBox1.Refresh();
            };

            gr = pictureBox1.CreateGraphics();
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        { 
            foreach (Figure fig in Figures)
                fig.Draw(e.Graphics);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (FigureCreator != null)
            {
                var figure = FigureCreator.Create(e.X, e.Y, 50, 50);
                figure.Draw(gr);
                Figures.Add(figure);
            }

            else if (resize)
            {
                CurrentFig = null;
                foreach(var fig in Figures)
                    if(fig.Touch(gr, e.X, e.Y))
                        CurrentFig = fig;


            }

            else
                foreach (var fig in Figures)
                    if (fig.Touch(gr, e.X, e.Y))
                    {
                        CurrentFig = fig;
                        P = e.Location;
                        break;
                    }
        }

        [DebuggerStepThrough]
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!resize)
            {
                if (CurrentFig != null)
                {
                    CurrentFig.Move(e.X - P.X, e.Y - P.Y);
                    P = e.Location;
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(!resize)
                CurrentFig = null;
            
        }
    }
}
