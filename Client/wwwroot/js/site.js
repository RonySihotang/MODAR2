// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    let table = $("#myTable").DataTable({
        ajax: {
            url: "https://localhost:7057/api/projectlists/masterprojectlist",
            dataType: "Json",
            dataSrc: "data", //need notice, kalau misal API kalian 
       
        },

        columns: [
            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "id"
            },
            {
                "data": "name"
            },
            {
                "data": "description"
            },
            {
                "data": "status",
                render: function (data, type, row) {
                    if (row.status == 0) {
                        return 'Pending';
                    } else if (row.status == 1) {
                        return 'OnProgress';
                    } else if (row.status == 2) {
                        return 'Complete'
                    }
                }
            },
            {
                "data": "startDate",
                render: DataTable.render.date(),
            },
            {
                "data": "endDate",
                render: DataTable.render.date(),
            },
            {
                "data": "manager"
            },
            {
                "data": "employee"
            },
            {
                render: function (data, type, row) {

                    return `<button class="btn btn-primary" data-bs-toggle="modal" onclick="updateProject('${row.id}')" data-bs-target='#exampleModal' ><i class="bi bi-pencil-square"></i></button>
                        <button id="Delete" class="btn btn-danger " onclick="Delete('${row.id}')" ><span class="bi bi-trash"></span></button>`;
                }
            }
        ],
        dom: "<'row'<'col-lg-4'l><'col-lg-4'B><'col-lg-4'f>>" +
            "<'row'<'col-lg-12'tr>>" +
            "<'row'<'col-lg-5'i><'col-lg-7'p>>",
        buttons: [
            {
                extend: 'pdf',
                className: 'btn-danger btn-xs ms-4',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'csvHtml5',
                className: 'btn-primary btn-xs ms-4',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                className: 'btn-success btn-xs ms-4',
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'colvis',
                className: 'btn btn-dark btn-xs ms-4 mt-2',
            }
        ]
    });
});



function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7057/api/projectlists?id=" + id,
                
                type: 'DELETE',
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    $('#myTable').DataTable().ajax.reload();
                },
                error: function (response) {
                    alert("Something went wrong.");
                    console.log(response);
                },
            });
        }
    })
}



function Insert() {

    let table = $("#myTable").DataTable();
    var obj = new Object();
    obj.id = $("#id").val();
    obj.name = $("#name").val();
    obj.description = $("#description").val();
    obj.status = parseInt($("#status").val());
    obj.startDate = $("#startdate").val();
    obj.endDate = $("#enddate").val();
    obj.managerId = parseInt($("#manager").val());
    obj.employeeId = parseInt($("#employees").val());
    console.log("INI ID UPDATE", obj);

    $.ajax({
        url: "https://localhost:7057/api/projectlists",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj)
    }).done((result) => {
        Swal.fire(
            'Successfully Insert!',
            'New employee has been added!',
            'success',
        )
        $('#exampleModal').modal('hide');
        $('.modal-backdrop').remove();
        table.ajax.reload(false);
    }).fail((error) => {
        Swal.fire(
            'Oops...',
            'Check your input field carefully !',
            'error'
        )
    })


}

// Action Function Insert & Update
$("#Insertbutton").click(function (e) {
    e.preventDefault();
    if ($("#formModal").valid()) {

        var data_action = $(this).attr("data-name");
        if (data_action == "insert") {
            console.log("INI INSERT");
            Insert();
        } else if (data_action == "update") {
            console.log("INI UPDATE");
            UpdatePostProject();
        }
    }
    else Swal.fire(
        'Oops...',
        'Check your inputmu field carefully !',
        'error'
    )

})

// Clear Modal Insert Employee
function InsertEmployee() {
    clearModalEmployee();
}
//Clear Field
function clearModalEmployee() {
    //$('#labelText').html("Create New Employee");
    $('#exampleModalLabel').html("Insert Employee");
    $('#id').val("");
    $('#id').removeAttr('readonly');
    $('#name').val("");
    $('#description').val("");
    $("[name=status]").attr('checked', false);
    $('#startdate').val("");
    $('#enddate').val("");
    $('#manager').val("");
    $("[name=employees]").attr('checked', false);
    $('#Insertbutton').attr('data-name', 'insert').html("<span class='fas fa-save'>&nbsp;</span>Save");

}

