using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (object.ReferenceEquals(null, startDate))
            {
                throw new ArgumentException();
            }

            if (object.ReferenceEquals(null, dayCount) || dayCount < 0)
            {
                throw new ArgumentException();
            }

            if (!object.ReferenceEquals(null, weekEnds))
            {
                DateTime resultDate = startDate;
                foreach (WeekEnd weekEnd in weekEnds)
                {
                    if (object.ReferenceEquals(null, weekEnd))
                    {
                        throw new ArgumentException();
                    }

                    if (weekEnd.StartDate > weekEnd.EndDate)
                    {
                        throw new ArgumentException();
                    }

                    int daysBeforeWeekEnd = (weekEnd.StartDate - resultDate).Days;
                    if (dayCount > daysBeforeWeekEnd)
                    {
                        dayCount -= daysBeforeWeekEnd;
                        resultDate = weekEnd.EndDate;
                    }
                    else
                    {
                        return resultDate.AddDays(dayCount);
                    }
                }
                return resultDate.AddDays(dayCount);
            }
            else
            {
                return startDate.AddDays(dayCount - 1);
            }
            throw new NotImplementedException();
        }
    }
}
