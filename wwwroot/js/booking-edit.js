$(document).ready(function () {
    $('#CheckInDate, #CheckOutDate, #RoomSelect').change(function () {
        var checkInDate = new Date($('#CheckInDate').val());
        var checkOutDate = new Date($('#CheckOutDate').val());
        var roomPrice = $('#RoomSelect option:selected').data('price');

        if (checkInDate && checkOutDate && roomPrice) {
            var days = (checkOutDate - checkInDate) / (1000 * 3600 * 24);
            if (days > 0) {
                var totalPrice = roomPrice * days;
                $('#TotalPrice').val(totalPrice + ' UAH');
            } else {
                $('#TotalPrice').val('');
            }
        } else {
            $('#TotalPrice').val('');
        }
    });
});
