const h1Canal = document.getElementById("TituloCanal");
const backUp = document.getElementById("back");
backUp.style.display = "none";
let triggerCanal = false;
let triggerMens = false;

function changeChannelName(name) {
    h1Canal.innerHTML = name;
}

document.addEventListener("click", function (event) {
    Menu(event);
});

function Menu(event) {
    if (event.target.dataset.menu) {
        if (triggerCanal == false) {
            menuChannels.style.display = "grid";
            mensajesPrivados.style.display = "none";
            document.body.style.overflow = 'hidden';
            triggerCanal = true;
            triggerMens = false;
            while (historial.firstChild) {
                historial.removeChild(historial.firstChild);
            }
        }
        else {
            menuChannels.style.display = "none";
            document.body.style.overflow = 'visible';
            triggerCanal = false;
            while (historial.firstChild) {
                historial.removeChild(historial.firstChild);
            }
        }
    }
    if (event.target.dataset.mensajes) {
        if (triggerMens == false) {
            mensajesPrivados.style.display = "grid";
            menuChannels.style.display = "none";
            document.body.style.overflow = 'hidden';
            triggerMens = true;
            triggerCanal = false;
        }
        else {
            mensajesPrivados.style.display = "none";
            document.body.style.overflow = 'visible';
            triggerMens = false;
            while (historial.firstChild) {
                historial.removeChild(historial.firstChild);
            }
        }
    }
}
document.addEventListener("click", function (event) {
    cambiarChat(event);
});

function cambiarChat(event) {
    if (event.target.dataset.chat) {
        cargarMensajesPrivados(event);
    }
    else if (event.target.dataset.mensaje) {
        let modal = document.getElementById(event.target.dataset.mensaje)
        modal.style.display = "grid";
    }
}

window.addEventListener('scroll', function () {
    if (window.scrollY < 400) {
        backUp.style.display = "none";
    }
    else {
        backUp.style.display = "grid";
    }
})

backUp.addEventListener('click', function () {
    window.scrollTo(0, 0);
});

function ocultarMenu() {
    menuChannels.style.display = "none";
    document.body.style.overflow = 'visible';
    triggerCanal = false;
}

function toggleBtn() {
    const Btns = document.querySelector(".btns");
    const add = document.getElementById("add");
    const remove = document.getElementById("remove");
    const btn = document.querySelector(".btns").querySelectorAll("a");
    Btns.classList.toggle("open");
    if (Btns.classList.contains("open")) {
        remove.style.display = "block";
        add.style.display = "none";
        btn.forEach((e, i) => {
            setTimeout(() => {
                bottom = 40 * i;
                e.style.bottom = bottom + "px";
            }, 100 * i);
        });
    } else {
        add.style.display = "block";
        remove.style.display = "none";
        btn.forEach((e, i) => {
            e.style.bottom = "0px";
        });
    }
}