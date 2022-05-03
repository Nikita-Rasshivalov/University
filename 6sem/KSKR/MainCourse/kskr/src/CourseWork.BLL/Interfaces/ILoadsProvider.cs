using CourseWork.BLL.Models;
using System.Collections.Generic;

namespace CourseWork.BLL.Interfaces
{
    public interface ILoadsProvider
    {
        List<NodeLoad> GetNodeLoads();
    }
}
