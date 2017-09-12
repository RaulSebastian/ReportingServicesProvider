using System;
using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface.Repositories
{
    public interface IServersRepository : IDisposable
    {
        bool Exists(Server server);
        List<Server> GetAll();
        Server GetById(int id);
        Server GetByName(string name);
        int Rename(Server server, string newName);
        Server Save(Server server);
        int SetInactive(Server server);
    }
}