var tyreslots = function () {
    $('#TyreHotel').change(function () {
        if ($(this).is(":checked")) {
            $("#TyreSlots").removeAttr('disabled');
        } else {
            $("#TyreSlots").attr('disabled', '');
        }
    });
};
