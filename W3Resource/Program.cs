/*1. Write a C# Sharp program to print Hello and your name in a separate line. Go to the editor
Expected Output :
Hello: Alexandra Abramov*/

/*Console.WriteLine("Hello");
Console.WriteLine("Alexandra Abramov");

//2. Write a C# Sharp program to print the sum of two numbers.
Console.WriteLine(5 + 5);

//3. Write a C# Sharp program to print the result of dividing two numbers.
Console.WriteLine(20/4);

//4. Write a C# Sharp program to print the result of the specified operations. Go to the editor
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

//5. Write a C# Sharp program to swap two numbers. 

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

//6. Write a C# Sharp program to print the output of multiplication of three numbers which will be entered by the user.
int num1, num2, num3;
Console.WriteLine("Enter First number: ");
num1 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter Second number: ");
num2 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter Third number: ");
num3 = int.Parse(Console.ReadLine());
Console.WriteLine($"{num1} x {num2} x {num3} = {num1 * num2 * num3}");

//7. Write a C# Sharp program to print on screen the output of adding, subtracting, multiplying and dividing of two numbers which will be entered by the user.
Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
Console.WriteLine($"{num1} x {num2} = {num1 * num2}");
Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
Console.WriteLine($"{num1} mod {num2} = {num1 % num2}");

//8. Write a C# Sharp program that takes a number as input and print its multiplication table. 
for(int i = 0; i < 11; i++)
{
    Console.WriteLine($"{num3} * {i} = {num3 * i}");
}

//9. Write a C# Sharp program that takes four numbers as input to calculate and print the average.
int num4;
Console.WriteLine("Enter Fourth number: ");
num4 = int.Parse(Console.ReadLine());
Console.WriteLine($"The average of {num1}, {num2}, {num3}, {num4} is: {(num1 + num2 + num3 + num4)/4}");

//10. Write a C# Sharp program to that takes three numbers(x,y,z) as input and print the output of (x+y).z and x.y + y.z. 
Console.WriteLine($"Result of specified numbers {num1}, {num2} and {num3}, (x+y).z is {(num1+num2)*num3} and x.y + y.z is {(num1*num2)+(num2*num3)}");

//11. Write a C# Sharp program that takes an age (for example 20) as input and prints something as "You look older than 20".
int age;
Console.WriteLine("Enter your age");
age = int.Parse(Console.ReadLine());
Console.WriteLine($"You look older than {age}");

//12. Write a C# program to that takes a number as input and display it four times in a row (separated by blank spaces), and then four times in the next row, with no separation. You should do it two times: Use Console. Write and then use {0}. 
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

//13. Write a C# program that takes a number as input and then displays a rectangle of 3 columns wide and 5 rows tall using that digit. 
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

//14. Write a C# program to convert from celsius degrees to Kelvin and Fahrenheit.
double weather = 30;
double kelvin = weather + 273.15;
Console.WriteLine($"Weather in kelvin is {kelvin} K");
double fahrenheit = weather * 9/5+32;
Console.WriteLine($"Weather in fahrenhiet is {fahrenheit} F");


//15. Write a C# program remove specified a character from a non-empty string using index of a character.
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

//16. Write a C# program to create a new string from a given string where the first and last characters will change their positions.
/*string test = "HelloWorld";
string ss = test.Substring(9, 1) + test.Substring(1,8) + test.Substring(0, 1);
Console.WriteLine(ss);


//17. Write a C# program to create a new string from a given string (length 1 or more ) with the first character added at the front and back.
string demo = "HelloWorld";
if(demo.Length >= 1)
{
    var s = demo.Substring(0, 1);
    Console.WriteLine(s + demo + s);
}

//18. Write a C# program to check two given integers and return true if one is negative and one is positive. 
int num1, num2;
Console.WriteLine("Enter First number: ");
num1 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter second number: ");
num2 = int.Parse(Console.ReadLine());

Console.WriteLine(((num1 < 0 && num2 > 0) || (num2 < 0 && num1 > 0)));

if((num1 < 0 && num2 > 0)||(num2 < 0 && num1 > 0)){
    Console.WriteLine("True");
}
else
{
    Console.WriteLine("False");
}

//19. Write a C# program to compute the sum of two given integers, if two values are equal then return the triple of their sum.
if(num1 == num2)
{
    Console.WriteLine((num1 + num2) * 3);
}
else
{
    Console.WriteLine(num2 + num1);
}

//public static int SumTriple(int a, int b)
//{
//    return a == b ? (a + b) * 3 : a + b;
//}

//20. Write a C# program to get the absolute value of the difference between two given numbers. Return double the absolute value of the difference if the first number is greater than second number.
if (num1 > num2)
{
    Console.WriteLine((num1 - num2) * 2);
}
else
{
    Console.WriteLine(num2 - num1);
}

//21. Write a C# program to check the sum of the two given integers and return true if one of the integer is 20 or if their sum is 20.
int num1 = 80;
int num2 = 25;
if((num1 == 20) || (num2 == 20) || (num1 + num2 == 20))
{
    Console.WriteLine("True");
}
else
{
    Console.WriteLine("False");
}

//22. Write a C# program to check if an given integer is within 20 of 100 or 200.
if((Math.Abs(num1 - 100) <= 20) || (Math.Abs(num1 - 200) <= 20))
{
    Console.WriteLine("Within 20 of 100 or 200");
}
else
{
    Console.WriteLine("Not within");
}

//23. Write a C# program to convert a given string into lowercase.
string test = "HELLOWORLD";
Console.WriteLine(test.ToLower());

//24. Write a C# program to find the longest word in a string.
string line = "Hello Worlds";
string[] words = line.Split(new[] { " " }, StringSplitOptions.None);
string word = "";
int ctr = 0;
foreach (String s in words)
{
    if (s.Length > ctr)
    {
        word = s;
        ctr = s.Length;
    }
}

Console.WriteLine(word);

//25. Write a C# program to print the odd numbers from 1 to 99. Prints one number per line.
for (int i = 0; i < 100; i++)
{
    if(i %2 != 0)
    {
        Console.WriteLine(i);
    }
}*/

//26. Write a C# program to compute the sum of the first 500 prime numbers.
long sum = 0;
int ctr = 1;
int n = 2;
while(ctr < 501)
{
    if(ctr%2 == 0)
    {
        Console.WriteLine(ctr);
        ctr++;
    }
    else
    {
        Console.Write($"Not Prime: {ctr}");
        ctr++;
    }
}


//27. Write a C# program and compute the sum of the digits of an integer. 
int num1 = 123;

//28. Write a C# program to reverse the words of a sentence. 


//29. Write a C# program to find the size of a specified file in bytes.


//30. Write a C# program to convert a hexadecimal number to decimal number.




//https://www.w3resource.com/csharp-exercises/
//https://techstudy.org/csharp/csharp-programming-example-and-solutions/