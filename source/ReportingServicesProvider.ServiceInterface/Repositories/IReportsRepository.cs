using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface.Repositories
{
    public interface IReportsRepository
    {
        bool Exists(Report report);
        List<Report> GetAll();
        Report GetById(int id);
        Report Save(Report report);
        int SetInactive(Report report);
    }
}