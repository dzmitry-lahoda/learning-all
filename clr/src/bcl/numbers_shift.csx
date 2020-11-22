using System;

// shift unsigned 32 bit integer to 32 does not works (ignored)
//Shift by 32 does nothing of uint
//I.e. 31 (0b1_1111) shifts.But when 32(0b10_0000) nothing happens.It is 
unsafe
{
    uint ones = 0b1111_1111_1111_1111_1111_1111_1111_1111;
    int shift31 = 31;// 0b1_1111
    uint onesRight31 = ones >> shift31;
    uint onesLeft31 = ones << shift31;

    int shift32 = 32;// 0b10_0000
    uint onesRight32 = ones >> shift32;
    uint onesLeft32 = ones << shift32;


    Console.WriteLine(shift31);
    Console.WriteLine(Convert.ToString(*(int*)&ones, 2));
    Console.WriteLine(Convert.ToString(*(int*)&onesRight31, 2));
    Console.WriteLine(Convert.ToString(*(int*)&onesLeft31, 2));

    Console.WriteLine(shift32);
    Console.WriteLine(Convert.ToString(*(int*)&ones, 2));
    Console.WriteLine(Convert.ToString(*(int*)&onesRight32, 2));
    Console.WriteLine(Convert.ToString(*(int*)&onesLeft32, 2));

    Console.WriteLine();

    var desiredBits = 3;
    Console.WriteLine(desiredBits);
    var value = 0b10000001;
    Console.WriteLine(value);
    var bitsCount = sizeof(byte) * 8;
    Console.WriteLine(bitsCount);
    var signBitMask = 1 << (bitsCount - 1);
    Console.WriteLine(signBitMask);
    var signBit = value & signBitMask;
    Console.WriteLine(signBit);
    var shiftedSignBit = signBit >> (bitsCount - desiredBits);
    Console.WriteLine(shiftedSignBit);
    var result = (value & ~signBitMask) | shiftedSignBit;
    Console.WriteLine(result);
    Console.WriteLine(Convert.ToString(result, 2).PadLeft(bitsCount, '0'));
}

