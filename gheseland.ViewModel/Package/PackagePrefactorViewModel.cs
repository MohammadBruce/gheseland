using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Package
{
    public class PackagePrefactorViewModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public int PayPrice { get; set; }
        public int PackageId { get; set; }
        public string PakageName { get; set; }
        public int PakagePrice { get; set; }
        public int PakageDays { get; set; }
        public int DiscountPercent { get; set; }
        public int DiscountPrice { get; set; }
        public int GiftDays { get; set; }
        public string GiftCode { get; set; }

        public int PreCreidit { get; set; }
        public int LastCreidit { get; set; }
        public int BazzarPakageID { get; set; }
        public int TotalDays { get; set; }
        public int BankGetwayType { get; set; }
    }
}
