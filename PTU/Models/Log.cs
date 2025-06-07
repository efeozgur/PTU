namespace PTU.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int? PersonelId { get; set; } 
        public string Islem { get; set; }    
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; } 
    }
}
