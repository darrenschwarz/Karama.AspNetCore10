using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareApplication.EfCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace SoftwareApplication.EfCore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly SoftwareApplicationsContext _softwareApplicationsContext;

        public ValuesController(SoftwareApplicationsContext softwareApplicationsContext)
        {
            _softwareApplicationsContext = softwareApplicationsContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var p = _softwareApplicationsContext.Packages.ToList();

            var searchTerm = "GRP05TCY";
            //var p2 = _softwareApplicationsContext.BroadSearch.FromSql("Execute dbo.BroadSearch {0}", searchTerm).ToList();

            //var p3 = _softwareApplicationsContext.Packages.Join(_softwareApplicationsContext.PackageDetails,
            //    package => package.Id, detail => detail.PackageId,
            //    (package, detail) => new BroadSearch
            //    {
            //        PackageIdentifier = package.PackageIdentifier,                    
            //        LicenceType = detail.LicenceType
            //    }).ToList();


            //var p4 = _softwareApplicationsContext.Packages.Join(_softwareApplicationsContext.PackageDetails,
            //    package => package.Id, detail => detail.PackageId,
            //    (package, detail) => new BroadSearch
            //    {
            //        PackageId = package.Id,
            //        PackageIdentifier = package.PackageIdentifier,
            //        LicenceType = detail.LicenceType
            //    }).Join(_softwareApplicationsContext.SoftwareAutomation,
            //        bs => bs.PackageId, softwareautomation => softwareautomation.PackageId,
            //        (bs, sa) => new BroadSearch()
            //        {
            //            PackageId = bs.PackageId,
            //            PackageIdentifier = bs.PackageIdentifier,
            //            LicenceType = bs.LicenceType,
            //            Version = sa.Version,
            //            Manufacturer = sa.Manufacturer
            //        }
            //    ).ToList();


            var p4 = _softwareApplicationsContext.Packages.Join(_softwareApplicationsContext.PackageDetails,
                package => package.Id, detail => detail.PackageId,
                (package, detail) => new BroadSearch
                {
                    PackageId = package.Id,
                    PackageIdentifier = package.PackageIdentifier,
                    LicenceType = detail.LicenceType
                }).Join(_softwareApplicationsContext.SoftwareAutomation,
                    bs => bs.PackageId, softwareautomation => softwareautomation.PackageId,
                    (bs, sa) => new 
                    {
                        SaId = sa.Id == null ? 0 :sa.Id,
                        PackageId = bs.PackageId,
                        PackageIdentifier = bs.PackageIdentifier,
                        LicenceType = bs.LicenceType.DefaultIfEmpty(),
                        Version = sa.Version.DefaultIfEmpty(),
                        Manufacturer = sa.Manufacturer.DefaultIfEmpty()
                    }
                ).Join(_softwareApplicationsContext.Gsr,
                    bs1 => bs1.SaId, gsr => gsr.SotwareAutomationId,
                    (bs1, gsr) => new 
                    {
                        PackageId = bs1.PackageId,
                        PackageIdentifier = bs1.PackageIdentifier,
                        LicenceType = bs1.LicenceType.DefaultIfEmpty(),
                        Version = bs1.Version.DefaultIfEmpty(),
                        Manufacturer = bs1.Manufacturer.DefaultIfEmpty(),
                        GsrRef = gsr.GsrRef.DefaultIfEmpty()
                    }
                ).Select(lr=>new
                {
                    PackageId = lr.PackageId,
                    PackageIdentifier = lr.PackageIdentifier,
                    LicenceType = lr.LicenceType.DefaultIfEmpty(),
                    Version = lr.Version.DefaultIfEmpty(),
                    Manufacturer = lr.Manufacturer.DefaultIfEmpty(),
                    GsrRef = lr.GsrRef.DefaultIfEmpty()
                }).ToList();

            var query = from package in _softwareApplicationsContext.Packages where package.PackageIdentifier.Contains("GRP05TCY")
                        join packageDetail in _softwareApplicationsContext.PackageDetails 
                            on package.PackageIdentifier equals packageDetail.Package.PackageIdentifier into gj
                                from sub in gj//.DefaultIfEmpty()
                                select new { package.PackageIdentifier, LicenceType = (sub == null ? String.Empty : sub.LicenceType) };

            var y = query.ToList();

            var u = _softwareApplicationsContext.Packages
                .Include(p => p.PackageDetails)
                .Include(p => p.SoftwareAutomation)
                .ThenInclude(sa => sa.Gsr)
                .Select(r => new
                {
                    pi = r.PackageIdentifier,
                    pn = r.PackageName,
                    pm = r.SoftwareAutomation.Select(sa=>sa.Manufacturer),
                    gsr = r.SoftwareAutomation.Select(sa => sa.Gsr.Select(gsr=>gsr.GsrRef))
                }).ToList();

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
