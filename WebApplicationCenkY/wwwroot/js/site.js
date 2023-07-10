// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.








let remote_url = "https://drive.google.com/uc?export=download&id=1RmyT-xa9OuwU05OUatS5vcQNFZgo0q1i";

$(document).ready(function () {
    //debugger;
    //get_url();


})
$('#load_data').click(function () {
    debugger;
    $.ajax({
        url: "https://drive.google.com/uc?export=download&id=1RmyT-xa9OuwU05OUatS5vcQNFZgo0q1i",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        dataType: "text/csv",
        success: function (data) {
            var u_data = data.split(/\r?\n|\r/);
            var table_data = '<table class="table table-bordered table-striped">';
            for (var count = 0; count < u_data.length; count++) {
                var cell_data = u_data[count].split(",");
                table_data += '<tr>';
                for (var cell_count = 0; cell_count < cell_data.length; cell_count++) {
                    if (count === 0) {
                        table_data += '<th>' + cell_data[cell_count] + '</th>';
                    }
                    else {
                        table_data += '<td>' + cell_data[cell_count] + '</td>';
                    }
                }
                table_data += '</tr>';
            }
            table_data += '</table>';
            $('#xlSupplyTable').html(table_data);
        }
    });

})

/*<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<CSV upload >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>*/
//$(function () {
//    $("#upload").bind("click", function () {
//        alert('osman');
//        debugger;
//        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.csv|.txt)$/;
//        if (regex.test($("#fileUpload").val().toLowerCase())) {

//            var reader = new FileReader();
//            reader.onload = function (e) {
//                //var table = $("<table />");
//                //var rows = e.target.result.split("\n");
//                //for (var i = 0; i < rows.length; i++) {
//                //    var row = $("<tr />");
//                //    var cells = rows[i].split(",");
//                //    for (var j = 0; j < cells.length; j++) {
//                //        var cell = $("<td />");
//                //        cell.html(cells[j]);
//                //        row.append(cell);
//                //    }
//                //    table.append(row);
//                //}
//                //$("#tblCSV").html('');
//                //$("#tblCSV").append(table);
//                debugger;
//                $.ajax({
//                    type: "POST",
//                    url: "/Feed/Create,
//                    data: e.target.result,
//                    success: success,
//                    dataType: 'text/csv'
//                });

//            }
//            reader.readAsText($("#fileUpload")[0].files[0]);
//        } else {
//            alert("Please upload a valid CSV file.");
//        }
//    });
//});

$("#upload").click(function () {
    //alert("çalıştı");
    debugger;
    let file = $("#fileUpload")[0].files[0];
    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.csv|.txt)$/;
    if (regex.test($("#fileUpload").val().toLowerCase())) {
        let uploadFile = new FormData();
        //uploadFile = $("#fileUpload").val();
        if (file.size > 0) {
            uploadFile.append("file", $("#fileUpload")[0].files[0]);
            $.ajax({
                url: "../Feed/Create",
                contentType: false,
                processData: false,
                data: uploadFile,
                type: 'POST',
                success: function () {
                    alert("Successfully Added & processed");
                }
            });
        }

    } else {
        alert("Please upload a valid CSV file.");
    }
})