using System;
using System.Collections.Generic;
using System.IO;
using Facturi.Domain;

namespace Facturi.Repository
{
    public class AchizitieRepository : IRepository<string, Achizitie>
    {
        private List<Achizitie> achizitii;

        public AchizitieRepository(string filePath)
        {
            achizitii = new List<Achizitie>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 5)
                        {
                            string idAchizitie = parts[0].Trim();
                            string produs = parts[1].Trim();
                            int cantitate = int.Parse(parts[2].Trim());
                            double pretProdus = double.Parse(parts[3].Trim());
                            string idDocument = parts[4].Trim();

                            Achizitie achizitie = new Achizitie
                            {
                                Id = idAchizitie,
                                Produs = produs,
                                Cantitate = cantitate,
                                PretProdus = pretProdus,
                                IdDocument = idDocument
                            };

                            achizitii.Add(achizitie);
                        }
                        else
                        {
                            Console.WriteLine($"Linie invalida in fisierul {filePath}: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citirea din fisierul {filePath}: {ex.Message}");
            }
        }

        public IEnumerable<Achizitie> FindAll()
        {
            return achizitii;
        }
        
        public Achizitie FindOne(string idAchizitie)
        {
            foreach (var a in achizitii)
            {
                if (a.Id.Equals(idAchizitie))
                    return a;
            }

            return null;
        }
    }
}