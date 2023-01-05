
const API = "https://jeancarlo.itesrc.net/api";

const feed = document.getElementById("divAvisos");
const plantillaAviso = document.getElementById("plantillaAviso");


const menuChannels = document.getElementById("divCanales");
const plantillaCanal = document.getElementById("plantillaCanal");

const mensajesPrivados = document.getElementById("divUsuarios")
const historial = document.getElementById("historial");
const listaUs = document.getElementById("listaUsuarios");
const plantillaUsuario = document.getElementById("plantillaUsAlumno");
const plantillaMensaje = document.getElementById("plantillaMensaje");

const materiasUsuario = "";

let IdUsuario = 1;
let Canal = "Canal General";
let btnCanal = document.getElementById("Canal General");

const currentTime = Date.now();
const oneDayInMilliseconds = 86400000 / 2;

var actualizar = window.setInterval(function () {
    mostrarAvisos();
}, 5000);


function cambioCanal(btn) {
    btnCanal.classList.toggle("select");
    btnCanal = document.getElementById(btn);
    btnCanal.classList.toggle("select");
    Canal = btnCanal.value;
    changeChannelName(btnCanal.value);
    ocultarMenu();
    mostrarAvisos();
}

function Newest(a, b) {
    return new Date(b.fecha).getTime() - new Date(a.fecha).getTime();
}


async function mostrarCanales() {
    var result = await fetch(API + "/AluXMat/GetByAlumno/" + IdUsuario);
    if (result.ok) {
        var datos = await result.json();

        datos.sort(function (a, b) {
            if (b.nombreMateria > a.nombreMateria) {
                return -1;
            }
            if (a.nombreMateria > b.nombreMateria) {
                return 1;
            }
            return 0;
        });


        mostrarAvisos(datos);
        mostrarCanalesNombres(datos);
    }
}

function mostrarCanalesNombres(datos) {
    for (var x = 0; x < datos.length; x++) {
        var clone = plantillaCanal.content.children[0].cloneNode(true);
        menuChannels.append(clone);
    }
    
    datos.forEach((o, i) => {
        let div = menuChannels.children[i+1];
        div.children[0].innerHTML = o.nombreMateria;
        div.children[0].value = o.nombreMateria;
        div.children[0].id = o.nombreMateria;
    });
}

mostrarUsuarios();

async function mostrarUsuarios() {
    var result = await fetch(API + "/Mensajes/GetByAlumno/" + IdUsuario);
    if (result.ok) {
        var datos = await result.json();
        const Us = datos.reverse().filter((obj, index, self) => {
            const duplicate = self.find((t) => t.Fecha === obj.Fecha && t.idDocente === obj.idDocente);
            return index === self.indexOf(duplicate);
        });
        Us.sort(Newest);
        mostrarMensajes(Us);
    }

}

function mostrarMensajes(Us) {
    let cant = Us.length;
    if (cant > listaUs.children.length) {
        let n = cant - listaUs.children.length;
        for (var x = 0; x < n; x++) {
            var clone = plantillaUsuario.content.children[0].cloneNode(true);
            listaUs.append(clone);
        }
    }
    else if (cant < listaUs.children.length) {
        let n = listaUs.children.length - cant;
        for (var x = 0; x < n; x++) {
            listaUs.lastChild.remove();
        }
    }

    Us.forEach((o, i) => {
        let div = listaUs.children[i];
        div.dataset.id = o.idDocente;
        div.children[0].innerHTML = getFirstLetters(o.nombreDocente);
        div.children[0].dataset.id = o.idDocente;
        div.children[1].innerHTML = o.nombreDocente;
        div.children[1].dataset.id = o.idDocente;
        div.children[2].innerHTML = truncateLabel(o.mensaje, 20);
        div.children[2].dataset.id = o.idDocente;
        div.children[3].innerHTML = o.fecha;
        div.children[3].dataset.id = o.idDocente;

    });
}

async function cargarMensajesPrivados(event) {
    var result = await fetch(API + "/Mensajes/DocenteByAlumno/" + event.target.dataset.id + "-" + IdUsuario);
    if (result.ok) {
        var datos = await result.json();
        mostrarHistorial(datos);
    }
}

