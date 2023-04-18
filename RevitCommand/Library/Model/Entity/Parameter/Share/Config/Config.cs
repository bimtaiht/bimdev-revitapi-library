using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity.ShareParameterFactoryNS
{
    public class Config
    {
        private DefinitionFile? definitionFile;
        public DefinitionFile DefinitionFile => this.definitionFile ??= this.GetDefinitionFile();

        public string? Name { get; set; }

        private string? definitionGroupName;
        public string DefinitionGroupName
        {
            get => this.definitionGroupName ??= this.GetDefinitionGroupName();
            set => this.definitionGroupName = value;
        }

        private ParameterType? parameterType;
        public ParameterType ParameterType
        {
            get => this.parameterType ??= this.GetParameterType();
            set => this.parameterType = value;
        }

        private BuiltInParameterGroup? parameterGroup;
        public BuiltInParameterGroup ParameterGroup
        {
            get => this.parameterGroup ??= this.GetParameterGroup();
            set => this.parameterGroup = value;
        }

        public List<BuiltInCategory>? BuiltInCategories { get; set; }
    }
}
