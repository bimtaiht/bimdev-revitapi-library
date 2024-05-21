using Autodesk.Revit.DB;
using System.Linq;

namespace Utility
{
    public static class ParameterUtil
    {
        public static void CopyParameterValue(this Parameter targetParameter, Parameter sourceParameter)
        {
            targetParameter.SetValue(sourceParameter.GetValue());
        }

        public static void CopyParameterValue(this Element targetElement, Element sourceElement, BuiltInParameter builtInParameter)
        {
            targetElement.get_Parameter(builtInParameter).CopyParameterValue(sourceElement.get_Parameter(builtInParameter));
        }

        public static void CopyParameterValue(this Element targetElement, Element sourceElement, string parameterName)
        {
            targetElement.LookupParameter(parameterName).CopyParameterValue(sourceElement.LookupParameter(parameterName));
        }

        public static void SetValue(this Parameter parameter, object? value)
        {
            if (parameter.IsReadOnly) return;
            if (value is null) return;

            switch (parameter.StorageType)
            {
                case StorageType.Integer:
                    parameter.Set((int)value);
                    break;
                case StorageType.Double:
                    parameter.Set((double)value);
                    break;
                case StorageType.String:
                    parameter.Set((string)value);
                    break;
                case StorageType.ElementId:
                    parameter.Set((ElementId)value);
                    break;
            }
        }

        public static object? GetValue(this Parameter parameter)
        {
            return parameter.StorageType switch
            {
                StorageType.String => parameter.AsString(),
                StorageType.Integer => parameter.AsInteger(),
                StorageType.Double => parameter.AsDouble(),
                StorageType.ElementId => parameter.AsElementId(),
                _ => null,
            };
        }

        public static void CopyAllValues(Element sourceElement, Element targetElement)
        {
            var bic = targetElement.Category.GetBuiltInCategory();

            var isSource_UnconnectedHeight_ReadOnly = false;

            var sourceParameters = sourceElement.ParametersMap.Cast<Parameter>().Where(x =>
            {
                if (bic == BuiltInCategory.OST_Walls)
                {
                    if ((BuiltInParameter)x.Id.IntegerValue == BuiltInParameter.WALL_USER_HEIGHT_PARAM)
                    {
                        isSource_UnconnectedHeight_ReadOnly = x.IsReadOnly;
                    }
                }
                return !x.IsReadOnly;
            }).ToList();

            Parameter? needAddParameter = null;
            var targetParameters = targetElement.ParametersMap.Cast<Parameter>().Where(x =>
            {
                if (bic == BuiltInCategory.OST_Walls)
                {
                    if (isSource_UnconnectedHeight_ReadOnly)
                    {
                        if ((BuiltInParameter)x.Id.IntegerValue == BuiltInParameter.WALL_USER_HEIGHT_PARAM)
                        {
                            return false;
                        }
                        if ((BuiltInParameter)x.Id.IntegerValue == BuiltInParameter.WALL_TOP_OFFSET)
                        {
                            needAddParameter = x;
                        }
                    }
                    else
                    {
                        if ((BuiltInParameter)x.Id.IntegerValue == BuiltInParameter.WALL_USER_HEIGHT_PARAM)
                        {
                            needAddParameter = x;
                        }
                        if ((BuiltInParameter)x.Id.IntegerValue == BuiltInParameter.WALL_TOP_OFFSET)
                        {
                            return false;
                        }
                    }
                }
                return !x.IsReadOnly;
            }).ToList();

            if (needAddParameter != null)
            {
                targetParameters.Add(needAddParameter);
            }

            foreach (var parameter in targetParameters)
            {
                var sourceParamater = sourceParameters.First(x => x.Id == parameter.Id);
                parameter.SetValue(sourceParamater);
            }
        }
    }
}
