using System;
using System.ComponentModel.DataAnnotations;

namespace AlsCompras.Models.AreaCrm
{
    public class CrmClient
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

    }
}