/*Write a C# Sharp program to print Hello and your name in a separate line. Go to the editor
Expected Output :
Hello: Alexandra Abramov*/

/*Console.WriteLine("Hello");
Console.WriteLine("Alexandra Abramov");

//Write a C# Sharp program to print the sum of two numbers.
Console.WriteLine(5 + 5);

//Write a C# Sharp program to print the result of dividing two numbers.
Console.WriteLine(20/4);

//Write a C# Sharp program to print the result of the specified operations. Go to the editor
//Test data:

//-1 + 4 * 6
//( 35+ 5 ) % 7
//14 + -4 * 6 / 11
//2 + 15 / 6 * 1 - 7 % 2
//Expected Output:
//23
//5
//12
//3

Console.WriteLine(-1 + 4 * 6);
Console.WriteLine((35 + 5) % 7);
Console.WriteLine(14+-4*6/11);
Console.WriteLine(2+15/6*1-7%2);

//Write a C# Sharp program to swap two numbers. 

int number1, number2, temp;
Console.Write("Input first number: ");
number1 = int.Parse(Console.ReadLine());
Console.Write("");
Console.Write("Input Second number: ");
number2 = int.Parse(Console.ReadLine());
temp = number1;
number1 = number2;
number2 = temp;
Console.WriteLine("After Swapping:");
Console.WriteLine("First number: "+ number1);
Console.WriteLine("Second number: " + number2);

//Write a C# Sharp program to print the output of multiplication of three numbers which will be entered by the user.
int num1, num2, num3;
Console.WriteLine("Enter First number: ");
num1 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter Second number: ");
num2 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter Third number: ");
num3 = int.Parse(Console.ReadLine());
Console.WriteLine($"{num1} x {num2} x {num3} = {num1 * num2 * num3}");

//Write a C# Sharp program to print on screen the output of adding, subtracting, multiplying and dividing of two numbers which will be entered by the user.
Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
Console.WriteLine($"{num1} x {num2} = {num1 * num2}");
Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
Console.WriteLine($"{num1} mod {num2} = {num1 % num2}");

//Write a C# Sharp program that takes a number as input and print its multiplication table. 
for(int i = 0; i < 11; i++)
{
    Console.WriteLine($"{num3} * {i} = {num3 * i}");
}

//Write a C# Sharp program that takes four numbers as input to calculate and print the average.
int num4;
Console.WriteLine("Enter Fourth number: ");
num4 = int.Parse(Console.ReadLine());
Console.WriteLine($"The average of {num1}, {num2}, {num3}, {num4} is: {(num1 + num2 + num3 + num4)/4}");

//Write a C# Sharp program to that takes three numbers(x,y,z) as input and print the output of (x+y).z and x.y + y.z. 
Console.WriteLine($"Result of specified numbers {num1}, {num2} and {num3}, (x+y).z is {(num1+num2)*num3} and x.y + y.z is {(num1*num2)+(num2*num3)}");

//Write a C# Sharp program that takes an age (for example 20) as input and prints something as "You look older than 20".
int age;
Console.WriteLine("Enter your age");
age = int.Parse(Console.ReadLine());
Console.WriteLine($"You look older than {age}");

//Write a C# program to that takes a number as input and display it four times in a row (separated by blank spaces), and then four times in the next row, with no separation. You should do it two times: Use Console. Write and then use {0}. 
int number;
Console.Write("Enter a number: ");
Console.WriteLine();
number = int.Parse(Console.ReadLine());
for(int i = 0; i < 4; i++)
{
    Console.Write(number);
    Console.Write(" ");
}
Console.WriteLine();

for(int i = 0;i < 4; i++)
{
    Console.Write(number);
}

//Write a C# program that takes a number as input and then displays a rectangle of 3 columns wide and 5 rows tall using that digit. 
int number;
Console.Write("Enter a number: ");
Console.WriteLine();
number = int.Parse(Console.ReadLine());

for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 3; j++)
    {
        Console.Write(number);
    }
    Console.WriteLine();
}

//Write a C# program to convert from celsius degrees to Kelvin and Fahrenheit.
double weather = 30;
double kelvin = weather + 273.15;
Console.WriteLine($"Weather in kelvin is {kelvin} K");
double fahrenheit = weather * 9/5+32;
Console.WriteLine($"Weather in fahrenhiet is {fahrenheit} F");


//Write a C# program remove specified a character from a non-empty string using index of a character.
string test = "neelshah";
for(int i = 0; i < test.Length; i++)
{
    if(test[i].ToString() != "e")
    {
        Console.Write(test[i]);
    }
    else
    {
        test.Remove(i);
    }
}*/

//https://www.w3resource.com/csharp-exercises/
//https://techstudy.org/csharp/csharp-programming-example-and-solutions/