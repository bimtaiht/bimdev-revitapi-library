using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : RevitCommand
    {
        public override void Execute()
        {

        }
    }
}