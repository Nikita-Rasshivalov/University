using FEM.BLL.Data;
using FEM.DAL.Data;
using FEM.DAL.Loaders;
using System.Collections.Generic;

namespace FEM.BLL.Services
{
    /// <summary>
    /// Сервис для получения сущностей узловых нагрузок
    /// </summary>
    public class NodeLoadService
    {
        private readonly ILoader<IList<NodeLoad>> _nodeLoadsLoader;
        /// <summary>
        /// Создание сервиса для получения сущностей узловых нагрузок
        /// </summary>
        /// <param name="nodeLoadsLoader">Загрузчик узловых нагрузок</param>
        public NodeLoadService(ILoader<IList<NodeLoad>> nodeLoadsLoader)
        {
            _nodeLoadsLoader = nodeLoadsLoader;
        }

        /// <summary>
        /// Список узловых нагрузок
        /// </summary>
        public IList<NodeLoadDTO> NodeLoads
        {
            get
            {
                IList<NodeLoadDTO> nodeLoadsDTO = new List<NodeLoadDTO>();
                var nodeLoads = _nodeLoadsLoader.Load();
                foreach (var load in nodeLoads)
                {
                    nodeLoadsDTO.Add(new NodeLoadDTO(load.Id,
                        load.NodeLoadType == DAL.Data.NodeLoadType.Fixed ? Data.NodeLoadType.Fixed : Data.NodeLoadType.Force,
                        load.NodeLoadType == DAL.Data.NodeLoadType.Fixed ? null : new NodeForceVector(load.XForce, load.YForce, load.ZForce)));
                }
                return nodeLoadsDTO;
            }
        }
    }
}
