using MASBusiness.Interfaces;
using MASBusiness.DTO;
using MASDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;
using MASBusiness.DTOs;
using System.Text.RegularExpressions;

namespace MASBusiness.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DbMechanicalAssistanceShopContext _context;
        private readonly IColorService _colorService;
        private readonly IModelService _modelService;
        private readonly IUtilsService _utilsService;

        public VehicleService(DbMechanicalAssistanceShopContext context, IColorService colorService, IModelService modelService, IUtilsService utilsService)
        {
            _context = context;
            _colorService = colorService;
            _modelService = modelService;
            _utilsService = utilsService;
        }
        public async Task<ServiceResponse<VehicleDto>> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            if (!_utilsService.IsValidLicensePlate(licensePlate))
            {
                return new ServiceResponse<VehicleDto>(false, "Invalid license plate.");
            }
            try
            {
                var vehicle = await _context.Vehicles
                    .Include(v => v.Color)
                    .Include(v => v.Model)
                    .Include(v => v.Model.Brand)
                    .Where(v => v.Valid == true)
                    .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

                if (vehicle == null)
                {
                    return new ServiceResponse<VehicleDto>(false, "Vehicle not found");
                }
                VehicleDto vehicleDto = new VehicleDto(vehicle) {};
                return new ServiceResponse<VehicleDto>(true, "Vehicle retrieved successfully", vehicleDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching vehicle: {ex.Message}");

                return new ServiceResponse<VehicleDto>(false, "An error occurred while fetching the vehicle.");
            }
        }

        public async Task<ServiceResponse<VehicleDto>> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles
                    .Include(v => v.Color)
                    .Include(v => v.Model)
                    .Include(v => v.Model.Brand)
                    .Where(v => v.Valid == true)
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (vehicle == null)
                {
                    return new ServiceResponse<VehicleDto>(false, "Vehicle not found");
                }
                VehicleDto vehicleDto = new VehicleDto(vehicle) {};
                return new ServiceResponse<VehicleDto>(true, "Vehicle retrieved successfully", vehicleDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching vehicle: {ex.Message}");

                return new ServiceResponse<VehicleDto>(false, "An error occurred while fetching the vehicle.");
            }
        }

        public async Task<ServiceResponse<VehicleDto>> CreateVehicleAsync(VehicleDto vehicleDto)
        {
            if (vehicleDto.LicensePlate is not null && !_utilsService.IsValidLicensePlate(vehicleDto.LicensePlate))
            {
                return new ServiceResponse<VehicleDto>(false, "Invalid license plate.", vehicleDto);
            }

            Vehicle? existingVehicle = await _context.Vehicles.FirstOrDefaultAsync(v =>
                v.ChassisNumber == vehicleDto.ChassisNumber || v.LicensePlate == vehicleDto.LicensePlate);

            if (existingVehicle is not null)
            {
                return existingVehicle.ChassisNumber == vehicleDto.ChassisNumber
                    ? new ServiceResponse<VehicleDto>(false, "Vehicle Chassis Number (VIN) already exists.", vehicleDto)
                    : new ServiceResponse<VehicleDto>(false, "Vehicle License Plate already exists.");
            }
            
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Vehicle vehicle = new Vehicle
                {
                    LicensePlate = vehicleDto.LicensePlate,
                    ChassisNumber = vehicleDto.ChassisNumber,
                    EngineNumber = vehicleDto.EngineNumber,
                    Valid = true
                };

                var color = await _colorService.GetOrCreateColorAsync(vehicleDto.Color.Name);
                vehicle.ColorId = color.Id;

                var model = await _modelService.GetOrCreateModelAsync(vehicleDto.Model.Name, vehicleDto.Model.Brand.Name);
                vehicle.ModelId = model.Id;

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                vehicleDto.Id = vehicle.Id;
                return new ServiceResponse<VehicleDto>(true, "Vehicle created successfully.", vehicleDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("An error occurred while creating the vehicle.", ex);
            }
        }
        
        public async Task<ServiceResponse<VehicleDto>> CreateVehicleAsync2(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
            {
                return new ServiceResponse<VehicleDto>(false, "Invalid data. Please check the information you are sending.", vehicleDto);
            }

            Vehicle? existingVehicle = await _context.Vehicles.FirstOrDefaultAsync(v =>
                v.ChassisNumber == vehicleDto.ChassisNumber || v.LicensePlate == vehicleDto.LicensePlate);

            if (existingVehicle is not null)
            {
                return existingVehicle.ChassisNumber == vehicleDto.ChassisNumber
                    ? new ServiceResponse<VehicleDto>(false, "Vehicle Chassis Number (VIN) already exists.", vehicleDto)
                    : new ServiceResponse<VehicleDto>(false, "Vehicle License Plate already exists.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == vehicleDto.Model.Brand.Name)
                    ?? new Brand { Name = vehicleDto.Model.Brand.Name };

                Model model = await _context.Models.FirstOrDefaultAsync(m => m.Name == vehicleDto.Model.Name)
                    ?? new Model { Name = vehicleDto.Model.Name, Brand = brand };

                Color color = await _context.Colors.FirstOrDefaultAsync(c => c.Name == vehicleDto.Color.Name)
                    ?? new Color { Name = vehicleDto.Color.Name };

                var newVehicle = new Vehicle
                {
                    LicensePlate = vehicleDto.LicensePlate,
                    ChassisNumber = vehicleDto.ChassisNumber,
                    EngineNumber = vehicleDto.EngineNumber,
                    Model = model,
                    Color = color
                };

                _context.Vehicles.Add(newVehicle);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var createdVehicleDto = new VehicleDto
                {
                    LicensePlate = newVehicle.LicensePlate,
                    ChassisNumber = newVehicle.ChassisNumber,
                    EngineNumber = newVehicle.EngineNumber,
                    Model = new ModelDto
                    {
                        Id = newVehicle.Model.Id,
                        Name = newVehicle.Model.Name,
                        Brand = new BrandDto
                        {
                            Id = newVehicle.Model.Brand.Id,
                            Name = newVehicle.Model.Brand.Name
                        }
                    },
                    Color = new ColorDto
                    {
                        Id = newVehicle.Color.Id,
                        Name = newVehicle.Color.Name
                    }
                };

                return new ServiceResponse<VehicleDto>(true, "Vehicle created successfully.", createdVehicleDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ServiceResponse<VehicleDto>(false, "An error occurred while processing the request:\n" + ex.Message);
            }
        }

        public async Task<ServiceResponse<VehicleDto>> UpdateVehicleAsync(int id, VehicleDto vehicleDto)
        {
            try
            {
                Vehicle vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return new ServiceResponse<VehicleDto>(false, "Vehicle does not exist.", vehicleDto);
                }

                vehicle.LicensePlate = vehicleDto.LicensePlate;
                vehicle.ChassisNumber = vehicleDto.ChassisNumber;
                vehicle.ManufacturingYear = vehicleDto.ManufacturingYear;
                vehicle.ModelId = await _context.Models
                    .Where(m => m.Name == vehicleDto.Model.Name)
                    .Select(m => m.Id)
                    .FirstOrDefaultAsync();
                Color? retrievedColor = await _colorService.GetOrCreateColorAsync(vehicleDto.Color.Name);
                vehicle.ColorId = retrievedColor.Id;

                _context.Entry(vehicle).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return new ServiceResponse<VehicleDto>(true, "Vehicle Updated successfully.", vehicleDto);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the vehicle.", ex);
            }
        }

        public async Task<ServiceResponse<VehicleDto>> DeleteVehicleAsync(int id)
        {
            Vehicle? vehicle = await _context.Vehicles
                    .Include(v => v.Color)
                    .Include(v => v.Model)
                    .Include(v => v.Model.Brand)
                    .Where(v => v.Valid == true)
                    .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return new ServiceResponse<VehicleDto>(false, "Vehicle does not exist.", null);
            }

            VehicleDto? vehicleDto = new VehicleDto(vehicle);
            
            vehicle.Valid = false;
            vehicle.DeletedAt = DateTime.Now;

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return new ServiceResponse<VehicleDto>(true, "Vehicle Deleted succesfully.", vehicleDto);
        }

        public Task<ServiceResponse<VehicleDto>> UpdateVehicleAsync(VehicleDto vehicleDto)
        {
            throw new NotImplementedException();
        }
    }
}
