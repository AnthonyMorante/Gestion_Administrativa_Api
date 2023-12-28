using Dapper;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Serialization;
using static Gestion_Administrativa_Api.Dtos.Interfaz.RetencionDto;

namespace Gestion_Administrativa_Api.Utilities
{
    public static class Tools
    {
        public static IConfiguration? config;
        public static string? rootPath;

        public static void Initialize(IConfiguration Configuration, IWebHostEnvironment _host)
        {
            config = Configuration;
            rootPath = _host.WebRootPath;
        }

        public class userData
        {
            public string? usuario { get; set; }
            public string? clave { get; set; }
            public string? nombre { get; set; }
            public string? email { get; set; }
            public string? idcentro { get; set; }
            public bool? persistent { get; set; }
            public string? sexo { get; set; }
        }

        public class Column
        {
            public string data { get; set; }
            public string name { get; set; }
            public bool? searchable { get; set; }
            public bool? orderable { get; set; }
            public Search search { get; set; }
        }

        public class DataTableModel
        {
            public int draw { get; set; }

            public int start { get; set; }
            public int length { get; set; }
            public List<Column> columns { get; set; }
            public Search search { get; set; }
            public List<Order> order { get; set; }
        }

        public class Order
        {
            public int column { get; set; }
            public string dir { get; set; }
        }

        public class Search
        {
            public string value { get; set; }
            public bool? regex { get; set; }
        }

        public class DataTableResponse
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public IEnumerable<dynamic>? data { get; set; }
        }

        [XmlRoot(ElementName = "factura")]
        public class Factura
        {

            [XmlElement(ElementName = "infoTributaria")]
            public InfoTributaria InfoTributaria { get; set; }
            [XmlElement(ElementName = "infoFactura")]
            public InfoFactura InfoFactura { get; set; }
            [XmlElement(ElementName = "detalles")]
            public Detalles Detalles { get; set; }
            [XmlElement(ElementName = "infoAdicional")]
            public InfoAdicional InfoAdicional { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlAttribute(AttributeName = "version")]
            public string Version { get; set; }
        }
        public class Detalle
        {
            [XmlElement(ElementName = "codigoPrincipal")]
            public string CodigoPrincipal { get; set; }
            [XmlElement(ElementName = "descripcion")]
            public string Descripcion { get; set; }
            [XmlElement(ElementName = "cantidad")]
            public string Cantidad { get; set; }
            [XmlElement(ElementName = "precioUnitario")]
            public string PrecioUnitario { get; set; }
            [XmlElement(ElementName = "descuento")]
            public string Descuento { get; set; }
            [XmlElement(ElementName = "precioTotalSinImpuesto")]
            public string PrecioTotalSinImpuesto { get; set; }
            [XmlElement(ElementName = "impuestos")]
            public Impuestos Impuestos { get; set; }
        }

