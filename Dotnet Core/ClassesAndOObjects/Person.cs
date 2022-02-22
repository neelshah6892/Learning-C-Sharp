using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesAndOObjects
{
    class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int Age { get; set; }

        private double _salary;

        public double getSalary()
        {
            return _salary;
        }

        public void setSalary(double salary)
        {
            _salary = salary;
        }

        public string getFullName()
        {
            return firstName + lastName;
        }
    }
}
