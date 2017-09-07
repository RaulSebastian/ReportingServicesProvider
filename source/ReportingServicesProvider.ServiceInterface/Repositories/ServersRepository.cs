using System;
using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ReportingServicesProvider.ServiceInterface.Repositories
{
    public class ServersRepository : IServersRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ServersRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Server> GetAll()
        {
            using (var db = _dbFactory.Open())
            {
                return db.Select<Server>(s => s.Active);
            }
        }

        public Server GetById(int id)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<Server>(s => s.Id == id && s.Active);
            }
        }

        public Server GetByName(string name)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<Server>(s => s.Name == name && s.Active);
            }
        }

        public bool Exists(Server server)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Exists<Server>(s => s.Active && (s.Id == server.Id || s.Name == server.Name));
            }
        }

        public Server Save(Server server)
        {
            using (var db = _dbFactory.Open())
            {
                db.Save(server);
            }
            return server;
        }
        
        public int SetInactive(Server server)
        {
            using (var db = _dbFactory.Open())
            {
                return db.UpdateOnly(new Server { Active = false, Modified = DateTime.Now}
                    , f => new {f.Active, f.Modified}
                    , s => s.Id == server.Id);
            }
        }
    }
}