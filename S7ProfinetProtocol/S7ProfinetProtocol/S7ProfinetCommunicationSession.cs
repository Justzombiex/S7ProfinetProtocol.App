using Domain.Core.Concrete;
using S7.Net;
using S7.Net.Types;
using S7Profinet.Implementacion.dataSourceCommunication;
using S7ProfinetProtocol.dataSourceCommunication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S7ProfinetProtocol.S7ProfinetProtocol
{
    public class S7ProfinetCommunicationSession : IDataSourceCommunicationSession
    {
        private Plc Plc;

        public Guid DataSourceId => throw new NotImplementedException();

        public Guid SessionId => throw new NotImplementedException();

        /// <summary>
        /// Constructor de S7ProfinetCommunicationSession
        /// </summary>
        /// <param name="cpuType">Esto especifica a qué CPU se está conectando</param>
        /// <param name="ip">Especifica la dirección IP de la CPU o de la tarjeta Ethernet externa</param>
        /// <param name="rack">Contiene el rack del plc, que puedes encontrar en configuración de hardware</param>
        /// <param name="slot">Esta es la ranura de la CPU, que puedes encontrar en la configuración de hardware.</param>
        public S7ProfinetCommunicationSession(CpuType cpuType, string ip, short rack, short slot)
        {
            Plc plc = new Plc(cpuType, ip, rack, slot);
        }

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
            try
            {
                Plc.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result.Failure(ex.Message);
            }

            return Result.Success();
        }

        public Result Disconnect()
        {
            try
            {
                Plc.Close();
                return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result.Failure(ex.Message);
            }
        }
        public Result Discovery(string endpoint)
        {
            if(Plc.ReadStatus() == (byte)0x08)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure("No está corriendo el PLC");
            }
        }

        public void ReadValue(Node node, out DataValue dataValue)
        {
            S7ProfinetNode s7ProfinetNode = (S7ProfinetNode)node;

            DataValue readValue = new DataValue(Plc.Read(s7ProfinetNode.Tag));

            dataValue = readValue;
        }

        public void ReadValues(Node node, out DataValue dataValue)
        {
            S7ProfinetNode s7ProfinetNode = (S7ProfinetNode)node;

            DataValue readValue = new DataValue(Plc.Read(s7ProfinetNode.Tag));

            dataValue = readValue;
        }

        public void WriteValue(Node node, DataValue dataValue, out Result results)
        {
            S7ProfinetNode s7ProfinetNode = (S7ProfinetNode)node;

            try
            {
                Plc.Write(s7ProfinetNode.Tag, dataValue.Value);
            }
            catch (Exception ex) 
            {
                results = Result.Failure(ex.Message);
            }

            results = Result.Success();

        }

        public void WriteValues(List<Node> nodes, List<DataValue> dataValues, out Result results)
        {
            List<S7ProfinetNode> s7ProfinetNodes = new List<S7ProfinetNode>();

            foreach (Node node in nodes) 
            {
                s7ProfinetNodes.Add((S7ProfinetNode)node);
            }

            for (int i = 0; i <= s7ProfinetNodes.Count && i <= dataValues.Count; i++)
            {
                try
                {
                    Plc.Write(s7ProfinetNodes[i].Tag, dataValues[i].Value);
                }
                catch(Exception ex)
                {
                    results = Result.Failure(ex.Message);
                }
            }

            if (!dataValues.Any() || !s7ProfinetNodes.Any())
            {
                results = Result.Failure("Las listas están vacías");
            }
            else
            {
                results = Result.Success();
            }

        }
    }
}
