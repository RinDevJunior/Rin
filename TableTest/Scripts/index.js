
$(document).ready(function () {

    $("#datatab tfoot th").each(function () {
        $(this).html('<input type="text" />');
    });




    var oTable = $("#datatab")
        .DataTable({
            "bStateSave": true, // save page hien tai, khi 
            "bDeferRender": true, //more information in http://legacy.datatables.net/usage/features 
            "serverSide": true, // paging server side
            "searching": true,

            "ajax": {
                "type": "POST",
                "url": "api/Values/DataHandler",
                "contentType": "application/json; charset=utf-8",
                "error": function (xhr, error, thrown) {
                    $("#datatab_processing").hide();
                    alert(thrown);
                },
                'data': function (data) { return JSON.stringify(data); },
                "failure": function () { alert("Fail to connect to server!"); }

            },
            "processing": true,
            "paging": true,
            "columns": [
                { "data": "Name" },
                { "data": "City" },
                { "data": "Postal" },
                { "data": "Email" },
                { "data": "Company" },
                { "data": "Account" },
                { "data": "CreditCard" }
            ],
            "order": [1, "asc"] // sort theo cot thu 2

        });


    $(".filter").on("keyup change",
        function () {
            //clear global search values
            oTable.search("");
            oTable.column($(this).data("columnIndex")).search(this.value).draw();
        });

    $(".dataTables_filter input").on("keyup change",
        function () {
            //clear column search values
            oTable.columns().search("");
            //clear input values
            $(".filter").val("");
        });

});