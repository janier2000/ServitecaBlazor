using Microsoft.EntityFrameworkCore;
using Serviteca.Shared.Entities;

namespace Serviteca.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
       
        public SeedDb(DataContext context)
        {
            _context = context;
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDocumentTypeAsync();

            await CheckCustomersAsync();
        }


        

        private async Task CheckDocumentTypeAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _ = _context.DocumentTypes.Add(new DocumentType
                {
                    Name =  "Cedula Ciudadania"
                });
                _ = _context.DocumentTypes.Add(new DocumentType
                {
                    Name = "Targeta identidad"
                });
                _ = _context.DocumentTypes.Add(new DocumentType
                {
                    Name = "Cedula Extranjeria"
                });
            }
            await _context.SaveChangesAsync();
        }


        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {

                var documentTypeCC = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Name == "Cedula Ciudadania");

                var documentTypeTI = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Name == "Targeta identidad");


                _ = _context.Customers.Add(new Customer
                {
                    Document = "73009144",
                    ClientSince = "2-may-2026",
                    DocumentType = documentTypeTI,
                    Email = "ADRAINA@hotmail.com",
                    FirstName = "ADRAINA",
                    LastName = "MUÑOZ",
                    gender = 2,
                    phone = "316270185",
                  

                });
                _context.Customers.Add(new Customer
                {
                    Document = "4545454",
                    ClientSince = "2-junio-2026",
                    DocumentType = documentTypeTI! ,
                    Email = "EDUARDO@hotmail.com",
                    FirstName = "EDUARDO",
                    LastName = "AGUSTIN",
                    gender = 1,
                    phone = "3215614589",
                    
                });
                _context.Customers.Add(new Customer
                {
                    Document = "73896523",
                    ClientSince = "1-junio-2026",
                    DocumentType = documentTypeCC,
                    Email = "ALAN@hotmail.com",
                    FirstName = "ALAN",
                    LastName = "ORTIZ",
                    gender = 1,
                    phone = "3605245253",
                    
                });
                _context.Customers.Add(new Customer
                {
                    Document = "75008466",
                    ClientSince = "8-junio-2026",
                    DocumentType = documentTypeCC,
                    Email = "ALEJANDRA@hotmail.com",
                    FirstName = "ALEJANDRA",
                    LastName = "MENDOZA",
                    gender = 2,
                    phone = "3152476421",
                    
                });
                _context.Customers.Add(new Customer
                {
                    Document = "78332466",
                    ClientSince = "10-junio-2026",
                    DocumentType = documentTypeCC,
                    Email = "camila@hotmail.com",
                    FirstName = "camila",
                    LastName = "buitrago",
                    gender = 2,
                    phone = "326579854",
                });
                _context.Customers.Add(new Customer
                {
                    Document = "85662744",
                    ClientSince = "13-junio-2026",
                    DocumentType = documentTypeCC,
                    Email = "jose@hotmail.com",
                    FirstName = "jose",
                    LastName = " rodrigues martines",
                    gender = 1 ,
                    phone = "3162705461",
                    
                });
                _context.Customers.Add(new Customer
                {
                    Document = "98444599",
                    ClientSince = "20-junio-2026",
                    DocumentType = documentTypeCC,
                    Email = "milena@hotmail.com",
                    FirstName = "milena",
                    LastName = "rojas",
                    gender = 2,
                    phone = "316277091",
                   
                });
                _context.Customers.Add(new Customer
                {
                    Document = "89664188",
                    ClientSince = "24-junio-2026",
                    DocumentType = documentTypeTI,
                    Email = "gonzales@hotmail.com",
                    FirstName = "elian jose",
                    LastName = " gonzales martinez",
                    gender = 1,
                    phone = "316707131",
                   
                });
                _context.Customers.Add(new Customer
                {
                    Document = "45338155",
                    ClientSince = "30-junio-2026",
                    DocumentType = documentTypeTI,
                    Email = "josefinapatricio@hotmail.com",
                    FirstName = "josefina patricio",
                    LastName = "veltran aurisio",
                    gender = 2 ,
                    phone = "3124587225",
                    
                });
            }
            await _context.SaveChangesAsync();
        }

    }
}
