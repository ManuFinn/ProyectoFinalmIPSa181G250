const h1Canal = document.getElementById("TituloCanal");
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