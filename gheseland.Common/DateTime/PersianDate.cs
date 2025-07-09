using PersianDate;
namespace gheseland.Common.DateTime
{
    public static class PersianDate
    {
        public static string ToPersianDate(this System.DateTime date)
        {
            return ConvertDate.ToFa(System.DateTime.Now, "D");
        }

        public static string ToShortPersianDate(this System.DateTime date)
        {
            return ConvertDate.ToFa(date);
        }
        public static string ConvertToShortPersianDate(System.DateTime date)
        {
          return ConvertDate.ToFa(date);
        }
  }
}
