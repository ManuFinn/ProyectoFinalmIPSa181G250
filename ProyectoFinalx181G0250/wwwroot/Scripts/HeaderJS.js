const h1Canal = document.getElementById("TituloCanal");
const backUp = document.getElementById("back");
backUp.style.display = "none";
let trigger = true;

function changeChannelName(name) {
    h1Canal.innerHTML = name;
}

document.addEventListener("click", function (event) {
    Menu(event);
});

function Menu(event) {
    if (event.target.dataset.menu) {
        if (trigger == true) {
            mostrarMenu();
        }
        else {
            ocultarMenu();
        }
    }
    if (event.target.dataset.mensajes) {
        if (trigger == true) {
            console.log("LOL");
        }
        else {
            console.log("Pipo");
        }
    }
}

function ocultarMenu() {
    menuChannels.style.display = "none";
    document.body.style.overflow = 'visible';
    trigger = true;
}
function mostrarMenu() {
    menuChannels.style.display = "grid";
    document.body.style.overflow = 'hidden';
    trigger = false;
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