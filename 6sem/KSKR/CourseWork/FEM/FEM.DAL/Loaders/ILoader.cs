namespace FEM.DAL.Loaders
{
    /// <summary>
    /// Интерфейс, добавляющий функционал загрузки определенного объекта
    /// </summary>
    /// <typeparam name="T">Тип загружаемого объекта</typeparam>
    public interface ILoader<T>
    {
        /// <summary>
        /// Загрузка объекта
        /// </summary>
        /// <returns>Загруженный объект</returns>
        T Load();
    }
}
