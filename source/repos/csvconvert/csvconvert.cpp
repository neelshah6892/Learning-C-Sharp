#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>
#include <ctime>
using namespace std;



int main()
{
    std::ifstream ascfile;
    std::ofstream csvFile;
    ascfile.open("C:\\Users\\Administrator\\Desktop\\rms_copy\\new.txt");
    csvFile.open("C:\\Users\\Administrator\\Desktop\\rms_copy\\new.csv");

    if (ascfile.is_open())
    {
        std::string line;
        while (std::getline(ascfile, line))
        {
      line.begin(), line.end(), ' ', ',';
            csvFile << line << std::endl;
        }

    }
    else
    {
        std::cout << "Sorry, the file could not be openend." << std::endl;
        return -1;
    }
    ascfile.close();    
    csvFile.close();
    return 0;
}