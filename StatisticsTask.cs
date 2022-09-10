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
			   .GroupBy(visit => visit.UserId);
			var visitGroups = tempVisitGroups.Select(visitGroup => visitGroup.OrderBy(x => x.DateTime));
			var timeBigramGroups = visitGroups.Select(visitGroup => visitGroup
			.Select(visit => visit.DateTime.TimeOfDay.TotalMinutes)
			.Bigrams());
			var slideTypes = visitGroups.SelectMany(visitGroup => visitGroup.Select(visit => visit.SlideType)).ToList();
			var typeTimeTupleGroups = timeBigramGroups
				.Select(
					timeBigramGroup => timeBigramGroup
					.Select(
						timeBigram =>
						{ 
							var resultTuple = Tuple.Create(slideTypes[0], timeBigram);
							slideTypes.RemoveAt(0);
							return resultTuple;
						}));
			var readyTupleGroups = typeTimeTupleGroups
				.Select(
					typeTimeTupleGroup => typeTimeTupleGroup
					.Where(x => x.Item2.Item2 - x.Item2.Item1 >= 1 && x.Item2.Item2 - x.Item2.Item1 <= 120)
					.Where(x => x.Item1 == slideType)
					.Where(x => x != null));
			double result = 0;
			if (readyTupleGroups.Any())
				try
				{
					result = readyTupleGroups.SelectMany(x => x.Select(t => t.Item2.Item2 - t.Item2.Item1)).Median();
				}
				catch { return 0.0; }
			return result;
		}
	}
}