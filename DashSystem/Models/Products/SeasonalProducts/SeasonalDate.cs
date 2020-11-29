using System;
// ReSharper disable All

namespace DashSystem.Models.Products.SeasonalProducts
{
    public struct SeasonalDate : IComparable<SeasonalDate>
    {
        public int Day { get; }
        public int Month { get; }

        public static SeasonalDate Now
        {
            get
            {
                DateTime now = DateTime.Now;
                return new SeasonalDate(now.Day, now.Month);
            }
        }

        #region CONSTRUCTORS
        public SeasonalDate(int day, int month)
        {
            Day = day;
            Month = month;
        }

        public SeasonalDate(DateTime date)
        {
            Day = date.Day;
            Month = date.Month;
        }
        #endregion

        public bool IsWithinRangeOf(SeasonalDate lowerBound, SeasonalDate upperBound)
        {
            if (lowerBound.Equals(upperBound))
            {
                return Equals(lowerBound);
            }
            else if (Equals(lowerBound) || Equals(upperBound))
            {
                return true;
            }
            else if (lowerBound.IsLessThan(upperBound))
            {
                return IsGreaterThan(lowerBound) && IsLessThan(upperBound);
            }
            else
            {   // If we get here, it means that the range passes new year
                return IsLessThan(upperBound) || IsGreaterThan(lowerBound);
            }
        }

        #region AUXILIARY_COMPARE_METHODS
        public bool IsGreaterThan(SeasonalDate date)
        {
            return CompareTo(date) > 0;
        }

        public bool IsLessThan(SeasonalDate date)
        {
            return CompareTo(date) < 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is SeasonalDate seasonalDatedate)
            {
                return CompareTo(seasonalDatedate) == 0;
            }
            else if (obj is DateTime dateTime)
            {
                SeasonalDate seasonalDateTime = new SeasonalDate(dateTime);
                return CompareTo(seasonalDateTime) == 0;
            }

            return false;
        }

        public int CompareTo(SeasonalDate other)
        {
            int monthComparison = Month.CompareTo(other.Month);
            if (monthComparison != 0) return monthComparison;

            return Day.CompareTo(other.Day);
        }


        public override int GetHashCode()
        {
            return Day.GetHashCode() * 17 + Month.GetHashCode();
        }
        #endregion

        public override string ToString()
        {
            return $"{Day}/{Month}";
        }
    }
}
