function select2Dropdown() {

    $('.select2').select2({
        placeholder: function () {
            $(this).data('placeholder');
        },
        allowClear: true,
        
    });

}
