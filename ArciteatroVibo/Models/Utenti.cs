using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Utenti
{
    public int IdUtente { get; set; }

    public bool? Newsletter { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Ruolo { get; set; }

    public virtual ICollection<Richieste> Richiestes { get; set; } = new List<Richieste>();
}
