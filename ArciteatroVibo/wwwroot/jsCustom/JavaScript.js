
window.onscroll = function () { myFunction() };

var navbarCustom = document.getElementById("navbarCustom");
var sticky = navbarCustom.offsetTop;

function myFunction() {
    if (window.scrollY >= sticky) {
        navbarCustom.classList.add("stickyNav")
        navbarCustom.classList.add("border-3")
    } else {
        navbarCustom.classList.remove("stickyNav")
        navbarCustom.classList.remove("border-3");
    }
}
var position = document.getElementById("navbarCustom");