
window.onscroll = function () { myFunction() };

var navbarCustom = document.getElementById("navbarCustom");
var sticky = navbarCustom.offsetTop;

function myFunction() {
    if (window.scrollY >= sticky) {
        navbarCustom.classList.add("stickyNav")
    } else {
        navbarCustom.classList.remove("stickyNav");
    }
}
