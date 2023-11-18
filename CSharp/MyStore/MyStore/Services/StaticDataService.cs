using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore.Services
{
    public static class StaticDataService
    {
        static MyStoreContext DB { get; set; }
        static StaticDataService()
        {
            DB = new MyStoreContext();

            // пересоздаем БД
            //DB.Database.EnsureDeleted();
            // гарантируем, что БД создана
            DB.Database.EnsureCreated();
            // Тестовые данные
            //SeedData();

            DB.Employees
                .Include(e => e.Departament)
                .Load();

            DB.Departaments
                .Include(d => d.Employees)
                .Load();

            DB.Orders
                .Include(o => o.Employee)
                .Include(o => o.Tags)
                .Load();

            DB.Tags
                .Load();
        }

        private static void SeedData()
        {
            // департаменты
            Departament dep1 = new() { Name = "Dep1" };
            Departament dep2 = new() { Name = "Dep2" };
            DB.Departaments.AddRange(dep1, dep2);
            DB.SaveChanges();

            // Сотрудники
            int id = DB.Departaments.First().DepartamentId;
            Employee emp1 = new() { LastName = "Ivanov", FirstName = "Ivan", Sex = Sex.мужской, BirthDate = new DateTime(2021, 1, 1), DepartamentId = id };
            Employee emp2 = new() { LastName = "Petrov", FirstName = "Petr", Sex = Sex.мужской, BirthDate = new DateTime(2020, 1, 1), DepartamentId = id };
            DB.Employees.AddRange(emp1, emp2);
            DB.SaveChanges();

            // Заказы
            id = DB.Employees.First().EmployeeId;
            Order order = new() { Number = 123, ProductName = "Product1", EmployeeId = id };
            DB.Orders.Add(order);
            DB.SaveChanges();
        }

        // Сотрудники
        public static IEmployee? GetEmloyee(int employeeId)
        {
            return (IEmployee?)DB.Employees.Local?.FirstOrDefault(e => e.EmployeeId == employeeId);
        }
        public static ObservableCollection<Employee> GetEmloyeesList()
        {
            return DB.Employees.Local.ToObservableCollection();
        }

        // Подразделения
        public static IDepartament? GetDepartament(int departamentId)
        {
            return (IDepartament?)DB.Departaments.Local?.FirstOrDefault(d => d.DepartamentId == departamentId);
        }
        public static ObservableCollection<Departament> GetDepartamentsList()
        {
            return DB.Departaments.Local.ToObservableCollection();
        }

        // Заказы
        /*public IOrder? GetOrder(int orderId)
        {
            return (IOrder?)db.Orders.Local?.FirstOrDefault(o => o.OrderId == orderId);
        }*/
        public static ObservableCollection<Order>? GetOrdersList()
        {
            return DB.Orders.Local.ToObservableCollection();
        }

        public static void SaveChages()
        {
            DB.SaveChanges();
        }

        public static void AddEmloyee(IEmployee employee)
        {
            DB.Add(employee);
            SaveChages();
        }

        public static void EditEmloyee(IEmployee employee)
        {
            DB.Entry(employee).State = EntityState.Modified;
            SaveChages();
        }

        public static void DeleteEmloyee(IEmployee employee)
        {
            DB.Remove(employee);
            SaveChages();
        }

        public static void AddDepartament(IDepartament departament)
        {
            DB.Add(departament);
            SaveChages();
        }

        public static void EditDepartament(IDepartament departament)
        {
            DB.Entry(departament).State = EntityState.Modified;
            SaveChages();
        }

        public static void DeleteDepartament(IDepartament departament)
        {
            DB.Remove(departament);
            SaveChages();
        }

        public static void AddOrder(IOrder order)
        {
            DB.Add(order);
            SaveChages();
        }

        public static void EditOrder(IOrder order)
        {
            DB.Entry(order).State = EntityState.Modified;
            SaveChages();
        }

        public static void DeleteOrder(IOrder order)
        {
            DB.Remove(order);
            SaveChages();
        }

        public static ObservableCollection<Tag> GetTagsForOrders(int orderId)
        {
            return new ObservableCollection<Tag>(
                DB.Tags.Local
                .Where(t => t.OrderId == orderId));
        }
    }
}
