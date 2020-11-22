

/*
Functions to assist making JavaScript hosting environment more like of Octave or R, but NOT utilitues for visualiation nor calculation.

*/

var rom = rom || {};

(function(pub)
{
    
//http://stackoverflow.com/questions/2255689/how-to-get-the-file-path-of-the-currently-executing-javascript-code/27369985#27369985
     var getCurrentScript = function () {
      if (document.currentScript) {
        return document.currentScript.src;
      } else {
        var scripts = document.getElementsByTagName('script');
        if (scripts.length === 0)
            return "";
        return scripts[scripts.length-1].src;

      }
    };
        var getCurrentScriptPath = function () {
      var script = getCurrentScript();
      var path = script.substring(0, script.lastIndexOf('/'));
      return path;
    };
    
    if (!(pub.root))
        pub.root = getCurrentScriptPath();
    
    
    var isAbsolute =  function (path) {
       return path.charAt(0) === "/" // posix
           || path.charAt(1) === ":" // nt
           || (path.indexOf("://") > 0); // url
    };
    pub.import = function (deps){
         for (var d of deps){
           var script = document.createElement("script");
           if (isAbsolute(d)) 
           {script.src = d;}
           else {script.src = pub.root + d;}
          if (!(document.body))
               document.createElement("body");
            document.body.appendChild(script);
          
         }
      };
    
    //http://stackoverflow.com/questions/3179861/javascript-get-function-body/25229488#25229488
    function getFunctionBody(fn) {
        //TODO: clearn `return` and last `;`
        //TODO: fail to many lines expressions
        //TODO: chech works with `() => `
        function removeCommentsFromSource(str) {
            return str.replace(/(?:\/\*(?:[\s\S]*?)\*\/)|(?:([\s;])+\/\/(?:.*)$)/gm, '$1');
        }
           var s = removeCommentsFromSource( fn.toString() );
            return s.substring(s.indexOf('{')+1, s.lastIndexOf('}'));
            };
    
    
    pub.log = function(obj){
     if (typeof(obj) === "function"){
         var left = getFunctionBody(obj);
         var right = obj();
         console.log(left + " = " + right);
      } else {
        console.log(obj)
      }
    }
    
    pub.assert = function (obj) {
       if (typeof(obj) === "boolean"){
         if (obj === false) throw Error("Failed assertion");
       } else if (typeof(obj) === "function"){
           var result = obj();
           if (typeof(result) === "boolean"){
              if (result === false) throw Error("Failed assertion for " + getFunctionBody(obj));
           }
           else throw Error("Unsupported expression" + getFunctionBody(obj)); 
       }
        else throw Error("Unsupported expression");
    };    
}(rom));


