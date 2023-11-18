using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using MyStore.Views;

namespace MyStore.ViewModels
{
    public class OrdersListViewModel : NotifyPropertyChanged
    {
        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;


        OrdersListWindow OrdersListWindow { get; }
        public ObservableCollection<Order>? Orders { get; set; }

        public OrdersListViewModel()
        {

            Orders = StaticDataService.GetOrdersList();

            OrdersListWindow = new OrdersListWindow(this);
            OrdersListWindow.ShowDialog();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return 
                  (addCommand = new RelayCommand((o) =>
                  {
                      Order vm = new();
                      if (new OrderViewModel(vm).OpenWindow())
                      {
                          StaticDataService.AddOrder(vm);
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
                        if (selectedItem is not Order order) return;

                        Order vm = new()
                        {
                            Number = order.Number,
                            ProductName = order.ProductName
                        };

                        if (order.Employee != null)
                            vm.Employee = (Employee?)StaticDataService.GetEmloyee(order.Employee.EmployeeId);

                        if (order.Tags != null)
                            vm.Tags = StaticDataService.GetTagsForOrders(order.OrderId);

                        if (new OrderViewModel(vm).OpenWindow())
                        {
                            order.Number = vm.Number;
                            order.ProductName = vm.ProductName;

                            if (vm.Employee != null)
                                order.Employee = (Employee?)StaticDataService.GetEmloyee(vm.Employee.EmployeeId);

                            if (vm.Tags != null)
                            {
                                if (order?.Tags?.Count > 0) 
                                    order.Tags.Clear();
                                order!.Tags = vm.Tags;
                            }

                            StaticDataService.EditOrder(order);
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
                      // если ни одного объекта не выделено, выходим
                      if (selectedItem is not Order order) return;
                      StaticDataService.DeleteOrder(order);
                  }));
            }
        }
    }
}
