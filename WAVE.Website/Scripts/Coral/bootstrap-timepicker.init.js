$(function()
{
    /*
     * bootstrap-timepicker
     */
    //$('#timepicker1').timepicker();
    //$('#timepicker2').timepicker({
    //    minuteStep: 1,
    //    template: 'modal',
    //    showSeconds: true,
    //    showMeridian: false,
    //    modalBackdrop: true
    //});
    $('#timepicker1').timepicker({
        minuteStep: 5,
        showInputs: false,
        disableFocus: true,
        showMeridian: false
    });
    $('#timepicker2').timepicker({
        minuteStep: 5,
        showInputs: false,
        showMeridian: false,
        disableFocus: true
    });
    $('#timepicker4').timepicker({
        minuteStep: 1,
        secondStep: 5,
        showInputs: false,
        showSeconds: true,
        showMeridian: false
    });
    $('#timepicker5').timepicker({
        template: false,
        showInputs: false,
        showMeridian: false,
        minuteStep: 5
    });

});