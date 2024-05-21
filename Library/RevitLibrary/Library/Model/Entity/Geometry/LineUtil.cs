using System;
using Autodesk.Revit.DB;

namespace Utility
{
    public static partial class LineUtil
    {
        public static Line Transform(this Line line, Func<XYZ, XYZ> handleEndPoint)
        {
            return Line.CreateBound(handleEndPoint(line.GetEndPoint(0)), handleEndPoint(line.GetEndPoint(1)));
        }
    }
}
