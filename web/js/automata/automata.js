
var turnstile = {
        states : { Locked : "Locked", Unlocked : "Unlocked" },
        inputs : { Coin : "Coin", Push : "Push" },
        output : { CanPass : "Unlock turnstile so customer can push through", AfterPass: "When customer has pushed through, lock turnstile", None : "None" },
        //represent raws as states and columns as inputs
        transitions : [
            ["currentState", turnstile.inputs.Coin, turnstile.inputs.Push],
            [turnstile.states.Locked, turnstile.states.Unlocked, turnstile.states.Locked],
            [turnstile.states.Unlocked, turnstile.states.Unlocked, turnstile.states.Locked]
        ]
    };




function Turnstile() {
    "use strict";
    this.currentState = turnstile.states.Locked;
    
    this.toDesciption = function () {
        return "Current state is " + this.currentState;
    };
    
    this.transition = function (input) {
        var output = turnstile.output.None;
        if (input === turnstile.inputs.Coin) {
            if (this.currentState === turnstile.states.Locked) {
                output = turnstile.output.CanPass;
            }
            this.currentState = turnstile.states.Unlocked;
        } else if (input === turnstile.inputs.Push) {
            if (this.currentState === turnstile.states.Unlocked) {
                output = turnstile.output.AfterPass;
            }
            this.currentState = turnstile.states.Locked;
        }
        return output;
    };
}

var t = new Turnstile();

function show(message) {
    "use strict";
    var log = document.getElementById("log");
    if (log !== null) {
        log.textContent = log.textContent + "\n\r" + message;
    } else {
        window.alert("Log not found!");
    }
}

function onload() {
    "use strict";
    show(t.toDesciption());
}

function push() {
    "use strict";
    show(t.transition(turnstile.inputs.Push));
    return true;
}

function coin() {
    "use strict";
    show(t.transition(turnstile.inputs.Coin));
    return true;
}


