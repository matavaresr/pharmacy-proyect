

const VISTA_BUSQUEDA = {

    busquedaFecha: () => {

        $("#txtFechaInicio").val("")
        $("#txtFechaFin").val("")
        $("#txtNumeroVenta").val("")

        $(".busqueda-fecha").show()
        $(".busqueda-venta").hide()
    }, busquedaVenta: () => {

        $("#txtFechaInicio").val("")
        $("#txtFechaFin").val("")
        $("#txtNumeroVenta").val("")

        $(".busqueda-fecha").hide()
        $(".busqueda-venta").show()
    }

}

$(document).ready(function () {
    VISTA_BUSQUEDA["busquedaFecha"]()


    $.datepicker.setDefaults($.datepicker.regional["es"])



    $("#txtFechaInicio").datepicker({ dateFormat: "dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" })


})

$("#cboBuscarPor").change(function () {

    if ($("#cboBuscarPor").val() == "fecha") {
        VISTA_BUSQUEDA["busquedaFecha"]()

    } else {
        VISTA_BUSQUEDA["busquedaVenta"]()
    }
})


$("#btnBuscar").click(function () {
    if ($("#cboBuscarPor").val() == "fecha") {

        if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
            toastr.warning("", "Debe ingresar fecha inicio y fin")
            return;
        }

    } else {
        if ($("#txtNumeroVenta").val().trim() == "") {
            toastr.warning("", "Debe ingresar el numero de Entrada")
            return;
        }

    }

    let numeroEntrada = $("#txtNumeroVenta").val()
    let fechaInicio = $("#txtFechaInicio").val()
    let fechaFin = $("#txtFechaFin").val()


    $(".card-body").find("div.row").LoadingOverlay("show");

    fetch(`/Entrada/Historial?numeroEntrada=${numeroEntrada}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
        .then(response => {

            $(".card-body").find("div.row").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            $("#tbventa tbody").html("");

            if (responseJson.length > 0) {

                responseJson.forEach((entrada) => {


                    $("#tbventa tbody").append(
                        $("<tr>").append(
                            $("<td>").text(entrada.fechaRegistro),
                            $("<td>").text(entrada.numeroEntrada),
                            //$("<td>").text(entrada.tipoDocumentoVenta),
                         /*   $("<td>").text(entrada.fechaCaducidad),*/
                            $("<td>").text(entrada.nombreCliente),
                            //$("<td>").text(entrada.total),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-info btn-sm").append(
                                    $("<i>").addClass("fas fa-eye")
                                ).data("entrada", entrada)

                            ),
                        )
                    )
                })
            }



        });



})

$("#tbventa tbody").on("click", ".btn-info", function () {
    let d = $(this).data("entrada");

    $("#txtFechaRegistro").val(d.fechaRegistro);
    $("#txtNumVenta").val(d.numeroEntrada);
    $("#txtUsuarioRegistro").val(d.usuario);
    //$("#txtTipoDocumento").val(d.tipoDocumentoVenta);
/*    $("#txtFechaCaducidad").val(d.fechaCaducidad);*/
    $("#txtNombreCliente").val(d.nombreCliente);
    //$("#txtSubTotal").val(d.subTotal);
    //$("#txtIGV").val(d.impuestoTotal);
    //$("#txtTotal").val(d.total);

    $("#tbProductos tbody").html("");

    d.detalleEntrada.forEach((item) => {
        $("#tbProductos tbody").append(
            $("<tr>").append(
                $("<td>").text(item.descripcionProducto),
                $("<td>").text(item.cantidad)
                //$("<td>").text(item.precio),
                //$("<td>").text(item.total)
            )
        );
    });


    $("#linkImprimir").attr("href", `/Entrada/MostrarPDFVenta?numeroEntrada=${d.numeroEntrada}`)

    $("#modalData").modal("show");
});
