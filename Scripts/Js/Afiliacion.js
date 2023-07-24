listAfiliacion();


$("#dtFechaAfiliacion").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

$.get("Cliente/GetAll", (data) => {
    fillComboCliente(data, document.getElementById("cboCliente"));
})

$.get("Seguro/GetAll", (data) => {
    fillComboSeguro(data, document.getElementById("cboSeguro"));
})

function fillComboCliente(data, control) {
    var content = "";
    for (var i = 0; i < data.length; i++) {
        content += "<option value='" + data[i].Id + "'>";
        content += data[i].DocumentoIdentidad + " " + data[i].ApellidoPaterno + " " + data[i].Nombres;
        content += "</option>";
    }
    control.innerHTML = content;
}

function fillComboSeguro(data, control) {
    var content = "";
    for (var i = 0; i < data.length; i++) {
        content += "<option value='" + data[i].Id + "'>";
        content += data[i].Descripcion;
        content += "</option>";
    }
    control.innerHTML = content;
}

function listAfiliacion() {
    $.get("Afiliacion/GetAll", function (data) {
        createListTableAfiliacion(["Id", "Cliente", "Seguro", "Fecha Afiliacion"], data);
    });
}

function createListTableAfiliacion(arrayColumnas, data) {
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
        contenido += "<button class='btn btn-danger' onclick='eliminar(" + data[i][llaveId] + ")' >D</i></button>"
        contenido += "<button class='btn btn-info' data-toggle='modal' data-target='#pagosModal' onclick='genPagos(" + data[i][llaveId] + ")' >P</i></button>"
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

function genPagos(id) {    
    $.get("Afiliacion/GetPagos/?idAfiliacion=" + id, function (data) {

        if (data.length >= 12) {
            openModalPagos(data);
        } else {
            var frm = new FormData();
            frm.append("Id", id);
            $.ajax({
                type: "POST",
                url: "Afiliacion/GenerarPagos",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {
                    switch (data) {
                        case '0':
                            alert("Ocurrio un error");
                            break;
                        case '1':
                            $.get("Afiliacion/GetPagos/?idAfiliacion=" + id, function (data) {
                                openModalPagos(data);
                            })
                            break;
                        default:
                            alert("Ocurrio un error inesperado");
                            break;
                    }
                }
            });

        }
    })




}

function openModalPagos(data) {
    var arrayColumnas = ["Id", "Año", "Mes", "Fecha", "Estado", "Cliente", "Seguro", "Cuota"]
    var contenido = "";
    contenido += "<table id='tablaPagos'  class='table' >";
    contenido += "<thead>";
    contenido += "<tr>";
    for (var i = 0; i < arrayColumnas.length; i++) {
        contenido += "<td>";
        contenido += arrayColumnas[i];
        contenido += "</td>";

    }
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

        contenido += "</tr>";
    }

    contenido += "</tbody>";
    contenido += "</table>";
    document.getElementById("tablaPagos").innerHTML = contenido;
}


function eliminar(id) {

    if (confirm("Desea eliminar?") == 1) {

        $.get("Afiliacion/Delete/?id=" + id, function (data) {
            if (data == 0) {
                alert("Ocurrio un error");
            } else {
                alert("Se elimino correctamente");
                listClientes();
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
        document.getElementById("lblTitulo").innerHTML = "Agregar Afiliacion";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Afiliacion";

        $.get("Afiliacion/Get/?id=" + id, function (data) {

            document.getElementById("txtIdAfiliacion").value = data.Id;
            document.getElementById("cboCliente").value = data.Cliente;
            document.getElementById("cboSeguro").value = data.Seguro;

            document.getElementById("dtFechaAfiliacion").value = data.FechaAfiliacion;

        });

    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idAfiliacion = document.getElementById("txtIdAfiliacion").value;
        var cliente = document.getElementById("cboCliente").value;
        var seguro = document.getElementById("cboSeguro").value;
        var fechaAfiliacion = document.getElementById("dtFechaAfiliacion").value;

        frm.append("Id", idAfiliacion);
        frm.append("Cliente", cliente);
        frm.append("Seguro", seguro);
        frm.append("FechaAfiliacion", fechaAfiliacion);

        if (confirm("Desea guardar los cambios?") == 1) {

            $.ajax({
                type: "POST",
                url: "Afiliacion/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {
                    switch (data) {
                        case '0':
                            alert("Ocurrio un error");
                            break;
                        case '1':
                            alert("Se ejecuto correctamente");
                            listClientes();
                            document.getElementById("btnCancelar").click();
                            break;
                        case '2':
                            alert("La cliente supera la edad permitida para el seguro elegido");
                            break;
                        default:
                            alert("Ocurrio un error inesperado");
                            break;
                    }
                }
            })
        }
    }
}