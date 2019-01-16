jQuery(document).ready(function($) {
    $("#menu").mouseleave(function() {
        var d = document.getElementsByClassName("container-fluid")[0];
        d.className = "container-fluid";
        var b = $("#menubutton > i")[0];
        b.className = "fa fa-chevron-down";
    });
    $("#menu").mouseenter(function() {
        var d = document.getElementsByClassName("container-fluid")[0];
        d.className = d.className + " openmenu";
        var b = $("#menubutton > i")[0];
        b.className = "fa fa-chevron-right";
    });
    $("#menubutton").mouseleave(function() {
        var d = document.getElementsByClassName("container-fluid")[0];
        d.className = "container-fluid";
        var b = $("#menubutton > i")[0];
        b.className = "fa fa-chevron-down";
    });
    $("#menubutton").mouseenter(function() {
        var d = document.getElementsByClassName("container-fluid")[0];
        d.className = d.className + " hovermenu";
        var b = $("#menubutton > i")[0];
        b.className = "fa fa-chevron-right";
    });
    $("#menubutton").click(function() {
        var d = document.getElementsByClassName("openmenu")[0];
        var b = $("#menubutton > i")[0];
        if (d != null) {
            d.className = "container-fluid";
            b.className = "fa fa-chevron-down";
            return;
        }
        d = document.getElementsByClassName("container-fluid")[0];
        d.className = "container-fluid openmenu";
        b.className = "fa fa-chevron-right";
    });


    $(".showbtn").click(function() { $("#result").css("display", "block"); });
});

function createProgressBars() {
    /* Bootstrap Progressbar */
    $('.progress .bar').progressbar({
        display_text: 'center',
        use_percentage: false,
        amount_format: function(p, t) {
            var diff = t - p;
            //return p + ' of ' + t;
            return diff + ' left';
        }
    });
}

function spin() {
    var opts = {
        lines: 17, // The number of lines to draw
        length: 40, // The length of each line
        width: 7, // The line thickness
        radius: 60, // The radius of the inner circle
        corners: 1, // Corner roundness (0..1)
        rotate: 0, // The rotation offset
        direction: 1, // 1: clockwise, -1: counterclockwise
        color: '#000', // #rgb or #rrggbb or array of colors
        speed: 1, // Rounds per second
        trail: 35, // Afterglow percentage
        shadow: false, // Whether to render a shadow
        hwaccel: false, // Whether to use hardware acceleration
        className: 'spinner', // The CSS class to assign to the spinner
        zIndex: 2e9, // The z-index (defaults to 2000000000)
        top: '50%', // Top position relative to parent
        left: '50%' // Left position relative to parent
    };
    var target = document.getElementsByClassName('layout-app')[0];
    var spinner = new Spinner(opts).spin(target);
}
$(document).ready(function () {
    $('.mytooltip').tooltipster(
    {
        theme: 'tooltipster-shadow'
    });
});