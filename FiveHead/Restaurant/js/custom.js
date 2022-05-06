function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function getTime() {
    let serverTime = new Date();
    serverTime = new Date(serverTime.getTime() + (8*60*60*1000));
    return serverTime.toUTCString() + "+8";
}
