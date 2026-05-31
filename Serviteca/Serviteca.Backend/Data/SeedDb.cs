using Microsoft.EntityFrameworkCore;
using Serviteca.Shared.Entities;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Serviteca.Backend.Data;

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
        await CheckTypesAsync();
        await CheckUseAsync();
        await CheckBrandsAsync();
        await CheckVehicleAsync();
        await CheckInsurersAsync();
        await CheckSoatsAsync();
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

    private async Task CheckTypesAsync()
    {
        if (!_context.Types.Any())
        {
            _ = _context.Types.Add(new TypeV
            {
                Name = "AUTOMOVILES"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "CAMIONES"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "TAXIS"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "CAMPEROS"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "CAMIONETAS"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "CARGA"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "BUSES"
            });
            _ = _context.Types.Add(new TypeV
            {
                Name = "VANS"
            });
        }
        await _context.SaveChangesAsync();
    }

    private async Task CheckUseAsync()
    {
        if (!_context.Uses.Any())
        {
            _ = _context.Uses.Add(new Use
            {
                Name = "Particular"
            });
            _ = _context.Uses.Add(new Use
            {
                Name = "Publico"
            });
            _ = _context.Uses.Add(new Use
            {
                Name = "Particular trabajo"
            });
        }
        await _context.SaveChangesAsync();
    }

    private async Task CheckBrandsAsync()
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

    private async Task CheckVehicleAsync()
    {
        if (!_context.Vehicles.Any())
        {
            var Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "CHEVROLET");
            var Type = await _context.Types.FirstOrDefaultAsync(x => x.Name == "AUTOMOVILES");
            var Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ADRAINA");
            var Use = await _context.Uses.FirstOrDefaultAsync(x => x.Name == "Particular");

            _ = _context.Vehicles.Add(new Vehicle
            {
                Brand = Brand,
                Use = Use,
                CurrentKm = 3000,
                Customer = Customer,
                Plate = "JUR540",
                TypeV = Type,
                Model = 2024,
                ReturnDate = Convert.ToDateTime("02-02-2026"),
            });

            Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "FORD");
            Type = await _context.Types.FirstOrDefaultAsync(x => x.Name == "CAMIONES");
            Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "EDUARDO");
            Use = await _context.Uses.FirstOrDefaultAsync(x => x.Name == "Publico");

            _ = _context.Vehicles.Add(new Vehicle
            {
                Brand = Brand,
                Use = Use,
                CurrentKm = 4000,
                Customer = Customer,
                Plate = "JUR580",
                TypeV = Type,
                Model = 1850,
                ReturnDate = Convert.ToDateTime("02-02-2026"),
            });

            Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "DAIHATSU");
            Type = await _context.Types.FirstOrDefaultAsync(x => x.Name == "TAXIS");
            Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ALAN");
            Use = await _context.Uses.FirstOrDefaultAsync(x => x.Name == "Publico");

            _ = _context.Vehicles.Add(new Vehicle
            {
                Brand = Brand,
                Use = Use,
                CurrentKm = 4000,
                Customer = Customer,
                Plate = "HTR894",
                TypeV = Type,
                Model = 2000,
                ReturnDate = Convert.ToDateTime("02-02-2026"),
            });

            Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "ALFA  ROMERO");
            Type = await _context.Types.FirstOrDefaultAsync(x => x.Name == "CAMPEROS");
            Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "ALEJANDRA");
            Use = await _context.Uses.FirstOrDefaultAsync(x => x.Name == "Particular trabajo");

            _ = _context.Vehicles.Add(new Vehicle
            {
                Brand = Brand,
                Use = Use,
                CurrentKm = 5000,
                Customer = Customer,
                Plate = "DRE852",
                TypeV = Type,
                Model = 2010,
                ReturnDate = Convert.ToDateTime("02-02-2026"),
            });

            Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "AMPLE");
            Type = await _context.Types.FirstOrDefaultAsync(x => x.Name == "CAMIONETAS");
            Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "camila");
            Use = await _context.Uses.FirstOrDefaultAsync(x => x.Name == "Particular trabajo");

            _ = _context.Vehicles.Add(new Vehicle
            {
                Brand = Brand,
                Use = Use,
                CurrentKm = 6000,
                Customer = Customer,
                Plate = "TRF456",
                TypeV = Type,
                Model = 2024,
                ReturnDate = Convert.ToDateTime("02-02-2026"),
            });

            Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == "AUDI");
            Type = await _context.Types.FirstOrDefaultAsync(x => x.Name == "CARGA");
            Customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == "jose");
            Use = await _context.Uses.FirstOrDefaultAsync(x => x.Name == "Particular");

            _ = _context.Vehicles.Add(new Vehicle
            {
                Brand = Brand,
                Use = Use,
                CurrentKm = 7000,
                Customer = Customer,
                Plate = "LKJ652",
                TypeV = Type,
                Model = 2018,
                ReturnDate = Convert.ToDateTime("02-02-2026"),
            });
        }
        await _context.SaveChangesAsync();
    }

    private async Task CheckInsurersAsync()
    {
        if (!_context.Insurers.Any())
        {
            _ = _context.Insurers.Add(new Insurer
            {
                Name = "ALLIANZ"
            });
            _ = _context.Insurers.Add(new Insurer
            {
                Name = "AXA COLPATRIA"
            });
            _ = _context.Insurers.Add(new Insurer
            {
                Name = "LIBERTY SEGUROS"
            });
            _ = _context.Insurers.Add(new Insurer
            {
                Name = "MAPFRE COLOMBIA"
            });
            _ = _context.Insurers.Add(new Insurer
            {
                Name = "SEGUROS BOLÍVAR"
            });
        }
        await _context.SaveChangesAsync();
    }

    private async Task CheckSoatsAsync()
    {
        if (!_context.Soats.Any())
        {
            var Insurer = await _context.Insurers.FirstOrDefaultAsync(x => x.Name == "ALLIANZ");
            var Vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == "JUR540");
            _ = _context.Soats.Add(new Soat
            {
                Insurer = Insurer,
                Vehicle = Vehicle,
                ExpirationDate = Convert.ToDateTime("02-05-2026"),
                Price = "500000",
                PolicyData = "SOAT CHEVROLET JUR540",
                RateCategory = "Categoria A",
                Status = "Vencido",
                Date = Convert.ToDateTime("02-05-2025")
            });

            Insurer = await _context.Insurers.FirstOrDefaultAsync(x => x.Name == "AXA COLPATRIA");
            Vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == "HTR894");
            _ = _context.Soats.Add(new Soat
            {
                Insurer = Insurer,
                Vehicle = Vehicle,
                ExpirationDate = Convert.ToDateTime("01-04-2026"),
                Price = "700000",
                PolicyData = "SOAT CHEVROLET HTR894",
                RateCategory = "Categoria BB",
                Status = "Vencido",
                Date = Convert.ToDateTime("01-04-2025")
            });
            Insurer = await _context.Insurers.FirstOrDefaultAsync(x => x.Name == "LIBERTY SEGUROS");
            Vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == "LKJ652");
            _ = _context.Soats.Add(new Soat
            {
                Insurer = Insurer,
                Vehicle = Vehicle,
                ExpirationDate = Convert.ToDateTime("20-10-2025"),
                Price = "5800000",
                PolicyData = "SOAT CHEVROLET LKJ652",
                RateCategory = "Categoria BB",
                Status = "Vencido",
                Date = Convert.ToDateTime("20-10-2024")
            });
            Insurer = await _context.Insurers.FirstOrDefaultAsync(x => x.Name == "AXA COLPATRIA");
            Vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == "DRE852");
            _ = _context.Soats.Add(new Soat
            {
                Insurer = Insurer,
                Vehicle = Vehicle,
                ExpirationDate = Convert.ToDateTime("04-05-2027"),
                Price = "7800000",
                PolicyData = "SOAT CHEVROLET DRE852",
                RateCategory = "Categoria BB",
                Status = "Vigente",
                Date = Convert.ToDateTime("04-05-2026")
            });
            Insurer = await _context.Insurers.FirstOrDefaultAsync(x => x.Name == "LIBERTY SEGUROS");
            Vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == "JUR580");
            _ = _context.Soats.Add(new Soat
            {
                Insurer = Insurer,
                Vehicle = Vehicle,
                ExpirationDate = Convert.ToDateTime("04-05-2027"),
                Price = "8600000",
                PolicyData = "SOAT CHEVROLET JUR580",
                RateCategory = "Categoria BB",
                Status = "Vigente",
                Date = Convert.ToDateTime("04-05-2026")
            });
            Insurer = await _context.Insurers.FirstOrDefaultAsync(x => x.Name == "ALLIANZ");
            Vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == "TRF456");
            _ = _context.Soats.Add(new Soat
            {
                Insurer = Insurer,
                Vehicle = Vehicle,
                ExpirationDate = Convert.ToDateTime("04-05-2027"),
                Price = "100000",
                PolicyData = "SOAT CHEVROLET TRF456",
                RateCategory = "Categoria BB",
                Status = "Vigente",
                Date = Convert.ToDateTime("04-05-2026")
            });
        }
        await _context.SaveChangesAsync();
    }
}