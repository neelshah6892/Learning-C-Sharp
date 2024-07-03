namespace FileOperationsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Change this path to the directory you want to read
            string path = "G:\\";
            DirectoryInfo dir = new DirectoryInfo(path);
            Console.WriteLine("File Name                       Size        Creation Date and Time");
            Console.WriteLine("=================================================================");
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                String name = flInfo.Name;
                long size = flInfo.Length;
                DateTime creationTime = flInfo.CreationTime;
                Console.WriteLine("{0, -30:g} {1,-12:N0} {2} ", name, size, creationTime);
            }
            foreach (FileInfo file in dir.GetFiles("*.exe"))
            {
                file.Delete();
            }
            Console.WriteLine("=================================================================");
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                String name = flInfo.Name;
                long size = flInfo.Length;
                DateTime creationTime = flInfo.CreationTime;
                Console.WriteLine("{0, -30:g} {1,-12:N0} {2} ", name, size, creationTime);
            }
            Console.ReadLine();
        }
    }
}