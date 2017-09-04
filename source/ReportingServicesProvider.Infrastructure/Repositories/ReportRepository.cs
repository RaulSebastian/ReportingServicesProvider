using System;
using System.Collections.Generic;
using ReportingServicesProvider.Infrastructure.Model.Reporting;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ReportingServicesProvider.Infrastructure.Repositories
{
    public class ReportRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        private static Report InactiveReportingServer(string oldName)
            => new Report
            {
                Active = false,
                Name = $"{oldName}_deleted_{DateTime.Now:yyyyMMddHHmmssmmm}",
                Modified = DateTime.Now
            };

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

        public Report Update(Report report)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Report>(
                    rs => rs.Id == report.Id && rs.Active);
                if (existing == null)
                    return null;

                db.Save(report);
            }
            return report;
        }

        public int Delete(int id)
        {
            using (var db = _dbFactory.Open())
            {
                var existing = db.Single<Report>(
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