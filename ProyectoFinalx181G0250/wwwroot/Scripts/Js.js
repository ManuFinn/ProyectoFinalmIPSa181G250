

const api = "https://jeancarlo.itesrc.net/api";
const apiMaterias = "https://jeancarlo.itesrc.net/api/AluXMat/GetByAlumno/";
const apiAvisosByMateria = "https://jeancarlo.itesrc.net/api/Avisos/GetByMateria/";

const feed = document.getElementById("divAvisos");
const plantillaAviso = document.getElementById("plantillaAviso");


const menuChannels = document.getElementById("divCanales");
const plantillaCanal = document.getElementById("plantillaCanal");



const btnActualizar = document.getElementById("btnActualizar");

btnActualizar.addEventListener("click", function (event) {
    mostrarAvisos();
    console.log("Actualizado prro");
});

localStorage.Canal = "Canal General";

function pipo(name) {
    console.log(name);
    localStorage.Canal = name;
    mostrarAvisos();
}

localStorage.idAlumno = 1;
console.log(localStorage.idAlumno);


function Newest(a, b) {
    return new Date(b.fecha).getTime() - new Date(a.fecha).getTime();
}


async function prueba() {
    var result1 = await fetch(apiAvisosByMateria + "A");
    var result2 = await fetch(apiAvisosByMateria + "Z");

    var res = result1.concat(result2);
    console.log(res);
}

prueba();

async function mostrarCanales() {
    var result = await fetch(apiMaterias + localStorage.idAlumno);
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

        console.log(datos);
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
    });
}


async function mostrarAvisos() {

    if (localStorage.Canal == "Canal General") {
        var result = await fetch(api + "/avisos/");
    }
    else {
        var result = await fetch(apiAvisosByMateria + localStorage.Canal);
    }
    
    if (result.ok) {
        var datos = await result.json();

        datos.sort(Newest);
        console.log(datos);
        mostrarAvisosDatos(datos);

    }
}

function mostrarAvisosDatos(datos) {
    let cant = datos.length;
    if (cant > feed.children.length) {
        let n = cant - feed.children.length;
        for (var x = 0; x <= n; x++) {
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

        div.children[0].innerHTML = o.nombreDocente;
        if (o.fechaUltAct) {
            div.children[1].innerHTML = o.fechaUltAct + "(Editado)";
        }
        else {
            div.children[1].innerHTML = o.fecha;
        }
        div.children[2].innerHTML = o.mensajeAviso;

    });
}

mostrarCanales();
/*mostrarAvisos();*/