using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SecuencialesProformas
{
    public Guid IdSecuencialesProforma { get; set; }

    public long? Nombre { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public Guid? IdEmpresa { get; set; }

    public Guid? IdTipoDocumento { get; set; }

    public virtual Empresas? IdEmpresaNavigation { get; set; }

    public virtual TipoDocumentos? IdTipoDocumentoNavigation { get; set; }
}
