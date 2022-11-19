const api = "https://jeancarlo.itesrc.net/api";

const feed = document.getElementById("divAvisos");

const plantillaAviso = document.getElementById("plantillaAviso");

const btnActualizar = document.getElementById("btnActualizar");

btnActualizar.addEventListener("click", function (event) {
    mostrarAvisos();
    console.log("Actualizado prro");
});

localStorage.idAlumno = 1;
console.log(localStorage.idAlumno);

function Newest(a, b) {
    return new Date(b.fecha).getTime() - new Date(a.fecha).getTime();
}

function ByUser(datos) {
    
}

async function mostrarAvisos() {
    var result = await fetch(api + "/avisos/");
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

mostrarAvisos();