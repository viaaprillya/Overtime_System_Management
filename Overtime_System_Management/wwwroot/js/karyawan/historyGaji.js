const Id = document.getElementById('UserId').value;
console.log(Id);

$(document).ready(function () {
    

    var t = $('#gajiTable').DataTable({

        ajax: {
            url: `https://localhost:44372/api/Gaji/HistoryGajiKaryawan?idKaryawan=${Id}`,
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
                data: "bulan"
            },
            {
                data: "bulan"
            },
            {
                data: "tahun",
            },
            {
                data: "gajiPokok"
            },
            {
                data: "tunjangan"
            },
            {
                data: 'totalBonusLembur',
            },
            {
                data: 'totalGaji',
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