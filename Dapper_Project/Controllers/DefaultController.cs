using Dapper_Project.DAL.DTOs;
using Dapper_Project.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Dapper_Project.Controllers
{
    public class DefaultController : Controller
    {
        private readonly string _connectionString = "Server =DESKTOP-ITA1D3N\\SQLEXPRESS; initial catalog = CARPLATES; integrated security = true";

        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);

            var brandMax = (await connection.QueryAsync<BrandResult>("SELECT TOP 5 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count DESC")).ToList();
            var brandMin = (await connection.QueryAsync<BrandResult>("SELECT TOP 1 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count ASC")).FirstOrDefault();

            var plateMax = (await connection.QueryAsync<PlateResult>("SELECT TOP 1 SUBSTRING(PLATE, 1, 2) AS plate, COUNT(*) AS count FROM PLATES GROUP BY SUBSTRING(PLATE, 1, 2) ORDER BY count DESC")).FirstOrDefault();
            var plateMin = (await connection.QueryAsync<PlateResult>("SELECT TOP 1 SUBSTRING(PLATE, 1, 2) AS plate, COUNT(*) AS count FROM PLATES GROUP BY SUBSTRING(PLATE, 1, 2) ORDER BY count ASC")).FirstOrDefault();

            var shiftType = (await connection.QueryAsync<ShiftTypeResult>("SELECT TOP 1 SHIFTTYPE, COUNT(*) AS count FROM PLATES GROUP BY SHIFTTYPE ORDER BY count DESC")).FirstOrDefault();

            var fuelType = (await connection.QueryAsync<FuelResult>("SELECT TOP 1 FUEL, COUNT(*) AS count FROM PLATES GROUP BY FUEL ORDER BY count DESC")).FirstOrDefault();

            var result = new ResultViewModel
            {
                BrandMax = brandMax,
                BrandMin = brandMin,
                PlateMax = plateMax,
                PlateMin = plateMin,
                ShiftType = shiftType,
                FuelType = fuelType,
            };
            return View(result);
        }



        public async Task<IActionResult> Search(string keyword)
        {

            string query = @"
            SELECT TOP 10000 BRAND, SUBSTRING(PLATE, 1, 2) AS PlatePrefix, SHIFTTYPE, FUEL
            FROM PLATES
            WHERE BRAND LIKE '%' + @Keyword + '%'
               OR PLATE LIKE '%' + @Keyword + '%'
               OR SHIFTTYPE LIKE '%' + @Keyword + '%'
               OR FUEL LIKE '%' + @Keyword + '%'
        ";

            await using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Sorguyu çalıştırın ve sonuçları alın
            var searchResults = await connection.QueryAsync<SearchResult>(query, new { Keyword = keyword });

            // Sonuçları JSON formatında döndürün
            return Json(searchResults);

        }

    }
}

//SQL içerisinde bunu excute etmeniz gerekmektedir indexleme işlemi yapıp veri çekmeyi hızlanıdrmak için
//CREATE NONCLUSTERED  INDEX IX_PLATE ON dbo.PLATES (PLATE);
//CREATE NONCLUSTERED  INDEX IX_SHIFTTYPE ON dbo.PLATES (SHIFTTYPE);
//CREATE NONCLUSTERED  INDEX IX_BRAND ON dbo.PLATES (BRAND);
//CREATE NONCLUSTERED  INDEX IX_FUEL ON dbo.PLATES (FUEL);