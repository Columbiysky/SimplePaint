using MyPaint.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Main : Form
    {
        List<Figure> Figures = new List<Figure>();
        List<IFigureCreator> Tools = new List<IFigureCreator>();
        IFigureCreator FigureCreator;
        Figure CurrentFIg;
        Point P;
        Graphics gr;


        public Main()
        {
            Tools.Add(null);
            Tools.Add(new MyRectangle.RectangleCreator());
            Tools.Add(new MyEllipse.EllipseCreator());

            this.ClientSize = new System.Drawing.Size(1500, 1000);

            InitializeComponent();
            //gr = panel1.CreateGraphics();

            RectangelButton.Click += (s, a) => { FigureCreator = Tools[1]; };
            EllipseButton.Click += (s, a) => { FigureCreator = Tools[2]; };
            MoveButton.Click += (s, a) => { FigureCreator = Tools[0]; };
            ClearButton.Click += (s, a) =>
            {
                Figures.Clear();
                pictureBox1.Refresh();
            };

            gr = pictureBox1.CreateGraphics();
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //this.DoubleBuffered = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure fig in Figures)
                fig.Draw(gr);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (FigureCreator != null)
            {
                var figure = FigureCreator.Create(e.X, e.Y, 50, 50);
                figure.Draw(gr);
                Figures.Add(figure);
            }

            else
                foreach(var fig in Figures)
                    if (fig.Touch(gr, e.X, e.Y))
                    {                      
                        CurrentFIg = fig;
                        P = e.Location;
                        break;
                    }
            
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentFIg != null)
            {
                panel1.Refresh();
                CurrentFIg.Move(e.X-P.X, e.Y-P.Y);
                P = e.Location;
                panel1.Refresh();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            CurrentFIg = null;
            panel1.Refresh();
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

            else
                foreach (var fig in Figures)
                    if (fig.Touch(gr, e.X, e.Y))
                    {
                        CurrentFIg = fig;
                        P = e.Location;
                        break;
                    }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentFIg != null)
            {
                CurrentFIg.Move(e.X - P.X, e.Y - P.Y);
                P = e.Location;
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            CurrentFIg = null; 
        }
    }
}
