using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MyStore.Models;
using MyStore.Views;
using MyStore.Services;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace MyStore.ViewModels
{
    public class OrderViewModel : NotifyPropertyChanged
    {
        RelayCommand? okCommand;
        OrderWindow? OrderWindow { get; set; }

        // данные формы
        public Order Order { get; set; }
        public ObservableCollection<Employee>? Employees { get; set; }

        public OrderViewModel(IOrder order)
        {
            Order = (Order)order;
            
            // Справочник сотрудников.
            Employees = StaticDataService.GetEmloyeesList();
        }

        public bool OpenWindow()
        {
            // Открываем окно
            OrderWindow = new OrderWindow(this);
            bool dialogResult = OrderWindow.ShowDialog() ?? false;

            return dialogResult;
        }

        // Взаимодействуем со справочником сотрудников
        public Employee EmployeeItem
        {
            get { return Order.Employee!; }
            set
            {
                if (Order.Employee == value) return;
                Order.Employee = value;
            }
        }

        public string TagsStringEditable
        {
            get { return Order.TagsString ?? ""; }
            set
            {
                if (Order.TagsString == value) return;

                Order.Tags = new();

                // Парсим строку с тегами.
                string[] tags = value.Split(",");
                for (int i = 0; i < tags.Length; i++)
                {
                    Order.Tags.Add(new Tag()
                    {
                        Name = tags[i].Trim(),
                        SortNumber = i
                    });
                }
            }
        }

        public RelayCommand OkCommand
        {
            get
            {
                return
                  (okCommand = new RelayCommand((o) =>
                  {
                      if (OrderWindow != null)
                      {
                          if ((Order?.Number == 0) || (Order?.Number == null))
                          {
                              MessageBox.Show("Не введен номер заказа (целое число).", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                              return;
                          }
                              
                          OrderWindow.DialogResult = true;
                      }    
                  }));
            }
        }
    }
}
