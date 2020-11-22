

// ad-hoc prototyping
function createMy2() {
    "use strict";
    var t = {};
    t.hello = "World2";
    return t;
}
function CreateMy() {
    "use strict";
    this.hello = "World1";
}

function CreateMy3() {
    "use strict";
}

function dummy() {
    "use strict";
}

function show() {
    "use strict";
    
    var t1 = new CreateMy();
    window.console.log(t1.hello);
    window.console.log(Object.prototype.hello);

    var t2 = createMy2();
    window.console.log(t2.hello);
    window.console.log(Object.prototype.hello);
    
    var t3 = new CreateMy3();
    try {
        t3.prototype.hello = "World3";
    }
    catch (err) {
            window.console.log(err);
        } //NOTE: JSLint by default requires this formatting of {} in try catch....
    
    var t4 = new CreateMy3();
    console.log("Adding properties to objects is possible post fact via prototype");
    CreateMy3.prototype.hello = "World4"; 
    window.console.log(t4.hello);
    window.console.log(Object.prototype.hello);
    
    
    dummy.prototype.hello = "Any function has prototype";
    
    
    return true;
}

show();