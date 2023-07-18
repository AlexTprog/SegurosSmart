listSeguro();

$("#dtFechaVig").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

function listSeguro() {
    $.get("Seguro/GetAll", function (data) {
        createListTable(["Id", "Compañia", "Descripcion", "Tipo", "Numero",
            "Edad Maxima", "Factor Impuesto", "Porcentaje Comision", "Prima", "Moneda", "Importe Mensual", "Cobertura", "Vigencia"], data);
    });
}

function createListTable(arrayColumnas, data) {
    var contenido = "";
    contenido += "<table id='tablas'  class='table' >";
    contenido += "<thead>";
    contenido += "<tr>";
    for (var i = 0; i < arrayColumnas.length; i++) {
        contenido += "<td>";
        contenido += arrayColumnas[i];
        contenido += "</td>";

    }
    contenido += "<td>Acciones</td>";
    contenido += "</tr>";
    contenido += "</thead>";
    var llaves = Object.keys(data[0]);
    contenido += "<tbody>";
    for (var i = 0; i < data.length; i++) {
        contenido += "<tr>";

        for (var j = 0; j < llaves.length; j++) {
            var valorLLaves = llaves[j];
            contenido += "<td>";
            contenido += data[i][valorLLaves];
            contenido += "</td>";

        }
        var llaveId = llaves[0];
        contenido += "<td>";
        contenido += "<button class='btn btn-primary' onclick='openModal(" + data[i][llaveId] + ")' data-toggle='modal' data-target='#myModal'>E</button> "
        contenido += "<button class='btn btn-danger' onclick='eliminar(" + data[i][llaveId] + ")' >D</button>"
        contenido += "</td>"

        contenido += "</tr>";
    }
    contenido += "</tbody>";
    contenido += "</table>";
    document.getElementById("tabla").innerHTML = contenido;
    $("#tablas").dataTable(
        {
            searching: false
        }

    );
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

function borrarDatos() {
    var controles = document.getElementsByClassName("borrar");
    var ncontroles = controles.length;
    for (var i = 0; i < ncontroles; i++) {
        controles[i].value = "";
    }
}

function datosObligarios() {
    var exito = true;
    var controlesObligatorio = document.getElementsByClassName("obligatorio");
    var ncontroles = controlesObligatorio.length;
    for (var i = 0; i < ncontroles; i++) {
        if (controlesObligatorio[i].value == "") {
            exito = false;
            controlesObligatorio[i].parentNode.classList.add("error");
        }
        else {
            controlesObligatorio[i].parentNode.classList.remove("error");
        }
    }

    return exito;
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
            document.getElementById("txtComp").value = data[0].Compania;
            document.getElementById("txtDesc").value = data[0].Descripcion;

            document.getElementById("txtTipo").value = data[0].Tipo;
            document.getElementById("txtNumero").value = data[0].Numero;
            document.getElementById("txtEdadMax").value = data[0].EdadMaxima;
            document.getElementById("txtFactImp").value = data[0].FactorImpuesto;
            document.getElementById("txtPorcentajeCom").value = data[0].PorcentajeComision;
            document.getElementById("txtPrima").value = data[0].Prima;
            document.getElementById("txtMensual").value = data[0].ImporteMensual;
            document.getElementById("txtCobertura").value = data[0].Cobertura;

            document.getElementById("txtMoneda").value = data[0].Moneda;            

            document.getElementById("dtFechaVig").value = data[0].FechaVigencia;
        });
    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idCompania = document.getElementById("txtIdComp").value;
        var descripcion = document.getElementById("txtDesc").value;
        var ruc = document.getElementById("txtRuc").value;
        var razonSocial = document.getElementById("txtRazonSocial").value;
        var contacto = document.getElementById("txtContacto").value;
        var celular = document.getElementById("txtCelular").value;
        var contrato = document.getElementById("txtContrato").value;

        var fechaRen = document.getElementById("dtFechaRen").value;

        frm.append("Id", idCompania);
        frm.append("Descripcion", descripcion);
        frm.append("Ruc", ruc);
        frm.append("RazonSocial", razonSocial);
        frm.append("Contacto", contacto);
        frm.append("Celular", celular);
        frm.append("Contrato", contrato);

        frm.append("FechaNacimiento", fechaRen);

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