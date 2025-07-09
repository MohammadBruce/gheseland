using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Package
{
  public class PackageViewModel
  {
    public int ID { get; set; }
    public int Priority { get; set; }
    public string PakageTitle { get; set; }
    public string PakageImage { get; set; }
    public int Days { get; set; }
    public int Price { get; set; }
    public int SalePercent { get; set; }
    public bool IsLimited { get; set; }
    public string Limited_From { get; set; }
    public string Limited_To { get; set; }
    public string GitfCode { get; set; }
    public bool IsActive { get; set; }

  }
}
