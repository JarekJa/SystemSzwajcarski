using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models.Games
{
    public enum TypeResult
    {
        [Display(Name = "Brak wyniku")]
        none,
        [Display(Name = "Wygrana białych")]
        white,
        [Display(Name = "Wygrana czarnyc")]
        black,
        [Display(Name = "Remis")]
        draw
    }
}
