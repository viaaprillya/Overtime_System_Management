function changeTab(val) {

}

$(document).ready(function () {

    loadTable('1');
    loadTable('2');
    $("#lemburTable2")[0].style.width = '100%';
    $("#lemburTable1")[0].style.width = '100%';
});

function loadTable(val) {
    var t = $(`#lemburTable${val}`).DataTable({
        processing: true,
        fixedColumns: true,
        ajax: {
            url: "https://localhost:44372/api/Lembur",
            dataSrc: function (json) {
                let result;
                if (val == '1') {
                    result = json.data.filter(i => i.approval == "Processing");
                }
                else if (val == '2') {
                    result = json.data.filter(i => i.approval != "Processing");
                }
                return result;
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
                        approvalBtn = `<button class="btn btn-warning btn-sm" onclick="getLembur(${data.id})" data-toggle="modal"
                        data-target="#approvalModal">Processing</button>`
                    }
                    else if (data.approval == "Approved") {
                        approvalBtn = `<button class="btn btn-success btn-sm">Approved</button>`
                    }
                    else if (data.approval == "Rejected") {
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
}

function getLembur(id) {
    document.getElementById('lemburApprovalId').value = id;
}

function Approve(opt) {
    const id = document.getElementById('lemburApprovalId').value;

    $.ajax({
        url: `https://localhost:44372/api/Lembur/ID?idLembur=${id}`,
        type: "GET",
    }).done((result) => {

        let lembur = result.data;
        if (opt == "no") {
            lembur.approval = "Rejected";
        }
        else if (opt == "yes") {
            lembur.approval = "Approved";
        }
        console.log(lembur);

        $.ajax({
            url: "https://localhost:44372/api/Lembur/",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(lembur)
        })
            .done((result) => {
                Swal.fire(
                    'Approval Processed',
                    `This request has been ${lembur.approval}`,
                    'success'
                )
                $('#lemburTable1').DataTable().ajax.reload();
                $('#lemburTable2').DataTable().ajax.reload();
            })
            .fail((error) => {
                console.log("post error");
                console.log(error);
            });

    }).fail((error) => {
        console.log(error);
    })
}

function Registrasi() {
    event.preventDefault();
    var obj = new Object(); 
    obj.namaLengkap = $("#registrasiNama").val();
    obj.email = $("#registrasiEmail").val();
    obj.nomerRekening = $("#registrasiNoRekening").val();
    obj.nomerTelepon = $("#registrasiNoTelepon").val();
    obj.jabatanID = parseInt($("#registrasiJabatan").val());
    $.ajax({
        contentType: "application/json",
        url: "https://localhost:44372/api/Account/Register/",
        type: "POST",
        data: JSON.stringify(obj) 
    }).done((result) => {
        
        Swal.fire(
            'User Registered',
            `Karyawan dengan nama ${obj.namaLengkap} berhasil diregistrasi!`,
            'success'
        )

    }).fail((error) => {
        console.log(error);
    })
}

function CetakSlipGaji() {
    const id = $("#cetakId").val();
    const tanggal = $("#cetakBulanTahun").val().split("-");

    $.ajax({
        url: `https://localhost:44372/api/Gaji/CetakSlipGaji?KaryawanID=${id}&Bulan=${tanggal[1]}&Tahun=${tanggal[0]}`,
        type: "GET",
    }).done((result) => {
        let gaji = result.data;
        console.log(gaji);
        $("#detail-id")[0].innerHTML = (gaji.karyawanID);
        $("#detail-nama")[0].innerHTML = (gaji.namaKaryawan);
        $("#detail-bulan")[0].innerHTML = (gaji.bulan);
        $("#detail-tahun")[0].innerHTML = (gaji.tahun);
        $("#detail-gaji")[0].innerHTML = (gaji.gajiPokok);
        $("#detail-tunjangan")[0].innerHTML = (gaji.tunjangan);
        $("#detail-lembur")[0].innerHTML = (gaji.totalBonusLembur);
        $("#detail-totalGaji")[0].innerHTML = (gaji.totalGaji);

        $("#slipGajiModal").modal();

    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Data does not exist!',
            text: 'Please check your input again!',
        })
    })
}