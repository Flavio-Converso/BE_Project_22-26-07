$(document).ready(function () {
    $('#searchForm').submit(function (event) {
        event.preventDefault(); 

        var form = $(this);
        var url = form.attr('action');
        var data = form.serialize(); 

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                if (response.redirectUrl) {
                    window.location.href = response.redirectUrl; 
                }
            },
            error: function () {
                alert('Si è verificato un errore. Per favore, riprova.');
            }
        });
    });
});