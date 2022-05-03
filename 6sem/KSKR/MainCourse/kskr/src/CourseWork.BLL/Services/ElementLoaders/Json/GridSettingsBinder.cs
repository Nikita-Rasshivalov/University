using CourseWork.Common.Models;
using CourseWork.BLL.Models.GridSettings;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork.BLL.Services.ElementLoaders.Json
{
    public class GridSettingsBinder : ISerializationBinder
    {
        public List<Type> KnownTypes { get; set; } 

        public GridSettingsBinder()
        {
            KnownTypes = GetKnownTypes();
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return KnownTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }

        private List<Type> GetKnownTypes()
        {
            var figureType = typeof(IFigure);
            var types = figureType.Assembly
                .GetTypes()
                .Where(t => figureType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();
            types.Add(typeof(GridSettings));
            types.Add(typeof(Point));

            return types;
        }
    }
}