        [XmlRoot(ElementName = "detalles")]
        public class Detalles
        {
            [XmlElement(ElementName = "detalle")]
            public List<Detalle> Detalle { get; set; }
        }
        [XmlRoot(ElementName = "impuesto")]
        public class Impuesto
        {
            [XmlElement(ElementName = "codigo")]
            public string Codigo { get; set; }
            [XmlElement(ElementName = "codigoPorcentaje")]
            public string CodigoPorcentaje { get; set; }
            [XmlElement(ElementName = "tarifa")]
            public string Tarifa { get; set; }
            [XmlElement(ElementName = "baseImponible")]
            public string BaseImponible { get; set; }
            [XmlElement(ElementName = "valor")]
            public string Valor { get; set; }
        }
        [XmlRoot(ElementName = "impuestos")]
        public class Impuestos
        {
            [XmlElement(ElementName = "impuesto")]
            public List<Impuesto> Impuesto { get; set; }
        }
        [XmlRoot(ElementName = "infoAdicional")]
        public class InfoAdicional
        {
            [XmlElement(ElementName = "campoAdicional")]
            public List<CampoAdicional> CampoAdicional { get; set; }
        }
        [XmlRoot(ElementName = "campoAdicional")]
        public class CampoAdicional
        {
            [XmlAttribute(AttributeName = "nombre")]
            public string Nombre { get; set; }
            [XmlText]
            public string Text { get; set; }
        }
        [XmlRoot(ElementName = "infoFactura")]
        public class InfoFactura
        {
            [XmlElement(ElementName = "fechaEmision")]
            public string FechaEmision { get; set; }
            [XmlElement(ElementName = "dirEstablecimiento")]
            public string DirEstablecimiento { get; set; }
            [XmlElement(ElementName = "contribuyenteEspecial")]
            public string ContribuyenteEspecial { get; set; }
            [XmlElement(ElementName = "obligadoContabilidad")]
            public string ObligadoContabilidad { get; set; }
            [XmlElement(ElementName = "tipoIdentificacionComprador")]
            public string TipoIdentificacionComprador { get; set; }
            [XmlElement(ElementName = "razonSocialComprador")]
            public string RazonSocialComprador { get; set; }
            [XmlElement(ElementName = "identificacionComprador")]
            public string IdentificacionComprador { get; set; }
            [XmlElement(ElementName = "totalSinImpuestos")]
            public string TotalSinImpuestos { get; set; }
            [XmlElement(ElementName = "totalDescuento")]
            public string TotalDescuento { get; set; }
            [XmlElement(ElementName = "totalConImpuestos")]
            public TotalConImpuestos TotalConImpuestos { get; set; }
            [XmlElement(ElementName = "propina")]
            public string Propina { get; set; }
            [XmlElement(ElementName = "importeTotal")]
            public string ImporteTotal { get; set; }
            [XmlElement(ElementName = "moneda")]
            public string Moneda { get; set; }
            [XmlElement(ElementName = "pagos")]
            public Pagos Pagos { get; set; }
        }
        [XmlRoot(ElementName = "infoTributaria")]
        public class InfoTributaria
        {
            [XmlElement(ElementName = "ambiente")]
            public string Ambiente { get; set; }
            [XmlElement(ElementName = "tipoEmision")]
            public string TipoEmision { get; set; }
            [XmlElement(ElementName = "razonSocial")]
            public string RazonSocial { get; set; }
            [XmlElement(ElementName = "nombreComercial")]
            public string NombreComercial { get; set; }
            [XmlElement(ElementName = "ruc")]
            public string Ruc { get; set; }
            [XmlElement(ElementName = "claveAcceso")]
            public string ClaveAcceso { get; set; }
            [XmlElement(ElementName = "codDoc")]
            public string CodDoc { get; set; }
            [XmlElement(ElementName = "estab")]
            public string Estab { get; set; }
            [XmlElement(ElementName = "ptoEmi")]
            public string PtoEmi { get; set; }
            [XmlElement(ElementName = "secuencial")]
            public string Secuencial { get; set; }
            [XmlElement(ElementName = "dirMatriz")]
            public string DirMatriz { get; set; }
        }
        [XmlRoot(ElementName = "pago")]
        public class Pago
        {
            [XmlElement(ElementName = "formaPago")]
            public string FormaPago { get; set; }
            [XmlElement(ElementName = "total")]
            public string Total { get; set; }
            [XmlElement(ElementName = "plazo")]
            public string Plazo { get; set; }
            [XmlElement(ElementName = "unidadTiempo")]
            public string UnidadTiempo { get; set; }
        }
        [XmlRoot(ElementName = "pagos")]
        public class Pagos
        {
            [XmlElement(ElementName = "pago")]
            public List<Pago> Pago { get; set; }
        }
        [XmlRoot(ElementName = "totalConImpuestos")]
        public class TotalConImpuestos
        {
            [XmlElement(ElementName = "totalImpuesto")]
            public List<TotalImpuesto> TotalImpuesto { get; set; }
        }
        [XmlRoot(ElementName = "totalImpuesto")]
        public class TotalImpuesto
        {
            [XmlElement(ElementName = "codigo")]
            public string Codigo { get; set; }
            [XmlElement(ElementName = "codigoPorcentaje")]
            public string CodigoPorcentaje { get; set; }
            [XmlElement(ElementName = "descuentoAdicional")]
            public string DescuentoAdicional { get; set; }
            [XmlElement(ElementName = "baseImponible")]
            public string BaseImponible { get; set; }
            [XmlElement(ElementName = "valor")]
            public string Valor { get; set; }
        }
        public static string getIdEmpresa(HttpContext _httpContext)
        {
            try
            {
                return getClaim(_httpContext, "idEmpresa");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public static string getIdUsuario(HttpContext _httpContext)
        {
            try
            {
                return getClaim(_httpContext, "idUsuario");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public static string getClaim(HttpContext _httpContext, string claim)
        {
            try
            {
                var identity = (ClaimsIdentity)_httpContext.User.Identity;
                return identity?.FindFirst(claim)?.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public static bool validateToken(string? token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) return false;
                token = token.Replace("Bearer ", "").TrimStart().TrimEnd();
                var validador = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var validToken = validador.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:key"]))
                }, out validatedToken);
                return DateTime.Now < validatedToken.ValidTo.AddHours(-5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static string validateTokenApi(string? token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) return "empty";
                token = token.Replace("Bearer ", "").TrimStart().TrimEnd();
                var validador = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var validToken = validador.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:key"]))
                }, out validatedToken);
                return (DateTime.Now < validatedToken.ValidTo.AddHours(-5)) ? "" : "expired";
            }
            catch (Exception ex)
            {
                handleError(ex);
                return (ex.Message.Contains("Lifetime")) ? "expired" : "El Token proporcionado no es válido";
            }
        }

        public static string getTokenValue(string _claim, string? _token)
        {
            try
            {
                if (string.IsNullOrEmpty(_token)) return "";
                _token = _token.Replace("Bearer ", "").TrimStart().TrimEnd();
                var validador = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var validToken = validador.ValidateToken(_token, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:key"]))
                }, out validatedToken);
                var identities = validToken.Identities.FirstOrDefault();
                _claim = _claim.ToLower();
                if (_claim == "user") _claim = "usuario";
                if (_claim == "mail") _claim = "correo";
                if (_claim == "email") _claim = "correo";
                if (_claim == "name") _claim = "nombre";
                return identities?.FindFirst(_claim)?.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public static string generateToken(userData _user)
        {
            try
            {
                var claims = new[]{
                new Claim("nombre", string.IsNullOrEmpty(_user.nombre)?"":_user.nombre),
                new Claim("correo", string.IsNullOrEmpty(_user.email)?"":_user.email),
                new Claim("sexo", string.IsNullOrEmpty(_user.sexo)?"":_user.sexo),
                new Claim("usuario",_user.usuario),
                new Claim("idcentro",string.IsNullOrEmpty(_user.idcentro)?"":_user.idcentro)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var durationHours = _user.persistent == true ?
                    Convert.ToDouble(config["JWT:times:persistent"]?.Replace(",", ".")) :
                    Convert.ToDouble(config["JWT:times:default"]?.Replace(",", "."));
                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(durationHours),
                    signingCredentials: credentials
                    );
                var expira = DateTime.Now.AddHours(durationHours);
                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public static IActionResult handleError(Exception error)
        {
            var result = new ObjectResult("");
            try
            {
                result.StatusCode = 500;
                if (error.Source == "modelError") result.StatusCode = 422;
                if (error.Source.ToLower().Contains("data")) result.StatusCode = 424;
                result.Value = error.Message;
                logError(error);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.StatusCode = 500;
                logError(ex);
                return result;
            }
        }

        private static void logError(Exception _error)
        {
            var dapper = new SqlConnection(config.GetConnectionString("angular_test"));
            try
            {
                var path = $@"{rootPath}/_errors.txt";
                var error = new
                {
                    error = $"Error: {_error.Message}  - Source: {_error.StackTrace}",
                    fechaRegistro = DateTime.Now
                };
                string sql = @"INSERT INTO ErrorLogs (fechaRegistro, error) VALUES(@fechaRegistro, @error);";
                dapper.Execute(sql, error);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Exception handleModelError(ModelStateDictionary modelState)
        {
            var _ex = new Exception();
            try
            {
                var errores = modelState?.Where(x => x.Value.ValidationState == ModelValidationState.Invalid);
                var elementos = new List<string>();
                var mensaje = "";
                foreach (var item in errores)
                {
                    mensaje += mensaje == "" ? $"- {item.Value?.Errors?.FirstOrDefault()?.ErrorMessage}" : $"<br>- {item.Value?.Errors?.FirstOrDefault()?.ErrorMessage}";
                    elementos.Add(item.Key);
                }

                _ex = new Exception(JsonConvert.SerializeObject(new { mensaje, elementos }));
                _ex.Source = "modelError";
                return _ex;
            }
            catch (Exception ex)
            {
                ex.Source = "modelError";
                return _ex = new Exception(JsonConvert.SerializeObject(new { mensaje = ex.Message, elementos = new List<string>() }));
            }
        }

        public class DataTableParams
        {
            public dynamic? parameters { get; set; }
            public string query { get; set; }
            public IDbConnection dapperConnection { get; set; }
            public DataTableModel? dataTableModel { get; set; }
        }

        public static async Task<DataTableResponse> DataTableSql(DataTableParams _dataParams)
        {
            var _params = _dataParams.dataTableModel;
            var _dapper = _dataParams.dapperConnection;
            var busqueda = _params.search.value != null ? _params.search.value : "";
            var filtro = "";
            //string sql = $@"SELECT TOP 1 * FROM({_dataParams.query}) as t_t_jclc_f";
            //var _fila = _dapper.QueryFirstOrDefault(sql);
            IEnumerable<string> queryParams = _dataParams.dataTableModel.columns.Select(x => x.data).ToList();
            //if (_fila != null)
            //{
            //    IDictionary<string, object> columnas = (IDictionary<string, object>)_fila;
            //    queryParams= columnas.Keys;
            //}

            var orderBy = queryParams.FirstOrDefault();
            var orderDirection = "";
            if (_params.order.Count > 0)
            {
                orderBy = _params.columns[_params.order[0].column].data;
                orderDirection = _params.order[0].dir;
            }
            if (!string.IsNullOrEmpty(busqueda.Trim()))
            {
                busqueda = $"%{busqueda}%";
                foreach (var item in queryParams)
                {
                    filtro += filtro == "" ? $" WHERE cast({item} as varchar) COLLATE Latin1_general_CI_AI LIKE @busqueda COLLATE Latin1_general_CI_AI" : $" OR cast({item} as varchar) COLLATE Latin1_general_CI_AI LIKE @busqueda COLLATE Latin1_general_CI_AI ";
                }
            }
            var parameters = new DynamicParameters();
            parameters.Add("busqueda", busqueda);
            if (_dataParams.parameters != null)
            {
                var properties = _dataParams.parameters.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var key = property.Name;
                    var value = _dataParams.parameters.GetType().GetProperty(property.Name).GetValue(_dataParams.parameters, null);
                    parameters.Add(key, value);
                }
            }
            string sql = $@"
                            SELECT TOP {_params.length}  * FROM (
                            SELECT ROW_NUMBER() over(order by {orderBy} {orderDirection}) as row,* from(
                            {_dataParams.query}
                            ) as t_t_t_jclc {filtro}
                            ) as t_t_t_jclc_tf
                            WHERE row >{_params.start}
                            ";

            var lista = await _dapper.QueryAsync(sql, parameters);
            //Total Global
            sql = $@"SELECT COUNT(*)
                         FROM ({_dataParams.query}) t_t_t_jclc";
            var recordsTotal = await _dapper.ExecuteScalarAsync<int>(sql, parameters);
            //Total Filtrado
            sql = $@" SELECT COUNT(*) FROM (
                            SELECT ROW_NUMBER() over(order by {orderBy} {orderDirection}) as row,* from(
                            {_dataParams.query}
                            ) as t_t_t_jclc {filtro}
                            ) as t_t_t_jclc_tf
                        ";
            var recordsFiltered = await _dapper.ExecuteScalarAsync<int>(sql, parameters);
            //Modelo DataTable Server Side
            return new DataTableResponse
            {
                draw = Convert.ToInt32(_params.draw),
                recordsTotal = recordsTotal,
                data = lista,
                recordsFiltered = recordsFiltered,
            };
        }

        public static async Task<DataTableResponse> DataTablePostgresSql(DataTableParams _dataParams)
        {
            var _params = _dataParams.dataTableModel;
            var _dapper = _dataParams.dapperConnection;
            var busqueda = _params.search.value != null ? _params.search.value : "";
            var filtro = "";
            //string sql = $@"SELECT TOP 1 * FROM({_dataParams.query}) as t_t_jclc_f";
            //var _fila = _dapper.QueryFirstOrDefault(sql);
            IEnumerable<string> queryParams = _dataParams.dataTableModel.columns.Select(x => x.data).ToList();
            //if (_fila != null)
            //{
            //    IDictionary<string, object> columnas = (IDictionary<string, object>)_fila;
            //    queryParams= columnas.Keys;
            //}

            var orderBy = queryParams.FirstOrDefault();
            var orderDirection = "";
            if (_params.order.Count > 0)
            {
                orderBy = _params.columns[_params.order[0].column].data;
                orderDirection = _params.order[0].dir;
            }
            if (!string.IsNullOrEmpty(busqueda.Trim()))
            {
                busqueda = $"%{busqueda}%";
                foreach (var item in queryParams)
                {
                    filtro += filtro == "" ? $@" WHERE UPPER(REPLACE(regexp_replace(CAST(""{item}"" AS varchar),'\t|\n|\r|\s',''),' ','')) LIKE UPPER(REPLACE(regexp_replace(CAST(@busqueda AS varchar),'\t|\n|\r|\s',''),' ','')) " : $@" OR UPPER(REPLACE(regexp_replace(CAST(""{item}"" AS varchar),'\t|\n|\r|\s',''),' ','')) LIKE UPPER(REPLACE(regexp_replace(CAST(@busqueda AS varchar),'\t|\n|\r|\s',''),' ','')) ";
                }
            }
            var parameters = new DynamicParameters();
            parameters.Add("busqueda", busqueda);
            if (_dataParams.parameters != null)
            {
                var properties = _dataParams.parameters.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var key = property.Name;
                    var value = _dataParams.parameters.GetType().GetProperty(property.Name).GetValue(_dataParams.parameters, null);
                    parameters.Add(key, value);
                }
            }
            string sql = $@"
                            SELECT * FROM (
                            SELECT ROW_NUMBER() over(order by UPPER(REPLACE(regexp_replace(CAST(""{orderBy}"" AS varchar),'\t|\n|\r|\s',''),' ','')) {orderDirection}) as row,* from(
                            {_dataParams.query}
                            ) as t_t_t_jclc {filtro}
                            ) as t_t_t_jclc_tf
                            WHERE row >{_params.start}
                            LIMIT {_params.length}
                            ";
            var lista = await _dapper.QueryAsync(sql, parameters);
            //Total Global
            sql = $@"SELECT COUNT(*)
                         FROM ({_dataParams.query}) t_t_t_jclc";
            var recordsTotal = await _dapper.ExecuteScalarAsync<int>(sql, parameters);
            //Total Filtrado
            sql = $@" SELECT COUNT(*) FROM (
                            SELECT ROW_NUMBER() over(order by UPPER(REPLACE(regexp_replace(CAST(""{orderBy}"" AS varchar),'\t|\n|\r|\s',''),' ','')) {orderDirection}) as row,* from(
                            {_dataParams.query}
                            ) as t_t_t_jclc {filtro}
                            ) as t_t_t_jclc_tf
                        ";
            var recordsFiltered = await _dapper.ExecuteScalarAsync<int>(sql, parameters);
            //Modelo DataTable Server Side
            return new DataTableResponse
            {
                draw = Convert.ToInt32(_params.draw),
                recordsTotal = recordsTotal,
                data = lista,
                recordsFiltered = recordsFiltered,
            };
        }

        public static Guid toGuid(Guid? guid)
        {
            return guid ?? Guid.Empty;
        }

        public static Factura XmlToFacturaModel(Stream fileStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tools.Factura));
            return (Tools.Factura)serializer.Deserialize(fileStream);
        }

