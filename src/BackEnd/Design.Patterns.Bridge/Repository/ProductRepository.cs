﻿using Design.Patterns.Bridge.Interfaces;
using Design.Patterns.Bridge.Model;
using System;

namespace Design.Patterns.Bridge.Repository
{
    public class ProductRepository : RepositoryBase, IRepository<Product>
    {
        private readonly IConnectionDataBase _connectionDataBase;
        public ProductRepository(string connectionString, IConnectionDataBase connectionDataBase) : base(connectionString)
        {
            _connectionDataBase = connectionDataBase;
        }

        public int Insert(Product entity)
        {
            _connectionDataBase.OpenConnection(ConnectionString);
            entity.Id = new Random().Next(0, 100);
            Console.WriteLine($"inserido Produto {entity.ToString()}");
            _connectionDataBase.CloseConnection();
            return entity.Id;
        }
    }
}
