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
            var instance = sel.PickElement<ImportInstance>();
            var cadType = instance.GetTypeId().GetElement<CADLinkType>();
            var geometry = cadType.get_Geometry(new Options());
            foreach (var geoIns in geometry.Cast<GeometryInstance>().ToList())
            {
                var styleId = geoIns.Symbol.GetEntitySchemaGuids();
            } 
        }
    }
}