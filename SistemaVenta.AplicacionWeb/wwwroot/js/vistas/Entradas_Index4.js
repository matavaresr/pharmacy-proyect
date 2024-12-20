﻿




let tablaData;
$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional["es"])
    $("#txtFechaInicio").datepicker({ dateFormat: "dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": '/Entradas/ReporteEntrada',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fechaRegistro" },
            { "data": "numeroEntrada" },
            //{ "data": "tipoDocumento" },
            //{ "data": "documentoCliente" },
            /*            { "data": "fechaCaducidad" },*/
            { "data": "nombreCliente" },
            //{ "data": "subTotalVenta" },
            //{ "data": "impuestoTotalVenta" },
            //{ "data": "totalVenta" },
            { "data": "producto" },
            { "data": "cantidad" },
            //{ "data": "precio" },
            //{ "data": "total" },

        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Entradas',
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
        "deferLoading": 0,
    });
});


$("#btnBuscar").click(function () {

    if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
        toastr.warning("", "Debe ingresar fecha inicio y fin")
        return;
    }
    let fechaInicio = $("#txtFechaInicio").val().trim();
    let fechaFin = $("#txtFechaFin").val().trim();

    let nueva_url = `/Reporte/ReporteEntrada?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`

    tablaData.ajax.url(nueva_url).load(); 
})