 using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			var tempVisitGroups = visits
			   //.Where(visit => visit.SlideType == slideType)
			   .GroupBy(visit => visit.UserId);
			var visitGroups = tempVisitGroups.Select(visitGroup => visitGroup.OrderBy(x => x.DateTime));
			var j = visitGroups.Select(visitGroup => visitGroup
			.Select(visit => (double)visit.DateTime.Minute)
			.Bigrams()
			.Where(t => t.Item2 - t.Item1 >= 1 && t.Item2 - t.Item1 <= 120));
			var r = visitGroups.Select(visitGroup => visitGroup.Where(t => visitGroup.Select));
			//var result = j.SelectMany(x => x.Select(t => t.Item1)).Median();
            return 2.0;
		}
	}
}