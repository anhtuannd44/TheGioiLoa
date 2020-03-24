(function ($) {
    "use strict";

    $('[data-toggle="tooltip"]').tooltip();
    // Preloader (if the #preloader div exists)
    $(window).on('load', function () {
        if ($('#preloader').length) {
            $('#preloader').delay(200).fadeOut(500, function () {
                $(this).remove();
            });
        }
    });

    //Scroll backtop and menufixtop
    $(window).scroll(function () {
        var height = 100;
        if ($("#menuCategoryShow #main-nav").data("homepage") == "True") {
            height = 400;
        }
        if ($(this).scrollTop() > height) {
            $('#back-to-top').fadeIn();
            if (!window.matchMedia("(max-width: 767px)").matches) {
                $("#navbarMain").addClass("fixed-top").removeClass("py-md-4").addClass("py-0");
                $(".hidetop").css("opacity", "0");
                $("#headerBottom").removeClass("bg-white border-bottom");
                $("#headerBottom").addClass("fixed-top");

                if ($("#menuCategoryShow #main-nav").data("homepage") == "True")
                    $("#menuCategoryShow #main-nav").addClass("menu-not-expand");

            }
            else {
                $("#searchBar").addClass("fixed-top bg-primary py-2");
            }

        } else {
            $('#back-to-top').fadeOut();
            if (!window.matchMedia("(max-width: 767px)").matches) {

                $("#navbarMain").removeClass("fixed-top").addClass("py-md-4").removeClass("py-0");
                $(".hidetop").css("opacity", "1");
                $("#headerBottom").addClass("bg-white border-bottom");
                $("#headerBottom").removeClass("fixed-top");
                if ($("#menuCategoryShow #main-nav").data("homepage") == "True")
                    $("#menuCategoryShow #main-nav").removeClass("menu-not-expand");
            }
            else {
                $("#searchBar").removeClass("fixed-top bg-primary py-2");
            }
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        $('#back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });
    // Initiate the wowjs animation library
    //new WOW().init();

    // Header scroll class
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#header').addClass('header-scrolled');
        } else {
            $('#header').removeClass('header-scrolled');
        }
    });

    if ($(window).scrollTop() > 100) {
        $('#header').addClass('header-scrolled');
    }

    // Smooth scroll for the navigation and links with .scrollto classes
    $('.main-nav a, .mobile-nav a, .scrollto').on('click', function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            if (target.length) {
                var top_space = 0;

                if ($('#header').length) {
                    top_space = $('#header').outerHeight();

                    if (!$('#header').hasClass('header-scrolled')) {
                        top_space = top_space - 20;
                    }
                }

                $('html, body').animate({
                    scrollTop: target.offset().top - top_space
                }, 1500, 'easeInOutExpo');

                if ($(this).parents('.main-nav, .mobile-nav').length) {
                    $('.main-nav .active, .mobile-nav .active').removeClass('active');
                    $(this).closest('li').addClass('active');
                }

                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('.mobile-nav-toggle i').toggleClass('fa-times fa-bars');
                    $('.mobile-nav-overly').fadeOut();
                }
                return false;
            }
        }
    });

    // Navigation active state on scroll
    var nav_sections = $('section');
    var main_nav = $('.main-nav, .mobile-nav');
    var main_nav_height = $('#header').outerHeight();

    $(window).on('scroll', function () {
        var cur_pos = $(this).scrollTop();

        nav_sections.each(function () {
            var top = $(this).offset().top - main_nav_height,
                bottom = top + $(this).outerHeight();

            if (cur_pos >= top && cur_pos <= bottom) {
                main_nav.find('li').removeClass('active');
                main_nav.find('a[href="#' + $(this).attr('id') + '"]').parent('li').addClass('active');
            }
        });
    });

    //// jQuery counterUp (used in Whu Us section)
    //$('[data-toggle="counter-up"]').counterUp({
    //  delay: 10,
    //  time: 1000
    //});

    //// Porfolio isotope and filter
    //$(window).on('load', function () {
    //  var portfolioIsotope = $('.portfolio-container').isotope({
    //    itemSelector: '.portfolio-item'
    //  });
    //  $('#portfolio-flters li').on( 'click', function() {
    //    $("#portfolio-flters li").removeClass('filter-active');
    //    $(this).addClass('filter-active');

    //    portfolioIsotope.isotope({ filter: $(this).data('filter') });
    //  });
    //});

    //// Testimonials carousel (uses the Owl Carousel library)
    //$(".testimonials-carousel").owlCarousel({
    //  autoplay: true,
    //  dots: true,
    //  loop: true,
    //  items: 1
    //});

})(jQuery);

