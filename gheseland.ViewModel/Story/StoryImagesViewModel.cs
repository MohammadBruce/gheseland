using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Story
{
  public class StoryImagesViewModel
  {
    public Guid ID { get; set; }
    public int StoryID { get; set; }
    public int StoryImageSecound { get; set; }
    public string StoryImageFile { get; set; }
  }
}
