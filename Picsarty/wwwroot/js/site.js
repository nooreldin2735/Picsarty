// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Sign_in_click() {
    document.getElementById("main_container_body").style.opacity = ".8";
    document.getElementById("main_container_body").style.filter = 'brightness(0.5)';
    document.getElementById("Sign_in_contatiner").style.display = "flex";
    document.getElementById("Sign_up_contatiner").style.display = "none";
    document.getElementById("Category_select").style.display = "none";

    document.body.style.overflow = "hidden";
}
function ReturnToHome() {
    document.getElementById("main_container_body").style.opacity = "1";
    document.getElementById("Sign_in_contatiner").style.display = "none";
    document.getElementById("Sign_up_contatiner").style.display = "none";
    document.getElementById("main_container_body").style.filter = 'brightness(1)';
    document.body.style.overflow = "scroll";
}
function Sign_up_click() {
    document.getElementById("main_container_body").style.opacity = "0.8";
    document.getElementById("Sign_in_contatiner").style.display = "none";
    document.getElementById("Sign_up_contatiner").style.display = "flex";
    document.getElementById("main_container_body").style.filter = 'brightness(0.5)';
    document.body.style.overflow = "hidden";
}
