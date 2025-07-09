using System.Collections.Generic;

namespace gheseland.ViewModel.Story
{
    public class ItemViewModel
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }

    }
    public class StoryItemsViewModel
    {
        public List<ItemViewModel> ParentList { get; set; }
        public List<ItemViewModel> ChildList { get; set; }
    }
}
