using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Story
{
  public class StoryShortDetailViewModel
  {
    public int ID { get; set; }
    public int StoryCatID { get; set; }
    public int StoryPriority { get; set; }
    public string StoryName { get; set; }
    public string StoryImageThum { get; set; }
    public string StoryImage { get; set; }
    public int StoryNarratorID { get; set; }
    public bool IsMultiImage { get; set; }
    public DateTime RegDate { get; set; }

  }
}
