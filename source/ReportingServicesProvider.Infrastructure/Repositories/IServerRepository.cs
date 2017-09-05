using System.Collections.Generic;
using ReportingServicesProvider.Infrastructure.Model.Reporting;

namespace ReportingServicesProvider.Infrastructure.Repositories
{
    public interface IServerRepository
    {
        Server Create(Server server);
        int Delete(int id);
        int Delete(string name);
        Server Read(int id);
        List<Server> ReadAll();
        Server ReadByName(string name);
        Server Update(Server server);
    }
}