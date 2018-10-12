var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.ui-btn-active').off('click').on('click', function (e){
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                datatype: "json",
                type:"POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('kích hoạt');
                        console.log("kích hoạt!");
                    }
                    else {
                        btn.text('khoá');
                        console.log("khoá!");
                    }

                }
            });
        })
    }
}

user.init();