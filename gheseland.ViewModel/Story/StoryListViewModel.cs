using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Story
{
  public class StoryListViewModel
  {
    public IEnumerable<StoryViewModel> AllStories { get; set; }
    public IEnumerable<StoryViewModel> SimularStories { get; set; }
    public IEnumerable<StoryViewModel> NaratorStories { get; set; }
  }
}
