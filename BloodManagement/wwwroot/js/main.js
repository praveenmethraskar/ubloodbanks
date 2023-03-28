(function ($) {
    "use strict";
    /* -------------------------------------
               Prealoder
         -------------------------------------- */
    $(window).on('load', function (event) {
        $('.js-preloader').delay(500).fadeOut(500);
    });

    /* -------------------------------------
           Tweenmax
     -------------------------------------- */
    $('.hero-wrap').mousemove(function (e) {
        var wx = $(window).width();
        var wy = $(window).height();
        var x = e.pageX - this.offsetLeft;
        var y = e.pageY - this.offsetTop;
        var newx = x - wx / 2;
        var newy = y - wy / 2;
        $('.hero-content, .hero-text, .hero-shape-1, .hero-shape-2, .hero-video, .link').each(function () {
            var speed = $(this).attr('data-speed');
            if ($(this).attr('data-revert')) speed *= -.4;
            TweenMax.to($(this), 1, { x: (1 - newx * speed), y: (1 - newy * speed) });
        });
    });

    /* -------------------------------------
          Open Search
    -------------------------------------- */
    $('.searchbtn').on('click', function () {
        $('.search-area').addClass('open');
    });
    $('.close-searchbox').on('click', function () {
        $('.search-area').removeClass('open');
    });


    /* -------------------------------------
          Language Dropdown
    -------------------------------------- */
    $(".language-option").each(function () {
        var each = $(this)
        each.find(".lang-name").html(each.find(".language-dropdown-menu a:nth-child(1)").text());
        var allOptions = $(".language-dropdown-menu").children('a');
        each.find(".language-dropdown-menu").on("click", "a", function () {
            allOptions.removeClass('selected');
            $(this).addClass('selected');
            $(this).closest(".language-option").find(".lang-name").html($(this).text());
        });
    })
    $('.user-option').on('click', function () {
        $('.user-menuitem').toggleClass('open');
    });
    /* -------------------------------------
              Counter 
    -------------------------------------- */
    $(".odometer").appear(function (e) {
        var odo = $(".odometer");
        odo.each(function () {
            var countNumber = $(this).attr("data-count");
            $(this).html(countNumber);
        });
    });
    /* -------------------------------------
           Progressbar 
    // -------------------------------------- */

    $(window).scroll(function () {
        // if ($(window).scrollTop() > 100) { // scroll down abit and get the action   

        $('.progress-bar').each(function () {
            $(this).find('.progress-content').animate({
                width: $(this).attr('data-percentage')
            }, 2000);

            $(this).find('.progress-number-mark').animate({ left: $(this).attr('data-percentage') }, {
                duration: 2000,
                step: function (now, fx) {
                    var data = Math.round(now);
                    $(this).find('.percent').html(data + '%');
                }
            });
        });
        // }
    });


    /* -------------------------------------
           Hero Slider 
    // -------------------------------------- */

    $(".hero-slider-one").owlCarousel({
        items: 1,
        nav: true,
        dots: false,
        loop: true,
        margin: 30,
        animateOut: 'fadeOut',
        smartSpeed: 2000,
        autoplay: false,
        navText: ['<i class="flaticon-back"></i>', '<i class="flaticon-right-arrow-angle"></i>'],
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true
    });
    /* -------------------------------------
           Feature Slider 
     -------------------------------------- */
    $(".feature-slider-one").owlCarousel({
        nav: true,
        dots: false,
        loop: true,
        margin: 25,
        navText: ['<i class="flaticon-left-arrow-1"></i>', '<i class="flaticon-right-arrow-angle"></i>'],
        smartSpeed: 1300,
        autoHeight: true,
        autoplay: false,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            768: {
                items: 2,

            },
            1200: {
                items: 2,

            }
        }
    });
    /* -------------------------------------
           Service Slider 
     -------------------------------------- */
    $(".service-slider-one").owlCarousel({
        nav: false,
        dots: true,
        loop: true,
        margin: 25,
        smartSpeed: 1300,
        autoHeight: true,
        autoplay: false,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            768: {
                items: 2,

            },
            1200: {
                items: 3,

            }
        }
    });
    $(".service-slider-two").owlCarousel({
        nav: false,
        dots: true,
        loop: true,
        margin: 25,
        // center:true,
        smartSpeed: 1300,
        autoplay: false,
        autoHeight: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,

            },
            768: {
                items: 2,

            },
            1200: {
                items: 3,

            }
        }
    });
    /* -------------------------------------
           Project Slider 
     -------------------------------------- */
    $(".project-slider-one").owlCarousel({
        nav: true,
        dots: false,
        loop: true,
        margin: 30,
        navText: ['<i class="flaticon-left-arrow"></i>', '<i class="flaticon-right-arrow-2"></i>'],
        smartSpeed: 1300,
        autoplay: false,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                autoHeight: true

            },
            768: {
                items: 2,

            },
            1200: {
                items: 3,

            }
        }
    });
    $(".project-slider-two").owlCarousel({
        nav: true,
        dots: false,
        loop: true,
        margin: 30,
        navText: ['<i class="flaticon-left-arrow-1"></i>', '<i class="flaticon-right-arrow-angle"></i>'],
        smartSpeed: 1300,
        autoplay: false,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                autoHeight: true

            },
            768: {
                items: 2,

            },
            1200: {
                items: 3,

            }
        }
    });
    /* -------------------------------------
           Partner Slider 
     -------------------------------------- */
    $(".partner-slider").owlCarousel({
        nav: false,
        dots: false,
        loop: true,
        margin: 30,
        smartSpeed: 1300,
        autoplay: true,
        autoHeight: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 3,

            },

            768: {
                items: 4,

            },
            992: {
                items: 4,

            },
            1200: {
                items: 6,

            }
        }
    });
    /* ----------------------------------------
           Magnific Popup Video
     ------------------------------------------*/
    $('.play-now').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        preloader: true,
    });
    /* -------------------------------------
          team Slider 
    -------------------------------------- */
    $(".team-slider-one").owlCarousel({
        nav: true,
        dots: false,
        loop: true,
        margin: 25,
        navText: ['<i class="flaticon-left-arrow-1"></i>', '<i class="flaticon-right-arrow-angle"></i>'],
        smartSpeed: 1300,
        autoplay: false,
        autoHeight: true,
        autoplayTimeout: 7000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                center: false


            },
            768: {
                items: 2,
                center: false


            },
            992: {
                items: 3,
                center: false

            },
            1200: {
                items: 4,

            }
        }
    });
    /* -------------------------------------
           Testimonials Slider 
     -------------------------------------- */
    $(".testimonial-slider-one").owlCarousel({
        nav: false,
        dots: true,
        loop: true,
        margin: 25,
        center: true,
        smartSpeed: 1300,
        autoplay: true,
        autoHeight: true,
        autoplayTimeout: 7000,
        autoplayHoverPause: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                center: false


            },
            768: {
                items: 2,
                center: false


            },
            992: {
                items: 2,
                center: false

            },
            1200: {
                items: 3,

            }
        }
    });

    /* -------------------------------------
          Mobile Topbar 
    -------------------------------------- */

    $('.mobile-top-bar').on('click', function () {
        $('.header-top-right').addClass('open')
    });
    $('.close-header-top').on('click', function () {
        $('.header-top-right').removeClass('open')
    });

    /* -------------------------------------
          sticky Header
    -------------------------------------- */
    var wind = $(window);
    var sticky = $('.header-wrap');
    wind.on('scroll', function () {
        var scroll = wind.scrollTop();
        if (scroll < 100) {
            sticky.removeClass('sticky');
        } else {
            sticky.addClass('sticky');
        }
    });

    /*---------------------------------
        Responsive mmenu
    ---------------------------------*/
    $('.mobile-menu a').on('click', function () {
        $('.main-menu-wrap').addClass('open');
        $('.mobile-bar-wrap.style2 .mobile-menu').addClass('open');
    });

    $('.mobile_menu a').on('click', function () {
        $(this).parent().toggleClass('open');
        $('.main-menu-wrap').toggleClass('open');
    });

    $('.menu-close').on('click', function () {
        $('.main-menu-wrap').removeClass('open')
    });
    $('.mobile-top-bar').on('click', function () {
        $('.header-top').addClass('open')
    });
    $('.close-header-top button').on('click', function () {
        $('.header-top').removeClass('open')
    });
    var $offcanvasNav = $('.main-menu'),
        $offcanvasNavSubMenu = $offcanvasNav.find('.sub-menu');
    $offcanvasNavSubMenu.parent().prepend('<span class="menu-expand"><i class="las la-angle-down"></i></span>');

    $offcanvasNavSubMenu.slideUp();

    $offcanvasNav.on('click', 'li a, li .menu-expand', function (e) {
        var $this = $(this);
        if (($this.attr('href') === '#' || $this.hasClass('menu-expand'))) {
            e.preventDefault();
            if ($this.siblings('ul:visible').length) {
                $this.siblings('ul').slideUp('slow');
            } else {
                $this.closest('li').siblings('li').find('ul:visible').slideUp('slow');
                $this.siblings('ul').slideDown('slow');
            }
        }
        if ($this.is('a') || $this.is('span') || $this.attr('class').match(/\b(menu-expand)\b/)) {
            $this.parent().toggleClass('menu-open');
        } else if ($this.is('li') && $this.attr('class').match(/\b('has-children')\b/)) {
            $this.toggleClass('menu-open');
        }
    });

    /*---------------------------------
         Scroll animation
    ----------------------------------*/
    AOS.init();
    /*-----------------------------------
         Scroll to top
    ----------------------------------*/

    // Show or hide the  button
    $(window).on('scroll', function (event) {
        if ($(this).scrollTop() > 600) {
            $('.back-to-top').fadeIn(300)
            $('.back-to-top').addClass('open')
        } else {
            $('.back-to-top').fadeOut(300)
            $('.back-to-top').removeClass('open')
        }
    });


    //Animate the scroll to top
    $('.back-to-top').on('click', function (event) {
        event.preventDefault();

        $('html, body').animate({
            scrollTop: 0,
        }, 1500);
    });


})(jQuery);