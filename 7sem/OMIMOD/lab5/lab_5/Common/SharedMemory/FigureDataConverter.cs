using System.Runtime.Serialization.Formatters.Binary;

namespace Common.SharedMemory
{
    public class FigureDataConverter
    {
        public static byte[] ObjectToByteArray(ImageFigures figure)
        {
            if (figure == null) throw new ArgumentNullException(nameof(figure));

            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
#pragma warning disable SYSLIB0011 // Тип или член устарел
            binaryFormatter.Serialize(memoryStream, figure);
#pragma warning restore SYSLIB0011 // Тип или член устарел
            return memoryStream.ToArray();
        }

        public static ImageFigures ByteArrayToObject(byte[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            memoryStream.Write(array, 0, array.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // Тип или член устарел
            var data = (ImageFigures)binaryFormatter.Deserialize(memoryStream);
#pragma warning restore SYSLIB0011 // Тип или член устарел
            return data;
        }
    }
}
