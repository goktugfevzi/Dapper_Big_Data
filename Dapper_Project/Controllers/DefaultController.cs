using Dapper_Project.DAL.DTOs;
using Dapper_Project.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Dapper_Project.Controllers
{
    public class DefaultController : Controller
    {
        private readonly string _connectionString = "Server =DESKTOP-ITA1D3N\\SQLEXPRESS; initial catalog = CARPLATES; integrated security = true";
        private readonly IMemoryCache _cache;

        public DefaultController(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var cacheKey = "ResultViewModel";
            var result = _cache.Get<ResultViewModel>(cacheKey);

            if (result is not null)
            {
                return View(result);
            }
            await using var connection = new SqlConnection(_connectionString);

            var brandMax = (await connection.QueryAsync<BrandResult>("SELECT TOP 5 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count DESC")).ToList();
            var brandMin = (await connection.QueryAsync<BrandResult>("SELECT TOP 5 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count ASC")).ToList();
            var plateMax = (await connection.QueryAsync<PlateResult>("SELECT TOP 5 SUBSTRING(PLATE, 1, 2) AS plate, COUNT(*) AS count FROM PLATES GROUP BY SUBSTRING(PLATE, 1, 2) ORDER BY count DESC")).ToList();
            var plateMin = (await connection.QueryAsync<PlateResult>("SELECT TOP 5 SUBSTRING(PLATE, 1, 2) AS plate, COUNT(*) AS count FROM PLATES GROUP BY SUBSTRING(PLATE, 1, 2) ORDER BY count ASC")).ToList();
            var shiftType = (await connection.QueryAsync<ShiftTypeResult>("SELECT TOP 3 SHIFTTYPE, COUNT(*) AS count FROM PLATES GROUP BY SHIFTTYPE ORDER BY count DESC")).ToList();
            var fuelType = (await connection.QueryAsync<FuelResult>("SELECT TOP 3 FUEL, COUNT(*) AS count FROM PLATES GROUP BY FUEL")).ToList();
            var caseType = (await connection.QueryAsync<CaseTypeResult>("SELECT TOP 3 CASETYPE, COUNT(*) AS count FROM PLATES GROUP BY CASETYPE ORDER BY count DESC")).ToList();


            result = new ResultViewModel
            {
                BrandMax = brandMax,
                BrandMin = brandMin,
                PlateMax = plateMax,
                PlateMin = plateMin,
                ShiftType = shiftType,
                FuelType = fuelType,
                CaseType = caseType,
            };

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15)
            };
            _cache.Set(cacheKey, result, cacheEntryOptions);

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