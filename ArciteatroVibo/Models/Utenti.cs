using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArciteatroVibo.Models;

public partial class Utenti
{
    public int IdUtente { get; set; }

    public bool? Newsletter { get; set; }
    [Required(ErrorMessage = "Il Nome è obbligatorio.")]
    public string Nome { get; set; } = null!;
    [Required(ErrorMessage = "Il Cognome è obbligatorio.")]
    public string Cognome { get; set; } = null!;
    [Required(ErrorMessage = "la Email è obbligatoria.")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "la Password è obbligatoria.")]
    public string Password { get; set; } = null!;

    public bool Ruolo { get; set; }

    public virtual ICollection<Richieste> Richiestes { get; set; } = new List<Richieste>();

    public static implicit operator Utenti(int v)
    {
        throw new NotImplementedException();
    }
}
