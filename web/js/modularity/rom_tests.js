

console.log(rom);
rom.assert(true);

try
{
rom.assert(function(){ return 1 === 2;});
}
catch (err)
{ 
    console.log(err.message.indexOf("1 === 2") > -1);
}

try {
rom.assert(false);
}
catch (err)
{
    console.log(err.message.indexOf("Failed assertion") > -1);
}