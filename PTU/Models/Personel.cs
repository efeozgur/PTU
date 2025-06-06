using System.ComponentModel.DataAnnotations;

namespace PTU.Models
{
    public class Personel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string SicilNo { get; set; }

        [Required, StringLength(100)]
        public string AdSoyad { get; set; }

        [Required, StringLength(50)]
        public string Unvan { get; set; }

        [Required, StringLength(100)]
        public string Sifre { get; set; } // Hash'lenmiş şekilde sakla!

        // İsteğe bağlı: Email, Telefon vs. ekleyebilirsin
        public bool IsAdmin { get; set; }   
        public virtual ICollection<TayinTalebi> TayinTalepleri { get; set; }
    }

}
