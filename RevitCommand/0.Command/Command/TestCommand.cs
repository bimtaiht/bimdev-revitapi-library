using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Model.Form;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : RevitCommand
    {
        public override void Execute()
        {
            var form = new Form.Form();
            form.ShowDialog();

            //var form = new Form.FormTemplate();
            //form.ShowDialog();
        }
    }
}