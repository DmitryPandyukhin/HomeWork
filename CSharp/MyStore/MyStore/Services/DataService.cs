using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyStore.Services
{
    /// <summary>
    /// Только работа с контекстом: получение, запись, изменение, удаление.
    /// Функционала по изменению обхектов быть не должно.
    /// </summary>
    /*public interface IDataService
    {
        // Сотрудники
        IEmployee? GetEmloyee(int employeeId);
        void AddEmloyee(IEmployee employee);
        void EditEmloyee(IEmployee employee);
        void DeleteEmloyee(IEmployee employee);
        ObservableCollection<Employee>? GetEmloyeesList();

        // Подразделения
        IDepartament? GetDepartament(int departamentId);
        void AddDepartament(IDepartament departament);
        
        void EditDepartament(IDepartament departament);
        void DeleteDepartament(IDepartament departament);
        ObservableCollection<Departament>? GetDepartamentsList();

        // Заказы
        //IOrder? GetOrder(int orderId);
        void AddOrder(IOrder order);
        // Здесь просто сохраняем. ОБъект уже изменен по ссылке
        void EditOrder(IOrder order);
        void DeleteOrder(IOrder order);
        ObservableCollection<Order>? GetOrdersList();

        // Теги
        ObservableCollection<Tag> GetTagsForOrders(int orderId);

        void SaveChages();
    }
    public class DataService : IDataService
    {
        MyStoreContext DB { get; }
        public DataService()
        {
            DB = new MyStoreContext();

            // пересоздаем БД
            //db.Database.EnsureDeleted();
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

        *//*private void SeedData()
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
            Order order = new () { Number = 123, ProductName = "Product1", EmployeeId = id };
            DB.Orders.Add(order);
            DB.SaveChanges();
        }*//*

        // Сотрудники
        public IEmployee? GetEmloyee(int employeeId)
        {
            return (IEmployee?)DB.Employees.Local?.FirstOrDefault(e => e.EmployeeId == employeeId);
        }
        public ObservableCollection<Employee> GetEmloyeesList()
        {
            return DB.Employees.Local.ToObservableCollection();
        }

        // Подразделения
        public IDepartament? GetDepartament(int departamentId)
        {
            return (IDepartament?)DB.Departaments.Local?.FirstOrDefault(d => d.DepartamentId == departamentId);
        }
        public ObservableCollection<Departament> GetDepartamentsList()
        {
            return DB.Departaments.Local.ToObservableCollection();
        }

        // Заказы
        *//*public IOrder? GetOrder(int orderId)
        {
            return (IOrder?)db.Orders.Local?.FirstOrDefault(o => o.OrderId == orderId);
        }*//*
        public ObservableCollection<Order>? GetOrdersList()
        {
            return DB.Orders.Local.ToObservableCollection();
        }

        public void SaveChages()
        {
            DB.SaveChanges();
        }

        public void AddEmloyee(IEmployee employee)
        {
            DB.Add(employee);
            SaveChages();
        }

        public void EditEmloyee(IEmployee employee)
        {
            DB.Entry(employee).State = EntityState.Modified;
            SaveChages();
        }

        public void DeleteEmloyee(IEmployee employee)
        {
            DB.Remove(employee);
            SaveChages();
        }

        public void AddDepartament(IDepartament departament)
        {
            DB.Add(departament); 
            SaveChages();
        }

        public void EditDepartament(IDepartament departament)
        {
            DB.Entry(departament).State = EntityState.Modified;
            SaveChages();
        }

        public void DeleteDepartament(IDepartament departament)
        {
            DB.Remove(departament);
            SaveChages();
        }

        public void AddOrder(IOrder order)
        {
            DB.Add(order);
            SaveChages();
        }

        public void EditOrder(IOrder order)
        {
            DB.Entry(order).State = EntityState.Modified;
            SaveChages();
        }

        public void DeleteOrder(IOrder order)
        {
            DB.Remove(order);
            SaveChages();
        }

        public ObservableCollection<Tag> GetTagsForOrders(int orderId)
        {
            return new ObservableCollection<Tag>(
                DB.Tags.Local
                .Where(t => t.OrderId == orderId));
        }
    }*/
}
