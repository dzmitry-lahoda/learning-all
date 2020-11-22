

rom.import(["automata/automatas.js"]);

var generator = new Generator(2);
var generated = generator.generate();
generated.length = 4;
console.log(generated);
console.log(_.isEqual(generated[0],[0]));
console.log(_.isEqual(generated[1],[1]));
console.log(_.isEqual(generated[2],[0, 1]));
console.log(_.isEqual(generated[3],[1, 0]));

var automataData = {
        "states" : ["A", "B", "C", "D"],
        "finals" : ["D"],
        "table" : [
            // start state is top left corner
            ["A", 0, 1],
            ["A", "B", "C" ],
            ["B", undefined ,"D" ],
            ["C", "D", undefined],
            ["D", "A", "B"],
        ]
    };

//tests
var automata = new UnnamedTableAutomata(automataData);
console.log(automata.state === "A");
console.log(automata.inputs);
console.log(automata.finals[0] === "D");
console.log(_.isEqual(automata.inputs, [0,1]));
console.log(automata.transitions.length === 4);


 automata.transition([0,1]);
 console.log(automata.state === "D");
 console.log("[0,1]="+automata.final);
 automata.reset();


var inputs = [[1,0,0],[0,1,0,1,0],[0,1,0,1,1],[], [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1]];
for (var i of inputs){
  console.log("==========INPUT START============");
  console.log(i);
  automata.transition(i);
  console.log("got into " + automata.state + " which is " + (this.final ? "" : "not") + (" final"));
  automata.reset();
}

/**

 k = k(n-2) + 2 k(n-3) 
 
 what is number of ward this automata accepts of lenght k?
  k(1) = 0 
  k(2) = 2
  k(3) = 0 
  k(4) = 2
  k(5) = 4
  k(6) = 2
  k(7) = 8  
  k(8) = 10  // k(n-1) + 2
  k(9) = 12  // k(n-1) + 2
  k(10)= 26  // k(n-2) + k(n-1) + 4 ; k(n-1)*2 + 2
  k(11) = 32 // k(n-2)*3 + 2 ; k(n-1) + 6
  k(12) = 50 //
  k(13) = 84
  k(14) = 114 
  

 k=0
 0
 
 k=1
 0
 
 k = 2;
 01
 10  
 2
 
 k = 3
 0
 
 k=4
 01 11
 10 11
 2
 
 k=5
 01 0 10
 01 0 01
 10  0 10
 10  0 01
 4
 
 k=6
 01 11 11
 10 11 11
 2
 
 k=7
 01 0 10 11
 01 0 01 11 
 
 01 11 0 10 
 01 11 0 01 
 
 10 0 01 11 
 10 0 10 11
 
 10 11 0 01   
 10 11 0 10  
 8
 
 k=8
 01 11 11 11
 10 11 11 11
 
 01 0 01 0 01
 01 0 01 0 10
 01 0 10 0 01
 01 0 10 0 10
 
 10 0 01 0 01
 10 0 01 0 10
 10 0 10 0 01
 10 0 10 0 10
 
 10

 k=9
 01 0 01 11 11 
 01 0 10 11 11
 01 11 0 01 11
 01 11 0 10 11
 01 11 11 0 01
 01 11 11 0 10
 
 10 0 01 11 11 
 10 0 10 11 11
 10 11 0 01 11
 10 11 0 10 11
 10 11 11 0 01
 10 11 11 0 10
 
 12

 k=10
 01 11 11 11 11
 10 11 11 11 11
 
 01 0 01 0 01 11
 01 0 01 0 10 11
 01 0 10 0 01 11
 01 0 10 0 10 11
 
 01 11 0 01 0 01
 01 11 0 10 0 10
 01 11 0 01 0 01
 01 11 0 10 0 10
 
 01 0 01 11 0 01
 01 0 01 11 0 10
 01 0 10 11 0 01
 01 0 10 11 0 10

 10 0 01 0 10 11
 10 0 01 0 01 11
 10 0 10 0 01 11
 10 0 10 0 10 11
 // like 01 start we get 3*4 for 10 start
 
 26 
 **/
 
var maxLength = 14;
var gAll14 = new Generator(maxLength).generate();
var gg =  _.filter (gAll14, function (a) { 
    
    var accepts = function (input) {
         automata.reset();
         automata.transition(input);
        return automata.final;
    }

    return (a.length === maxLength) && accepts(a);

});

console.log("gg = " + gg.length);






