using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class closures_for_loop_array
{
    public static void Do()
    {
        SimplesLoopPrint3();
        LoopWithExternalInitPrint3();
        LoopWithSizeEffectPrint3();
        LoopWithStackCopyPrint123();
        CopyOntoStackChangeAfterUsePrint3();
    }

    private static void CopyOntoStackChangeAfterUsePrint3()
    {
        var actionChange = new Action[3];
        for (var f = 0; f < 3; f++)
        {
            var l = f;
            actionChange[l] = () => Console.WriteLine(l);
            l = 3;
        }
        foreach (var item in actionChange) item();
    }

    private static void LoopWithStackCopyPrint123()
    {
        var action2 = new Action[3];
        for (var j = 0; j < 3; j++)
        {
            var k = j;
            action2[k] = () => Console.WriteLine(k);
        }
        foreach (var item in action2) item();
    }

    private static void LoopWithSizeEffectPrint3()
    {
        var action4 = new Action[3];
        int y = 0;
        for (; y < 3; y++)
        {
            action4[y] = () => { y = y; Console.WriteLine(y); };
        }
        foreach (var item in action4) item();
    }

    private static void LoopWithExternalInitPrint3()
    {
        var action3 = new Action[3];
        int z = 0;
        for (; z < 3; z++)
        {
            action3[z] = () => Console.WriteLine(z);
        }
        foreach (var item in action3) item();
    }

    private static void SimplesLoopPrint3()
    {
        var action1 = new Action[3];
        for (int i = 0; i < 3; i++)
        {
            action1[i] = () => Console.WriteLine(i);
        }
        foreach (var item in action1) item();
    }
}
