﻿const Id = document.getElementById('UserId').value;

$(document).ready(function () {

    loadTable('1');
    
    //$("#lemburTable2")[0].style.width = '100%';
    //$("#lemburTable1")[0].style.width = '100%';
});

function loadTable(val) {
    var t = $(`#karyawanDTable${val}`).DataTable({
        processing: true,
        fixedColumns: true,
        ajax: {
            url: "https://localhost:44372/api/Karyawan",
            dataSrc: function (json) {
                return json.data.filter(i => i.email != "agus@email.com");
            },
            dataType: "JSON",
        },
        columnDefs: [
            {
                searchable: false,
                orderable: false,
                width: 50,
                targets: 0
            },
        ],
        order: [[0, 'asc']],
        columns: [
            {
                data: "",
                render: function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: "id"
            },
            {
                data: "namaLengkap"
            },
            {
                data: "email"
            },
            {
                data: "nomerRekening"
            },
            {
                data: "nomerTelepon"
            },
            {
                data: "jabatan",
                render: function (data) {
                    return data.namaJabatan;
                }
            },
            {
                data: null,
                render: function (data, type, meta) {
                    return `<button class="btn btn-warning btn-sm" onclick="editKaryawan(${data.id})" data-toggle="modal"
                        data-target="#editFormModal">Edit</button>
                            <button class="btn btn-danger btn-sm" onclick="deleteKaryawan(${data.id})" data-toggle="modal"
                        data-target="#deleteModal">Delete</button>`;
                }
            }
        ]
    });

    t.on('order.dt search.dt', function () {
        let i = 1;

        t.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
            this.data(i++);
        });
    }).draw();
}

function editKaryawan(id) {
    $.ajax({
        url: `https://localhost:44372/api/Karyawan/id?id=${id}`,
        type: "GET",
    }).done((result) => {
        let karyawan = result.data;
        $("#editId").val(karyawan.id);
        console.log(karyawan.id);
        $("#editNama").val(karyawan.namaLengkap);
        console.log(karyawan.namaLengkap);
        $("#editEmail").val(karyawan.email);
        console.log(karyawan.email);
        $("#editNoRek").val(karyawan.nomerRekening);
        console.log(karyawan.nomerRekening);
        $("#editNoTelp").val(karyawan.nomerTelepon);
        console.log(karyawan.nomerTelepon);
        $("#editJabatan").val(karyawan.jabatanID);
        console.log(karyawan.jabatanID);
    });
}

function editedKaryawan() {
    event.preventDefault();
    Swal.fire({
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
            var obj = new Object();
            obj.id = parseInt($("#editId").val());
            obj.namaLengkap = $("#editNama").val();
            obj.email = $("#editEmail").val();
            obj.nomerRekening = $("#editNoRek").val();
            obj.nomerTelepon = $("#editNoTelp").val();
            obj.jabatanID = parseInt($("#editJabatan").val());
            $.ajax({
                contentType: "application/json",
                url: "https://localhost:44372/api/Karyawan/EditKaryawan",
                type: "PUT",
                data: JSON.stringify(obj)
            }).done((result) => {
                Swal.close();
                Swal.fire({
                    allowOutsideClick: false,
                    title: 'User Edited',
                    text: `Employee with name ${obj.namaLengkap} has been successfully edited!`,
                    icon: 'success'
                });
                $('#karyawanDTable1').DataTable().ajax.reload();
                $('#editFormModal').modal('hide');
                $('#editFormModal').on('hidden.bs.modal', function () {
                    $(this).find('form').trigger('reset');
                });
            }).fail((error) => {
                console.log(error);
            });
        }
    });
}

function deleteKaryawan(id) {
    document.getElementById('deleteApproval').value = id;
    $.ajax({
        url: `https://localhost:44372/api/Karyawan/id?id=${id}`,
        type: "GET",
    }).done((result) => {
        let karyawan = result.data;
        $("#detail-id").html(karyawan.id);
        console.log(karyawan.id);
        $("#detail-namaLengkap").html(karyawan.namaLengkap);
        console.log(karyawan.namaLengkap);
        $("#detail-email").html(karyawan.email);
        console.log(karyawan.email);
        $("#detail-noRek").html(karyawan.nomerRekening);
        console.log(karyawan.nomerRekening);
        $("#detail-noTelp").html(karyawan.nomerTelepon);
        console.log(karyawan.nomerTelepon);
        $("#detail-jabatan").html(karyawan.jabatan.namaJabatan);
        console.log(karyawan.jabatan.namaJabatan);
    });
}

function deletedKaryawan(opt) {
    event.preventDefault();
    const id = document.getElementById('deleteApproval').value;
    if (opt == "no") {
        $('#deleteModal').modal('hide');
    }
    else if (opt == "yes") {
        Swal.fire({
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading();
                $.ajax({
                    contentType: "application/json",
                    url: `https://localhost:44372/api/Karyawan/id?ID=${id}`,
                    type: "DELETE"
                });
                Swal.close();
                Swal.fire({
                    allowOutsideClick: false,
                    title: 'User Deleted',
                    text: `Employee has been successfully deleted!`,
                    icon: 'success'
                });
                $('#karyawanDTable1').DataTable().ajax.reload();
                $('#deleteModal').modal('hide');
            }
        });
    }
}

$.ajax({
    url: "https://localhost:44372/api/Jabatan"
}).done((result) => {
    //console.log(result);
    test = "";
    $.each(result.data, function (key, val) {
        test += `<option value="${key + 1}">${val.namaJabatan}</option>`;
    });
    //console.log(test);
    $("#editJabatan").html(test);
}).fail((error) => {
    console.log(error);
});