function mostrarHistorial(datos) {
    let cant = datos.length;
    if (cant > historial.children.length) {
        let n = cant - historial.children.length;
        for (var x = 0; x < n; x++) {
            var clone = plantillaMensaje.content.children[0].cloneNode(true);
            historial.append(clone);
        }
    }
    else if (cant < historial.children.length) {
        let n = historial.children.length - cant;
        for (var x = 0; x < n; x++) {
            historial.lastChild.remove();
        }
    }

    datos.forEach((o, i) => {
        let div = historial.children[i];
        div.children[0].innerHTML = o.mensaje;

        var date = new Date(o.fecha);
        const currentTime = Date.now();
        const oneDayInMilliseconds = 86400000 / 2;

        if (currentTime - date < oneDayInMilliseconds) {
            var hora = date.getHours();
            var min = date.getMinutes();
            var hoy = hora + ":" + min;
            div.children[1].innerHTML = hoy;
        } else {

            var dia = date.getDate();
            var mes = date.getMonth();
            var ano = date.getFullYear();
            var fecha = dia + " de " + getMonthName(mes) + " del " + ano;
            div.children[1].innerHTML = fecha;
        }
    });
}


async function mostrarAvisos(materias) {

    if (Canal == "Canal General") {
        var result = await fetch(API + "/AluXMat/GetByAlumno/" + IdUsuario);
        if (result.ok) {
            var materias = await result.json();
        }
        let url = "https://jeancarlo.itesrc.net/api/Avisos/GetByMaterias/";

        materias.forEach(element => {
            url += "a";
            url += element.idMateria;
        });
        var result = await fetch(url);
    }
    else {
        var result = await fetch(API + "/Avisos/GetByMateria/" + Canal);
    }
    
    if (result.ok) {
        var datos = await result.json();

        datos.sort(Newest);
        mostrarAvisosDatos(datos);

    }
}

function getFirstLetters(str) {
    const firstLetter = str
        .split(' ')
        .map(word => word[0])
        .join('');
    const SecondLetter = str
        .split(' ')
        .map(word => word[0])
        .join('');

    return firstLetter[0] + SecondLetter[2];
}

function getMonthName(monthNumber) {
    const date = new Date();
    date.setMonth(monthNumber - 1);
    return date.toLocaleString('es-MX', { month: 'long' });
}

function truncateLabel(label, maxLength) {
    if (label.length > maxLength) {
        const words = label.split(" ");
        let truncatedLabel = "";
        let i = 0;
        while (truncatedLabel.length < maxLength) {
            truncatedLabel += words[i] + " ";
            i++;
        }
        return truncatedLabel.trim() + "...";
    }
    return label;
}


function mostrarAvisosDatos(datos) {
    let cant = datos.length;
    if (cant > feed.children.length) {
        let n = cant - feed.children.length;
        for (var x = 0; x < n; x++) {
            var clone = plantillaAviso.content.children[0].cloneNode(true);
            feed.append(clone);
        }
    }
    else if (cant < feed.children.length) {
        let n = feed.children.length - cant;
        for (var x = 0; x < n; x++) {
            feed.lastChild.remove();
        }
    }

    datos.forEach((o, i) => {
        let div = feed.children[i];
        div.children[0].innerHTML = getFirstLetters(o.nombreDocente);
        div.children[1].innerHTML = o.nombreDocente;
        if (o.fechaUltAct) {
            const date = new Date(o.fechaUltAct);
            var dia = date.getDate();
            var mes = date.getMonth();
            var ano = date.getFullYear();
            var fecha = dia + " de " + getMonthName(mes) + " del " + ano + " (Editado)";
            div.children[3].innerHTML = fecha;
        }
        else {
            var date = new Date(o.fecha);

            if (currentTime - date < oneDayInMilliseconds) {
                var hora = date.getHours();
                var min = date.getMinutes();
                var hoy = hora + ":" + min;
                div.children[3].innerHTML = hoy;
            } else {

                var dia = date.getDate();
                var mes = date.getMonth();
                var ano = date.getFullYear();
                var fecha = dia + " de " + getMonthName(mes) + " del " + ano;
                div.children[3].innerHTML = fecha;
            }
        }
        div.children[2].innerHTML = o.nombreMateria;
        div.children[4].innerHTML = o.mensajeAviso;
    });
}


mostrarCanales();