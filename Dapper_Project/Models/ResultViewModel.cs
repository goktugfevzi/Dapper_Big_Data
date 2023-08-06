using Dapper_Project.DAL.DTOs;

namespace Dapper_Project.Models
{
    public class ResultViewModel
    {
        public List<BrandResult>? BrandMax { get; set; }
        public List<BrandResult>? BrandMin { get; set; }
        public List<PlateResult>? PlateMax { get; set; }
        public List<PlateResult>? PlateMin { get; set; }
        public List<ShiftTypeResult>? ShiftType { get; set; }
        public List<FuelResult>? FuelType { get; set; }
        public List<CaseTypeResult>? CaseType { get; set; }

    }
}
