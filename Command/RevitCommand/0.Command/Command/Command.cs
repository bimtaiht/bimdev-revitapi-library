using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Form;
using System;
using System.Linq;
using System.Collections.Generic;
using Model.Entity;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class Command : RevitCommand
    {
        public override void Execute()
        {
            
        }

        public override RibbonConfig? RibbonConfig => new()
        {
            Tab = "<tab name>",
            Panel = "<panel name>",
            Name = "<command name>",
            IconPath = "Resource/Icon/addin.ico"
        };
    }
}