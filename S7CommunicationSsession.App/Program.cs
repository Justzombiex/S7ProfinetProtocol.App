using S7.Net;
using S7ProfinetProtocol.S7ProfinetProtocol;

namespace S7CommunicationSsession.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            S7ProfinetCommunicationSession session = new S7ProfinetCommunicationSession(CpuType.S71500,"127.0.0.1",0,0,new Guid());

            session.Connect("");

        }
    }
}
