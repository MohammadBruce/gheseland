function config() {
    return {
        apiUrl: {
            getAllStories: "/story/GetAllStories",
            getStoriesByIDs: "/story/GetStoriesByIDs?ids=",
            getFilteredStories: "/story/GetFilteredStories",
            getBanners: "/story/GetBanners",
            simularStoriesUrl: "/story/GetSimilarStories?storyId=",
            naratorStoriesUrl: "/story/GetNaratorStories?naratorId=",
            searchStoriesUrl: "/story/GetStoriesBySearch?text=",
            storyKeywordsUrl: "/story/GetstoryKeywords?storyId=",
            storySubjectListUrl: "/story/GetstorySubjectList?storyId=",
            storyAgeListUrl: "/story/GetstoryAgeList?storyId=",
            storyImageListUrl: "/story/GetStoryImages?storyId=",
            likeStoryUrl: "/story/like",

        }
    };
}