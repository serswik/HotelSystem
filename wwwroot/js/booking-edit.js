$(document).ready(function () {
    // Функция для пересчета итоговой стоимости
    function calculateTotalPrice() {
        // Получаем даты и цену выбранного номера
        var checkInDate = new Date($('#CheckInDate').val());
        var checkOutDate = new Date($('#CheckOutDate').val());
        var roomPrice = $('#RoomSelect option:selected').data('price'); // Получаем цену из data-price

        // Проверка, что цена и даты валидны
        if (isNaN(checkInDate.getTime()) || isNaN(checkOutDate.getTime()) || isNaN(roomPrice)) {
            $('#TotalPrice').val(''); // Если одно из значений невалидно, очищаем поле
            return;
        }

        var days = (checkOutDate - checkInDate) / (1000 * 3600 * 24); // Разница в днях

        // Если количество дней больше 0, пересчитываем итоговую стоимость
        if (days > 0) {
            var totalPrice = roomPrice * days; // Пересчитываем стоимость
            $('#TotalPrice').val(totalPrice); // Обновляем скрытое поле с итоговой ценой
        } else {
            $('#TotalPrice').val(''); // Если дни меньше 1, очищаем поле
        }
    }

    // При изменении даты или номера пересчитываем стоимость
    $('#CheckInDate, #CheckOutDate, #RoomSelect').change(function () {
        calculateTotalPrice(); // Вызываем функцию пересчета
    });

    // Призначаем изначальную цену при загрузке страницы, если данные уже присутствуют
    calculateTotalPrice();
});
