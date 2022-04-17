$(document).ready(function () {
    RandomLoginImage();
});

function RandomLoginImage() {
    const bg = [
        "url(img/galaxy-brain-1.png)",
        "url(img/galaxy-brain-2.png)",
        "url(img/galaxy-brain-3.png)"
    ];
    const i = Math.floor(Math.random() * 3);
    $(".bg-login-image").css("background-image", bg[i])
}