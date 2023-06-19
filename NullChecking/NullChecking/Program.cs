using NullChecking;

var rnd = new Random(69);

var user = rnd.Next(1,10) < 3 ?
    new User("Nick Chapsas")
    {
        Child = new User("Rick Chapsas")
    }: null;
    
    
if(user == null){
    Console.WriteLine("User was null");
}
else{
    Console.WriteLine("User was not null");
}