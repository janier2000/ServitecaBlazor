using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Serviteca.Shared.Entities;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            await CheckVehicleTypesAsync();
            await CheckCartTypeAsync();
            await CheckBrandAsync();
            await CheckVehicleAsync();
        }

        private async Task CheckVehicleAsync()
        {
            if (!_context.Vehicles.Any())
            {
                var Brands = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "CHEVROLET");
                var CartType = await _context.CarTypes.FirstOrDefaultAsync(x => x.Name == "AUTOMOVILES");
                var Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ADRAINA");
                var VehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "Particular");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    Brand = Brands,
                    CartType = CartType,
                    CurrentKm = 3000,
                    Customer = Customer,
                    Plate = "JUR540",
                    VehicleType = VehicleType,
                    Model = 2024,
                    ReturnDate = "02-02-2026",

                });

                await _context.SaveChangesAsync();

                Brands = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "FORD");
                CartType = await _context.CarTypes.FirstOrDefaultAsync(x => x.Name == "CAMIONES");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "EDUARDO");
                VehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "Publico");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    Brand = Brands,
                    CartType = CartType,
                    CurrentKm = 4000,
                    Customer = Customer,
                    Plate = "JUR580",
                    VehicleType = VehicleType,
                    Model = 1850,
                    ReturnDate = "02-02-2026",
                });

                await _context.SaveChangesAsync();

                Brands = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "DAIHATSU");
                CartType = await _context.CarTypes.FirstOrDefaultAsync(x => x.Name == "TAXIS");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ALAN");
                VehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "Publico");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    Brand = Brands,
                    CartType = CartType,
                    CurrentKm = 4000,
                    Customer = Customer,
                    Plate = "HTR894",
                    VehicleType = VehicleType,
                    Model = 2000,
                    ReturnDate = "02-02-2026",
                });


                await _context.SaveChangesAsync();

                Brands = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "ALFA  ROMERO");
                CartType = await _context.CarTypes.FirstOrDefaultAsync(x => x.Name == "CAMPEROS");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ALEJANDRA");
                VehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "Particular trabajo");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    Brand = Brands,
                    CartType = CartType,
                    CurrentKm = 5000,
                    Customer = Customer,
                    Plate = "DRE852",
                    VehicleType = VehicleType,
                    Model = 2010,
                    ReturnDate = "02-02-2026",
                });

                await _context.SaveChangesAsync();

                Brands = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "AMPLE");
                CartType = await _context.CarTypes.FirstOrDefaultAsync(x => x.Name == "CAMIONETAS");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "camila");
                VehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "Particular trabajo");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    Brand = Brands,
                    CartType = CartType,
                    CurrentKm = 6000,
                    Customer = Customer,
                    Plate = "TRF456",
                    VehicleType = VehicleType,
                    Model = 2024,
                    ReturnDate = "02-02-2026",
                });

                await _context.SaveChangesAsync();

                Brands = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "AUDI");
                CartType = await _context.CarTypes.FirstOrDefaultAsync(x => x.Name == "CARGA");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "jose");
                VehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "Particular");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    Brand = Brands,
                    CartType = CartType,
                    CurrentKm = 7000,
                    Customer = Customer,
                    Plate = "LKJ652",
                    VehicleType = VehicleType,
                    Model = 2018,
                    ReturnDate = "02-02-2026",
                });

                await _context.SaveChangesAsync();
            }
            //await _context.SaveChangesAsync();
        }
        private async Task CheckBrandAsync()
        {
            if (!_context.Brands.Any())
            {
                _ = _context.Brands.Add(new Brand
                {
                    Name = "CHEVROLET"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "FORD"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "DAIHATSU"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "ALFA  ROMERO"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "AMPLE"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "AUDI"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "AUSTIN"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "BMW"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "KIA"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "BENTLEY"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "BRILLANCE"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "BUGATTI"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "BUICK"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "CADILLAC"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "CHANA"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "CHANGHE"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "CHERY"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "DAEWOO"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "DATSUN"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "DODGE"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "FERRARI"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "MG"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "GLOW"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "GREEN"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "HAFEI"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "HONDA"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "HYUNDAI"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "JAGUAR"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "MITSUBISHI"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "NISSAN"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "RENAULT"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "PEUGEOT"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "SUBARU"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "MAZDA"
                });
                _ = _context.Brands.Add(new Brand
                {
                    Name = "TOYOTA"
                });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckCartTypeAsync()
        {
            if (!_context.CarTypes.Any())
            {
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "AUTOMOVILES"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "CAMIONES"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "TAXIS"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "CAMPEROS"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "CAMIONETAS"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "CARGA"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "BUSES"
                });
                _ = _context.CarTypes.Add(new CartType
                {
                    Name = "VANS"
                });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckVehicleTypesAsync()
        {
            if (!_context.VehicleTypes.Any())
            {
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Particular"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Publico"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Particular trabajo"
                });
            }
            await _context.SaveChangesAsync();
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
