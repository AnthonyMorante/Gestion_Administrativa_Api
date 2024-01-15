using FirmaXadesNetCore;
using FirmaXadesNetCore.Crypto;
using FirmaXadesNetCore.Signature;
using FirmaXadesNetCore.Signature.Parameters;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using wsSriRecepcion;
using static Gestion_Administrativa_Api.Interfaces.Utilidades.UtilidadesI;

namespace Gestion_Administrativa_Api.Interfaces.Utilidades
{
    public interface IUtilidades
    {
        Task<string> modulo11(string claveAcceso);

        Task<string> claveAcceso(Facturas? _factura);

        Task<string> claveAccesoRetencion(Retenciones? _retencion);

        Task<SignatureDocument> firmar(string codigo, string rutaFirma, XDocument documento);

        Task<bool> envioXmlSRI(XmlDocument? documentoFirmado);

        Task<estadoSri> verificarEstadoSRI(string claveAcceso);

        Task<bool> envioCorreo(string email, byte[] archivo, byte[] xml, string nombreArchivo);
    }

    public class UtilidadesI : IUtilidades
    {
        public async Task<string> modulo11(string claveAcceso)
        {
            try
            {
                char[] ClaveAcceso = claveAcceso.ToCharArray();
                int[] ValoresMultiplicados = new int[claveAcceso.Length];
                var sumaValores = 0;
                var verificador = 0;
                int multiplicando = 1;
                Array.Reverse(ClaveAcceso);

                for (int i = 0; i < ClaveAcceso.Length; i++)
                {
                    multiplicando++;
                    ValoresMultiplicados[i] = Convert.ToInt32(ClaveAcceso[i].ToString()) * multiplicando;
                    if (multiplicando == 7) multiplicando = 1;
                }

                sumaValores = ValoresMultiplicados.Sum();
                verificador = 11 - sumaValores % 11;

                Console.WriteLine(claveAcceso + verificador);

                if (verificador == 10)
                {
                    verificador = 1;
                }

                if (verificador == 11)
                {
                    verificador = 0;
                }
                var claveAccesoFinal = claveAcceso + verificador;
                return claveAccesoFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> claveAcceso(Facturas? _factura)
        {
            try
            {
                string fechaFormateada = _factura.FechaEmision?.ToString("ddMMyyyy");
                string tipoDocumento = _factura.TipoDocumento.ToString().PadLeft(2, '0');
                string ruc = _factura.EmisorRuc;
                int ambiente = Convert.ToInt16(Tools.config!["SRI:ambiente"]);
                string establecimiento = _factura.Establecimiento.ToString().PadLeft(3, '0');
                string puntoEmision = _factura.PuntoEmision.ToString().PadLeft(3, '0');
                string secuencial = _factura.Secuencial.ToString().PadLeft(9, '0');
                int numeroRandom = this.numerosRandom();
                int tipoEmision = Convert.ToInt16(Tools.config["SRI:tipoEmision"]);
                string claveAcceso = $"{fechaFormateada}{tipoDocumento}{ruc}{ambiente}{establecimiento}{puntoEmision}{secuencial}{numeroRandom}{tipoEmision}";
                string claveAccesoVerificador = await modulo11(claveAcceso);
                return claveAccesoVerificador;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> claveAccesoRetencion(Retenciones? _retencion)
        {
            try
            {
                string fechaFormateada = _retencion.FechaEmision?.ToString("ddMMyyyy");
                string tipoDocumento = _retencion.TipoDocumento.ToString().PadLeft(2, '0');
                string ruc = _retencion.EmisorRuc;
                int ambiente = Convert.ToInt16(Tools.config!["SRI:ambiente"]);
                string establecimiento = _retencion.Establecimiento.ToString().PadLeft(3, '0');
                string puntoEmision = _retencion.PuntoEmision.ToString().PadLeft(3, '0');
                string secuencial = _retencion.Secuencial.ToString().PadLeft(9, '0');
                int numeroRandom = this.numerosRandom();
                int tipoEmision = Convert.ToInt16(Tools.config["SRI:tipoEmision"]);
                string claveAcceso = $"{fechaFormateada}{tipoDocumento}{ruc}{ambiente}{establecimiento}{puntoEmision}{secuencial}{numeroRandom}{tipoEmision}";
                string claveAccesoVerificador = await modulo11(claveAcceso);
                _retencion.Ambiente = ambiente;
                _retencion.TipoEmision = tipoEmision;
                _retencion.ClaveAcceso = claveAcceso;
                return claveAccesoVerificador;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int numerosRandom()
        {
            try
            {
                Random random = new Random();
                int randomNumber = 0;

                for (int i = 0; i < 8; i++)
                {
                    randomNumber = random.Next(10000000, 99999999);
                }

                return randomNumber;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SignatureDocument?> firmar(string codigo, string rutaFirma, XDocument documento)
        {
            try
            {
                var cert = new X509Certificate2(rutaFirma, codigo, X509KeyStorageFlags.MachineKeySet);
                XadesService xadesService = new XadesService();
                SignatureParameters parametros = new SignatureParameters();
                parametros.SignaturePolicyInfo = new SignaturePolicyInfo();
                parametros.SignatureMethod = SignatureMethod.RSAwithSHA512;
                parametros.SigningDate = DateTime.Now;
                parametros.SignaturePackaging = SignaturePackaging.ENVELOPED;
                parametros.DataFormat = new DataFormat();
                parametros.Signer = new Signer(cert);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream)) documento.Save(xmlWriter);
                    memoryStream.Position = 0;
                    SignatureDocument docFirmado = xadesService.Sign(memoryStream, parametros);
                    return docFirmado;
                }
            }
            catch (Exception exc)
            {
                await Console.Out.WriteLineAsync(exc.Message);
                Tools.logError(new Exception($"Error al firmar ruta: {rutaFirma} {exc.Message}"));
                throw;
            }
        }

        public async Task<bool> envioCorreo(string email, byte[] archivo, byte[] xml, string nombreArchivo)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.Attachments.Add((new Attachment(new MemoryStream(archivo), nombreArchivo + ".pdf")));
                correo.Attachments.Add((new Attachment(new MemoryStream(xml), nombreArchivo + ".xml")));
                correo.From = new MailAddress(Tools.config["EnvioCorreo:Email"]);
                correo.To.Add(email);
                correo.Subject = "Mega Aceros - Documento Emitido";
                correo.Body = @"<div style='width:100%;margin-top:25px;'>
                                   <h1>Mega Aceros</h1>
                                   <p>Gracias por su compra adjuntamos el documento electrónico correspondiente.</p>
                                </div>";
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(Tools.config["EnvioCorreo:Email"], Tools.config["EnvioCorreo:Clave"]);

                smtp.Send(correo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> envioXmlSRI(XmlDocument documentoFirmado)
        {
            try
            {
                var xmlByte = Encoding.ASCII.GetBytes(documentoFirmado.InnerXml);
                var response = await new RecepcionComprobantesOfflineClient().validarComprobanteAsync(xmlByte);
                var estado = response.RespuestaRecepcionComprobante.estado;
                return estado == "RECIBIDA";
            }
            catch (Exception exc)
            {
                await Console.Out.WriteLineAsync(exc.Message);
                return false;
            }
        }

        public async Task<estadoSri> verificarEstadoSRI(string claveAcceso)
        {
            try
            {
                await Task.Delay(2000);
                var content = new StringContent(GetAuthorizationSoap(claveAcceso), Encoding.ASCII, "text/xml");
                var peticion = await new HttpClient().PostAsync(Tools.config["SRI:urlEstado"], content);
                peticion.EnsureSuccessStatusCode();
                var response = await peticion.Content.ReadAsStringAsync();
                XmlSerializer serializer = new XmlSerializer(typeof(AuthorizationEnvelope));
                AuthorizationEnvelope respuesta;
                using (StringReader reader = new StringReader(response)) respuesta = (AuthorizationEnvelope)serializer.Deserialize(reader);
                return procesarAutorizacion(respuesta.Body.AutorizacionComprobanteResponse.RespuestaAutorizacionComprobante.Authorizations.Last());
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new estadoSri()
                {
                    idTipoEstadoSri = 6,
                };
            }
        }

        private estadoSri procesarAutorizacion(AuthorizationSri _autorizacion)
        {
            try
            {
                var estado = new estadoSri();
                switch (_autorizacion.Estado)
                {
                    case "EN PROCESAMIENTO":
                        estado.idTipoEstadoSri = 1;
                        estado.fechaAutorizacion = _autorizacion.FechaAutorizacion;
                        break;

                    case "AUTORIZADO":
                        estado.idTipoEstadoSri = 2;
                        estado.fechaAutorizacion = _autorizacion.FechaAutorizacion;
                        break;

                    case "NO AUTORIZADO":
                        estado.idTipoEstadoSri = 3;
                        estado.fechaAutorizacion = null;
                        break;

                    case "RECHAZADO":
                        estado.idTipoEstadoSri = 4;
                        estado.fechaAutorizacion = null;
                        break;

                    default:
                        throw new Exception($"No se ha encontrado el código de tipoEstadoSri {_autorizacion.Estado}");
                }

                return estado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new estadoSri()
                {
                    idTipoEstadoSri = 6,
                };
            }
        }

        public class estadoSri
        {
            public int idTipoEstadoSri { get; set; }
            public DateTime? fechaAutorizacion { get; set; }
        }

        private string GetAuthorizationSoap(string claveAcceso)
        {
            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ec=""http://ec.gob.sri.ws.autorizacion"">
                      <soapenv:Header/>
                       <soapenv:Body>
                         <ec:autorizacionComprobante>
                            <!--Optional:-->
                            <claveAccesoComprobante>{claveAcceso}</claveAccesoComprobante>
                          </ec:autorizacionComprobante>
                        </soapenv:Body>
                      </soapenv:Envelope>";
        }

        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class ReceptionEnvelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }

            [XmlAttribute(AttributeName = "soap")]
            public string Soap { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Body
        {
            [XmlElement(ElementName = "autorizacionComprobanteResponse", Namespace = "http://ec.gob.sri.ws.autorizacion")]
            public AutorizacionComprobanteResponse AutorizacionComprobanteResponse { get; set; }
        }

        [XmlRoot(ElementName = "autorizacionComprobanteResponse", Namespace = "http://ec.gob.sri.ws.autorizacion")]
        public class AutorizacionComprobanteResponse
        {
            [XmlElement(ElementName = "RespuestaAutorizacionComprobante", Namespace = "")]
            public AuthorizationResponse RespuestaAutorizacionComprobante { get; set; }

            [XmlAttribute(AttributeName = "ns2")]
            public string Ns2 { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "RespuestaAutorizacionComprobante")]
        public class AuthorizationResponse
        {
            [XmlElement(ElementName = "claveAccesoConsultada")]
            public string ClaveAccesoConsultada { get; set; }

            [XmlElement(ElementName = "numeroComprobantes")]
            public int NumeroComprobantes { get; set; }

            [XmlArray(ElementName = "autorizaciones")]
            [XmlArrayItem(typeof(AuthorizationSri), ElementName = "autorizacion")]
            public List<AuthorizationSri> Authorizations { get; set; }
        }

        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class AuthorizationEnvelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }

            [XmlAttribute(AttributeName = "soap")]
            public string Soap { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "autorizacion")]
        public class AuthorizationSri
        {
            [XmlElement(ElementName = "estado")]
            public string Estado { get; set; }

            [XmlElement(ElementName = "numeroAutorizacion")]
            public string NumeroAutorizacion { get; set; }

            [XmlElement(ElementName = "fechaAutorizacion")]
            public DateTime FechaAutorizacion { get; set; }

            [XmlElement(ElementName = "ambiente")]
            public string Ambiente { get; set; }

            [XmlElement(ElementName = "comprobante")]
            public string Comprobante { get; set; }

            [XmlElement(ElementName = "mensajes")]
            public object Mensajes { get; set; }
        }
    }
}