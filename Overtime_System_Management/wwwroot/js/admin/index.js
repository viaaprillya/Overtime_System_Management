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
                data: 'approval',
                render: function (data, type, meta) {
                    let approvalBtn;
                    if (data == "Processing") {
                        approvalBtn = `<button class="btn btn-warning btn-sm" data-toggle="modal"
                        data-target="#approvalModal">Processing</button>`
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
}
