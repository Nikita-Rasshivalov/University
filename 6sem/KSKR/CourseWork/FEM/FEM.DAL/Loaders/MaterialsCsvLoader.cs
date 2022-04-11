using FEM.DAL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace FEM.DAL.Loaders
{
    /// <summary>
    /// Класс для загрузки списка материалов из csv файла
    /// </summary>
    public class MaterialsCsvLoader : ILoader<IList<Material>>
    {
        readonly string _pathToCsv;
        readonly bool _hasHeader;
        readonly char _delimiter;
        /// <summary>
        /// Создание класса загрузки
        /// </summary>
        /// <param name="pathToCsv">Путь к csv файлу</param>
        /// <param name="hasHeader">Есть ли у данных в csv файле шапка</param>
        /// <param name="delimiter">Разделитель ячеек csv файла</param>
        public MaterialsCsvLoader(string pathToCsv, bool hasHeader = true, char delimiter = ';')
        {
            _pathToCsv = pathToCsv;
            _hasHeader = hasHeader;
            _delimiter = delimiter;
        }
        /// <summary>
        /// Загрузка списка материалов из csv файла
        /// </summary>
        /// <returns>Список материалов</returns>
        public IList<Material> Load()
        {
            try
            {
                IList<Material> materials = new List<Material>();
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
                        (byte a, byte r, byte g, byte b) = GetARGBFromString(values[2]);
                        materials.Add(new Material(int.Parse(values[0]),
                            values[1],
                            Color.FromArgb(a, r, g, b),
                            double.Parse(values[3]),
                            double.Parse(values[4]))
                            );
                    }
                }
                return materials;
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось загрузить материалы. {ex.Message}");
            }
        }

        private (byte, byte, byte, byte) GetARGBFromString(string color)
        {
            color = color.Trim();
            string a = color.Substring(0, 2);
            string r = color.Substring(2, 2);
            string g = color.Substring(4, 2);
            string b = color.Substring(6, 2);
            return (Convert.ToByte(a, 16), Convert.ToByte(r, 16), Convert.ToByte(g, 16), Convert.ToByte(b, 16));
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
