using System.Collections.Generic;

namespace iTechArt.Common.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Swap<T>(this List<T> list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
            return list;
        }
    }
}
