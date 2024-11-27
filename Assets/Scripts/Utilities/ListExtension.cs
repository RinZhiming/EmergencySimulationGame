using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class ListExtension
    {
        private static Random random = new Random();
        
        public static void Shuffle<T>(this IList<T> list)
        {
            var index = list.Count;
            while (index > 1)
            {
                index--;
                var k = random.Next(index + 1);
                (list[k], list[index]) = (list[index], list[k]);
            }
        }
    }
}