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

    var win = $(window);
    var scroll = true;
    //Scroll backtop and menufixtop
    win.scroll(function () {
        $(function () {
            $('.lazy').lazy();
        });

        var height = 100;
        if ($("#menuCategoryShow #main-nav").data("homepage") == "True") {
            height = 534;
        }
        if ($(this).scrollTop() > height) {
            var left = "calc(50% - 592px)";
            $('#back-to-top').fadeIn();
            if (!window.matchMedia("(max-width: 767px)").matches) {
                $("#navbarMain").addClass("fixed-top").addClass("py-md-2");
                $(".hidetop").hide();
                $("#menuCategoryShow #categoryProduct-a").css("padding", "23px 15px 24px 15px");
                $("#searchBar").addClass("offset-3");
                $("#menuCategoryShow").addClass("fixed-top").css("margin-left", left).css("width", "300px");
                if ($("#menuCategoryShow #main-nav").data("homepage") == "True")
                    $("#menuCategoryShow #main-nav").addClass("menu-not-expand");

            }
            else {
                $("#searchBar").addClass("fixed-top bg-primary py-2");
            }

        } else {
            $('#back-to-top').fadeOut();
            if (!window.matchMedia("(max-width: 767px)").matches) {
                $("#navbarMain").removeClass("fixed-top").removeClass("py-md-2");
                $(".hidetop").show();
                $("#searchBar").removeClass("offset-3");
                $("#menuCategoryShow").removeClass("fixed-top").css("margin-left", "unset").css("width", "unset");
                $("#menuCategoryShow #categoryProduct-a").css("padding", "12px 15px");
                if ($("#menuCategoryShow #main-nav").data("homepage") == "True")
                    $("#menuCategoryShow #main-nav").removeClass("menu-not-expand");
            }
            else {
                $("#searchBar").removeClass("fixed-top bg-primary py-2");
            }
        }

        //Load Social Footer

        if (($("#renderFacebook").offset().top < (win.scrollTop() + win.height() + 500)) && (scroll)) {
            $.ajax({
                url: '/Home/LoadSocial',
                data: { social: "Facebook" },
                success: function (data) {
                    $('#renderFacebook').html(data);
                }
            });
            $.ajax({
                url: '/Home/LoadSocial',
                data: { social: "Youtube" },
                success: function (data) {
                    $('#renderYoutube').html(data);
                }
            });
            $.ajax({
                url: '/Home/LoadGoogleMap',
                success: function (data) {
                    $('#renderGoolgeMap').html(data);
                }
            });
            scroll = false;
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        $('#back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 1000, 'easeInOutExpo');
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
    $('.scrollto').on('click', function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            if (target.length) {
                var top_space = 95;
                $('html, body').animate({
                    scrollTop: target.offset().top - top_space
                }, 1000, 'easeInOutExpo');

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


})(jQuery);

function loadingGifAccountModal() {
    $("#modal-account-loading").show();
}

function loginSuccess(data) {
    if (data.status == "error") {
        $("#loginModalContent").html(data.partial);
    }
    $("#modal-account-loading").hide();
}

