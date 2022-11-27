const API = "https://jeancarlo.itesrc.net/api";

const apiAvisosByMateria = "https://jeancarlo.itesrc.net/api/Avisos/GetByMateria/";

const feed = document.getElementById("divAvisos");
const plantillaAviso = document.getElementById("plantillaAviso");


const menuChannels = document.getElementById("divCanales");
const plantillaCanal = document.getElementById("plantillaCanal");


const btnActualizar = document.getElementById("btnActualizar");

let IdUsuario = 1;
let Canal = "Canal General";


btnActualizar.addEventListener("click", function (event) {
    mostrarAvisos();
    console.log("Actualizado prro");
});

document.addEventListener("click", async function (event) {
    if (event.target.dataset.modal) {
        let modal = document.getElementById(event.target.dataset.modal)
        var editar = event.target.dataset.modal.includes("Editar");
        var idAviso = event.target.dataset.id;
        if (editar) {
            let res = await fetch(API + "/Avisos/GetById/" + idAviso);
            if (res.ok) {
                let aviso = await res.json();
                console.log(aviso);
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
    json["idDocenteAviso"] = IdUsuario;
    console.log(json);
    let resp = await fetch(API + "/Avisos/" + form.dataset.action, {
        method: form.method,
        body: JSON.stringify(json),
        headers: {
            "content-type": "application/json"
            }
    })

    if (resp.ok) {
        form.reset();
        form.closest(".modal").style.display = "none";
        mostrarAvisos();
    }
    else {
        let text = await resp.text();
        alert(text);
    }
});



function cambioCanal(name) {
    console.log(name);
    Canal = name;
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
    });
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

        div.children[0].innerHTML = o.nombreDocente;
        if (o.fechaUltAct) {
            div.children[1].innerHTML = o.fechaUltAct + "(Editado)";
        }
        else {
            div.children[1].innerHTML = o.fecha;
        }
        div.children[2].innerHTML = o.mensajeAviso;
        div.children[3].dataset.id = o.id;
        div.children[4].dataset.id = o.id;
    });
}


mostrarCanales();