

var sfondo = document.getElementById("SfondoEvento");


function Backgroud() {
    if (window.scrollY >= sticky) {
        navbarCustom.classList.add("stickyNav")
        navbarCustom.classList.add("border-3")
    } else {
        navbarCustom.classList.remove("stickyNav")
        navbarCustom.classList.remove("border-3");
    }
}
var position = document.getElementById("navbarCustom");



