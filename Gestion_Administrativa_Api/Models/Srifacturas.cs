using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Srifacturas
{
    public int Idfactura { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public DateOnly? Fechaemision { get; set; }

    public bool? Compra { get; set; }

    public DateTime? Fechaautorizacion { get; set; }

    public int? Codigoestado { get; set; }

    public string? Claveacceso { get; set; }

    public Guid? Idempresa { get; set; }

    public Guid? Idusuario { get; set; }

    public string? Coddoc { get; set; }

    public string? Obligadocontabilidad { get; set; }

    public string? Ambiente { get; set; }

    public string? Dirmatriz { get; set; }

    public string? Estab { get; set; }

    public string? Nombrecomercial { get; set; }

    public string? Ptoemi { get; set; }

    public string? Razonsocial { get; set; }

    public string? Ruc { get; set; }

    public string? Secuencial { get; set; }

    public string? Version { get; set; }

    public string? Contribuyenteespecial { get; set; }

    public string? Direstablecimiento { get; set; }

    public string? Identificacioncomprador { get; set; }

    public string? Razonsocialcomprador { get; set; }

    public string? Tipoidentificacioncomprador { get; set; }

    public string? Moneda { get; set; }

    public decimal? Propina { get; set; }

    public decimal? Totaldescuento { get; set; }

    public decimal? Totalsinimpuesto { get; set; }

    public decimal? Importetotal { get; set; }

    public virtual Sriambientes? AmbienteNavigation { get; set; }

    public virtual Sritiposdocumentos? CoddocNavigation { get; set; }

    public virtual Personas? IdentificacioncompradorNavigation { get; set; }

    public virtual Srimonedas? MonedaNavigation { get; set; }

    public virtual Sritiposidentificaciones? TipoidentificacioncompradorNavigation { get; set; }
}
