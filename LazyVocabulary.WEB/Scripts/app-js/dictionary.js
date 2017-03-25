$(function () {
    $.ajaxSetup({ cache: false });
    $(document).on("click", "a[data-modal]", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');

            bindForm(this);
        });
        return false;
    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#progress').hide();

                    $('#target').load('/Song/PostFilter #ajaxGrid',
                        {
                            searchString: document.getElementById("searchString").value,
                            albumID: document.getElementById("AlbumID").value
                        });
                    //location.reload();
                    //alert("Информация об альбоме была успешно удалена.");
                } else {
                    $('#myModalContent').html(result);
                    bindForm(this);
                }
            }
        });
        return false;
    });
}
