//BYCF
$(document).ready(function () {
    $('#searchByCFForm').submit(function (event) {
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

//BYTIPOPENSIONE
$(document).ready(function () {
    $("#searchByTipoPensioneForm").submit(function (event) {
        event.preventDefault();

        var form = $(this);
        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl; 
                } 
            },
            error: function () {
                alert("An error occurred.");
            }
        });
    });
});