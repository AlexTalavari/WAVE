if (typeof $.fn.bdatepicker == 'undefined')
	$.fn.bdatepicker = $.fn.datepicker.noConflict();

$(function()
{
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = d.getFullYear() + '/' +
        (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day;
    var d1 = new Date();

    var currDate = d1.getDate();
    var currMonth = d1.getMonth();
    var currYear = d1.getFullYear();

    var dateStr = currDate + "-" + currMonth + "-" + currYear;

	/* DatePicker */
    // default
    $("#datepicker1").bdatepicker({
        format: 'yyyy-mm-dd',
        startDate: output,
        defaultDate: dateStr,
        autoclose: true, showToday: true,
        useCurrent: true
    });

    // component
    $('#datepicker2').bdatepicker({
        format: "yyyy-mm-dd",
        startDate: output,
        defaultDate: dateStr,
        useCurrent: true, showToday: true,
        autoclose: true
    });
    // default
    $("#datepicker11").bdatepicker({
        format: 'yyyy-mm-dd',
        startDate: output, showToday: true,
        defaultDate: dateStr,
        useCurrent: true,
        autoclose: true
    });

    // component
    $('#datepicker22').bdatepicker({
        format: "yyyy-mm-dd",
        startDate: output, showToday: true,
        defaultDate: dateStr,
        useCurrent: true,
        autoclose: true
    });

	// today button
	$('#datepicker3').bdatepicker({
		format: "dd MM yyyy",
		startDate: "2013-02-14",
		useCurrent: true,
		defaultDate: dateStr,
		todayBtn: true, showToday: true,
		autoclose: true
	});

	// advanced
	$('#datetimepicker4').bdatepicker({
		format: "dd MM yyyy - hh:ii",
		autoclose: true, showToday: true,
		defaultDate: dateStr,
        todayBtn: true,
        startDate: "2013-02-14 10:00",
        minuteStep: 10
	});
	
	// meridian
	$('#datetimepicker5').bdatepicker({
		format: "dd MM yyyy - HH:ii P",
		showMeridian: true,
		defaultDate: dateStr,
	    autoclose: true,
	    startDate: "2013-02-14 10:00",
	    todayBtn: true
	});

	// other
	if ($('#datepicker').length) $("#datepicker").bdatepicker({ showOtherMonths:true });
	if ($('#datepicker-inline').length) $('#datepicker-inline').bdatepicker({ inline: true, showOtherMonths: true });
	$('[id^=datepicker]').datepicker('setDate', new Date());
	$('[id^=datepicker]').datepicker('update');
});