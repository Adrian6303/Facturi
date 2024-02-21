namespace Facturi.Domain
{
    public class Achizitie : Entity<string>
    {
        public string Produs { get; set; }
        public int Cantitate { get; set; }
        public double PretProdus { get; set; }
        public string IdDocument { get; set; }
        
        
    }
}