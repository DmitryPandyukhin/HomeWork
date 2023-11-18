using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Models
{
    public interface IDepartament
    {
        public int DepartamentId { get; set; }
        public string Name { get; set; }
    }
    public class Departament : NotifyPropertyChanged, IDepartament, IDataErrorInfo
    {
        public int DepartamentId { get; set; }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged("Name");
            }
        }

        // Ссылка на руководителя.
        public int? ManagerId { get; set; }
        Employee? manager;
        public Employee? Manager
        {
            get { return manager; }
            set
            {
                if (manager == value) return;
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        [InverseProperty("Departament")]
        public virtual ObservableCollection<Employee>? Employees { get; set; }

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
                    case "Name": 
                        if (string.IsNullOrEmpty(Name)) 
                            result = "Введите название подразделения."; break;
                };
                return result;
            }
        }
    }
}
