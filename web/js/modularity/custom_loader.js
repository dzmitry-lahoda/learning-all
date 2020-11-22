var script = document.createElement("script");
script.src = "file:///W:/src/web/js/lib/lodash.js";
document.body.appendChild(script);

console.log(_ !== undefined);
console.log(_.map !== undefined);