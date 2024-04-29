
const currentDate = new Date().toDateString();
const date = document.getElementById("DataEvento");
const inCorso = document.getElementById("inCorso");
if (currentDate == date || currentDate > date) {
    inCorso = true;
}