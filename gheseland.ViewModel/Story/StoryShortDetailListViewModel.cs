using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Story
{
  public class StoryShortDetailListViewModel
  {
    public IEnumerable<StoryShortDetailViewModel> AllStories { get; set; }
    public IEnumerable<StoryShortDetailViewModel> SimularStories { get; set; }
    public IEnumerable<StoryShortDetailViewModel> NaratorStories { get; set; }
  }
}
