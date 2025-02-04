namespace MASRepository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DbMechanicalAssistanceShop _context;

        public VehicleRepository(YourDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string chasisNumber, string licensePlate)
        {
            return await _context.Vehicles.AnyAsync(v => v.ChasisNumber == chasisNumber || v.LicensePlate == licensePlate);
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Color)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }
    }

}
