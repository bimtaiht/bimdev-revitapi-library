using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Form;
using System;
using System.Collections.Generic;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : RevitCommand
    {
        public override void Execute()
        {
            var form = new Form.Form();
            form.DataContext = new
            {
                Data = new List<string> {"Hello", "Worl", "Ngu64n" }
            };

            form.ShowDialog();

            //var form = new Form.FormTemplate();
            //form.ShowDialog();
        }
    }
}