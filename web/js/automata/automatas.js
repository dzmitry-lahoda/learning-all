/*

Automanons, regular expressions and turing machines. 

Eager, but should be made lazy.

TODO: if we get to final state, and from this input leads to undefined state should machine be final?

*/


rom.import(["lib/lodash.js"]);

/**
 Generates all  permutatuins of defined length for symbols 0 and 1


**/
function Generator(up) {
   
   var generate_rec = function (collection,last,step) {
     
     var new0 = _.map(last,_.clone).concat(0);
     var new1 = _.map(last,_.clone).concat(1);
     collection[collection.length] = new0;
     collection[collection.length] = new1;
     if (step >= up){
         return;
     } else {
        step++;
         generate_rec(collection, new0, step);
        generate_rec(collection, new1, step);
     }
   }
    
   this.generate = function () {
     var result = [];
     generate_rec(result, [], 1);
     return result;
   }  
}


var toAutomata = function(data,result){
  result.transitions = _.rest(data.table);
  result.start = this.transitions[0][0];
  result.state = start;
  result.finals = data.finals;
  result.inputs = _.rest(data.table[0]);
} 

/**
 Calculates autotmata defined as table of transitions, start position and inputs
**/
function UnnamedTableAutomata(data){
  toAutomata(data,this);
  this.final = false;
  this.transition = function (input)  {
    //if (this.final === true && ) return; could break here if need just check if ever got to final state
      
    for (var v of input) {
     var self = this;
     var rule = _.findIndex(this.transitions,function (e,i) {
        return e[0] === self.state;     
     });
     var next = this.transitions[rule][v+1];
     this.state = next;
     if (this.state === undefined)
         return;//input ask for impossible transition
     if (_.indexOf(this.finals, this.state) >= 0) {
         this.final = true;
         //break; could break here if need just check if ever got to final state
      }
      else this.final = false;
    };
  }
  this.reset = function() { this.state = this.start;this.final = false; };
}

var nonDeterministicData = {
        "states" : ["A", "B", "C"],
        "finals" : ["C"],
        "table" : [
            // start state is top left corner
            ["A", 0, 1],
            ["A", "A", "B" ],
            ["B", ["A","C"] ,undefined ],
            ["C", undefined, ["A","B"]],
        ]
    };

var flatten = {
      "states" : ["A", "B", "C","AC","AB"],
        "finals" : ["C"],
        "table" : [
            // start state is top left corner
            ["A", 0, 1],
            ["A", "A", "B" ],
            ["B", "AC" ,undefined ],
            ["AC", "A", "AB"],
            ["AB", "AC", "B"],
            ["C", undefined, ["A","B"]],
        ]  
};



var makeFlat = function (input) {
    var automata = {};
    var flatten = {};
    toAutomata(input,automata);
    for (var r of automata.transitions){
       var into = _.rest(r);
       for 
       
    }
    return flatten;
};