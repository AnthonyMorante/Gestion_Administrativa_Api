﻿using FirmaXadesNetCore;
using FirmaXadesNetCore.Crypto;
using FirmaXadesNetCore.Signature;
using FirmaXadesNetCore.Signature.Parameters;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Gestion_Administrativa_Api.Interfaces.Utilidades
{
    public interface IUtilidades
    {
        Task<string> modulo11(string claveAcceso);

        Task<string> claveAcceso(Facturas? _factura);

        Task<SignatureDocument> firmar(string codigo, string rutaFirma, XDocument documento);

        Task<object?> envioXmlSRI(XmlDocument? documentoFirmado);

        Task<bool> envioCorreo(string email, byte[] archivo, string nombreArchivo);
    }

    public class UtilidadesI : IUtilidades
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UtilidadesI(HttpClient httpClient, IConfiguration configuratio)
        {
            _httpClient = httpClient;
            _configuration = configuratio;
        }

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
                int ambiente = Convert.ToInt16(_configuration["SRI:ambiente"]);
                string establecimiento = _factura.Establecimiento.ToString().PadLeft(3, '0');
                string puntoEmision = _factura.Establecimiento.ToString().PadLeft(3, '0');
                string secuencial = _factura.Secuencial.ToString().PadLeft(9, '0');
                int numeroRandom = this.numerosRandom();
                int tipoEmision = Convert.ToInt16(_configuration["SRI:tipoEmision"]);
                string claveAcceso = $"{fechaFormateada}{tipoDocumento}{ruc}{ambiente}{establecimiento}{puntoEmision}{secuencial}{numeroRandom}{tipoEmision}";
                string claveAccesoVerificador = await modulo11(claveAcceso);
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
                var cert = new X509Certificate2(rutaFirma, codigo);
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
                return null;
            }
        }

        public async Task<bool> envioCorreo(string email, byte[] archivo, string nombreArchivo)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.Attachments.Add((new Attachment(new MemoryStream(archivo), nombreArchivo + ".pdf")));
                correo.From = new MailAddress(_configuration["EnvioCorreo:Email"]);
                correo.To.Add(email);
                correo.Subject = "Mega Aceros - Documento Emitido";
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(_configuration["EnvioCorreo:Email"], _configuration["EnvioCorreo:Clave"]);

                smtp.Send(correo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<object?> envioXmlSRI(XmlDocument documentoFirmado)
        {
            string linea = documentoFirmado.InnerXml;
            var xmlByte = Encoding.UTF8.GetBytes(linea);
            try
            {
                string arraybyte = Convert.ToBase64String(xmlByte);
                string xml = @$"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ec='http://ec.gob.sri.ws.recepcion'>
                                 <soapenv:Header/>
                                 <soapenv:Body>
                                 <ec:validarComprobante>
                                 <xml>{arraybyte}</xml>
                                 </ec:validarComprobante>
                                 </soapenv:Body>
                                 </soapenv:Envelope>
                              ";

                var content = new StringContent(xml, Encoding.ASCII, "text/xml");
                var peticion = await _httpClient.PostAsync(_configuration["SRI:urlEnvioComprobantes"], content);
                //var peticionTask = _httpClient.PostAsync(_configuration["SRI:urlEnvioComprobantes"], content);
                peticion.EnsureSuccessStatusCode();
                var consulta = await peticion.Content.ReadAsStringAsync();
                //consulta += $"<ns2:validarComprobanteClaveAcceso xmlns:ns2=\"http://ec.gob.sri.ws.recepcion\">{claveAcceso}</ns2:validarComprobanteClaveAcceso>";
                //consulta += $"<ns2:validarComprobanteDocumento xmlns:ns2=\"http://ec.gob.sri.ws.recepcion\">{documentoProcesado}</ns2:validarComprobanteDocumento>";
                return consulta.ToString();
            }
            catch (Exception exc)
            {
                return null;
            }
        }
    }
}