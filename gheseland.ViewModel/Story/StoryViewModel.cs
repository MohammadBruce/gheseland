using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Story
{
  public class StoryViewModel
  {
    public int ID { get; set; }
    public int StoryCatID { get; set; }
    public int StoryPriority { get; set; }
    public string StoryName { get; set; }
    public string StoryImageThum { get; set; }
    public string StoryImage { get; set; }
    public string StoryText { get; set; }
    public string StorySummaryFile { get; set; }
    public string StoryFile { get; set; }
    public string StoryWriter { get; set; }
    public string StoryNarrator { get; set; }
    public int StoryNarratorID { get; set; }
    public string StoryCharacters { get; set; }
    public string StoryTime { get; set; }
    public int StoryViews { get; set; }
    public bool IsFree { get; set; }
    public bool IsActive { get; set; }
    public bool IsMultiImage { get; set; }
    public bool ForAndroid { get; set; }
    public int StoryUserLikes { get; set; }
    public DateTime RegDate { get; set; }

  }
}
