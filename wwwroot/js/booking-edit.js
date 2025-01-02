$(document).ready(function () {
    $('#RoomId').change(function () {
        updateTotalPrice();
    });

    $('#CheckInDate, #CheckOutDate').change(function () {
        updateTotalPrice();
    });

    function updateTotalPrice() {
        var roomId = $('#RoomId').val();
        var checkInDate = $('#CheckInDate').val();
        var checkOutDate = $('#CheckOutDate').val();

        if (checkInDate && checkOutDate) {
            var days = (new Date(checkOutDate) - new Date(checkInDate)) / (1000 * 3600 * 24);

            var selectedRoom = $('#RoomId option:selected').text();
            var roomPrice = parseFloat(selectedRoom.split('(')[1].split(' ₴')[0]);

            var totalPrice = roomPrice * days;
            $('#TotalPrice').val(totalPrice);
        }
    }
});
