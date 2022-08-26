using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
        /// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
        /// </summary>
        /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var items1 = items.OrderBy(x => x).ToArray();
            if (items1.Count() == 0) throw new InvalidOperationException();
            if (items1.Count() % 2 == 0)
            {
                return (items1.Take(items1.Count() / 2).LastOrDefault() + 
                    items1.Take(items1.Count() / 2 + 1).LastOrDefault()) / 2;
            }
            else return items1.Take(items1.Count() / 2 + 1).LastOrDefault();
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            T oldItem = default(T);
            bool isFirstIteration = true;
            foreach (var item in items)
            {
                if(!isFirstIteration) yield return Tuple.Create(oldItem, item);
                oldItem = item;
                isFirstIteration = false;   
            }

        }
    }
}