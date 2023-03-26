'use strict';

const toggle = document.querySelector(".container__toggle");
const icon = document.querySelector(".icon__toggle");
const menuText = document.querySelector(".container__text");

function toggleMenu() {
    menuText.classList.toggle("container__hidden");
    icon.classList.toggle("icon__toggle--rotate");
}

toggle.addEventListener("click", toggleMenu);