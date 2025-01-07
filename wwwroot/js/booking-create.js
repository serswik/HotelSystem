$(document).ready(function () {
    $('#HotelSelect').change(function () {
        var hotelId = $(this).val();
        if (hotelId) {
            $.ajax({
                url: '/Booking/GetRoomsByHotel',
                data: { hotelId: hotelId },
                success: function (data) {
                    $('#RoomSelect').empty();
                    $('#RoomSelect').append('<option value="">Select a room</option>');
                    $.each(data, function (i, room) {
                        $('#RoomSelect').append('<option value="' + room.value + '" data-price="' + room.price + '">' + room.text + '</option>');
                    });
                }
            });
        }
    });

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