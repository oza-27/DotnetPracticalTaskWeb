var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Categories/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "isActive", "width": "15%" },
            {
                "data": "categoryId",
                "render": function (data) {
                    return `
                      <div class="w-75 btn-group" role="group">
                        <a class="btn btn-primary mx-2" onclick="outputEdit('Categories/Edit/?id=${data}','Edit Category')"> Edit </a>
                        <a class="btn btn-danger mx-2" onclick="outputDelete('Categories/Delete/?id=${data}','Delete Category')"> Delete </a>
                        <a class="btn btn-primary mx-2" onclick="outputChild('Categories/CreateChild/?id=${data}', 'Add a child')"> Add Child </a>
                       </div>
                            `
                },
                "width":"15%"
            }
        ]
    });
}


