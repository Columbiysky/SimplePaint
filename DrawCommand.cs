using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    class DrawCommand : ICommand
    {
        Figure fig;
        Graphics gr;

        public DrawCommand(Figure f, Graphics gr_)
        {
            fig = f;
            gr = gr_;
        }

        public void Execute()
        {
            fig.Draw(gr);
        }

        public void Undo()
        {
            MessageBox.Show("КУДА???");
        }
    }


}
