using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace MyStore.Models
{
    public interface IEmployee
    {
        int EmployeeId { get; set; }
        string? LastName { get; set; }
        string? FirstName { get; set; }
        string? MiddleName { get; set; }
        DateTime BirthDate { get; set; }
        Sex Sex { get; set; }
    }
    public class Employee : NotifyPropertyChanged, IEmployee, IDataErrorInfo
    {
        public int EmployeeId { get; set; }
        string lastName;
        public string? LastName
        {
            get { return lastName; }
            set
            {
                if (lastName == value) return;
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        string firstName;
        public string? FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName == value) return;
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        string? middleName;
        public string? MiddleName
        {
            get { return middleName; }
            set
            {
                if (middleName == value) return;
                middleName = value;
                OnPropertyChanged("MiddleName");
            }
        }
        DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate == value) return;
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        Sex sex;
        public Sex Sex
        {
            get { return sex; }
            set
            {
                if (sex == value) return;
                sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public string? FullName
        {
            // сеттер во ViewModel
            get
            {
                return string.Join(" ", LastName, FirstName, MiddleName, BirthDate.ToShortDateString());
            }
        }

        // ссылка на подразделение
        public int? DepartamentId { get; set; }
        Departament? departament;
        public Departament? Departament
        {
            get { return departament; }
            set
            {
                if (departament == value) return;
                departament = value;
                OnPropertyChanged("Departament");
            }
        }

        public string Error
        {
            get { throw new System.NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                            result = "Введите имя."; break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName))
                            result = "Введите фамилию."; break;
                    case "BirthDate":
                        if (DateTime.Now.AddYears(-14) < BirthDate)
                            result = "Сотрудник не может быть моложе 14 лет."; break;
                };
                return result;
            }
        }
    }
}
