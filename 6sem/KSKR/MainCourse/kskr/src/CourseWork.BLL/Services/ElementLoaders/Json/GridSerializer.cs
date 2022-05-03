using CourseWork.BLL.Interfaces;
using Newtonsoft.Json;
using System.IO;
using CourseWork.BLL.Models.GridSettings;

namespace CourseWork.BLL.Services.ElementLoaders.Json
{
    public class GridSerializer : IGridSerializer
    {
        public void Serialize(GridSettings grid, string path)
        {
            var json = JsonConvert.SerializeObject(
                grid,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, SerializationBinder = new GridSettingsBinder() });
            using var writer = new StreamWriter(path);
            writer.Write(json);
        }

        public GridSettings Deserialize(string path)
        {
            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<GridSettings>(
                json,
                new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Objects,
                    SerializationBinder = new GridSettingsBinder()
                });
        }
    }
}
