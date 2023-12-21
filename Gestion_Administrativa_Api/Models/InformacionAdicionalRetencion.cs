using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class InformacionAdicionalRetencion
{
    public Guid IdInformacionAdicionalRetencion { get; set; }

    public string? Nombre { get; set; }

    public string? Valor { get; set; }

    public Guid IdRetencion { get; set; }

    public virtual Retenciones IdRetencionNavigation { get; set; } = null!;
}
