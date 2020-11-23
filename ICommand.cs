using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    interface ICommand
    {
        void Execute();
        void Undo();
    }
}
