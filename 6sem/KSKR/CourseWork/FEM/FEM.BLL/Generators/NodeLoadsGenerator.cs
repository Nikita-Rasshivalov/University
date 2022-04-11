using FEM.BLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FEM.BLL.Generators
{
    /// <summary>
    /// Генератор узловых нагрузок
    /// </summary>
    public class NodeLoadsGenerator
    {
        public enum Face
        {
            Top,
            Bottom,
            Left,
            Right,
            Front,
            Back
        }

        /// <summary>
        /// Генерация списка узловых нагрузок, как если бы на деталь давил пресс
        /// </summary>
        /// <param name="mesh">Сетка для которой необходимо сформировать вектор узловых нагрузок</param>
        /// <param name="pressureValue">Давление пресса в тоннах</param>
        /// <param name="area">Площадь поверхности детали</param>
        /// <returns>Список узловых нагрузок детали</returns>
        public IList<NodeLoadDTO> GeneratePressLoads(MeshDTO mesh, double pressureValue, double area)
        {
            double pascales = pressureValue * 1000 * 9.8 / area;

            var topNodes = mesh.Nodes.Where(node => node.Y == mesh.Nodes.Select(n => n.Y).Max());
            var bottomNodes = mesh.Nodes.Where(node => node.Y == mesh.Nodes.Select(n => n.Y).Min());

            var oneNodePascal = pascales / topNodes.Count();

            IList<NodeLoadDTO> loads = new List<NodeLoadDTO>();

            foreach (var node in topNodes)
            {
                loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, new NodeForceVector(0, -oneNodePascal, 0)));
            }

            foreach (var node in bottomNodes)
            {
                loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, new NodeForceVector(0, 0, 0)));
            }

            return loads;
        }

        /// <summary>
        /// Генерирует силовые нагрузки для определенной поверхности сетки
        /// </summary>
        /// <param name="face">Поверхность сетки</param>
        /// <param name="mesh">Сетка</param>
        /// <param name="forceVector">Вектор узловых нагрузок</param>
        /// <param name="isSurfaceForce">Является ли сила поверхностной или действует на каждый узел</param>
        /// <returns>Список узловых нагрузок детали</returns>
        public IList<NodeLoadDTO> GenerateForceLoads(Face face, MeshDTO mesh, NodeForceVector forceVector, bool isSurfaceForce = true)
        {
            IList<NodeLoadDTO> loads = new List<NodeLoadDTO>();
            int nodesCount;
            switch (face)
            {
                case Face.Top:
                    var topNodes = mesh.Nodes.Where(node => node.Y == mesh.Nodes.Select(n => n.Y).Max());
                    nodesCount = topNodes.Count();
                    if (isSurfaceForce)
                    {
                        forceVector = new NodeForceVector(forceVector.X / nodesCount, forceVector.Y / nodesCount, forceVector.Z / nodesCount);
                    }
                    foreach (var node in topNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, forceVector));
                    }
                    return loads;
                case Face.Bottom:
                    var bottomNodes = mesh.Nodes.Where(node => node.Y == mesh.Nodes.Select(n => n.Y).Min());
                    nodesCount = bottomNodes.Count();
                    if (isSurfaceForce)
                    {
                        forceVector = new NodeForceVector(forceVector.X / nodesCount, forceVector.Y / nodesCount, forceVector.Z / nodesCount);
                    }
                    foreach (var node in bottomNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, forceVector));
                    }
                    return loads;
                case Face.Left:
                    var leftNodes = mesh.Nodes.Where(node => node.X == mesh.Nodes.Select(n => n.X).Min());
                    nodesCount = leftNodes.Count();
                    if (isSurfaceForce)
                    {
                        forceVector = new NodeForceVector(forceVector.X / nodesCount, forceVector.Y / nodesCount, forceVector.Z / nodesCount);
                    }
                    foreach (var node in leftNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, forceVector));
                    }
                    return loads;
                case Face.Right:
                    var rightNodes = mesh.Nodes.Where(node => node.X == mesh.Nodes.Select(n => n.X).Max());
                    nodesCount = rightNodes.Count();
                    if (isSurfaceForce)
                    {
                        forceVector = new NodeForceVector(forceVector.X / nodesCount, forceVector.Y / nodesCount, forceVector.Z / nodesCount);
                    }
                    foreach (var node in rightNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, forceVector));
                    }
                    return loads;
                case Face.Front:
                    var frontNodes = mesh.Nodes.Where(node => node.Z == mesh.Nodes.Select(n => n.Z).Max());
                    nodesCount = frontNodes.Count();
                    if (isSurfaceForce)
                    {
                        forceVector = new NodeForceVector(forceVector.X / nodesCount, forceVector.Y / nodesCount, forceVector.Z / nodesCount);
                    }
                    foreach (var node in frontNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, forceVector));
                    }
                    return loads;
                case Face.Back:
                    var backNodes = mesh.Nodes.Where(node => node.Z == mesh.Nodes.Select(n => n.Z).Min());
                    nodesCount = backNodes.Count();
                    if (isSurfaceForce)
                    {
                        forceVector = new NodeForceVector(forceVector.X / nodesCount, forceVector.Y / nodesCount, forceVector.Z / nodesCount);
                    }
                    foreach (var node in backNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Force, forceVector));
                    }
                    return loads;
                default:
                    throw new Exception("Данная позиция не реализована");
            }
        }

        /// <summary>
        /// Генерирует закрепления для определенной поверхности сетки
        /// </summary>
        /// <param name="face">Поверхность сетки</param>
        /// <param name="mesh">Сетка</param>
        /// <returns>Список закреплений детали</returns>
        public IList<NodeLoadDTO> GenerateFixationLoads(Face face, MeshDTO mesh)
        {
            IList<NodeLoadDTO> loads = new List<NodeLoadDTO>();
            switch (face)
            {
                case Face.Top:
                    var topNodes = mesh.Nodes.Where(node => node.Y == mesh.Nodes.Select(n => n.Y).Max());
                    foreach (var node in topNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, null));
                    }
                    return loads;
                case Face.Bottom:
                    var bottomNodes = mesh.Nodes.Where(node => node.Y == mesh.Nodes.Select(n => n.Y).Min());
                    foreach (var node in bottomNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, null));
                    }
                    return loads;
                case Face.Left:
                    var leftNodes = mesh.Nodes.Where(node => node.X == mesh.Nodes.Select(n => n.X).Min());
                    foreach (var node in leftNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, null));
                    }
                    return loads;
                case Face.Right:
                    var rightNodes = mesh.Nodes.Where(node => node.X == mesh.Nodes.Select(n => n.X).Max());
                    foreach (var node in rightNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, null));
                    }
                    return loads;
                case Face.Front:
                    var frontNodes = mesh.Nodes.Where(node => node.Z == mesh.Nodes.Select(n => n.Z).Max());
                    foreach (var node in frontNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, null));
                    }
                    return loads;
                case Face.Back:
                    var backNodes = mesh.Nodes.Where(node => node.Z == mesh.Nodes.Select(n => n.Z).Min());
                    foreach (var node in backNodes)
                    {
                        loads.Add(new NodeLoadDTO(node.Id, NodeLoadType.Fixed, null));
                    }
                    return loads;
                default:
                    throw new Exception("Данная позиция не реализована");
            }
        }
    }
}
