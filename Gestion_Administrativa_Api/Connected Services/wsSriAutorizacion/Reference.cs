﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsSriAutorizacion
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", ConfigurationName="wsSriAutorizacion.AutorizacionComprobantesOffline")]
    public interface AutorizacionComprobantesOffline
    {
        
        // CODEGEN: El parámetro "RespuestaAutorizacionComprobante" requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es "Microsoft.Xml.Serialization.XmlElementAttribute".
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(mensaje[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(autorizacion[]))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="RespuestaAutorizacionComprobante")]
        wsSriAutorizacion.autorizacionComprobanteResponse autorizacionComprobante(wsSriAutorizacion.autorizacionComprobante request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<wsSriAutorizacion.autorizacionComprobanteResponse> autorizacionComprobanteAsync(wsSriAutorizacion.autorizacionComprobante request);
        
        // CODEGEN: El parámetro "RespuestaAutorizacionLote" requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es "Microsoft.Xml.Serialization.XmlElementAttribute".
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(mensaje[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(autorizacion[]))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="RespuestaAutorizacionLote")]
        wsSriAutorizacion.autorizacionComprobanteLoteResponse autorizacionComprobanteLote(wsSriAutorizacion.autorizacionComprobanteLote request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<wsSriAutorizacion.autorizacionComprobanteLoteResponse> autorizacionComprobanteLoteAsync(wsSriAutorizacion.autorizacionComprobanteLote request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class respuestaComprobante
    {
        
        private string claveAccesoConsultadaField;
        
        private string numeroComprobantesField;
        
        private autorizacion[] autorizacionesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string claveAccesoConsultada
        {
            get
            {
                return this.claveAccesoConsultadaField;
            }
            set
            {
                this.claveAccesoConsultadaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string numeroComprobantes
        {
            get
            {
                return this.numeroComprobantesField;
            }
            set
            {
                this.numeroComprobantesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public autorizacion[] autorizaciones
        {
            get
            {
                return this.autorizacionesField;
            }
            set
            {
                this.autorizacionesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class autorizacion
    {
        
        private string estadoField;
        
        private string numeroAutorizacionField;
        
        private System.DateTime fechaAutorizacionField;
        
        private bool fechaAutorizacionFieldSpecified;
        
        private string ambienteField;
        
        private string comprobanteField;
        
        private mensaje[] mensajesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string estado
        {
            get
            {
                return this.estadoField;
            }
            set
            {
                this.estadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string numeroAutorizacion
        {
            get
            {
                return this.numeroAutorizacionField;
            }
            set
            {
                this.numeroAutorizacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public System.DateTime fechaAutorizacion
        {
            get
            {
                return this.fechaAutorizacionField;
            }
            set
            {
                this.fechaAutorizacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fechaAutorizacionSpecified
        {
            get
            {
                return this.fechaAutorizacionFieldSpecified;
            }
            set
            {
                this.fechaAutorizacionFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string ambiente
        {
            get
            {
                return this.ambienteField;
            }
            set
            {
                this.ambienteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string comprobante
        {
            get
            {
                return this.comprobanteField;
            }
            set
            {
                this.comprobanteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public mensaje[] mensajes
        {
            get
            {
                return this.mensajesField;
            }
            set
            {
                this.mensajesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class mensaje
    {
        
        private string identificadorField;
        
        private string mensaje1Field;
        
        private string informacionAdicionalField;
        
        private string tipoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string identificador
        {
            get
            {
                return this.identificadorField;
            }
            set
            {
                this.identificadorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("mensaje", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string mensaje1
        {
            get
            {
                return this.mensaje1Field;
            }
            set
            {
                this.mensaje1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string informacionAdicional
        {
            get
            {
                return this.informacionAdicionalField;
            }
            set
            {
                this.informacionAdicionalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class respuestaLote
    {
        
        private string claveAccesoLoteConsultadaField;
        
        private string numeroComprobantesLoteField;
        
        private autorizacion[] autorizacionesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string claveAccesoLoteConsultada
        {
            get
            {
                return this.claveAccesoLoteConsultadaField;
            }
            set
            {
                this.claveAccesoLoteConsultadaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string numeroComprobantesLote
        {
            get
            {
                return this.numeroComprobantesLoteField;
            }
            set
            {
                this.numeroComprobantesLoteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public autorizacion[] autorizaciones
        {
            get
            {
                return this.autorizacionesField;
            }
            set
            {
                this.autorizacionesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobante", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobante
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string claveAccesoComprobante;
        
        public autorizacionComprobante()
        {
        }
        
        public autorizacionComprobante(string claveAccesoComprobante)
        {
            this.claveAccesoComprobante = claveAccesoComprobante;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobanteResponse", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobanteResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public wsSriAutorizacion.respuestaComprobante RespuestaAutorizacionComprobante;
        
        public autorizacionComprobanteResponse()
        {
        }
        
        public autorizacionComprobanteResponse(wsSriAutorizacion.respuestaComprobante RespuestaAutorizacionComprobante)
        {
            this.RespuestaAutorizacionComprobante = RespuestaAutorizacionComprobante;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobanteLote", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobanteLote
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string claveAccesoLote;
        
        public autorizacionComprobanteLote()
        {
        }
        
        public autorizacionComprobanteLote(string claveAccesoLote)
        {
            this.claveAccesoLote = claveAccesoLote;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobanteLoteResponse", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobanteLoteResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public wsSriAutorizacion.respuestaLote RespuestaAutorizacionLote;
        
        public autorizacionComprobanteLoteResponse()
        {
        }
        
        public autorizacionComprobanteLoteResponse(wsSriAutorizacion.respuestaLote RespuestaAutorizacionLote)
        {
            this.RespuestaAutorizacionLote = RespuestaAutorizacionLote;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface AutorizacionComprobantesOfflineChannel : wsSriAutorizacion.AutorizacionComprobantesOffline, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class AutorizacionComprobantesOfflineClient : System.ServiceModel.ClientBase<wsSriAutorizacion.AutorizacionComprobantesOffline>, wsSriAutorizacion.AutorizacionComprobantesOffline
    {
        
        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AutorizacionComprobantesOfflineClient() : 
                base(AutorizacionComprobantesOfflineClient.GetDefaultBinding(), AutorizacionComprobantesOfflineClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.AutorizacionComprobantesOfflinePort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AutorizacionComprobantesOfflineClient(EndpointConfiguration endpointConfiguration) : 
                base(AutorizacionComprobantesOfflineClient.GetBindingForEndpoint(endpointConfiguration), AutorizacionComprobantesOfflineClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AutorizacionComprobantesOfflineClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AutorizacionComprobantesOfflineClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AutorizacionComprobantesOfflineClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AutorizacionComprobantesOfflineClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AutorizacionComprobantesOfflineClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        wsSriAutorizacion.autorizacionComprobanteResponse wsSriAutorizacion.AutorizacionComprobantesOffline.autorizacionComprobante(wsSriAutorizacion.autorizacionComprobante request)
        {
            return base.Channel.autorizacionComprobante(request);
        }
        
        public wsSriAutorizacion.respuestaComprobante autorizacionComprobante(string claveAccesoComprobante)
        {
            wsSriAutorizacion.autorizacionComprobante inValue = new wsSriAutorizacion.autorizacionComprobante();
            inValue.claveAccesoComprobante = claveAccesoComprobante;
            wsSriAutorizacion.autorizacionComprobanteResponse retVal = ((wsSriAutorizacion.AutorizacionComprobantesOffline)(this)).autorizacionComprobante(inValue);
            return retVal.RespuestaAutorizacionComprobante;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<wsSriAutorizacion.autorizacionComprobanteResponse> wsSriAutorizacion.AutorizacionComprobantesOffline.autorizacionComprobanteAsync(wsSriAutorizacion.autorizacionComprobante request)
        {
            return base.Channel.autorizacionComprobanteAsync(request);
        }
        
        public System.Threading.Tasks.Task<wsSriAutorizacion.autorizacionComprobanteResponse> autorizacionComprobanteAsync(string claveAccesoComprobante)
        {
            wsSriAutorizacion.autorizacionComprobante inValue = new wsSriAutorizacion.autorizacionComprobante();
            inValue.claveAccesoComprobante = claveAccesoComprobante;
            return ((wsSriAutorizacion.AutorizacionComprobantesOffline)(this)).autorizacionComprobanteAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        wsSriAutorizacion.autorizacionComprobanteLoteResponse wsSriAutorizacion.AutorizacionComprobantesOffline.autorizacionComprobanteLote(wsSriAutorizacion.autorizacionComprobanteLote request)
        {
            return base.Channel.autorizacionComprobanteLote(request);
        }
        
        public wsSriAutorizacion.respuestaLote autorizacionComprobanteLote(string claveAccesoLote)
        {
            wsSriAutorizacion.autorizacionComprobanteLote inValue = new wsSriAutorizacion.autorizacionComprobanteLote();
            inValue.claveAccesoLote = claveAccesoLote;
            wsSriAutorizacion.autorizacionComprobanteLoteResponse retVal = ((wsSriAutorizacion.AutorizacionComprobantesOffline)(this)).autorizacionComprobanteLote(inValue);
            return retVal.RespuestaAutorizacionLote;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<wsSriAutorizacion.autorizacionComprobanteLoteResponse> wsSriAutorizacion.AutorizacionComprobantesOffline.autorizacionComprobanteLoteAsync(wsSriAutorizacion.autorizacionComprobanteLote request)
        {
            return base.Channel.autorizacionComprobanteLoteAsync(request);
        }
        
        public System.Threading.Tasks.Task<wsSriAutorizacion.autorizacionComprobanteLoteResponse> autorizacionComprobanteLoteAsync(string claveAccesoLote)
        {
            wsSriAutorizacion.autorizacionComprobanteLote inValue = new wsSriAutorizacion.autorizacionComprobanteLote();
            inValue.claveAccesoLote = claveAccesoLote;
            return ((wsSriAutorizacion.AutorizacionComprobantesOffline)(this)).autorizacionComprobanteLoteAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.AutorizacionComprobantesOfflinePort))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.AutorizacionComprobantesOfflinePort))
            {
                return new System.ServiceModel.EndpointAddress("https://celcer.sri.gob.ec/comprobantes-electronicos-ws/AutorizacionComprobantesOf" +
                        "fline");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return AutorizacionComprobantesOfflineClient.GetBindingForEndpoint(EndpointConfiguration.AutorizacionComprobantesOfflinePort);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return AutorizacionComprobantesOfflineClient.GetEndpointAddress(EndpointConfiguration.AutorizacionComprobantesOfflinePort);
        }
        
        public enum EndpointConfiguration
        {
            
            AutorizacionComprobantesOfflinePort,
        }
    }
}
