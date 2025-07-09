namespace gheseland.Common
{
    public static class Utility
    {
        public static string ToGlobalPhone(this string phone, string ext)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                if (phone.Substring(0, 1) == "0")
                {
                    return ext + phone.Substring(1, phone.Length - 1);
                }
                else
                {
                    return ext + phone;
                }
            }
            else
            {
                return phone;
            }
        }
    }
}
