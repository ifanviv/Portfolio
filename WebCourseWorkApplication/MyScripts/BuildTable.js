$(document).ready(function () {

    $.ajax({
        url: '/Announcements/BuildAnnouncementsTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    })
});