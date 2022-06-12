// const sertification = document.getElementById("sertification");
// const image= document.getElementsByClassName("hidden-image");
// sertification.addEventListener("mouseover", function() {
//     sertification.style.visibility="hidden";
//     iconn.style.visibility="hidden";
//     image.style.visibility = "inherit";
     
// });
// sertification.addEventListener("mouseleave", function() {
//     sertification.style.visibility=" inherit";
//     iconn.style.visibility=" inherit";
//     image.style.visibility = "hidden";
// });

// const iconn = document.getElementById("iconn");
// const imagee= document.getElementById("hidden-imagee");
// iconn.addEventListener("mouseover", function() {
//     iconn.style.visibility="hidden";
//     icon.style.visibility="hidden";
//     imagee.style.visibility = "inherit";
     
// });
// iconn.addEventListener("mouseleave", function() {
//     iconn.style.visibility=" inherit";
//     icon.style.visibility=" inherit";
//     imagee.style.visibility = "hidden";
// });
const singUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container=document.getElementById('container');
singUpButton.addEventListener('click',() =>{
    container.classList.add("right-panel-active");
});
signInButton.addEventListener('click',() =>{
    container.classList.remove("right-panel-active");
});