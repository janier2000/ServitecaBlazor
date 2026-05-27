using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;
using System;
using System.Xml.Linq;

namespace Serviteca.Backend.Repositories.Implementations;

public class VehiclesRepository : GenericRepository<Vehicle>, IVehiclesRepository
{
    private readonly DataContext _context;

    public VehiclesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ActionResponse<Vehicle>> CreateAsync(VehicleDTO vehicleDTO)
    {
        try
        {
            var ResponseVehicle = await CheckVehicle(vehicleDTO);
            if (ResponseVehicle.WasSuccess)
            {
                Vehicle vehicleENT = ResponseVehicle.Result!;
                vehicleENT.Plate = vehicleDTO.Plate;
                vehicleENT.CurrentKm = vehicleDTO.CurrentKm;
                vehicleENT.Model = vehicleDTO.Model;
                vehicleENT.ReturnDate = vehicleDTO.ReturnDate.ToString();
                _context.Add(vehicleENT);
                await _context.SaveChangesAsync();
                return new ActionResponse<Vehicle>
                {
                    WasSuccess = true,
                    Result = vehicleENT
                };
            }
            else
            {
                return new ActionResponse<Vehicle>
                {
                    WasSuccess = false,
                    Message = ResponseVehicle.Message
                };
            }
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Ya existe vehiculo que estas intentando crear"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public async Task<ActionResponse<Vehicle>> UpdateAsync(VehicleDTO vehicleDTO)
    {
        try
        {
            var idCheckResponse = await CheckIdVehicle(vehicleDTO.Id);
            if (!idCheckResponse.WasSuccess)
            {
                return idCheckResponse;
            }

            var ResponseVehicle = await CheckVehicle(vehicleDTO);
            if (ResponseVehicle.WasSuccess)
            {
                Vehicle vehicleENT = new Vehicle()
                {
                    Plate = vehicleDTO.Plate,
                    CurrentKm = vehicleDTO.CurrentKm,
                    Model = vehicleDTO.Model,
                    ReturnDate = vehicleDTO.ReturnDate.ToString(),
                    VehicleBrand = ResponseVehicle.Result!.VehicleBrand,
                    VehicleType = ResponseVehicle.Result!.VehicleType,
                    VehicleUse = ResponseVehicle.Result!.VehicleUse,
                };
                _context.Update(vehicleENT);
                await _context.SaveChangesAsync();
                return new ActionResponse<Vehicle>
                {
                    WasSuccess = true,
                    Result = vehicleENT
                };
            }
            else
            {
                return new ActionResponse<Vehicle>
                {
                    WasSuccess = false,
                    Message = ResponseVehicle.Message
                };
            }
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Error actualizar vehiculo"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public override async Task<ActionResponse<IEnumerable<Vehicle>>> GetAsync()
    {
        var customers = await _context.Vehicles
                                      .Include(s => s.Customer!)
                                      .Include(s => s.VehicleBrand!)
                                      .Include(s => s.VehicleType!)
                                      .Include(s => s.VehicleUse)
                                      .OrderBy(x => x.Plate)
                                      .ToListAsync();
        return new ActionResponse<IEnumerable<Vehicle>>
        {
            WasSuccess = true,
            Result = customers
        };
    }

    public override async Task<ActionResponse<IEnumerable<Vehicle>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Vehicles
                                .Include(s => s.Customer!)
                                .Include(s => s.VehicleBrand!)
                                .Include(s => s.VehicleType!)
                                .Include(s => s.VehicleUse)
                                .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Plate.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Vehicle>>
        {
            WasSuccess = true,
            Result = await queryable.OrderBy(x => x.Plate)
                                    .Paginate(pagination)
                                    .ToListAsync()
        };
    }

    public override async Task<ActionResponse<Vehicle>> GetAsync(int id)
    {
        var vehicle = await _context.Vehicles
                                    .Include(s => s.Customer!)
                                    .Include(s => s.VehicleBrand!)
                                    .Include(s => s.VehicleType!)
                                    .Include(s => s.VehicleUse)
                                    .FirstOrDefaultAsync(c => c.Id == id);
        if (vehicle == null)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Vehiculo no existe"
            };
        }

        return new ActionResponse<Vehicle>
        {
            WasSuccess = true,
            Result = vehicle
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

    public async Task<ActionResponse<Vehicle>> CheckVehicle(VehicleDTO vehicleDTO)
    {
        var customer = await _context.Customers.FindAsync(vehicleDTO.CustomerId);
        if (customer == null && vehicleDTO.Id == 0)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Cliente no existe"
            };
        }

        var vehicleUse = await _context.VehicleUses.FindAsync(vehicleDTO.VehicleUseId);
        if (vehicleUse == null)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Uso de vehículo no existe"
            };
        }

        var vehicleType = await _context.VehicleTypes.FindAsync(vehicleDTO.VehicleTypeId);
        if (vehicleType == null)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Tipo de vehículo no existe";
            };
        }

        var vehicleBrand = await _context.VehicleBrands.FindAsync(vehicleDTO.VehicleBrandId);
        if (vehicleBrand == null)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Marca de vehículo no existe";
            };
        }

        Vehicle vehicleENT = new Vehicle()
        {
            VehicleUse = vehicleUse,
            VehicleType = vehicleType,
            VehicleBrand = vehicleBrand,
            Customer = customer
        };

        return new ActionResponse<Vehicle>
        {
            WasSuccess = true,
            Result = vehicleENT
        };
    }

    public async Task<ActionResponse<Vehicle>> CheckIdVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle == null)
        {
            return new ActionResponse<Vehicle>
            {
                WasSuccess = false,
                Message = "Vehiculo no existe"
            };
        }
        return new ActionResponse<Vehicle>
        {
            WasSuccess = true,
            Result = vehicle
        };
    }
}