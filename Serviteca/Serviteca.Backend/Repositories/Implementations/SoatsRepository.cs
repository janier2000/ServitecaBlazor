using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Implementations;

public class SoatsRepository : GenericRepository<Soat>, ISoatsRepository
{
    private readonly DataContext _context;

    public SoatsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ActionResponse<Soat>> CreateAsync(SoatDTO soatDTO)
    {
        try
        {
            var ResponseSoat = await CheckSoat(soatDTO);
            if (ResponseSoat.WasSuccess)
            {
                Soat soatENT = ResponseSoat.Result!;
                soatENT.Date = soatDTO.Date;
                soatENT.ExpirationDate = soatDTO.ExpirationDate;
                soatENT.RateCategory = soatDTO.RateCategory;
                soatENT.PolicyData = soatDTO.PolicyData;
                soatENT.Price = soatDTO.Price;
                _context.Add(soatENT);
                await _context.SaveChangesAsync();
                return new ActionResponse<Soat>
                {
                    WasSuccess = true,
                    Result = soatENT
                };
            }
            else
            {
                return new ActionResponse<Soat>
                {
                    WasSuccess = false,
                    Message = ResponseSoat.Message
                };
            }
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = "Ya existe Soat que estas intentando crear"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public async Task<ActionResponse<Soat>> UpdateAsync(SoatDTO soatDTO)
    {
        try
        {
            var idCheckResponse = await CheckIdSoat(soatDTO.Id);
            if (!idCheckResponse.WasSuccess)
            {
                return idCheckResponse;
            }

            var ResponseSoat = await CheckSoat(soatDTO);
            if (ResponseSoat.WasSuccess)
            {
                Soat soatENT = idCheckResponse.Result!;
                soatENT.Date = soatDTO.Date;
                soatENT.ExpirationDate = soatDTO.ExpirationDate;
                soatENT.RateCategory = soatDTO.RateCategory;
                soatENT.PolicyData = soatDTO.PolicyData;
                soatENT.Price = soatDTO.Price;
                _context.Update(soatENT);
                await _context.SaveChangesAsync();
                return new ActionResponse<Soat>
                {
                    WasSuccess = true,
                    Result = soatENT
                };
            }
            else
            {
                return new ActionResponse<Soat>
                {
                    WasSuccess = false,
                    Message = ResponseSoat.Message
                };
            }
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = "Error actualizar Soat"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public override async Task<ActionResponse<IEnumerable<Soat>>> GetAsync()
    {
        var customers = await _context.Soats.Include(s => s.Insurer!)
                                            .Include(s => s.Vehicle!)
                                            .ToListAsync();
        return new ActionResponse<IEnumerable<Soat>>
        {
            WasSuccess = true,
            Result = customers
        };
    }

    public override async Task<ActionResponse<IEnumerable<Soat>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Soats
                                .Include(s => s.Insurer!)
                                .Include(s => s.Vehicle!)
                                .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Vehicle!.Plate
                                              .ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Soat>>
        {
            WasSuccess = true,
            Result = await queryable.OrderBy(x => x.Id)
                                    .Paginate(pagination)
                                    .ToListAsync()
        };
    }

    public override async Task<ActionResponse<Soat>> GetAsync(int id)
    {
        var soat = await _context.Soats.Include(s => s.Insurer!)
                                       .Include(s => s.Vehicle!)
                                       .FirstOrDefaultAsync(c => c.Id == id);
        if (soat == null)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = "Soat no existe"
            };
        }

        return new ActionResponse<Soat>
        {
            WasSuccess = true,
            Result = soat
        };
    }

    public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
    {
        var queryable = _context.Vehicles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Plate.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = totalPages
        };
    }

    public async Task<IEnumerable<Vehicle>> GetComboAsync()
    {
        return await _context.Vehicles
                             .OrderBy(c => c.Plate)
                             .ToListAsync();
    }

    public async Task<ActionResponse<Soat>> CheckSoat(SoatDTO soatDTO)
    {
        var vehicle = await _context.Vehicles.FindAsync(soatDTO.VehicleId);
        if (vehicle == null)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = "vehiculo no existe"
            };
        }

        var Insurer = await _context.Insurers.FindAsync(soatDTO.InsurerId);
        if (Insurer == null)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = "aseguradora no existe"
            };
        }

        Soat soatENT = new Soat()
        {
            Insurer = Insurer,
            Vehicle = vehicle,
        };

        return new ActionResponse<Soat>
        {
            WasSuccess = true,
            Result = soatENT
        };
    }

    public async Task<ActionResponse<Soat>> CheckIdSoat(int id)
    {
        var soat = await _context.Soats.FindAsync(id);
        if (soat == null)
        {
            return new ActionResponse<Soat>
            {
                WasSuccess = false,
                Message = "Soat no existe"
            };
        }
        return new ActionResponse<Soat>
        {
            WasSuccess = true,
            Result = soat
        };
    }

    Task<IEnumerable<Soat>> ISoatsRepository.GetComboAsync()
    {
        throw new NotImplementedException();
    }
}