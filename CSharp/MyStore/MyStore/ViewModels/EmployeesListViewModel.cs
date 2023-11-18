using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Services;
using MyStore.Views;

namespace MyStore.ViewModels
{
    public class EmployeesListViewModel
    {
        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;

        EmployeesListWindow EmployeesListWindow { get; }
        public ObservableCollection<Employee>? Employees { get; set; }
        public EmployeesListViewModel()
        {
            Employees = StaticDataService.GetEmloyeesList();

            EmployeesListWindow = new EmployeesListWindow(this);
            EmployeesListWindow.ShowDialog();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return 
                  (addCommand = new RelayCommand((o) =>
                  {
                      Employee vm = new() { BirthDate = DateTime.Now.AddYears(-14) };
                      if (new EmployeeViewModel(vm).OpenWindow())
                      {
                          StaticDataService.AddEmloyee(vm);
                      };
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return 
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      // если ни одного объекта не выделено, выходим
                      if (selectedItem is not Employee employee) return;

                      Employee vm = new()
                      {
                          LastName = employee.LastName,
                          FirstName = employee.FirstName,
                          MiddleName = employee.MiddleName,
                          BirthDate = employee.BirthDate,
                          Sex = employee.Sex,
                      };
                      if (employee.Departament != null)
                          vm.Departament = (Departament?)StaticDataService.GetDepartament(employee.Departament.DepartamentId);

                      if (new EmployeeViewModel(vm).OpenWindow())
                      {
                          employee.LastName = vm.LastName;
                          employee.FirstName = vm.FirstName;
                          employee.MiddleName = vm.MiddleName;
                          employee.BirthDate = vm.BirthDate;
                          employee.Sex = vm.Sex;
                          if (vm.Departament != null)
                            employee.Departament = (Departament?)StaticDataService.GetDepartament(vm.Departament.DepartamentId);

                          StaticDataService.EditEmloyee(employee);
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      if (selectedItem is not Employee employee) return;

                      StaticDataService.DeleteEmloyee(employee);
                  }));
            }
        }
    }
}
