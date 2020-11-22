

var arrayOfArray = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

var getArrayStartingWith = function (start) {
    for (var a of arrayOfArray){
                    if (a[0] === start){
                        return a;
                    }
                }
       return undefined;
        }                         


getArrayStartingWith(4);