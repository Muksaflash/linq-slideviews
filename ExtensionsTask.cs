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
			items = items.OrderBy(x => x);
			if (items.Count() == 0) throw new InvalidOperationException();
            if (items.Count() % 2 == 0)
			{
				return (items.Take(items.Count() / 2 ).LastOrDefault() + items.Take(items.Count() / 2+1).LastOrDefault()) / 2;
			}
			else return items.Take(items.Count()/2+1).LastOrDefault();
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var sequence = items.ToArray();
			var count = sequence.Count() - 1;
            var result = new Tuple<T, T>[count];
			
            for (int i =0; i< count; i++)
			{
                var temp = sequence.Skip(i).Take(2).ToArray();
				result[i] = Tuple.Create(temp[0], temp[1]);
			}
			return result;
		}
	}
}