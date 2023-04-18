using Autodesk.Revit.DB;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity.ShareParameterFactoryNS
{
    public static class ConfigUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static DefinitionFile GetDefinitionFile(this Config q)
        {
            var app = revitData.Application;

            return app.OpenSharedParameterFile();
        }

        public static string GetDefinitionGroupName(this Config q)
        {
            return "Group1";
        }

        public static ParameterType GetParameterType(this Config q)
        {
            return ParameterType.Number;
        }

        public static BuiltInParameterGroup GetParameterGroup(this Config q)
        {
            return BuiltInParameterGroup.PG_DATA;
        }
    }
}
