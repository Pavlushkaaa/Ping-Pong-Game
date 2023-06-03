using System.Collections.Generic;

namespace Core
{
    public static class ListExtention
    {
        public static SerializableList<T> ToSerializable<T>(this List<T> list)
        {
            return new SerializableList<T>() { List = list };
        }
    }
}
