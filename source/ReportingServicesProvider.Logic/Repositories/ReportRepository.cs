using System.Collections.Generic;
using ReportingServicesProvider.Logic.Model.Reporting;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ReportingServicesProvider.Logic.Repositories
{
    public class ReportRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ReportRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Report Create(Report entity)
        {
            using (var db = _dbFactory.Open())
            {
                db.Save(entity);
            }
            return entity;
        }

        public List<Report> ReadAll()
        {
            using (var db = _dbFactory.Open())
            {
                return db.Select<Report>(rs => rs.Active);
            }
        }

        public Report Read(int id)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<Report>(rs => rs.Id == id && rs.Active);
            }
        }

        public Report Update(Report server)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Report>(
                    rs => rs.Id == server.Id && rs.Active);
                if (existing == null)
                    return null;

                db.Save(server);
            }
            return server;
        }
    }
}