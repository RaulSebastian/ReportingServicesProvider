using System;
using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface.Repositories
{
    public interface IReportsRepository : IDisposable
    {
        bool Exists(Report report);
        bool ServerExists(Report report);
        List<Report> GetAllByServerId(int serverId);
        Report GetById(int id);
        Report Save(Report report);
        int SetInactive(Report report);
    }
}