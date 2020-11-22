



console.log("window attached all globals");

var iAmGlobal = "I am global";
console.log("window.console === console ? " +  consolesAreEqual); //true

var consolesAreEqual = window.console === console;
console.log("window.iAmGlobal ? " +  window.iAmGlobal); // "I am global"

iAmglobalUndeclared = "I am global undeclared";
console.log(window.iAmglobalUndeclared);



