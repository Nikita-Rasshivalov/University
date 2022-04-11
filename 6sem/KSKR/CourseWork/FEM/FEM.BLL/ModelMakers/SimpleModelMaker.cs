using FEM.BLL.Data;
using FEM.BLL.Data.Elements;
using FEM.BLL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FEM.BLL.ModelMakers
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
        public Model3DGroup GenerateGeometry(MeshDTO mesh, MaterialGroup material, IList<NodeLoadDTO> loads)
        {
            Model3DGroup model = new Model3DGroup();
            var modelGeometries = new Model3D[mesh.Elements.Count];

            for (int i = 0; i < mesh.Elements.Count; i++)
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                geometryModel3D.Geometry = GetGeometryFromElement(mesh.Elements[i]);

                MaterialGroup elementMaterial = GetElementMaterial(mesh.Elements[i], material, loads);
                geometryModel3D.Material = elementMaterial;
                geometryModel3D.BackMaterial = elementMaterial;
                modelGeometries[i] = geometryModel3D;
            }

            model.Children = new Model3DCollection(modelGeometries);

            return model;
        }

        /// <summary>
        /// Вычисляет модель с градиентным окрасом. Градиент показывает изменения характеристики элементов сетки
        /// </summary>
        /// <param name="mesh">Сетка, на основе который строится модель</param>
        /// <param name="elementsCharacteristic">Характеристика (Напряжение, деформации и т.д.) конечных элементов в этой сетке</param>
        /// <returns>Модель с градиентным окрасом в зависимости от характеристики</returns>
        public Model3DGroup GenerateCharacteristicModel(MeshDTO mesh, double[] elementsCharacteristic)
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

        private MaterialGroup GetElementMaterial(ElementDTO element, MaterialGroup defaultMaterial, IList<NodeLoadDTO> loads)
        {
            foreach (var node in element.Nodes)
            {
                if (loads.Select(load => load.Id).Contains(node.Id))
                {
                    var load = loads.First(load => load.Id == node.Id);
                    MaterialGroup material = new MaterialGroup();
                    System.Windows.Media.Media3D.Material[] materials = new System.Windows.Media.Media3D.Material[1];
                    if (load.NodeLoadType == Data.NodeLoadType.Force)
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

        private MeshGeometry3D GetGeometryFromElement(ElementDTO element)
        {
            MeshGeometry3D triangleGeometry3D = new MeshGeometry3D();
            if (element is LinearTetrahedronDTO)
            {
                Point3D[] positionsArray = new Point3D[element.Nodes.Length];
                for (int i = 0; i < element.Nodes.Length; i++)
                {
                    positionsArray[i] = new Point3D(element.Nodes[i].X, element.Nodes[i].Y, element.Nodes[i].Z);
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
            }
            else
            {
                throw new Exception("Геометрия такого типа конечного элемента не реализована");
            }
            return triangleGeometry3D;
        }
    }
}