/*/UPDATE*/
function updateProject(key) {
    let table = $("#myTable").DataTable();
    console.log(key);
    $.ajax({
        url: 'https://localhost:7057/api/projectlists/' + key
    }).done((result) => {
        console.log(result);

        $('#exampleModalLabel').html("Update Project");
        $('#id').prop('readonly', true);
        $('#id').val(key).readonly;
        $('#name').val(result.data.name);
        $('#description').val(result.data.description)
        $("[name=status][value=" + result.data.status + "]").attr('checked', 'checked'); //setvalue
        var startdate = result.data.startDate;
        var tmp = new Date(startdate);
        var startdate_modified = tmp.getFullYear() + '-' + ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '-' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate()));
        $('#startdate').val(startdate_modified);
        var enddate = result.data.endDate;
        var tmp = new Date(enddate);
        var enddate_modified = tmp.getFullYear() + '-' + ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '-' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate()));
        $('#enddate').val(enddate_modified);
        $('#manager').prop('readonly', true);
        $('#manager').val(result.data.managerId).readonly;
        $("[name=employees][value=" + result.data.employeeId + "]").attr('checked', 'checked'); //setvalue
        $('#Insertbutton').attr('data-name', 'update').html("<span class='fas fa-save'>&nbsp;</span>Update");

        $('#exampleModal').modal('hide');
        table.ajax.reload();

    }).fail((error) => {
        console.log(error);
        Swal.fire(
            'Opps!',
            'Something went wrong!',
            'error'
        )
    });
}

function UpdatePostProject() {
    let table = $("#myTable").DataTable();
    var obj = new Object();
    obj.id = $("#id").val();
    obj.name = $("#name").val();
    obj.description = $("#description").val();
    obj.status = parseInt($("#status").val());
    obj.startDate = $("#startdate").val();
    obj.endDate = $("#enddate").val();
    obj.managerId = $("#manager").val();
    obj.employeeId = parseInt($("#employees").val());
    console.log("INI ID UPDATE", obj);
    $.ajax({
        url: "https://localhost:7057/api/projectlists",
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json"
    }).done((result) => {
        Swal.fire(
            'Success',
            'ID ' + obj.id + ' Berhasil diubah',
            'success'
        )
        table.ajax.reload();
        $('#exampleModal').modal('hide');
        $('.modal-backdrop').remove();
    }).fail((error) => {
        Swal.fire(
            'Opps!',
            'Something went wrong!',
            'error'
        )
    })
}


//$(document).ready(function () {
//    $('#employees').select2({
//        tags:true
//        });
//});




$(document).ready(function() {
    $("#manager").change(function () {
        var m3 = ($("#manager option:selected").val());
   
    //var m3 = $("[id$=manager] option:selected").val();
    $.ajax({
        url: "https://localhost:7057/api/Employees",
        type: "GET",
        dataType: "Json",
        typeof: "data"
    }).done((result) => {
        let emp = "";
        console.log(m3);
        for (let i = 0; i < result.data.length; i++) {
            if (m3 == result.data[i].managerId) {
                emp += `<option value="${result.data[i].id}">${result.data[i].firstName}</option>`
            }
            //else if (m4 == result.data[i].managerId) {
            //    emp += `<option value="${result.data[i].id}">${result.data[i].firstName}</option>`
            //}
        }


        $("#employees").html(emp).select2({
            placeholder: "Select a employees",
            allowClear: true
        });


        //var manager = `<option value="${result.data[3].id}">${result.data.firstName}</option>`


        //$("#manager").html(manager).select2({
        //    placeholder: "Select a manager",
        //    allowClear: true
        //});


    }).fail((error) => {
        console.log(error);

    });
    });
}); 




//function test() {
//    var stringArray = new Array();
//    stringArray[0] = "item1";
//    stringArray[1] = "item2";
//    stringArray[2] = "item3";
//    var postData = { values: stringArray };

//    $.ajax({
//        type: "POST",
//        url: "https://localhost:7057/api/projectlists",
//        data: {
//            employeeId : []
//            },
//        success: function (data) {
//            alert(data.Result);
//        },
//        dataType: "json",
//        traditional: true
//    });
//}
////multiselect
//$.ajax({
//    url: "https://localhost:7057/api/Employees",
//    type: "GET",
//    dataType: "Json",
//    typeof: "data"
//}).done((result) => {
//    var manager = ""
//    if (result.data.managerId == null) {
//        for (let i = 0; i < result.data.length; i++) {
//            manager += `<option value="${result.data[i].id}">${result.data[i].firstName}</option>`
//        }
//    }
//    $("#manager").html(manager);
//}).fail((error) => {
//    console.log(error);
//})