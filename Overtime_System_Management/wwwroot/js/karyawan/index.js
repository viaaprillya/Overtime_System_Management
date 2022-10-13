const Id = document.getElementById('UserId').value;
console.log(Id);
const GenderId = document.getElementById('GenderId').value;
console.log(GenderId);

$(document).ready(function () {

    var t = $('#karyawanTable1').DataTable({

        ajax: {
            url: `https://localhost:44372/api/Lembur/KaryawanID?karyawanId=${Id}`,
            dataSrc: "data",
            dataType: "JSON"
        },
        columnDefs: [
            {
                searchable: false,
                orderable: false,
                targets: 0,
            },
        ],
        order: [[1, 'asc']],
        columns: [
            {
                data: "durasi"
            },
            {
                data: "id"
            },
            {
                data: "tanggal",
                render: function (data) {
                    const month = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    let tanggal = new Date(data);
                    let date = tanggal.getDate() > 9 ? tanggal.getDate() : "0" + tanggal.getDate();
                    return `${date} ${month[tanggal.getMonth()]} ${tanggal.getFullYear()}`;
                }
            },
            {
                data: "durasi"
            },
            {
                data: "keterangan"
            },
            {
                data: null,
                render: function (data, type, meta) {
                    let approvalBtn;
                    if (data.approval == "Processing") {
                        approvalBtn = `<button class="btn btn-warning btn-sm" onclick="editLembur(${data.id})" data-toggle="modal"
                        data-target="#editFormModal")>Pending</button>`
                    }
                    else if (data.approval == "Approved") {
                        approvalBtn = `<button class="btn btn-success btn-sm" style="pointer-events: none;">Approved</button>`
                    }
                    else if (data.approval == "Rejected") {
                        approvalBtn = `<button class="btn btn-danger btn-sm" style="pointer-events: none;">Rejected</button>`
                    }
                    return approvalBtn;
                    
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

    //Chart JS
    const months = [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December",
    ];

    $.ajax({
        url: `https://localhost:44372/api/Lembur/KaryawanID?karyawanId=${Id}`,
        type: "GET",
    })
        .done((result) => {
            const data = {
                labels: months,
                datasets: [
                    {
                        label: "Total Durasi Lembur (Jam)",
                        backgroundColor: "rgb(37, 117, 218 )",
                        borderColor: "rgb(33, 102, 190 , 0.5)",
                        data: [],
                        borderWidth: 5,
                    },
                ],
            };

            const config = {
                type: "line",
                data: data,
                options: {},
            };
            let dataLembur = result.data.filter((x) => x.approval == "Approved");
            let totalLembur = new Array(12).fill(0);
            $.each(dataLembur, function (index, element) {
                let bulan = new Date(element.tanggal).getMonth();
                totalLembur[bulan] += element.durasi;
            });

            data.datasets[0].data = totalLembur;
            config.options = {
                scales: {
                    y: {
                        max: Math.max(...totalLembur) * 1.5,
                    },
                },
            };
            const myChart = new Chart(document.getElementById("myChart"), config);
        })
        .fail((error) => {
            console.log(error);
        });
});

function Insert(event, karyawanId) {
    event.preventDefault();
    Swal.fire({
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
            var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
            //ini ngambil value dari tiap inputan di form nya
            //console.log(karyawanId);
            obj.karyawanId = parseInt(karyawanId);
            obj.tanggal = $("#lemburTanggal").val();
            obj.durasi = parseInt($("#lemburDurasi").val());
            obj.keterangan = $("#lemburKeterangan").val();
            //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
            $.ajax({
                contentType: "application/json",
                url: "https://localhost:44372/api/Lembur/PengajuanLembur",
                type: "POST",
                data: JSON.stringify(obj) //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
            }).done((result) => {
                //buat alert pemberitahuan jika success
                Swal.close()
                Swal.fire({
                    allowOutsideClick: false,
                    title: 'Request submitted',
                    text: 'Please wait for approval!',
                    icon: 'success'
                });
                $('#karyawanTable1').DataTable().ajax.reload();
                $('#requestFormModal').modal('hide');
                $('#requestFormModal').on('hidden.bs.modal', function () {
                    $(this).find('form').trigger('reset');
                });
            }).fail((error) => {
                console.log(error);
            })
        }
    });
};

function editLembur(id) {
    $.ajax({
        url: `https://localhost:44372/api/Lembur/ID?idLembur=${id}`,
        type: "GET",
    }).done((result) => {
        let lembur = result.data;
        $("#editId").val(lembur.id);
        console.log(lembur.id);
        $("#editKaryawanId").val(lembur.karyawanID);
        console.log(lembur.karyawanID);
        let tanggal = (lembur.tanggal).slice(0, -9);
        $("#editTanggal").val(tanggal);
        console.log(tanggal);

        const month = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        let dataTanggal = new Date(lembur.tanggal);
        let date = dataTanggal.getDate() > 9 ? dataTanggal.getDate() : "0" + dataTanggal.getDate();
        $("#detail-tanggal").html(`${date} ${month[dataTanggal.getMonth()]} ${dataTanggal.getFullYear()}`);
        
        parseInt($("#editDurasi").val(lembur.durasi));
        $("#detail-durasi").html(lembur.durasi);
        console.log(lembur.durasi);
        $("#editKeterangan").val(lembur.keterangan);
        $("#detail-keterangan").html(lembur.keterangan);
        console.log(lembur.keterangan);
        $("#editApproval").val(lembur.approval);
        console.log(lembur.approval);

        $("#deleteApproval").val(lembur.id);
    });
}

function editedLembur() {
    event.preventDefault();
    Swal.fire({
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
            var obj = new Object();
            obj.id = parseInt($("#editId").val());
            obj.karyawanID = parseInt($("#editKaryawanId").val());
            obj.durasi = parseInt($("#editDurasi").val());
            obj.tanggal = $("#editTanggal").val();
            obj.keterangan = $("#editKeterangan").val();
            obj.approval = $("#editApproval").val();
            $.ajax({
                contentType: "application/json",
                url: "https://localhost:44372/api/Lembur",
                type: "PUT",
                data: JSON.stringify(obj)
            }).done((result) => {
                Swal.close();
                Swal.fire({
                    allowOutsideClick: false,
                    title: 'Overtime Edited',
                    text: `Overtime request has been successfully edited!`,
                    icon: 'success'
                });
                $('#karyawanTable1').DataTable().ajax.reload();
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

function deletedLembur(opt) {
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
                    url: `https://localhost:44372/api/Lembur?ID=${id}`,
                    type: "DELETE"
                });
                Swal.close();
                Swal.fire({
                    allowOutsideClick: false,
                    title: 'Overtime Request Deleted',
                    text: `Overtime request has been successfully deleted!`,
                    icon: 'success'
                });
                $('#karyawanTable1').DataTable().ajax.reload();
                $('#deleteModal').modal('hide');
                $('#editFormModal').modal('hide');
            }
        });
    }
}
