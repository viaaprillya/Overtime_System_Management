const Id = document.getElementById('UserId').value;

$(document).ready(function () {

    
    var t = $('#lemburTable').DataTable({

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
                data: 'approval',
                render: function (data, type, meta) {
                    let approvalBtn;
                    if (data == "Processing") {
                        approvalBtn = `<button class="btn btn-warning btn-sm" >Processing</button>`
                    }
                    else if (data == "Approved") {
                        approvalBtn = `<button class="btn btn-success btn-sm">Approved</button>`
                    }
                    else if (data == "Rejected") {
                        approvalBtn = `<button class="btn btn-danger btn-sm">Rejected</button>`
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
});

function Insert(event, karyawanId) {
    event.preventDefault();
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    console.log(karyawanId);
    obj.karyawanId = parseInt(karyawanId);
    obj.tanggal = $("#lemburTanggal").val();
    obj.durasi = parseInt($("#lemburDurasi").val());
    obj.keterangan =$("#lemburKeterangan").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        contentType: "application/json",
        url: "https://localhost:44372/api/Lembur/PengajuanLembur",
        type: "POST",
        data: JSON.stringify(obj) //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire(
            'Request submitted',
            'Please wait for approval!',
            'success'
        )
        $('#lemburTable').DataTable().ajax.reload();

    }).fail((error) => {
        console.log(error);
    })
};

