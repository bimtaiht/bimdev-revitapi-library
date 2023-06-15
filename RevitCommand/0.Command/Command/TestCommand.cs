using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Form;
using System;
using System.Linq;
using System.Collections.Generic;
using Utility;
using Autodesk.Revit.DB.Plumbing;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : RevitCommand
    {
        public override void Execute()
        {
            ElementId systemTypeId = null!;
            ElementId pipeTypeId = null!;
            ElementId levelId = null!;

            List<XYZ> points = null!;
            var splitPoint = XYZ.Zero;

            Pipe pipe1 = null!;
            Pipe pipe2 = null!;
            Pipe pipe3 = null!;

            Pipe pipe = null!;

            var pipes = PipeUtil.CreatePipes(systemTypeId, pipeTypeId, levelId, points);

            var elbowFitting = pipe1.ConnectTo(pipe2);
            var teeFitting = pipe1.ConnectTo(pipe2, pipe3);

            pipes.AutoConnect();

            var splitPipes = pipe.Split(splitPoint);
        }
    }
}