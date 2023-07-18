listClientes();

$("#dtFechaNacimiento").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

function listClientes() {
    $.get("Cliente/GetAll", function (data) {
        createListTable(["Id", "Nombres", "Apellido Paterno", "Apellido Materno", "Fecha Nacimiento",
            "Genero", "Telefono", "Direccion", "Email", "Tipo Documento", "DNI",], data);
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
        contenido += "<button class='btn btn-danger' onclick='eliminar(" + data[i][llaveId] + ")'>D</button>"
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

        $.get("Cliente/Delete/?id=" + id, function (data) {
            if (data == 0) {
                alert("Ocurrio un error");
            } else {
                alert("Se elimino correctamente");
                listClientes();
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
        document.getElementById("lblTitulo").innerHTML = "Agregar Cliente";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Cliente";

        $.get("Cliente/Get/?id=" + id, function (data) {

            document.getElementById("txtIdCliente").value = data[0].Id;
            document.getElementById("txtnombre").value = data[0].Nombres;
            document.getElementById("txtapPaterno").value = data[0].ApellidoPaterno;

            document.getElementById("txtapMaterno").value = data[0].ApellidoMaterno;

            document.getElementById("dtFechaNacimiento").value = data[0].FechaNacimiento;

            if (data[0].Genero == 0)
                document.getElementById("rbMasculino").checked = true;
            else
                document.getElementById("rbFemenino").checked = true;

            document.getElementById("txttelefono").value = data[0].Telefono;
            document.getElementById("txtDireccion").value = data[0].Direccion;
            document.getElementById("txtEmail").value = data[0].Email;

            if (data[0].TipoDocumento == 0)
                document.getElementById("rbDni").checked = true;
            else
                document.getElementById("rbPasaporte").checked = true;

            document.getElementById("txtDocumentoIdentidad").value = data[0].DocumentoIdentidad;
        });

    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idCliente = document.getElementById("txtIdCliente").value;
        var nombre = document.getElementById("txtnombre").value;
        var apPaterno = document.getElementById("txtapPaterno").value;
        var apMaterno = document.getElementById("txtapMaterno").value;

        var fechaNac = document.getElementById("dtFechaNacimiento").value;

        var idGenero;
        if (document.getElementById("rbMasculino").checked == true) {
            idGenero = 0;
        } else {
            idGenero = 1;
        }
        var idTipoDocumento;
        if (document.getElementById("rbDni").checked == true) {
            idTipoDocumento = 0;
        } else {
            idTipoDocumento = 1;
        }

        var telefono = document.getElementById("txttelefono").value;
        var direccion = document.getElementById("txtDireccion").value;
        var Email = document.getElementById("txtEmail").value;
        var documentoIdentidad = document.getElementById("txtDocumentoIdentidad").value;

        frm.append("Id", idCliente);
        frm.append("Nombres", nombre);
        frm.append("ApellidoPaterno", apPaterno);
        frm.append("ApellidoMaterno", apMaterno);

        frm.append("FechaNacimiento", fechaNac);
        frm.append("Genero", idGenero);
        frm.append("Telefono", telefono);
        frm.append("Direccion", direccion);
        frm.append("Email", Email);
        frm.append("TipoDocumento", idTipoDocumento);
        frm.append("DocumentoIdentidad", documentoIdentidad);

        frm.append("Estado", 0);

        if (confirm("Desea guardar los cambios?") == 1) {

            $.ajax({
                type: "POST",
                url: "Cliente/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {

                    if (data == 0) {
                        alert("Ocurrio un error");
                    } else {
                        alert("Se ejecuto correctamente");
                        listClientes();
                        document.getElementById("btnCancelar").click();
                    }
                }
            })
        }
    }
}