listSeguro();

$("#dtFechaVig").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

$.get("Seguro/GetTipoSeguro", (data) => {
    fillComboEnum(data, document.getElementById("cboTipo"));
});

$.get("Seguro/GetMonedas", (data) => {
    fillComboEnum(data, document.getElementById("cboMoneda"));
});

$.get("Compania/GetAll", (data) => {
    fillComboCompania(data, document.getElementById("cboCompania"));
});

function fillComboCompania(data, control) {
    var content = "";
    for (var i = 0; i < data.length; i++) {
        content += "<option value='" + data[i].Id + "'>";
        content += data[i].RazonSocial;
        content += "</option>";
    }
    control.innerHTML = content;
}

function listSeguro() {
    $.get("Seguro/GetAll", function (data) {
        createListTable(["Id", "Compañia", "Descripcion", "Tipo", "Numero",
            "Edad Maxima", "Factor Impuesto", "Porcentaje Comision", "Prima", "Moneda", "Importe Mensual", "Cobertura", "Vigencia"], data);
    });
}

function eliminar(id) {

    if (confirm("Desea eliminar?") == 1) {

        $.get("Seguro/Delete/?id=" + id, function (data) {
            if (data == 0) {
                alert("Ocurrio un error");
            } else {
                alert("Se elimino correctamente");
                listSeguro();
            }
        });
    }
}

function openModal(id) {

    var controlesObligatorio = document.getElementsByClassName("obligatorio");
    var ncontroles = controlesObligatorio.length;
    for (var i = 0; i < ncontroles; i++) {
        controlesObligatorio[i].parentNode.classList.remove("error");
    }

    if (id == 0) {
        borrarDatos();
        document.getElementById("lblTitulo").innerHTML = "Agregar Seguro";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Seguro";

        $.get("Seguro/Get/?id=" + id, function (data) {
            document.getElementById("txtIdSeguro").value = data[0].Id;
            document.getElementById("cboCompania").value = data[0].Compania;
            document.getElementById("txtDesc").value = data[0].Descripcion;

            document.getElementById("cboTipo").value = data[0].Tipo;
            document.getElementById("txtNumero").value = data[0].Numero;
            document.getElementById("txtEdadMax").value = data[0].EdadMaxima;
            document.getElementById("txtFactImp").value = data[0].FactorImpuesto;
            document.getElementById("txtPorcentajeCom").value = data[0].PorcentajeComision;
            document.getElementById("txtPrima").value = data[0].Prima;
            document.getElementById("txtMensual").value = data[0].ImporteMensual;
            document.getElementById("txtCobertura").value = data[0].Cobertura;

            document.getElementById("cboMoneda").value = data[0].Moneda;

            document.getElementById("dtFechaVig").value = data[0].FechaVigencia;
        });
    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idSeguro = document.getElementById("txtIdSeguro").value;
        var compania = document.getElementById("cboCompania").value;
        var descripcion = document.getElementById("txtDesc").value;
        var tipo = document.getElementById("cboTipo").value;
        var numero = document.getElementById("txtNumero").value
        var edadMaxima = document.getElementById("txtEdadMax").value;
        var factorImpuesto = document.getElementById("txtFactImp").value;
        var porcentajeCom = document.getElementById("txtPorcentajeCom").value;
        var prima = document.getElementById("txtPrima").value;
        var mensual = document.getElementById("txtMensual").value;
        var cobertura = document.getElementById("txtCobertura").value;
        var moneda = document.getElementById("cboMoneda").value;

        var fechaVigencia = document.getElementById("dtFechaVig").value;

        frm.append("Id", idSeguro);
        frm.append("Compania", compania);
        frm.append("Descripcion", descripcion);
        frm.append("Tipo", tipo);
        frm.append("Numero", numero);
        frm.append("EdadMaxima", edadMaxima);
        frm.append("FactorImpuesto", factorImpuesto);
        frm.append("PorcentajeComision", porcentajeCom);
        frm.append("Moneda", moneda);
        frm.append("Prima", prima);
        frm.append("ImporteMensual", mensual);
        frm.append("Cobertura", cobertura);

        frm.append("FechaVigencia", fechaVigencia);

        frm.append("Estado", 0);

        if (confirm("Desea guardar los cambios?") == 1) {

            $.ajax({
                type: "POST",
                url: "Seguro/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {

                    if (data == 0) {
                        alert("Ocurrio un error");
                    } else {
                        alert("Se ejecuto correctamente");
                        listSeguro();
                        document.getElementById("btnCancelar").click();
                    }
                }
            })
        }
    }
}