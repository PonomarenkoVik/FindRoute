using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RoutFinding
{
    class RouteShowEventArgs : EventArgs
    {
        public RouteShowEventArgs(Cell[,] field, Point curPoint, Point initPoint, Point finishPoint )
        {
            Field = field;
            CurPoint = curPoint;
            InitPoint = initPoint;
            FinishPoint = finishPoint;
        }

        public Point FinishPoint { get; set; }

        public Point InitPoint { get; set; }

        public Point CurPoint { get; set; }

        public Cell[,] Field { get; set; }
    }
}
