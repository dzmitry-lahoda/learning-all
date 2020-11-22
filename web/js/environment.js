
function log(x){
    var log = document.getElementById("log");
    if (log) {
        log.textContent +=  x + "\n";
    } else if (console){
        console.log(x);   
    }
    else window.alert(x);
}

function onload(){
    log(document.baseURI);
    log(document.URL);
    log(document.documentURI);
    log(document.location);
}
