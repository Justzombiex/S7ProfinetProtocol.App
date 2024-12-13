using S7.Net;
using S7Profinet.Implementacion.dataSourceCommunication;
using S7ProfinetProtocol.dataSourceCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S7ProfinetProtocol.S7ProfinetProtocol
{
    public class TaskManager
    {
        public Plc Plc { get; }

        private Dictionary<S7ProfinetNode, (DataValue dataValue, valueChanged callback)> nodeDictionary = new Dictionary<S7ProfinetNode, (DataValue dataValue, valueChanged Callback)>();

        public event valueChanged MyEvent;

        public TaskManager(Plc plc)
        {
            Plc = plc;
            Task myTask = Task.Run(() => CheckNodeValuesTask());
        }


        public void AddNodes(Node node, valueChanged callback, out object serverHandle)
        {
            S7ProfinetNode s7ProfinetNode = (S7ProfinetNode)node;

            nodeDictionary.Add(s7ProfinetNode, (ReadS7ProfinetData(s7ProfinetNode), callback));

            MyEvent += callback;

            serverHandle = s7ProfinetNode;

        }

        public void RemoveNodes(Node node)
        {
            MyEvent -= nodeDictionary[(S7ProfinetNode)node].callback;

            nodeDictionary.Remove((S7ProfinetNode)node);
        }

        public void RemoveAllNodes()
        {
            for (int i = 0; i < nodeDictionary.Count; i++)
            {
                MyEvent -= nodeDictionary[nodeDictionary.Keys.ElementAt(i)].callback;

                nodeDictionary.Remove(nodeDictionary.Keys.ElementAt(i));
            }
        }

        private async Task CheckNodeValuesTask()
        {
            while (true)
            {
                for (int i = 0; i < nodeDictionary.Count; i++)
                {
                    DataValue currentValue = ReadS7ProfinetData(nodeDictionary.Keys.ElementAt(i));

                    if (!CompareValues(currentValue.Value, nodeDictionary[nodeDictionary.Keys.ElementAt(i)].dataValue.Value))
                    {
                        if (MyEvent != null)
                        {
                            nodeDictionary[nodeDictionary.Keys.ElementAt(i)].callback(null, currentValue);
                        }
                        else
                        {
                            throw new Exception("El evento es nulo");
                        }
                    }
                }

                await Task.Delay(500);
            }

        }

        #region Helpers
        private DataValue ReadS7ProfinetData(S7ProfinetNode s7ProfinetNode)
        {
            DataValue dataValue = new DataValue(Plc.Read(s7ProfinetNode.Tag));

            return dataValue;
        }

        public bool CompareValues(object objA, object objB)
        {
            // Verifica si ambos objetos son nulos
            if (objA == null && objB == null)
            {
                return true;
            }

            // Si uno de los objetos es nulo y el otro no, no son iguales
            if (objA == null || objB == null)
            {
                return false;
            }

            // Si ambos objetos son arreglos de bool, comparamos sus contenidos
            if (objA is bool[] boolArrayA && objB is bool[] boolArrayB)
            {
                return boolArrayA.SequenceEqual(boolArrayB);
            }

            // Si ambos objetos son arreglos de ushort, comparamos sus contenidos
            if (objA is ushort[] ushortArrayA && objB is ushort[] ushortArrayB)
            {
                return ushortArrayA.SequenceEqual(ushortArrayB);
            }

            // Si los objetos no son ni arreglos de bool ni arreglos de ushort, los comparamos directamente
            return objA.Equals(objB);
        }
        #endregion Helpers
    }
}
