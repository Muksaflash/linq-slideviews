using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class ParsingTask
    {
        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
        /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
        /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            var result = new Dictionary<int, SlideRecord>();
            if (lines.Count() < 2) return result;
            result = lines.Skip(1).Where(line =>
            line.Split(';')
            .Any(x => x != null && x != "") &&
            line.Split(';').Count() == 3 &&
            int.TryParse(line.Split(';').FirstOrDefault(), out int num) &&
            Enum.TryParse(line.Split(';').Skip(1).FirstOrDefault(), true, out SlideType str)
            )
            .ToDictionary(line =>
            {
                var splitString = line.Split(';');
                int id = int.Parse(splitString.FirstOrDefault());
                return id;
            },
            line =>
            {
                var splitString = line.Split(';');
                var id = int.Parse(splitString.FirstOrDefault());
                var slideType = (SlideType)Enum.Parse(typeof(SlideType), splitString.ToArray()[1], true);
                var title = splitString.ToArray()[2];
                return new SlideRecord(id, slideType, title);
            }
            );
            return result;
        }

        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
        /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
        /// Такой словарь можно получить методом ParseSlideRecords</param>
        /// <returns>Список информации о посещениях</returns>
        /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return lines.Skip(1).Select(line =>
            {
                if (line.Split(';').Count() != 4) throw new FormatException(@"Wrong line [" + line + "]");
                string dateTime = null;
                DateTime datetime;
                dateTime = line.Split(';').Skip(2).First() + " " + line.Split(';').Skip(3).First();

                try
                {
                    datetime = DateTime.Parse(dateTime.Trim());
                }
                catch
                {
                    throw new FormatException(@"Wrong line [" + line + "]");
                };
                return new VisitRecord(int.Parse(line.Split(';').First()), int.Parse(line.Split(';').Skip(1).First()),
                    datetime,
                    slides[int.Parse(line.Split(';').Skip(1).First())].SlideType);
            });
        }
    }
}