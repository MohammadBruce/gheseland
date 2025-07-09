var bannerList = null;
var storyImageList = null;
function story() {

    function getStoryItem(data, colSize) {
        var multipleimage = '<img src="/img/icons/multiple.png" class="multipleimage">';
        var story = '';
        if (data !== null) {
            if (typeof colSize === "undefined") {
                colSize = "col-6  col-sm-4 col-md-6 float-right";
            }
            story = '<div class="' +
                colSize +
                '">' +
                '<div class="ghese-unit">' +
                '<a href="/story/detail/' +
                data.ID +
                '" class="ui-link"><div class="image">';
            if (data.IsMultiImage) {
                story += multipleimage;
            }
            story += '<img src="' + data.StoryImage + '" alt="' + data.StoryName + '">' +
                '</div>' +
                '<div class="text"><p class="font-black font-md">' + (data.StoryName !== null && data.StoryName.length <= 20 ? data.StoryName : data.StoryName.substring(0, 17) + "...") + '</p>' +
                '</div></a></div></div>';
        }

        return story;
    }

    function getBannerItem(data) {
        var bannerUrl = '';
        if (data != '') {
            switch (data.BannerTragetTypeID) {
                case 1:
                    bannerUrl = "/story/detail/" + data.BannerTarget + story().urlKey.title(data.BannerTitle);
                    break;
                case 2:
                    bannerUrl = story().urlKey.list + data.BannerTarget + story().urlKey.title(data.BannerTitle);
                    break;
                case 3:
                    bannerUrl = data.BannerTarget + story().urlKey.title(data.BannerTitle);
                    break;

                case 4:
                    bannerUrl = story().urlKey.filter + generateFilterParamsUrl(data.BannerTarget) + story().urlKey.title(data.BannerTitle);
                    break;


                default:
                    bannerUrl = 'javascript:void(0)';
                    break;
            }
        }

        var banner = '<div style="margin-top:15px; width:100%">' +
            '<a href="' + bannerUrl + '"><img class="slide-img" src = "' + data.BannerImage + '" alt = "' + data.BannerTitle + '" ></a>' +
            '</div >';
        return banner;
    }
    function getTagItem(data, filterType) {
        var tag = '<a href="' + story().urlKey.filter + filterType + '-' + data.ID + '"><span class="tag float-right font-xmd font-light-blue">' + data.Value + '</span></a>';
        return tag;
    }
    function generateFilterParams(paramsStr) {
        var data = {};
        var objectList = [];
        var paramsList = paramsStr.split('&');
        var paramCount = 0;

        for (var i = 0; i < paramsList.length; i++) {
            var param = paramsList[i];
            if (param !== '') {
                var object = {};
                object.FirstLevel = param.split('-')[0];
                object.SecoundLevel = param.split('-')[1];
                objectList.push(object);
                paramCount++;
            }

        }
        if (paramCount > 0) {
            //show filter icon
            $('.filterIcon').show();
        } else {
            //hide filter icon
            $('.filterIcon').hide();
        }
        $('.filter-count').text('(' + paramCount + ')');
        data.parameters = objectList;
        return data;
    }
    function generateFilterParamsUrl(paramsobj) {
        var numbers = paramsobj.match(/\d+/g).map(Number);

        var str = '';
        for (var i = 0; i < numbers.length; i++) {
            var num = numbers[i];
            if (i % 2 === 0) {
                str += num;
            } else {
                str += '-' + num + '&';
            }
        }
        return str;
    }

    return {
        urlKey: {
            filter: '/story/index#f=',
            list: '/story/index#l=',
            simular: '/story/index#s=',
            narator: '/story/index#n=',
            title: function (title) {
                return `^txt=${title}`;
            }
        },
        filter: {
            removeFilters: function () {
                window.location.replace('/story/index');

            },
            setFilterChileds: function (parentId) {
                $('.inner-items .item').removeClass('d-block').addClass('d-none');
                $('.main-items .item').removeClass('active');
                $('.main-items .item-' + parentId).addClass('active');
                $('.inner-items .item').each(function (index, current) {
                    if ($(current).data('parent') === parentId) {
                        $(current).addClass('d-block');
                    }
                });
            },
            setItem: function () {
                $('.main-items .item').each(function (index, current) {
                    var parentId = $(current).attr('name');
                    var checkCount = $('input[name="checkbox-' + parentId + '"]').filter(':checked').length;
                    $(current).find('span').text(checkCount);
                });
            },
            submitFilter: function () {
                var filterText = '';
                $('.inner-items input[type="checkbox"]').each(function (index, current) {
                    if ($(current).prop('checked')) {
                        filterText += $(current).attr('id').split('checkbox-')[1] + '&';
                    }
                });
                window.location = story().urlKey.filter + filterText;
            }
        },
        like: function (storyId, userId) {
            datas.call({
                url: config().apiUrl.likeStoryUrl,
                data: {
                    storyId: storyId,
                    userId: userId
                },
                method: methods.POST,
                callback: function (response) {
                    var element = $('.star');
                    if (response) {
                        element.attr('src', "/img/icons/audio/Star_on.png");
                    }
                    else {
                        element.attr('src', "/img/icons/audio/Star_off.png");

                    }
                }
            });
        },
        getLikeStatus: function (storyId, userId) {
            this.like(storyId, userId);
            this.like(storyId, userId);

        },
        mainLoader: {
            show: function () {
                $('.inner-main').hide();
                $('.main-loader').show();
            },
            hide: function () {
                $('.main-loader').hide();
                $('.inner-main').show();
            }
        },
        assignToContent: function (data, className, bannerList) {
            $(className).empty();
            var text = '';
            var bannerCounter = 0;
            for (var i = 0; i < data.length; i++) {
                var story = data[i];
                text += getStoryItem(story);
                if (typeof bannerList !== "undefined") {

                    var bannerInfo = this.bannerType(i, bannerList, bannerCounter);
                    if (bannerInfo.exist) {
                        bannerCounter = bannerInfo.counter;
                        text += getBannerItem(bannerInfo.banner);
                    }
                }
            }
            $(text).appendTo($(className));
        },
        bannerType: function (index, bannerList, counter) {
            if (index > 1) {
                for (var i = 0; i < bannerList.length; i++) {
                    var banner = bannerList[i];
                    if (banner.BannerPosition - counter == index) {
                        counter = counter + 1;
                        return {
                            exist: true,
                            banner: banner,
                            counter: counter
                        };
                    }
                }
            }

            return {
                exist: false,
                banner: null
            };
        },
        fillAllStories: function () {
            //hide filter icon
            $('.filterIcon').hide();
            //get hash value
            var firstPartHash = Hash().getUrlHash().split('^')[0];
            var secountPartHash = Hash().getUrlHash().split('^')[1];
            if (secountPartHash) {
                var sechashKey = secountPartHash.split('=')[0];
                var sechashValue = secountPartHash.split('=')[1];
                switch (sechashKey) {
                    case "txt":
                        // set new title value
                        $('.header-title').text(decodeURIComponent(sechashValue));
                        // hide filters
                        $('.filtersort').hide();
                        // hide sliders
                        bannerList = [];
                        // hide slide show
                        $('.main-slider').hide();

                        // hide search menu and show hide menu
                        $('.hide-menu').show();
                        $('.search-menu').hide();

                        break;
                    default:
                        break;
                }
            }

            var hash = firstPartHash;
            if (hash !== '') {
                var hashKey = hash.split('=')[0];
                var hashValue = hash.split('=')[1];
                switch (hashKey) {
                    case "#f":
                        //should filtered
                        var params = generateFilterParams(hashValue);
                        datas.call({
                            url: config().apiUrl.getFilteredStories,
                            data: params,
                            method: methods.POST,
                            callback: function (response) {

                                story().assignToContent(response, '.ghese-list .row', bannerList);
                                story().mainLoader.hide();
                            }
                        });
                        break;
                    case "#l":
                        //should listed
                        datas.call({
                            url: config().apiUrl.getStoriesByIDs + hashValue,
                            callback: function (response) {

                                if (response !== null) {
                                    var storyList = response.AllStories;
                                    story().assignToContent(storyList, '.ghese-list .row', bannerList);
                                }

                                story().mainLoader.hide();
                            }
                        });
                        break;
                    case "#s":
                        //simular stories
                        datas.call({
                            url: config().apiUrl.simularStoriesUrl + hashValue,
                            callback: function (response) {

                                if (response !== null) {
                                    var storyList = response;
                                    story().assignToContent(storyList, '.ghese-list .row', bannerList);
                                }

                                story().mainLoader.hide();
                            }
                        });
                        break;
                    case "#n":
                        //narator stories
                        datas.call({
                            url: config().apiUrl.naratorStoriesUrl + hashValue,
                            callback: function (response) {

                                if (response !== null) {
                                    var storyList = response.AllStories;
                                    story().assignToContent(storyList, '.ghese-list .row', bannerList);
                                }

                                story().mainLoader.hide();
                            }
                        });
                        break;
                }
            }
            else {
                //get all stories

                datas.call({
                    url: config().apiUrl.getAllStories,
                    callback: function (response) {
                        if (response) {
                            var storyList = response.AllStories;
                            story().assignToContent(storyList, '.ghese-list .row', bannerList);
                            story().mainLoader.hide();
                        }

                    }
                });
            }
        },
        getAllStoriesContent: function () {





            this.mainLoader.show();
            if (bannerList == null) {
                datas.call({
                    url: config().apiUrl.getBanners,
                    callback: function (response) {
                        //get all banners 
                        bannerList = response;
                        var mainElement = $('.main-slider');
                        //mainSlider.empty();
                        if (bannerList) {
                            for (var i = 0; i < bannerList.length; i++) {
                                var banner = bannerList[i];
                                if (banner !== null && banner.BannerPosition == 1) {
                                    mainElement.owlCarousel({ items: 1 }).trigger('add.owl.carousel',
                                        [
                                            $(getBannerItem(banner))
                                        ]).trigger('refresh.owl.carousel');
                                }

                            }
                        }

                        story().fillAllStories();

                    }
                });
            } else {
                story().fillAllStories();
            }


        },
        getStoriesContentBySearch: function (text) {
            var mainElement = $('.ghese-list .row');
            app.elementLoader(mainElement).show();

            datas.call({
                url: config().apiUrl.searchStoriesUrl + text,
                callback: function (response) {
                    var storyList = response;
                    story().assignToContent(storyList, '.ghese-list .row');
                    app.elementLoader(mainElement).hide();
                }
            });

        },
        getStoriesBySort: function (order) {
            this.mainLoader.show();
            datas.call({
                url: config().apiUrl.getAllStories + "?order=" + order,
                callback: function (response) {
                    var storyList = response.AllStories;
                    story().assignToContent(storyList, '.ghese-list .row', bannerList);
                    story().mainLoader.hide();
                }
            });
        },

        getSimularStories: function (storyId) {
            var mainElement = $('.story-slider');
            app.elementLoader(mainElement).show();
            datas.call({
                url: config().apiUrl.simularStoriesUrl + storyId,
                callback: function (response) {
                    var count = 0;
                    if (response !== null) {
                        if (response.length < 10) {
                            count = response.length;
                        }
                        else {
                            count = 10;
                        }
                    }
                    for (var i = 0; i < count; i++) {
                        if (response[i] !== null) {
                            var story = response[i];

                            if (story.ID !== currentStoryId) {
                                mainElement.owlCarousel().trigger('add.owl.carousel',
                                    [
                                        $(getStoryItem(story, "col-12"))
                                    ]).trigger('refresh.owl.carousel');
                            }

                        }
                    }
                    app.elementLoader(mainElement).hide();
                }
            });
        },
        getNaratorStories: function (naraytorId) {
            var mainElement = $('.relstory-slider');
            app.elementLoader(mainElement).show();
            datas.call({
                url: config().apiUrl.naratorStoriesUrl + naraytorId,
                callback: function (response) {
                    var result = response.AllStories;
                    var count = 0;
                    if (result !== null) {
                        if (result.length < 10) {
                            count = result.length;
                        }
                        else {
                            count = 10;
                        }
                    }
                    for (var i = 0; i < count; i++) {
                        if (result[i] !== null) {
                            var story = result[i];
                            if (story.ID !== currentStoryId) {
                                mainElement.owlCarousel().trigger('add.owl.carousel',
                                    [
                                        $(getStoryItem(story, "col-12"))
                                    ]).trigger('refresh.owl.carousel');
                            }

                        }
                    }
                    app.elementLoader(mainElement).hide();
                }
            });
        },
        getStorykewords: function (storyId) {
            var mainElement = $('.storyTagList');
            app.elementLoader(mainElement).show();
            datas.call({
                url: config().apiUrl.storyKeywordsUrl + storyId,
                callback: function (response) {
                    mainElement.empty();
                    for (var i = 0; i < response.length; i++) {
                        var tag = response[i];
                        $(getTagItem(tag, 2)).appendTo(mainElement);

                    }
                    app.elementLoader(mainElement).hide();
                }
            });
        },
        getStoryAges: function (storyId) {
            var mainElement = $('.storyAgeList');
            app.elementLoader(mainElement).show();
            datas.call({
                url: config().apiUrl.storyAgeListUrl + storyId,
                callback: function (response) {
                    mainElement.empty();
                    for (var i = 0; i < response.length; i++) {
                        var tag = response[i];
                        $(getTagItem(tag, 1)).appendTo(mainElement);

                    }
                    app.elementLoader(mainElement).hide();
                }
            });
        },
        getStorySubjects: function (storyId) {
            var mainElement = $('.storySubjectList');
            app.elementLoader(mainElement).show();
            datas.call({
                url: config().apiUrl.storySubjectListUrl + storyId,
                callback: function (response) {
                    mainElement.empty();
                    for (var i = 0; i < response.length; i++) {
                        var tag = response[i];
                        $(getTagItem(tag, 3)).appendTo(mainElement);

                    }
                    app.elementLoader(mainElement).hide();
                }
            });
        },
        getStoryImageList: function (storyId) {
            function getImage(url) {
                return `<img src="${url}" style="max-width:100%"/>`;
            }
            datas.call({
                url: config().apiUrl.storyImageListUrl + storyId,
                callback: function (response) {
                    if (response != null) {
                        storyImageList = response;
                        if (storyImageList.length > 1) {
                            var mainElement = $('.main-imageHost-multiple');
                            var singleImageElement = $('.main-imageHost');
                            var multipleimage = '<img src="/img/icons/multiple.png" class="multipleimage">';
                            singleImageElement.remove();
                            $.each(storyImageList, function (index, value) {
                                mainElement.owlCarousel(
                                    {
                                        autoplay: false, loop: false,
                                        autoplayHoverPause: false,
                                        rtl: false
                                    }
                                ).trigger('add.owl.carousel',
                                    [
                                        $(getImage(value.StoryImageFile))
                                    ]).trigger('refresh.owl.carousel');
                            });
                            $(multipleimage).appendTo(mainElement);


                        }
                    }
                }
            });
        },
        setStoryImage: function (songcurrentTime) {
            if (storyImageList != null) {

                for (var i = 0; i < storyImageList.length; i++) {
                    var storyImage = storyImageList[i];
                    if (parseInt(songcurrentTime) == storyImage.StoryImageSecound) {
                        var carousel = $('.main-imageHost-multiple');
                        carousel.owlCarousel(
                            {
                                autoplay: false, loop: false,
                                autoplayHoverPause: false,
                                rtl: false
                            }
                        ).trigger('to.owl.carousel', [i]).trigger('refresh.owl.carousel');
                    }
                }
            }
        }
    }
}
