using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TryingLinq;

namespace TryingLinq
{
    public class CalculatorService : ICalculator
    {
        public double Add(double n1, double n2)
        {
            double result = n1 + n2;
            Console.WriteLine("Received Add({0},{1})", n1, n2);
            // Code added to write output to the console window.
            Console.WriteLine("Return: {0}", result);
            return result;
        }

        public double Subtract(double n1, double n2)
        {
            double result = n1 - n2;
            Console.WriteLine("Received Subtract({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }

        public double Multiply(double n1, double n2)
        {
            double result = n1 * n2;
            Console.WriteLine("Received Multiply({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }

        public double Divide(double n1, double n2)
        {
            double result = n1 / n2;
            Console.WriteLine("Received Divide({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }
    }


    [Table(Name = "Customers")]
    internal class Customer
    {
        private string _CustomerID;

        [Column(IsPrimaryKey = true, Storage = "_CustomerID")]
        public string CustomerID
        {
            get { return this._CustomerID; }
            set { this._CustomerID = value; }
        }

        private string _City;

        [Column(Storage = "_City")]
        public string City
        {
            get { return this._City; }
            set { this._City = value; }
        }
    }

    public class LondonCustomers : ILondonCustomers
    {

        public string GetLondonCustomers()
        {
            string myAnswer = null;

            // Use a connection string.
            DataContext db = new DataContext
                (@"c:\Learning\linqtest5\northwnd.mdf");

            // Get a typed table to run queries.
            Table<Customer> Customers = db.GetTable<Customer>();

            // Attach the log to show generated SQL.
            db.Log = Console.Out;

            // Query for customers in London.
            IQueryable<Customer> custQuery =
                from cust in Customers
                where cust.City == "London"
                select cust;

            var myItem = Customers.First(i => i.City == "London");

            myAnswer = myItem.CustomerID + " " + myItem.City;
            return myAnswer;
        }

    }
}