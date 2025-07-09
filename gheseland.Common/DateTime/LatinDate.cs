using MD.PersianDateTime;

//using PersianDate;

namespace gheseland.Common.DateTime
{
    public static class LatinDate
    {
        public static System.DateTime ToLatinDate(this string data)
        {
            PersianDateTime persianDate = PersianDateTime.Parse(data);
            return persianDate.ToDateTime();
        }
    }
}
