using System;
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
        Group group = new Group(); //che-to tam
        Dictionary<string, IFigureCreator> Tools = new Dictionary<string, IFigureCreator>(); //Figure drawing tools
        IFigureCreator FigureCreator;
        Figure CurrentFig;
        Point P;
        Graphics gr;

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
                group.Clear();
                manipulator.Clear(gr);
                pictureBox1.Refresh();
            };

            EllipseButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                FigureCreator = Tools[EllipseButton.Text];
                group.Clear();
                manipulator.Clear(gr);
                pictureBox1.Refresh();
            };

            SelectButton.Click += (s, a) => {
                if (CurrentFig != null) CurrentFig = null;
                FigureCreator = Tools[""];
                group.Clear();
                manipulator.Clear(gr);
                pictureBox1.Refresh();
            };

            ClearButton.Click += (s, a) =>
            {
                if (CurrentFig != null) CurrentFig = null;
                Figures.Clear();
                group.Clear();
                manipulator.Clear(gr);
                pictureBox1.Refresh();
            };
            NewFigBtn.Click += (s, a) =>
            {
                 if (manipulator.fig == null) return;
                 GroupCreator grCreator = new GroupCreator();
                 grCreator.CopyCreated(group);
                 Tools.Add($"Group {comboBox1.Items.Count + 1}",grCreator);
                 comboBox1.Items.Add($"Group {comboBox1.Items.Count + 1}");
            };
            DrawGroupBtn.Click += (s, a) => 
            {
                if (comboBox1.SelectedIndex == -1)
                    return;
                FigureCreator = Tools[comboBox1.SelectedItem.ToString()];
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
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (FigureCreator != null)
            {
                var figure = FigureCreator.Create(e.X, e.Y/*, 50, 50*/);
                figure.Draw(gr);
                Figures.Add(figure);
            }

            else
            {
                if (manipulator.Touch(gr, e.X, e.Y)) return;
                foreach (var fig in Figures) {
                    if (fig.Touch(gr, e.X, e.Y))
                    {
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            group.Add(fig);
                            manipulator.Attach(group);
                            P = e.Location;
                            break;
                        }

                        else
                        {
                            manipulator.Attach(fig);
                            P = e.Location;
                            break;
                        }
                    }
                    else
                        manipulator.Clear(gr);

                }

            }
                
        }

        [DebuggerStepThrough]
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (manipulator.fig != null)
                    manipulator.Drag(e.X - P.X, e.Y - P.Y);
            P = e.Location;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X - P.X != 0 && e.Y - P.Y != 0)
                manipulator.Clear(gr);
        }
    }
}
