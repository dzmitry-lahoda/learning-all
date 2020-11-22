var r = new XMLHttpRequest();
var counter = 0;
r.onreadystatechange = function (){
    if (r.readyState === 1 && r.status === 0){
      counter++;
      console.log(r.responseText === "");
        
    }
    if (r.readyState === 4 && r.status === 200){
      counter++;
      console.log(r.responseText.length > 1);
    }
}
r.open("GET","http://example.org/",false);
r.send();
console.log(counter === 2);// callback is done before because of sync request
console.log(r.responseText.length > 1);