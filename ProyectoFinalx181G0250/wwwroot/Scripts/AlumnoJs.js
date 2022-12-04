
const API = "https://jeancarlo.itesrc.net/api";

const feed = document.getElementById("divAvisos");
const plantillaAviso = document.getElementById("plantillaAviso");


const menuChannels = document.getElementById("divCanales");
const plantillaCanal = document.getElementById("plantillaCanal");

const materiasUsuario = "";

const btnActualizar = document.getElementById("btnActualizar");

let IdUsuario = 1;
let Canal = "Canal General";

btnActualizar.addEventListener("click", function (event) {
    mostrarAvisos();
    console.log("Actualizado prro");
});

function cambioCanal(name) {
    console.log(name);
    Canal = name;
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
            div.children[2].innerHTML = fecha;
        }
        else {
            const date = new Date(o.fecha);
            var dia = date.getDate();
            var mes = date.getMonth();
            var ano = date.getFullYear();
            var fecha = dia + " de " + getMonthName(mes) + " del " + ano;
            div.children[2].innerHTML = fecha;
        }
        div.children[3].innerHTML = o.nombreMateria;
        div.children[4].innerHTML = o.mensajeAviso;
    });
}


mostrarCanales();