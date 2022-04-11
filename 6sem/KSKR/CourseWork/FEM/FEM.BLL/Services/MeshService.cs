using FEM.BLL.Data;
using FEM.BLL.Data.Elements;
using FEM.DAL.Data;
using FEM.DAL.Data.Elements;
using FEM.DAL.Loaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FEM.BLL.Services
{
    /// <summary>
    /// Сервис для получения сущности сетки
    /// </summary>
    public class MeshService
    {
        private readonly ILoader<Mesh> _meshLoader;
        /// <summary>
        /// Создание сервиса для получения сущности сетки
        /// </summary>
        /// <param name="meshLoader">Загрузчик сетки</param>
        public MeshService(ILoader<Mesh> meshLoader)
        {
            _meshLoader = meshLoader;
        }

        /// <summary>
        /// Конечно-элементная сетка
        /// </summary>
        public MeshDTO Mesh
        {
            get
            {
                var mesh = _meshLoader.Load();
                IList<NodeDTO> nodes = ConvertNodes(mesh.Nodes);
                IList<ElementDTO> elements = ConvertElements(mesh.Elements, nodes);
                return new MeshDTO(nodes, elements);
            }
        }

        /// <summary>
        /// Выполняет преобразование из конечных элементов DAL слоя к конечным элементам BLL слоя
        /// </summary>
        /// <param name="elements">Список конечных элементов, которые необходимо преобразовать</param>
        /// <param name="nodes">Список узлов конечных элементов, ссылки на которые будут храниться в этих конечных элементах</param>
        /// <returns>Список преобразованных конечных элементов</returns>
        private IList<ElementDTO> ConvertElements(IList<Element> elements, IList<NodeDTO> nodes)
        {
            IList<ElementDTO> convertedElements = new List<ElementDTO>();
            foreach (var element in elements)
            {
                if (element is LinearTetrahedron)
                {
                    convertedElements.Add(new LinearTetrahedronDTO(
                        element.Id,
                        element.NodeIndexes.Select(ind => nodes.FirstOrDefault(n => n.Id == ind)).ToArray(),
                        Color.Gray
                        ));
                }
                else
                {
                    throw new Exception("Такой тип конечного элемента не поддерживается");
                }
            }
            return convertedElements;
        }

        /// <summary>
        /// Преобразует узлы DAL слоя в узлы BLL слоя
        /// </summary>
        /// <param name="nodes">Узлы, которые необходимо преобразовать</param>
        /// <returns>Список преобразованных узлов</returns>
        private IList<NodeDTO> ConvertNodes(IList<Node> nodes)
        {
            IList<NodeDTO> convertedNodes = new List<NodeDTO>();
            foreach (var node in nodes)
            {
                convertedNodes.Add(new NodeDTO(
                    node.Id,
                    node.X,
                    node.Y,
                    node.Z
                    ));
            }
            return convertedNodes;
        }
    }
}
