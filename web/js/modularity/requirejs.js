
requirejs(["file:///W:/src/web/js/lib/lodash.js"],function () { console.log("lodash loaded");}, function() { console.log("failed to load lodash");});


console.log(_ !== undefined);
console.log(_.map !== undefined);