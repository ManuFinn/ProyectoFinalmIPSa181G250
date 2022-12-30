const API = "https://jeancarlo.itesrc.net/api";

const apiAvisosByMateria = "https://jeancarlo.itesrc.net/api/Avisos/GetByMateria/";

const feed = document.getElementById("divAvisos");
const plantillaAviso = document.getElementById("plantillaAviso");


const menuChannels = document.getElementById("divCanales");
const plantillaCanal = document.getElementById("plantillaCanal");


const btnActualizar = document.getElementById("btnActualizar");

let IdUsuario = 1;
let Canal = "Canal General";
let btnCanal = document.getElementById("Canal General");

var actualizar = window.setInterval(function () {
    mostrarAvisos();
}, 5000);


let idAviso;

document.addEventListener("click", async function (event) {
    if (event.target.dataset.modal) {
        let modal = document.getElementById(event.target.dataset.modal)
        let editar = event.target.dataset.modal.includes("Editar");
        idAviso = event.target.dataset.id;
        if (editar) {
            let res = await fetch(API + "/Avisos/GetById/" + idAviso);
            if (res.ok) {
                let aviso = await res.json();
                let form = modal.querySelector("Form");

                form.elements[0].value = aviso["mensajeAviso"];

            }
        }
        modal.style.display = "grid";
        return;
    }
    if (event.target.dataset.cancel) {
        event.target.closest(".modal").style.display = "none";
    }
});





document.addEventListener("submit", async function (event) {
    event.preventDefault();
    let form = event.target;
    let json = Object.fromEntries(new FormData(form));
    
    var metodo;

    if (form.dataset.action == "AgregarAviso") {
        metodo = "post";
        json["idDocenteAviso"] = IdUsuario;
    }
    else if (form.dataset.action == "EditarAviso") {
        metodo = "put";
        json["id"] = idAviso;
    }
    else {
        metodo = "delete"; 
        json["id"] = idAviso;
    }
    
    console.log(json);
    let resp = await fetch(API + "/Avisos/" + form.dataset.action, {
        method: metodo,
        body: JSON.stringify(json),
        headers: {
            "content-type": "application/json"
            }
    })

    if (resp.ok) {
        idAviso = 0;
        form.reset();
        form.closest(".modal").style.display = "none";
        mostrarAvisos();
    }
    else {
        let text = await resp.text();
        alert(text);
    }
});



function cambioCanal(btn) {
    btnCanal.classList.toggle("select");
    btnCanal = document.getElementById(btn);
    btnCanal.classList.toggle("select");
    Canal = btnCanal.value;
    changeChannelName(btnCanal.value);
    ocultarMenu();
    mostrarAvisos();
}

async function mostrarCanales() {
    var result = await fetch(API + "/Materias/GetByDocente/" + IdUsuario);
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

        var select = document.querySelectorAll("[name=idMateriaAviso]")

        select.forEach((x => {
            datos.forEach(t => {
                var opt = document.createElement("option");
                opt.innerText = t.nombreMateria;
                opt.value = t.id;
                x.options.add(opt);
            })
        }))

        mostrarAvisos();
        mostrarCanalesNombres(datos);
    }
}

function mostrarCanalesNombres(datos) {
    for (var x = 0; x < datos.length; x++) {
        var clone = plantillaCanal.content.children[0].cloneNode(true);
        menuChannels.append(clone);
    }

    datos.forEach((o, i) => {
        let div = menuChannels.children[i + 1];
        div.children[0].innerHTML = o.nombreMateria;
        div.children[0].value = o.nombreMateria;
        div.children[0].id = o.nombreMateria;
    });
    menuChannels.style.display = "none";
}

async function mostrarAvisos() {

    if (Canal == "Canal General") {
        var result = await fetch(API + "/Avisos/GetByDocente/" + IdUsuario);
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

function Newest(a, b) {
    return new Date(b.fecha).getTime() - new Date(a.fecha).getTime();
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
    monthNumber + 1;
    date.setMonth(monthNumber);
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
        div.children[5].dataset.id = o.id;
        div.children[6].dataset.id = o.id;
    });
}


mostrarCanales();