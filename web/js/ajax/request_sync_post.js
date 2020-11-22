var r = new XMLHttpRequest();
r.open("POST","http://example.org/",false);
r.setRequestHeader("Content-Type","applicaiton/json");
r.send("{\"a\" : \"b\"}");
console.log(r.responseText.length > 0);
