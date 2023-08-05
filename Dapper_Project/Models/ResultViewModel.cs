using Dapper_Project.DAL.DTOs;

namespace Dapper_Project.Models
{
    public class ResultViewModel
    {
        public List<BrandResult>? BrandMax { get; set; }
        public BrandResult? BrandMin { get; set; }
        public PlateResult? PlateMax { get; set; }
        public PlateResult? PlateMin { get; set; }
        public ShiftTypeResult? ShiftType { get; set; }
        public FuelResult? FuelType { get; set; }

    }
}
