var r = new XMLHttpRequest();
r.onreadystatechange = function (){
  if ( r.readyState === 4 && r.status === 200){
       console.log(r.responseText);
  }
  else {
    console.log("r.readyState  = " + r.readyState);
    console.log("r.status  = " + r.status);
  }
}
r.open("GET","http://example.org/",false)
r.send();