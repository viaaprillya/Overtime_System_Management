function changeTab(val) {

}

$(document).ready(function () {
    

    var t = $('#karyawanTable2').DataTable({

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
        order: [[0, 'asc']],
        columns: [
            {
                data: "",
                render: function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: "bulan",
                render: function (data) {
                    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    return months[data - 1];
                }
            },
            {
                data: "tahun",
            },
            {
                data: "gajiPokok",
                render: function (data) {
                    return new Intl.NumberFormat("id-ID", { style: "currency", currency: "IDR" }).format(data);
                }
            },
            {
                data: "tunjangan",
                render: function (data) {
                    return new Intl.NumberFormat("id-ID", { style: "currency", currency: "IDR" }).format(data);
                }
            },
            {
                data: 'totalBonusLembur',
                render: function (data) {
                    return new Intl.NumberFormat("id-ID", { style: "currency", currency: "IDR" }).format(data);
                }
            },
            {
                data: 'totalGaji',
                render: function (data) {
                    return new Intl.NumberFormat("id-ID", { style: "currency", currency: "IDR" }).format(data);
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

    //$("#karyawanTable2")[0].style.width = '100%';
});