using CourseWork.BLL.Interfaces;
using CourseWork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CourseWork.BLL.Services.ElementLoaders.Txt
{
    public class TxtMaterialsProvider : BaseTxtProvider, IMaterialsProvider
    {
        private const string Separator = "\t";
        private readonly string _nodeLoadsPath;

        public TxtMaterialsProvider(string nodeLoadsPath)
        {
            _nodeLoadsPath = nodeLoadsPath;
        }

        public List<DetailMaterial> GetMaterials()
        {
            var materials = new List<DetailMaterial>();
            var rows = GetValuesFromTextFile(_nodeLoadsPath, Separator, 1);
            foreach (var row in rows)
            {
                if (row.Length < 6)
                {
                    throw new InvalidOperationException($"Некорректный формат материала '{string.Join(" ", row)}.");
                }
                if (!string.IsNullOrEmpty(row[0])
                    && double.TryParse(row[1], out var youngModulus)
                    && double.TryParse(row[2], out var poissonCoefficient))
                {
                    materials.Add(new DetailMaterial(row[0], youngModulus, poissonCoefficient, GetColor(row.Skip(3).ToArray())));
                }
                else
                {
                    throw new InvalidOperationException($"Не удалось загрузить материал из '{string.Join(" ", row)}'.");
                }
            }

            return materials;
        }

        private Color GetColor(params string[] rowValues)
        {
            var components = new List<byte>();
            foreach(var value in rowValues)
            {
                if(!byte.TryParse(value, out var component))
                {
                    throw new InvalidOperationException($"Не удалось преобразовать компонетнт цвета '{value}'.");
                }

                components.Add(component);
            }

            return components.Count switch
            {
                3 => Color.FromRgb(components[0], components[1], components[2]),
                4 => Color.FromArgb(components[0], components[1], components[2], components[3]),
                _ => throw new InvalidOperationException($"Цвет не может быть преобразован из '{components.Count}' компонетнов")
            };
        }
    }
}
