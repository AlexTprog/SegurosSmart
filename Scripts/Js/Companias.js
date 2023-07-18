listCompania();

$("#dtFechaRen").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

function listCompania() {
    $.get("Compania/GetAll", function (data) {
        createListTable(["Id", "Descripcion", "Ruc", "Razon Social", "Contacto",
            "Celular", "Contrato", "Fecha Renovacion"], data);
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

        $.get("Compania/Delete/?id=" + id, function (data) {
            if (data == 0) {
                alert("Ocurrio un error");
            } else {
                alert("Se elimino correctamente");
                listCompania();
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
        document.getElementById("lblTitulo").innerHTML = "Agregar Compañia";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Compañia";

        $.get("Compania/Get/?id=" + id, function (data) {

            document.getElementById("txtIdComp").value = data[0].Id;
            document.getElementById("txtDesc").value = data[0].Descripcion;
            document.getElementById("txtRuc").value = data[0].Ruc;

            document.getElementById("txtRazonSocial").value = data[0].RazonSocial;
            document.getElementById("txtContacto").value = data[0].Contacto;
            document.getElementById("txtCelular").value = data[0].Celular;
            document.getElementById("txtContrato").value = data[0].Contrato;

            document.getElementById("dtFechaRen").value = data[0].FechaRenovacion;
            
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
                url: "Compania/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {

                    if (data == 0) {
                        alert("Ocurrio un error");
                    } else {
                        alert("Se ejecuto correctamente");
                        listCompania();
                        document.getElementById("btnCancelar").click();
                    }
                }
            })
        }
    }
}