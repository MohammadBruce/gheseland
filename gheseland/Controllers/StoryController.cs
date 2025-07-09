using gheseland.Services;
using gheseland.Services.Implements;
using gheseland.ViewModel.Banner;
using gheseland.ViewModel.Search;
using gheseland.ViewModel.Story;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace gheseland.Controllers
{
    [Route("{action=index}")]

    public partial class StoryController : Controller
    {
        private readonly IHttpService _httpservice;
        private const string baseUrl = "http://api.gheseland.com";
        private readonly string getAllStoriesStr = baseUrl + "/api/stories/GetStoriesList";
        private readonly string getFilteredStoriesStr = baseUrl + "/api/stories/FilterStories";
        private readonly string getBannerListStr = baseUrl + "/api/banner/Getbanners";
        private readonly string storyDetailUrl = baseUrl + "/api/Stories/GetStories/";
        private readonly string simularStoriesUrl = baseUrl + "/api/Stories/GetsimularStories?storyId=";
        private readonly string naratorStoriesUrl = baseUrl + "/api/Stories/GetnaratorStories/";
        private readonly string searchStoriesUrl = baseUrl + "/api/Search/GetSearchWord/";
        private readonly string filterItemsUrl = baseUrl + "/api/Stories/GETFilterPageParametrs/";
        private readonly string storyKeywordsUrl = baseUrl + "/api/Stories/GETStoryKeywords/";
        private readonly string storySubjectListUrl = baseUrl + "/api/Stories/GETStorySubjectList/";
        private readonly string storyAgeListUrl = baseUrl + "/api/Stories/GETStoryAgeList/";
        private readonly string getStoriesByIDsUrl = baseUrl + "/api/Stories/GetStoriesByIDs/";
        private readonly string getStoryImagesUrl = baseUrl + "/api/Stories/GetStoryImages/";
        private readonly string likeStoryUrl = baseUrl + "/api/UsersActivity/Like/";
        private readonly string errorSubjectUrl = baseUrl + "/api/UsersReport/GetErrorSubject/";
        private readonly string submitErrorUrl = baseUrl + "/api/UsersReport/ErrorReport/";

        public StoryController()
        {
            _httpservice = new HttpService();
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult ContactUs()
        {
            return View();
        }

        [Route("story/{id:int}")]
        public virtual async Task<ActionResult> Detail(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var storyData = await _httpservice.GetAsync<StoryListViewModel>(null, storyDetailUrl + id);
            if (storyData?.AllStories.FirstOrDefault() != null)
            {
                return View(storyData);
            }
            else
            {
                ///should redirect 
                return null;

            }


        }

        public virtual ActionResult Search()
        {
            return View();
        }

        public virtual async Task<ActionResult> Filter()
        {
            var model = new StoryItemsViewModel
            {
                ParentList = new List<ItemViewModel>()
        {
          new ItemViewModel() {ID = 1, Value = "گروه سنی"},
          new ItemViewModel() {ID = 2, Value = "کلید واژه"},
          new ItemViewModel() {ID = 3, Value = "موضوع"},
          new ItemViewModel() {ID = 4, Value = "سبک"}
        }
            };
            foreach (var item in model.ParentList)
            {
                var result = await _httpservice.GetAsync<IEnumerable<ItemViewModel>>(null, filterItemsUrl + item.ID);
                var childList = result.Select(child => new ItemViewModel()
                {
                    ID = child.ID,
                    Value = child.Value,
                    ParentID = item.ID
                })
                  .ToList();
                if (model.ChildList != null)
                {
                    model.ChildList.AddRange(childList);

                }
                else
                {
                    model.ChildList = childList;

                }
            }
            return View(model);
        }

        public virtual async Task<ActionResult> ReportError()
        {
            var _userService = new UserService();
            var user = _userService.GetUserInfo();
            if (user == null)
            {
                return RedirectToAction(MVC.Auth.RegisterUser());
            }
            var result = await _httpservice.GetAsync<IEnumerable<ItemViewModel>>(null, errorSubjectUrl);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ReportError(string errorSubject, string errorText)
        {
            var errorSubjectList = _httpservice.Get<IEnumerable<ItemViewModel>>(null, errorSubjectUrl);

            var _userService = new UserService();
            var user = _userService.GetUserInfo();
            if (user == null)
            {
                return RedirectToAction(MVC.Auth.RegisterUser());
            }
            if (string.IsNullOrEmpty(errorSubject) || string.IsNullOrEmpty(errorText))
            {
                ViewBag.message = "لطفا متن خطا را به درستی وارد کنید";
                return View(MVC.Story.Views.ReportError, errorSubjectList);
            }

            var data = new
            {
                UserID = user.UserID.ToString(),
                ErrorSubject = errorSubject,
                ErrorText = errorText
            };

            var result = _httpservice.Post(data, submitErrorUrl);
            dynamic dResult = result;
            if (bool.Parse(dResult.m_Item1.ToString()))
            {
                ViewBag.message = dResult.m_Item2.ToString();
                return View(MVC.Story.Views.ReportError, errorSubjectList);

            }
            else
            {
                ViewBag.message = "عملیات با موفقیت انجام نشد";
                return View(MVC.Story.Views.ReportError, errorSubjectList);
            }
        }

        #region ApiList

        [HttpGet]
        public virtual async Task<JsonResult> GetAllStories(string order)
        {
            var result = await _httpservice.GetAsync<StoryShortDetailListViewModel>(null, getAllStoriesStr + '/' + order);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual async Task<JsonResult> GetStoriesByIDs(string ids)
        {
            var result = await _httpservice.GetAsync<StoryShortDetailListViewModel>(null, getStoriesByIDsUrl + ids);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual async Task<JsonResult> GetFilteredStories(IEnumerable<FilterViewModel> parameters)
        {
            var result = await _httpservice.PostAsync<IEnumerable<StoryShortDetailViewModel>>(parameters, getFilteredStoriesStr);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual async Task<JsonResult> GetBanners()
        {
            var result = await _httpservice.GetAsync<IEnumerable<BannerViewModel>>(null, getBannerListStr);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual async Task<JsonResult> GetStoryImages(int storyId)
        {
            var result = await _httpservice.GetAsync<IEnumerable<StoryImagesViewModel>>(null, getStoryImagesUrl + storyId);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public virtual async Task<JsonResult> GetSimilarStories(int storyId)
        {
            var result = await _httpservice.GetAsync<IEnumerable<StoryShortDetailViewModel>>(null, simularStoriesUrl + storyId);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public virtual async Task<JsonResult> GetNaratorStories(int naratorId)
        {
            var result = await _httpservice.GetAsync<StoryShortDetailListViewModel>(null, naratorStoriesUrl + naratorId);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public virtual async Task<JsonResult> GetstoryKeywords(int storyId)
        {
            var result = await _httpservice.GetAsync<IEnumerable<ItemViewModel>>(null, storyKeywordsUrl + storyId);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public virtual async Task<JsonResult> GetstorySubjectList(int storyId)
        {
            var result = await _httpservice.GetAsync<IEnumerable<ItemViewModel>>(null, storySubjectListUrl + storyId);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public virtual async Task<JsonResult> GetstoryAgeList(int storyId)
        {
            var result = await _httpservice.GetAsync<IEnumerable<ItemViewModel>>(null, storyAgeListUrl + storyId);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public virtual async Task<JsonResult> GetStoriesBySearch(string text)
        {
            var result = await _httpservice.GetAsync<IEnumerable<StoryShortDetailViewModel>>(null, searchStoriesUrl + text);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public virtual async Task<JsonResult> Like(string userId, int storyId)
        {
            var parameters = new
            {
                UserID = userId,
                StoryID = storyId

            };
            var result = _httpservice.Post(parameters, likeStoryUrl);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
