using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Banner
{
  public class BannerViewModel
  {
    public int ID { get; set; }
    public int BannerTragetTypeID { get; set; }
    public int BannerPosition { get; set; }
    public string BannerTitle { get; set; }
    public string BannerImage { get; set; }
    public string BannerTarget { get; set; }
    public DateTime RegDate { get; set; }
  }
}
