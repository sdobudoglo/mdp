using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mdp.Models
{
    // Модели, возвращенные действиями MeController.
    public class GetViewModel
    {
        public string Address { get; set; }
    }
}