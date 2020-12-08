﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    interface IStrategy
    {
        void Select(Graphics gr, List<Figure> figures, Group group, Manipulator manipulator, float x, float y);
    }
}
