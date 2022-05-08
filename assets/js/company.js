const icon = document.getElementById("icon");
const image= document.getElementById("hidden-image");
icon.addEventListener("mouseover", function() {
    icon.style.visibility="hidden";
    iconn.style.visibility="hidden";
    image.style.visibility = "inherit";
     
});
icon.addEventListener("mouseleave", function() {
    icon.style.visibility=" inherit";
    iconn.style.visibility=" inherit";
    image.style.visibility = "hidden";
});

const iconn = document.getElementById("iconn");
const imagee= document.getElementById("hidden-imagee");
iconn.addEventListener("mouseover", function() {
    iconn.style.visibility="hidden";
    icon.style.visibility="hidden";
    imagee.style.visibility = "inherit";
     
});
iconn.addEventListener("mouseleave", function() {
    iconn.style.visibility=" inherit";
    icon.style.visibility=" inherit";
    imagee.style.visibility = "hidden";
});
 