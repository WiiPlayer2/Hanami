using System.Collections.Generic;

namespace Hanami.Shared
{
    public interface IDatabase : IModule
    {
        void AddData(CheckData data);

        void AddHost(Host host);

        void AddService(Service service);

        IEnumerable<CheckData> GetData();

        IEnumerable<CheckData> GetData(Service service);

        IEnumerable<CheckData> GetData(string host, string service);

        Host GetHost(string name);

        IEnumerable<Host> GetHosts();

        Service GetService(Host host, string service);

        Service GetService(string host, string service);

        IEnumerable<Service> GetServices();

        IEnumerable<Service> GetServices(Host host);

        IEnumerable<Service> GetServices(string host);
        void RemoveData(CheckData data);

        void RemoveHost(string host);

        void RemoveService(string service);

        void UpdateHost(Host host);
        void UpdateService(Service service);
    }
}