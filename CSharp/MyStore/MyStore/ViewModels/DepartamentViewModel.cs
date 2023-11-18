using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MyStore.Views;
using MyStore.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyStore.ViewModels
{
    public class DepartamentViewModel : NotifyPropertyChanged
    {
        RelayCommand? okCommand;

        // Редактируемый объект
        public Departament Departament { get; set; }
        DepartamentWindow? DepartamentWindow { get; set; }

        // Справочник
        public ObservableCollection<Employee>? Employees { get; set; }

        public DepartamentViewModel(IDepartament departament)
        {
            Departament = (Departament)departament;

            // Загружаем справочник сотрудников
            Employees = StaticDataService.GetEmloyeesList();
        }

        public bool OpenWindow()
        {
            DepartamentWindow = new DepartamentWindow(this);
            bool dialogResult = DepartamentWindow.ShowDialog() ?? false;

            return dialogResult;
        }

        // Считываем выбранное значение из справочника
        public Employee ManagerItem
        {
            get { return Departament.Manager!; }
            set
            {
                if (Departament.Manager == value) return;
                Departament.Manager = value;
                OnPropertyChanged("ManagerItem");
            }
        }

        public RelayCommand OkCommand
        {
            get
            {
                return 
                  (okCommand = new RelayCommand((o) =>
                  {
                      if (DepartamentWindow != null)
                      {
                          if ((Departament?.Name == null) || (Departament?.Name.Trim().Length == 0))
                          {
                              MessageBox.Show("Не введено название подразделения.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                              return;
                          }

                          DepartamentWindow.DialogResult = true;
                      }
                  }));
            }
        }
    }
}
