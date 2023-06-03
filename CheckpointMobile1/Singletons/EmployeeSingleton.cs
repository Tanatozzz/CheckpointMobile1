using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckpointMobile1.Singletons
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Patronomyc { get; set; }
        public string LastName { get; set; }
        public DateTime LastVisitDate { get; set; }
        public bool isInside { get; set; }
        public int IDRole { get; set; }
        public int IDAdditionAccess { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string INN { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeSingleton
    {
        private static EmployeeSingleton _instance;
        private Employee _employee;

        private EmployeeSingleton()
        {
            // Приватный конструктор для предотвращения создания экземпляров извне
        }

        public static EmployeeSingleton Instance
        {
            get
            {
                // Ленивая инициализация экземпляра синглтона
                if (_instance == null)
                {
                    _instance = new EmployeeSingleton();
                }
                return _instance;
            }
        }

        public Employee Employee
        {
            get { return _employee; }
            set { _employee = value; }
        }
    }
}
