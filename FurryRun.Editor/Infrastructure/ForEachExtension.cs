using System;
using System.Collections.Generic;

namespace FurryRun.Editor.Infrastructure
{
    public static class ForEachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}