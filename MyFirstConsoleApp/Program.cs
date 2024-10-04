
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
//Loop 1
int counter = 0;
int count = 5;
//nuint count = 5;
while(count > 0)
{
    count = count * 3;
    count = count - 1;
    Console.WriteLine(counter++ + "->" +count);
}