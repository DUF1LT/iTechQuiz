// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("dropdown-button").addEventListener("click",() => {
    document.getElementById("dropdown").classList.toggle("show");
});

window.onclick = function (event) {
    if (!event.target.closest(".navigation-dropdown-button")) {
        var dropdowns = document.getElementsByClassName("navigation-dropdown-menu");
        for (let i = 0; i < dropdowns.length; i++) {
            dropdowns[i].classList.toggle("show");
        }
    }
}