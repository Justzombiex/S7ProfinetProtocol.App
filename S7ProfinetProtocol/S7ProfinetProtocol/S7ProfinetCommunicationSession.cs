using Domain.Core.Concrete;
using S7Profinet.Implementacion.dataSourceCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S7ProfinetProtocol.S7ProfinetProtocol
{
    public class S7ProfinetCommunicationSession : IDataSourceCommunicationSession
    {
        public Guid DataSourceId => throw new NotImplementedException();

        public Guid SessionId => throw new NotImplementedException();

        public void AddSuscription(Node node, object clientHandle, valueChanged callback, out object serverHandle)
        {
            throw new NotImplementedException();
        }

        public Result Browse()
        {
            throw new NotImplementedException();
        }

        public Result Connect(string endpoint)
        {
            throw new NotImplementedException();
        }

        public Result Disconnect()
        {
            throw new NotImplementedException();
        }

        public void ReadValue(Node node, out DataValue dataValue)
        {
            throw new NotImplementedException();
        }

        public void ReadValues(Node node, out DataValue dataValue)
        {
            throw new NotImplementedException();
        }

        public void WriteValue(Node node, DataValue dataValue, out Result results)
        {
            throw new NotImplementedException();
        }

        public void WriteValues(DataValue dataValue, out Result results)
        {
            throw new NotImplementedException();
        }
    }
}
