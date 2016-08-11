namespace Hanami.Shared
{
    public class Service
    {
        public Service(Host host, string name)
        {
            Host = host;
            Name = name;
        }

        public Host Host { get; private set; }

        public Hanami.Shared.CheckData? LastData { get; set; }

        public string Name { get; private set; }
    }
}