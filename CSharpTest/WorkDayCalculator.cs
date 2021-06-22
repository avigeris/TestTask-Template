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
                IEnumerable<WeekEnd> we = weekEnds.OrderBy(weekend => weekend.StartDate);
                foreach (WeekEnd weekEnd in we)
                {
                    if (object.ReferenceEquals(null, weekEnd))
                    {
                        throw new ArgumentException();
                    }

                    if (weekEnd.StartDate > weekEnd.EndDate)
                    {
                        throw new ArgumentException();
                    }

                    // if weekend starts earlier than the current date
                    if (weekEnd.StartDate <= resultDate)
                    {
                        // if weekend ends after current date go to the weekend end 
                        if (weekEnd.EndDate > resultDate)
                        {
                            resultDate = weekEnd.EndDate;
                        }
                    }
                    else //weekend starts after current date
                    {

                        //calc days before next weekend
                        int daysBeforeWeekEnd = (weekEnd.StartDate - resultDate).Days;

                        //check if its one day weekend
                        if (weekEnd.StartDate.Equals(weekEnd.EndDate))
                        {
                            daysBeforeWeekEnd -= 1;
                        }

                        if (dayCount > daysBeforeWeekEnd)
                        {
                            dayCount -= daysBeforeWeekEnd;
                            resultDate = weekEnd.EndDate;
                            if (dayCount == 0)
                            {
                                return resultDate;
                            }
                        }
                        else
                        {
                            return resultDate.AddDays(dayCount);
                        }
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
