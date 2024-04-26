using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArciteatroVibo.Models;

public partial class Richieste
{
    public int IdRichiesta { get; set; }

    [Required(ErrorMessage = "Il Nome è obbligatorio.")]
    public string Nome { get; set; } = null!;
    [Required(ErrorMessage = "Il Cognome è obbligatorio.")]
    public string Cognome { get; set; } = null!;

    [Display(Name = "Raccontaci di più")]
    public string? CorpoRichiesta { get; set; }

    public int FkUtente { get; set; }

    public int FkLaboratorio { get; set; }

    [Required(ErrorMessage = "Il Cognome è obbligatorio.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Data di Nascita")]
    public DateOnly? DataNascita { get; set; }

    public virtual Laboratorio FkLaboratorioNavigation { get; set; } = null!;

    public virtual Utenti FkUtenteNavigation { get; set; } = null!;
}
