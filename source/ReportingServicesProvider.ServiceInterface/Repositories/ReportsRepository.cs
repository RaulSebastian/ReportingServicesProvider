using System;
using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ReportingServicesProvider.ServiceInterface.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ReportsRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Report> GetAll()
        {
            using (var db = _dbFactory.Open())
            {
                return db.Select<Report>(r => r.Active);
            }
        }

        public Report GetById(int id)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<Report>(r => r.Id == id && r.Active);
            }
        }

        public bool Exists(Report report)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Exists<Report>(r => r.Active && r.Id == report.Id);
            }
        }

        public Report Save(Report report)
        {
            using (var db = _dbFactory.Open())
            {
                db.Save(report);
            }
            return report;
        }
        
        public int SetInactive(Report report)
        {
            using (var db = _dbFactory.Open())
            {
                return db.UpdateOnly(new Report { Active = false, Modified = DateTime.Now}
                    , f => new {f.Active, f.Modified}
                    , r => r.Id == report.Id);
            }
        }
    }
}