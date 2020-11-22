

var request = new XMLHttpRequest();
request.open("GET", root + "json/one_string_property.json", false);
request.send();

console.log(request.response === "{ \"a\" : \"b\" }");
