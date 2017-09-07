using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface.Repositories
{
    public interface IServersRepository
    {
        bool Exists(Server server);
        List<Server> GetAll();
        Server GetById(int id);
        Server GetByName(string name);
        Server Save(Server server);
        int SetInactive(Server server);
    }
}