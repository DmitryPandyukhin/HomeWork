using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;

namespace MyStore.Models
{
    public interface IOrder
    {
        int OrderId { get; set; }
        int? Number { get; set; }
        string? ProductName { get; set; }
    }
    public class Order : NotifyPropertyChanged, IOrder, IDataErrorInfo
    {
        public int OrderId { get; set; }
        int? number;
        public int? Number
        {
            get { return number; }
            set
            {
                if (number == value) return;
                number = value;
                OnPropertyChanged("Number");
            }
        }
        string? productName;
        public string? ProductName
        {
            get { return productName; }
            set
            {
                if (productName == value) return;
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        public int? EmployeeId { get; set; }
        // Ссылка на сотрудника
        Employee? employee;
        public Employee? Employee
        {
            get { return employee; }
            set
            {
                if (employee == value) return;
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        // один заказ - много тегов
        ObservableCollection<Tag>? tags;
        public ObservableCollection<Tag>? Tags
        {
            get { return tags; }
            set
            {
                if (tags == value) return;
                tags = value;
                //OnPropertyChanged("Tags");
                // Вызов для обновления на формах.
                OnPropertyChanged("TagsString");
            }
        }
        // Отображение строки тегов с разделтелем запятой
        public string? TagsString
        {
            // Сеттер, содержащий парсинг строки тегов, создается во ViewModel.
            // Геттер создаем тут, так как отображение нужно в ремках списка заказов.
            get 
            {
                var tags = Tags?.Select(t => t.Name)?.ToList();

                string? tagsString = null;
                if (tags != null)
                    tagsString = string.Join(", ", tags!);

                return tagsString;
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
                    case "Number":
                        if ((Number == 0) || (Number == null))
                            result = "Введите номер заказа (целое число)."; break;
                    
                };
                return result;
            }
        }
    }
}
