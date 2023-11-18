using System;
using System.Linq;
using MyStore.Models;
using MyStore.Services;
using MyStore.Views;

namespace MyStore.ViewModels
{
    public class MainViewModel
    {

        RelayCommand? openOrdersCommand;
        RelayCommand? openEmployeesCommand;
        RelayCommand? openDepartamentsCommand;

        public RelayCommand OpenOrdersCommand
        {
            get
            {
                return
                  (openOrdersCommand = new RelayCommand((o) =>
                  {
                      _ = new OrdersListViewModel();
                  }));
            }
        }
        public RelayCommand OpenEmployeesCommand
        {
            get
            {
                return
                  (openEmployeesCommand = new RelayCommand((o) =>
                  {
                      _ = new EmployeesListViewModel();
                  }));
            }
        }

        public RelayCommand OpenDepartamentsCommand
        {
            get
            {
                return
                  (openDepartamentsCommand = new RelayCommand((o) =>
                  {
                      _ = new DepartamentsListViewModel();
                  }));
            }
        }

        
    }
}
