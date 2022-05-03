using CourseWork.BLL.Models.GridSettings;

namespace CourseWork.BLL.Interfaces
{
    public interface IGridSerializer
    {
        void Serialize(GridSettings grid, string path);

        GridSettings Deserialize(string path);
    }
}
