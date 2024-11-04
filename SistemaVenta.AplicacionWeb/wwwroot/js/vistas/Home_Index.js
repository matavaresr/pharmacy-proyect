let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Productos/ListaIsLowOnStock',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idProducto", "visible": false, "searchable": false },
            {
                "data": "urlImagen", render: function (data) {
                    return `<img style= "height:60px" src =${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "marca" },
            { "data": "descripcion" },
            { "data": "nombreCategoria", "visible": false, "searchable": false },
            //{ "data": "stock" },
            {
                "data": "stock", render: function (data) {
                    return '<span style="color:red;">' + data + '</span>';
                }
            },
            {
                "data": "esActivo", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Activo</span>';

                    else
                        return '<span class="badge badge-danger">noActivo</span>';
                }
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Productos',
                exportOptions: {
                    columns: [2, 3, 5, 6]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});