using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Richieste
{
    public int IdRichiesta { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string? CorpoRichiesta { get; set; }

    public int FkUtente { get; set; }

    public int FkLaboratorio { get; set; }

    public virtual Laboratorio FkLaboratorioNavigation { get; set; } = null!;

    public virtual Utenti FkUtenteNavigation { get; set; } = null!;
}
