// audio story codes

var song = null;
var updateBar = null;
var pageChanged = false;
var isrRepead = false;
var duration = 0;
var barSize = 0;
$(function () {
    barSize = $('.defaultBar').width();
})
var progressBar = $('.progressBar');
var progressBarImage = $('.progressBar img');
var bar = $('.defaultBar');
var playButton = $('.player');
var timer = $('.timer ');
var repeatD = $('.repeat img ');
function storyPlayer(url) {
    
    return {
        setSong: function () {
            if (pageChanged) {
                song = new Audio(url);
                pageChanged = false;
            }
            if (song == null) {
                if (typeof url !== "undefined") {

                    song = new Audio(url);
                }
            }
            duration = song.duration;

        },
        setUpdate: function () {
            if (!song.paused && !song.ended) {
                song.pause();
                playButton.innerHTML = 'Play';
                window.clearInterval(updateBar);
            } else {
                song.play();
                playButton.innerHTML = 'Pause';
                updateBar = setInterval(update, 500);
            }
        },
        playOrPause: function (event) {
            // alert("playOrPause");
            if (song !== null && !song.paused && !song.ended) {
                if (typeof event !== "undefined") {
                    event.preventDefault();
                }

                song.pause();
                playButton.attr('src', '/img/icons/audio/play.png');
                window.clearInterval(updateBar);
            } else {
                if (typeof event !== "undefined") {
                    event.preventDefault();
                }
                this.setSong();
                song.play();
                playButton.attr('src', '/img/icons/audio/pause.png');
                updateBar = setInterval(this.update, 500);
            }
        },
        update: function () {
            if (!song.ended) {
                var measuredTime = new Date(null);
                measuredTime.setSeconds(song.currentTime); // specify value of SECONDS
                var MHSTime = measuredTime.toISOString().substr(14, 5);
                $('.timer').text(MHSTime)
                var size = parseInt(song.currentTime * barSize / song.duration);
                progressBar.css("width", size + 'px');




                //check story image in current time : this variable declared in story.js
                if (storyImageList !== null) {
                    story().setStoryImage(song.currentTime);
                }
            } else {

                if (isrRepead) {
                    song.currentTime = 0;
                    if (song.paused) {
                        storyPlayer().playOrPause(event);
                    }
                } else {

                    playButton.attr('src', '/img/icons/audio/play.png');
                }
                progressBar.css("width", '0px');
                window.clearInterval(updateBar);
            }
        },
        drag: function (event ,ui) {
            // Keep the left edge of the element
            // at least 100 pixels from the container
            //ui.position.left = Math.min(bar.width(), ui.position.left);
            if (ui.position.left<0) {
                ui.position.left = 0;
            }
            if (ui.position.left > bar.width()) {
                ui.position.left = bar.width();
            }

            this.click(event, ui.position.left);
            ui.position.left = 0;
        },
        click: function (event, newDuration) {
            if (song&&!song.ended) {
                var newtime = 0;

                if (event === null && typeof newDuration != "undefined") {
                    newtime = newDuration;
                }
                else {
                    var mouseX = event.pageX - bar.offset().left;                   
                    newtime = mouseX * song.duration / barSize;
                }
                song.currentTime = newtime;
                progressBar.css("width", mouseX + 'px');
                progressBarImage.css('left',0);
                if (song.paused) {
                    this.playOrPause(event);
                }

            }
        },
        setLoop: function (isLoop) {
            story.loop = isLoop;
        },
        setMuted: function (event, current) {
            event.preventDefault();
            song.volume = 0;
            playButton.replaceWith('<a class="button gradient" id="muted" href="" titl');
        },
        setCurrentTime: function (time) {
            story.currentTime = time;
        },
        getDuration: story.duration,
        reset: function () {

            $('.timer').text('00:00');
            if (song != null) {
                song.currentTime = 0;
                song.pause();
            }
            pageChanged = true;
        },
        toggleRepeat: function () {
            if (isrRepead) {
                repeatD.attr('src', "/img/icons/audio/repeat.png");
            }
            else {
                repeatD.attr('src', "/img/icons/audio/repeat-presed.png");
            }
            isrRepead = (!isrRepead);
        },
        forward: function () {
            var newDuration = song.currentTime + 15;
            if (newDuration > song.duration) {
                newDuration = song.duration;
            }
            this.click(null, newDuration);
        },
        rewind: function () {
            var newDuration = song.currentTime - 15;
            if (newDuration <0 ) {
                newDuration = 0;
            }
            this.click(null, newDuration);
        }
    };
}




//(function ($) {
//    $.fn.drags = function (opt) {

//        opt = $.extend({ handle: "", cursor: "move" }, opt);

//        if (opt.handle === "") {
//            var $el = this;
//        } else {
//            var $el = this.find(opt.handle);
//        }

//        return $el.css('cursor', opt.cursor).on("mousedown", function (e) {
//            if (opt.handle === "") {
//                var $drag = $(this).addClass('draggable');
//            } else {
//                var $drag = $(this).addClass('active-handle').parent().addClass('draggable');
//            }
//            var z_idx = $drag.css('z-index'),
//                drg_h = $drag.outerHeight(),
//                drg_w = $drag.outerWidth(),
//                pos_y = $drag.offset().top + drg_h - e.pageY,
//                pos_x = $drag.offset().left + drg_w - e.pageX;
//            $drag.css('z-index', 1000).parents().on("mousemove", function (e) {
                
//                $('.draggable').offset({
//                    //top: e.pageY + pos_y - drg_h,
//                    left: e.pageX + pos_x - drg_w
//                }).on("mouseup", function () {
//                    $(this).removeClass('draggable').css('z-index', z_idx);
//                });
//            });
//            e.preventDefault(); // disable selection
//        }).on("mouseup", function () {
//            if (opt.handle === "") {
//                $(this).removeClass('draggable');
//            } else {
//                $(this).removeClass('active-handle').parent().removeClass('draggable');
//            }
//        });

//    }
//})(jQuery);
//$('.dragable').drags();
