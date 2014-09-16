// Hooks up Castle Windsor as the container for your Caliburn.Micro application. 
// Turns on support for delegate factory methods (e.g. passing the factory "Func<XyzEditViewModel>" as a constructor arg)
// Dependencies: In addition to Caliburn.Micro you will need to reference Castle.Core and Castle.Windsor

using System;
using System.Collections.Generic;

namespace FurryRun.Editor.Infrastructure
{
// If you don't already have a ForEach extension method in your project here you go:

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