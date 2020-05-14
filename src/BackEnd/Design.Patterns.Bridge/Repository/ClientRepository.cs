﻿using Design.Patterns.Bridge.Interfaces;
using Design.Patterns.Bridge.Model;
using System;

namespace Design.Patterns.Bridge.Repository
{
    public class ClientRepository : RepositoryBase, IRepository<Client>
    {
        private readonly IConnectionDataBase _connectionDataBase;
        public ClientRepository(string connectionString, IConnectionDataBase connectionDataBase) : base(connectionString)
        {
            _connectionDataBase = connectionDataBase;
        }

        public int Insert(Client entity)
        {
            _connectionDataBase.OpenConnection(ConnectionString);
            entity.Id = new Random().Next(0, 100);
            Console.WriteLine($"inserido cliente {entity.ToString()}");
            _connectionDataBase.CloseConnection();
            return entity.Id;
        }
    }
}
