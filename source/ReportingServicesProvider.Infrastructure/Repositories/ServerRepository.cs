using System;
using System.Collections.Generic;
using ReportingServicesProvider.Infrastructure.Model.Reporting;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ReportingServicesProvider.Infrastructure.Repositories
{
    public class ServerRepository : IServerRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        private static Server InactiveReportingServer(string oldName)
            => new Server
            {
                Active = false,
                Name = $"{oldName}_deleted_{DateTime.Now:yyyyMMddHHmmssmmm}",
                Modified = DateTime.Now
            };

        public ServerRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Server Create(Server server)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Server>(
                    rs => (
                              rs.Id == server.Id
                              || rs.Name == server.Name
                          )
                          && rs.Active);
                if (existing != null)
                    return null;

                db.Save(server);
            }
            return server;
        }

        public List<Server> ReadAll()
        {
            using (var db = _dbFactory.Open())
            {
                return db.Select<Server>(rs => rs.Active);
            }
        }

        public Server Read(int id)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<Server>(rs => rs.Id == id && rs.Active);
            }
        }

        public Server ReadByName(string name)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<Server>(
                    rs => rs.Name == name
                          && rs.Active);
            }
        }

        public Server Update(Server server)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Server>(
                    rs => (
                              rs.Id == server.Id
                              || rs.Name == server.Name
                          )
                          && rs.Active);
                if (existing == null)
                    return null;

                db.Save(server);
            }
            return server;
        }

        public int Delete(string name)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Server>(
                    rs => rs.Name == name && rs.Active);
                if (existing == null)
                    return 0;

                return db.UpdateOnly(InactiveReportingServer(existing.Name)
                    , f => new { f.Active, f.Name, f.Modified }
                    , rs => rs.Name == name);
            }
        }

        public int Delete(int id)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Server>(
                    rs => rs.Id == id && rs.Active);
                if (existing == null)
                    return 0;

                return db.UpdateOnly(InactiveReportingServer(existing.Name)
                    , f => new { f.Active, f.Name, f.Modified }
                    , rs => rs.Id == id);
            }
        }
    }
}