using S7.Net;
using S7Profinet.Implementacion.dataSourceCommunication;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Concrete;

namespace S7ProfinetProtocol.dataSourceCommunication
{
    /// <summary>
    /// Modela un nodo para s7 Profinet
    /// </summary>
    public class S7ProfinetNode : Node
    {
        /// <summary>
        /// Tag de la variable
        /// </summary>
        public string Tag { get; private init; }

        /// <summary>
        /// Requerido por EntityFramework
        /// </summary>
        protected S7ProfinetNode()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag">Tag de la variable</param>
        [Newtonsoft.Json.JsonConstructor]
        protected S7ProfinetNode(string tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag">Tag de la variable</param>
        /// <returns></returns>
        public static Result<S7ProfinetNode> Create(string tag)
        {
            return Result<S7ProfinetNode>
                .Success(new S7ProfinetNode(tag));
        }

        public override string ToString()
        {
            return $"{Tag}";
        }
    }
}
