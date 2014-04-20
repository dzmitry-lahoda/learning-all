/*Задание 1.1

Создать минимальное приложение «hello, world» в notepad.exe. 
 * Скомпилировать с помощью компилятора командной строки csc.exe

Разбить задачу на несколько файлов и научиться создавать модули. 
 * Научиться создавать как исполняемые сборки, так и библиотечные.*/

using System;
namespace EPAM.Zadanie1_1
{
    static class Hello
    {
        public static void SayHello()
        {
            Console.WriteLine("Hello World!");
        }

        static void Main()
        {
            SayHello();
        }
    }
}
