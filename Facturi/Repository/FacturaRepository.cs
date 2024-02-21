using System;
using System.Collections.Generic;
using System.IO;
using Facturi.Domain;

namespace Facturi.Repository
{
    public class FacturaRepository : IRepository<string, Factura>
    {
        private List<Factura> facturi;

        public FacturaRepository(string filePath)
        {
            facturi = new List<Factura>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 3)
                        {
                            string idDocument = parts[0].Trim();
                            DateTime dataScadenta = DateTime.Parse(parts[1].Trim());
                            Categoria categorie = (Categoria)Enum.Parse(typeof(Categoria), parts[2].Trim());
                            
                            Factura factura = new Factura
                            {
                                Id = idDocument,
                                DataScadenta = dataScadenta,
                                Categorie = categorie,
                                Achizitii = new List<Achizitie>(),  
                            };

                            facturi.Add(factura);
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

        public IEnumerable<Factura> FindAll()
        {
            return facturi;
        }

        public Factura FindOne(string idFactura)
        {
            foreach (var f in facturi)
            {
                if (f.Id.Equals(idFactura))
                    return f;
            }

            return null;
        }

    }
    
}