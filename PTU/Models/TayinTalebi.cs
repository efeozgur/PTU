using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PTU.Models
{
    public class TayinTalebi
    {
        public int Id { get; set; }
        [Required]
        public int PersonelId { get; set; }

        [BindNever]
        public virtual Personel Personel { get; set; }

        [Required]
        public string TalepTuru { get; set; }
        [Required]
        public string TercihAdliye { get; set; }
        [StringLength(500)]
        public string Aciklama { get; set; }
        [Required]
        public DateTime BasvuruTarihi { get; set; }
        public string TalepDurumu { get; set; } = "Bekliyor";
    }

}