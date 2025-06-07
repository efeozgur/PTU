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
        public string Sifre { get; set; } 

     
        public bool IsAdmin { get; set; }   
        public virtual ICollection<TayinTalebi> TayinTalepleri { get; set; }
    }

}
