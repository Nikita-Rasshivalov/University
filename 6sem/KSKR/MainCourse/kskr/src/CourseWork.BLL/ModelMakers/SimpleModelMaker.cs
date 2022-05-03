using CourseWork.BLL.Models;
using CourseWork.BLL.Tools;
using CourseWork.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CourseWork.BLL.ModelMakers
{
    /// <summary>
    /// Стандартный создатель геометрии сетки
    /// </summary>
    public class SimpleModelMaker : IMeshModelMaker
    {
        private readonly GradientMaker _gradientMaker;
        public SimpleModelMaker(GradientMaker gradientMaker)
        {
            _gradientMaker = gradientMaker;
        }
        /// <summary>
        /// Метод для создания стандартной геометрии сетки
        /// </summary>
        /// <param name="mesh">Конечно-элементная сетка</param>
        /// <param name="material">Материал детали</param>
        /// <param name="loads">Нагрузки детали</param>
        /// <returns>Геометрия сетки</returns>=
        public Model3DGroup GenerateGeometry(FiniteElementModel model, MaterialGroup material = default)
        {
            Model3DGroup mesh = new Model3DGroup();
            var modelGeometries = new List<Model3D>();

            foreach (var element in model.Elements)
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                geometryModel3D.Geometry = GetGeometryFromElement(element);

                MaterialGroup elementMaterial = material ?? GetMaterial();
                geometryModel3D.Material = elementMaterial;
                geometryModel3D.BackMaterial = elementMaterial;
                modelGeometries.Add(geometryModel3D);
            }

            foreach (var load in model.NodeLoads)
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                var node = model.Nodes.First(n => n.Id == load.NodeId);
                geometryModel3D.Geometry = GetPointGeometry(node.Position);

                var color = load.LoadType switch
                {
                    LoadType.Fixed => Color.FromRgb(24, 4, 180),
                    LoadType.Force => Color.FromRgb(203, 11, 32),
                    _ => throw new InvalidOperationException($"Тип нагрузки '{load.LoadType}' не поддерживется.")
                };
                geometryModel3D.Material = GetMaterial(color);
                geometryModel3D.BackMaterial = GetMaterial(color);
                modelGeometries.Add(geometryModel3D);
            }

            mesh.Children = new Model3DCollection(modelGeometries);

            return mesh;
        }

        /// <summary>
        /// Вычисляет модель с градиентным окрасом. Градиент показывает изменения характеристики элементов сетки
        /// </summary>
        /// <param name="mesh">Сетка, на основе который строится модель</param>
        /// <param name="elementsCharacteristic">Характеристика (Напряжение, деформации и т.д.) конечных элементов в этой сетке</param>
        /// <returns>Модель с градиентным окрасом в зависимости от характеристики</returns>
        public Model3DGroup GenerateCharacteristicModel(FiniteElementModel mesh, double[] elementsCharacteristic)
        {
            double minCharacteristic = elementsCharacteristic.Min();
            double maxCharacteristic = elementsCharacteristic.Max();

            Model3DGroup model = new Model3DGroup();
            var modelGeometries = new Model3D[mesh.Elements.Count];

            for (int i = 0; i < mesh.Elements.Count; i++)
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                geometryModel3D.Geometry = GetGeometryFromElement(mesh.Elements[i]);

                MaterialGroup elementMaterial = GetMaterialRGBGradientValue(minCharacteristic, maxCharacteristic, elementsCharacteristic[i]);

                geometryModel3D.Material = elementMaterial;
                geometryModel3D.BackMaterial = elementMaterial;
                modelGeometries[i] = geometryModel3D;
            }

            model.Children = new Model3DCollection(modelGeometries);

            return model;
        }

        /// <summary>
        /// Метод возвращает материал на основе градиента
        /// </summary>
        /// <param name="minValue">Минимальное значение параметра для градиента</param>
        /// <param name="maxValue">Максимальное значение параметра для градиента</param>
        /// <param name="value">Текущее значение параметра для градиента</param>
        /// <returns></returns>
        private MaterialGroup GetMaterialRGBGradientValue(double minValue, double maxValue, double value)
        {
            (byte R, byte G, byte B) = _gradientMaker.GetRGBGradientValue(minValue, maxValue, value);
            MaterialGroup material = new MaterialGroup();
            System.Windows.Media.Media3D.Material[] materials = new System.Windows.Media.Media3D.Material[1];
            materials[0] = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x99, R, G, B)));
            material.Children = new MaterialCollection(materials);

            return material;
        }

    /*    private MaterialGroup GetElementMaterial(Element element, MaterialGroup defaultMaterial, IList<NodeLoad> loads)
        {
            foreach (var node in element.Nodes)
            {
                if (loads.Select(load => load.Id).Contains(node.Id))
                {
                    var load = loads.First(load => load.Id == node.Id);
                    MaterialGroup material = new MaterialGroup();
                    System.Windows.Media.Media3D.Material[] materials = new System.Windows.Media.Media3D.Material[1];
                    if (load.LoadType == LoadType.Force)
                    {
                        materials[0] = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x99, 0xe8, 0x54, 0x8c)));
                    }
                    else
                    {
                        materials[0] = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x99, 0x53, 0xd2, 0xf5)));
                    }
                    material.Children = new MaterialCollection(materials);
                    return material;
                }
            }
            return defaultMaterial;
        }
*/
        private MaterialGroup GetMaterial(Color? color = default)
        {
            var material = new MaterialGroup();
            var materials = new Material[] { new DiffuseMaterial(new SolidColorBrush( color ?? Color.FromArgb(0x99, 0x53, 0xd2, 0xf5))) };
            material.Children = new MaterialCollection(materials);
            return material;
        }

        private MeshGeometry3D GetGeometryFromElement(Element element)
        {
            MeshGeometry3D triangleGeometry3D = new MeshGeometry3D();

            Point3D[] positionsArray = new Point3D[element.Nodes.Count];
            for (int i = 0; i < element.Nodes.Count; i++)
            {
                positionsArray[i] = new Point3D(element.Nodes[i].Position.X, element.Nodes[i].Position.Y, element.Nodes[i].Position.Z);
            }
            Point3DCollection positions = new Point3DCollection(positionsArray);

            int[] triangleIndeces = new int[]
            {
                    0, 3, 1,
                    0, 1, 2,
                    0, 2, 3,
                    1, 2, 3
            };
            Int32Collection indeces = new Int32Collection(triangleIndeces);

            triangleGeometry3D.Positions = positions;
            triangleGeometry3D.TriangleIndices = indeces;

            return triangleGeometry3D;
        }

        private MeshGeometry3D GetPointGeometry(Point position)
        {
            MeshGeometry3D triangleGeometry3D = new MeshGeometry3D();

            Point3D[] positionsArray = new Point3D[3];
            positionsArray[0] = new Point3D(position.X + 0.1 * position.X, position.Y + 0.1 * position.Y, position.Z + 0.1 * position.Z);
            positionsArray[1] = new Point3D(position.X + 0.55 * position.X, position.Y + 0.55 * position.Y, position.Z + 0.25 * position.Z);
            positionsArray[2] = new Point3D(position.X + 0.22 * position.X, position.Y + 0.12 * position.Y, position.Z + 0.12 * position.Z);
            Point3DCollection positions = new Point3DCollection(positionsArray);

            int[] triangleIndeces = new int[]
            {
                    0, 2, 1,
            };
            Int32Collection indeces = new Int32Collection(triangleIndeces);

            triangleGeometry3D.Positions = positions;
            triangleGeometry3D.TriangleIndices = indeces;

            return triangleGeometry3D;
        }
    }
}
