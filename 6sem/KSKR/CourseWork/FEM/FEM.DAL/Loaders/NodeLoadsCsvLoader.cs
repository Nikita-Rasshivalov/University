using FEM.DAL.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace FEM.DAL.Loaders
{
    /// <summary>
    /// Загрузчик информации о нагрузках в узле из csv файла
    /// </summary>
    public class NodeLoadsCsvLoader : ILoader<IList<NodeLoad>>
    {
        readonly string _pathToCsv;
        readonly bool _hasHeader;
        readonly char _delimiter;
        /// <summary>
        /// Создание класса загрузки информации о нагрузках в узле
        /// </summary>
        /// <param name="pathToCsv">Путь к csv файлу</param>
        /// <param name="hasHeader">Есть ли у данных в csv файле шапка</param>
        /// <param name="delimiter">Разделитель ячеек csv файла</param>
        public NodeLoadsCsvLoader(string pathToCsv, bool hasHeader = true, char delimiter = ';')
        {
            _pathToCsv = pathToCsv;
            _hasHeader = hasHeader;
            _delimiter = delimiter;
        }
        /// <summary>
        /// Возвращает список нагрузок узлов из csv файла
        /// </summary>
        /// <returns>Список нагрузок узлов</returns>
        public IList<NodeLoad> Load()
        {
            try
            {
                IList<NodeLoad> loads = new List<NodeLoad>();
                using (StreamReader sr = new StreamReader(_pathToCsv))
                {
                    if (_hasHeader)
                    {
                        sr.ReadLine();
                    }
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string row = ProcessRow(line);
                        string[] values = row.Split(_delimiter);
                        loads.Add(new NodeLoad(int.Parse(values[0]),
                            (NodeLoadType)Enum.Parse(typeof(NodeLoadType), values[1]),
                            double.Parse(values[2]),
                            double.Parse(values[3]),
                            double.Parse(values[4])
                            ));
                    }
                }
                return loads;
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось загрузить узловые нагрузки. {ex.Message}");
            }
        }

        /// <summary>
        /// Убирает лишние символы из строки csv файла
        /// </summary>
        /// <param name="line">Строка csv файла</param>
        /// <returns>Обработанная строка csv файла</returns>
        private string ProcessRow(string line)
        {
            return line.Trim(_delimiter).Replace('"', ' ');
        }
    }
}
