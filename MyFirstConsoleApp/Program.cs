﻿
/*using Microsoft.VisualBasic;

OperatorExamples();

void OperatorExamples()
{
    // This statement declares a variable and sets it to 3
    int width = 3;
    // The ++ operator increments a variable (adds 1 to it)
    width++;
    // Declare two more int variables to hold numbers and
    // use the + and * operators to add and multiply values
    int height = 2 + 4;
    int area = width * height;
    while (area < 50)
    {
        height++;
        area = width * height;
    }
    do
    {
        width--;
        area = width * height;
    } while (area > 25);
    Console.WriteLine(area);
    // The next two statements declare string variables
    // and use + to concatenate them (join them together)
    string result = "The area";
    result = result + " is " + area;
    Console.WriteLine(result);
    // A Boolean variable is either true or false
    bool truthValue = true;
    Console.WriteLine(truthValue);

    int x = 5, y = 5;
    Console.WriteLine(x<y);
    Console.WriteLine(x>y);
    Console.WriteLine(x == y);

    int someValue = 10;

    if (someValue == 10)
    {
        Console.WriteLine(someValue);
    }

    if (someValue == 15)
    {
        Console.WriteLine(someValue);
    }
    else { 
        Console.WriteLine(false);
    }

    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine("Iteration #" + i);
    }
}*/

//Loops Exercises
//Loop 1 (executes once)
//int counter = 1;
/*int count = 5;
//nuint count = 5;
while(count > 0)
{
    count = count * 3;
    count = count * -1;
    Console.WriteLine(counter++ + "->" +count);
}*/

//Loop 2 (executes 6 times)
/*int j = 2;
for (int i = 1; i < 100; i = i * 2)
{
    j = j - 1;
    while (j < 25)
    {
        // How many times will
        // the next statement
        // be executed?
        j = j + 5;
        Console.WriteLine(counter);
        counter++;
    }
}*/

//Loop 3 (executes 3 times)
/*int p = 2;
for (int q = 2; q < 32; q = q * 2)
{
    while (p < q)
    {
        // How many times will
        // the next statement
        // be executed?
        p = p * 2;
        Console.WriteLine(counter);
        counter++;
    }
    q = p - q;
}*/

//Loop 4 (executes infinite as i will always be 0)
/*int i = 0;
int count = 2;
while (i == 0)
{
    count = count * 3;
    count = count * -1;
}*/

//Loop 5 (infinite loop)
//while (true) { int i = 1; }

/*TryAnIf();
TrySomeLoops();
TryAnIfElse();
static void TryAnIf()
{
    int someValue = 4;
    string name = "Bobbo Jr.";
    if ((someValue == 3) && (name == "Joe"))
    {
        Console.WriteLine("x is 3 and the name is Joe");
    }
    Console.WriteLine("this line runs no matter what");
}
static void TryAnIfElse()
{
    int x = 5;
    if (x == 10)
    {
        Console.WriteLine("x must be 10");
    }
    else
    {
        Console.WriteLine("x isn’t 10");
    }
}
static void TrySomeLoops()
{
    int count = 0;
    while (count < 10)
    {
        count = count + 1;
    }
    for (int i = 0; i < 5; i++)
    {
        count = count - 1;
    }
    Console.WriteLine("The answer is " + count);
}*/