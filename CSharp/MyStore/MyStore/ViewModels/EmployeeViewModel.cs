using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MyStore.Models;
using MyStore.Views;
using MyStore.Services;
using System.Windows.Controls;
using System.Windows;

namespace MyStore.ViewModels
{
    public class EmployeeViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<Departament>? Departaments { get; set; }
        public ObservableCollection<string?>? Sexs { get; set; }
        public Employee Employee { get; set; }

        EmployeeWindow? EmployeeWindow { get; set; }
        RelayCommand? okCommand;
        public EmployeeViewModel(IEmployee employee)
        {
            Employee = (Employee)employee;

            // Cправочник подразделений
            Departaments = StaticDataService.GetDepartamentsList();

            // Справочник половой принадлежности
            Sexs = new(Enum.GetNames(typeof(Sex)));
        }

        public bool OpenWindow()
        {
            EmployeeWindow = new EmployeeWindow(this);
            bool dialogResult = EmployeeWindow.ShowDialog() ?? false;

            return dialogResult;
        }

        // Считываем выбранное значение из справочника
        public Departament DepartamentItem
        {
            get { return Employee.Departament!; }
            set
            {
                if (Employee.Departament == value) return;
                Employee.Departament = value;
            }
        }

        // Работаем со справочником половой принадлежности
        public string SexItem
        {
            get { return Employee.Sex.ToString()!; }
            set
            {
                if (Employee.Sex.ToString() == value) return;
                _ = Enum.TryParse(value, out Sex sex);
                Employee.Sex = sex;
            }
        }

        public RelayCommand OkCommand
        {
            get
            {
                return
                (okCommand = new RelayCommand((o) =>
                {
                    if (EmployeeWindow != null)
                    {
                        if ((Employee?.LastName == null) || (Employee?.LastName.Trim().Length == 0))
                        {
                            MessageBox.Show("Не введена фамилия сотрудника.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if ((Employee?.FirstName == null) || (Employee?.FirstName.Trim().Length == 0))
                        {
                            MessageBox.Show("Не введено имя сотрудника.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (DateTime.Now.AddYears(-14) < Employee?.BirthDate)
                        {
                            MessageBox.Show("Сотрудник не может быть моложе 14 лет.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        EmployeeWindow.DialogResult = true;
                    }
                }));
            }
        }
    }
}
