using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// DAO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDAO<T>
    {
        int Create(T element);
        bool Update(T element);
        bool Delete(T  element);
        T GetById(int id);
        List<T> GetAll();
    }
}
