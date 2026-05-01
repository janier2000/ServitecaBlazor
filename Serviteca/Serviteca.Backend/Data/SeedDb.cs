using Microsoft.EntityFrameworkCore;
using Serviteca.Shared.Entities;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
            await CheckVehicleUseAsync();
            await CheckVehicleBrandsAsync();
            await CheckVehicleAsync();
        }

        private async Task CheckDocumentTypeAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _ = _context.DocumentTypes.Add(new DocumentType
                {
                    Name = "Cedula Ciudadania"
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
                    DocumentType = documentTypeTI!,
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
                    gender = 1,
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
                    gender = 2,
                    phone = "3124587225",

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
                    Name = "AUTOMOVILES"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "CAMIONES"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "TAXIS"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "CAMPEROS"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "CAMIONETAS"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "CARGA"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "BUSES"
                });
                _ = _context.VehicleTypes.Add(new VehicleType
                {
                    Name = "VANS"
                });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckVehicleUseAsync()
        {
            if (!_context.VehicleUses.Any())
            {
                _ = _context.VehicleUses.Add(new VehicleUse
                {
                    Name = "Particular"
                });
                _ = _context.VehicleUses.Add(new VehicleUse
                {
                    Name = "Publico"
                });
                _ = _context.VehicleUses.Add(new VehicleUse
                {
                    Name = "Particular trabajo"
                });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckVehicleBrandsAsync()
        {
            if (!_context.VehicleBrands.Any())
            {
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "CHEVROLET"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "FORD"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "DAIHATSU"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "ALFA  ROMERO"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "AMPLE"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "AUDI"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "AUSTIN"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "BMW"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "KIA"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "BENTLEY"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "BRILLANCE"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "BUGATTI"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "BUICK"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "CADILLAC"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "CHANA"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "CHANGHE"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "CHERY"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "DAEWOO"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "DATSUN"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "DODGE"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "FERRARI"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "MG"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "GLOW"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "GREEN"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "HAFEI"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "HONDA"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "HYUNDAI"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "JAGUAR"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "MITSUBISHI"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "NISSAN"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "RENAULT"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "PEUGEOT"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "SUBARU"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "MAZDA"
                });
                _ = _context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "TOYOTA"
                });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckVehicleAsync()
        {
            if (!_context.Vehicles.Any())
            {
                var Brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Name == "CHEVROLET");
                var Type = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "AUTOMOVILES");
                var Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ADRAINA");
                var Use = await _context.VehicleUses.FirstOrDefaultAsync(x => x.Name == "Particular");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    VehicleBrand = Brand,
                    VehicleUse = Use,
                    CurrentKm = 3000,
                    Customer = Customer,
                    Plate = "JUR540",
                    VehicleType = Type,
                    Model = 2024,
                    ReturnDate = "02-02-2026",

                });

                Brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Name == "FORD");
                Type = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "CAMIONES");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "EDUARDO");
                Use = await _context.VehicleUses.FirstOrDefaultAsync(x => x.Name == "Publico");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    VehicleBrand = Brand,
                    VehicleUse = Use,
                    CurrentKm = 4000,
                    Customer = Customer,
                    Plate = "JUR580",
                    VehicleType = Type,
                    Model = 1850,
                    ReturnDate = "02-02-2026",
                });

                Brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Name == "DAIHATSU");
                Type = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "TAXIS");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ALAN");
                Use = await _context.VehicleUses.FirstOrDefaultAsync(x => x.Name == "Publico");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    VehicleBrand = Brand,
                    VehicleUse = Use,
                    CurrentKm = 4000,
                    Customer = Customer,
                    Plate = "HTR894",
                    VehicleType = Type,
                    Model = 2000,
                    ReturnDate = "02-02-2026",
                });

                Brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Name == "ALFA  ROMERO");
                Type = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "CAMPEROS");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ALEJANDRA");
                Use = await _context.VehicleUses.FirstOrDefaultAsync(x => x.Name == "Particular trabajo");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    VehicleBrand = Brand,
                    VehicleUse = Use,
                    CurrentKm = 5000,
                    Customer = Customer,
                    Plate = "DRE852",
                    VehicleType = Type,
                    Model = 2010,
                    ReturnDate = "02-02-2026",
                });

                Brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Name == "AMPLE");
                Type = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "CAMIONETAS");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "camila");
                Use = await _context.VehicleUses.FirstOrDefaultAsync(x => x.Name == "Particular trabajo");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    VehicleBrand = Brand,
                    VehicleUse = Use,
                    CurrentKm = 6000,
                    Customer = Customer,
                    Plate = "TRF456",
                    VehicleType = Type,
                    Model = 2024,
                    ReturnDate = "02-02-2026",
                });

                Brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Name == "AUDI");
                Type = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Name == "CARGA");
                Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "jose");
                Use = await _context.VehicleUses.FirstOrDefaultAsync(x => x.Name == "Particular");

                _ = _context.Vehicles.Add(new Vehicle
                {
                    VehicleBrand = Brand,
                    VehicleUse = Use,
                    CurrentKm = 7000,
                    Customer = Customer,
                    Plate = "LKJ652",
                    VehicleType = Type,
                    Model = 2018,
                    ReturnDate = "02-02-2026",
                });
            }
            await _context.SaveChangesAsync();
        }
 

    }
}
