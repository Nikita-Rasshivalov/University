using System;

namespace CourseWork.BLL.Models
{
    public enum ElementType
    {
        Tet4
    }

    public static class ElementTypeExtensions
    {
        public static int GetNodeCount(this ElementType elementType)
        {
            return elementType switch 
            {
                ElementType.Tet4 => 4,
                _ => throw new NotSupportedException($"Element type {elementType} is not supported."),
            };
        }
    }
}