        public static SriFacturas XmlToFacturaDbModel(Stream fileStream,bool? compra)
        {
            return ToFacturaDbModel(fileStream,compra);
        }

        public static SriFacturas ToFacturaDbModel(Stream fileStream, bool? compra)
        {
            try
            {
                var _factura = XmlToFacturaModel(fileStream);
                var _f=new SriFacturas();
                _f.Compra= compra;
                _f.Id=_factura.Id;
                _f.Secuencial = _factura.InfoTributaria.Secuencial;
                _f.FechaEmision = DateOnly.Parse(_factura.InfoFactura.FechaEmision);
                _f.ClaveAcceso = _factura.InfoTributaria.ClaveAcceso;
                _f.CodDoc = _factura.InfoTributaria.CodDoc;
                _f.ObligadoContabilidad = _factura.InfoFactura.ObligadoContabilidad;
                _f.Ambiente = _factura.InfoTributaria.Ambiente;
                _f.DirMatriz = _factura.InfoTributaria.DirMatriz;
                _f.Estab=_factura.InfoTributaria.Estab;
                _f.NombreComercial = _factura.InfoTributaria.NombreComercial;
                _f.PtoEmi = _factura.InfoTributaria.PtoEmi;
                _f.RazonSocial = _factura.InfoTributaria.RazonSocial;
                _f.Ruc=_factura.InfoTributaria.Ruc;
                _f.Version = _factura.Version;
                _f.ContribuyenteEspecial = _factura.InfoFactura.ContribuyenteEspecial;
                _f.DirEstablecimiento = _factura.InfoFactura.DirEstablecimiento;
                _f.IdentificacionComprador = _factura.InfoFactura.IdentificacionComprador;
                _f.RazonSocialComprador = _factura.InfoFactura.RazonSocialComprador;
                _f.TipoIdentificacionComprador = _factura.InfoFactura.TipoIdentificacionComprador;
                _f.Moneda=_factura.InfoFactura.Moneda;
                _f.Propina = Convert.ToDecimal(_factura.InfoFactura.Propina?.Replace(".", ","));
                _f.TotalDescuento = Convert.ToDecimal(_factura.InfoFactura.TotalDescuento?.Replace(".", ","));
                _f.TotalSinImpuesto = Convert.ToDecimal(_factura.InfoFactura.TotalSinImpuestos?.Replace(".", ","));
                _f.ImporteTotal = Convert.ToDecimal(_factura.InfoFactura.ImporteTotal?.Replace(".", ","));
                _f.SriDetallesFacturas = (from item in _factura.Detalles.Detalle
                                          select new SriDetallesFacturas()
                                          {
                                              Cantidad = Convert.ToDecimal(item.Cantidad?.Replace(".", ",")),
                                              CodigoPrincipal = item.CodigoPrincipal,
                                              Descripcion = item.Descripcion,
                                              Descuento = Convert.ToDecimal(item.Descuento?.Replace(".", ",")),
                                              PrecioTotalSinImpuesto = Convert.ToDecimal(item.PrecioTotalSinImpuesto?.Replace(".", ",")),
                                              PrecioUnitario = Convert.ToDecimal(item.PrecioUnitario?.Replace(".", ",")),
                                              PrecioTotalConImpuesto = item.Impuestos.Impuesto.Aggregate(Convert.ToDecimal("0"),(acc,x)=>acc+Convert.ToDecimal(x.Valor?.Replace(".", ","))),
                                              SriDetallesFacturasImpuestos = (from impuesto in item.Impuestos.Impuesto
                                                                              select new SriDetallesFacturasImpuestos()
                                                                              {
                                                                                  Codigo=impuesto.Codigo,
                                                                                  CodigoPorcentaje=impuesto.CodigoPorcentaje,
                                                                                  BaseImponible=Convert.ToDecimal(impuesto.BaseImponible?.Replace(".", ",")),
                                                                                  Tarifa=Convert.ToDecimal(impuesto.Tarifa?.Replace(".", ",")),
                                                                                  Valor=Convert.ToDecimal(impuesto.Valor?.Replace(".", ","))
                                                                              }
                                                                            ).ToList(),

                                          }).ToList();
                foreach (var total in _factura.InfoFactura.TotalConImpuestos.TotalImpuesto)
                {
                    var detalleTotal = new SriTotalesConImpuestos();
                    detalleTotal.Codigo = total.Codigo;
                    detalleTotal.CodigoPorcentaje = total.CodigoPorcentaje;
                    detalleTotal.DescuentoAdicional = Convert.ToDecimal(total.DescuentoAdicional?.Replace(".", ","));
                    detalleTotal.BaseImponible = Convert.ToDecimal(total.BaseImponible?.Replace(".", ","));
                    detalleTotal.Valor = Convert.ToDecimal(total.Valor?.Replace(".", ","));
                    _f.SriTotalesConImpuestos.Add(detalleTotal);
                }
                _f.SriPagos = (from item in _factura.InfoFactura.Pagos.Pago
                               select new SriPagos
                               {
                                   Plazo=Convert.ToInt32(item.Plazo),
                                   FormaPago=item.FormaPago,
                                   Total=Convert.ToDecimal(item.Total?.Replace(".", ",")),
                                   UnidadTiempo=item.UnidadTiempo
                               }).ToList();
                _f.SriCamposAdicionales = (from item in _factura.InfoAdicional.CampoAdicional
                                           select new SriCamposAdicionales
                                           {
                                               Nombre=item.Nombre,
                                               Text=item.Text
                                           }).ToList();
                return _f;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SriFacturas();
            }
        }

        public static Stream IFormFileToSteam(IFormFile _file)
        {
            var responseStream = _file.OpenReadStream();
            return responseStream;
        }
    }
}