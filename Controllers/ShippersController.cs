using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class ShippersController : ApiController
    {
        private NORTHWNDEntities _db;

        public ShippersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/shippers/reports/freight
        [HttpGet, Route("/api/shippers/reports/freight"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult GetFreightReport()
        {
            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/

            //    Write a query to return an array of anonymous objects that have two properties. 

            //    1. A Shipper property containing that particular shipper.
            //    2. A FreightTotals property containing the freight totals for that shipper

            //    Return the rows ordered by FreightTotals
            var newShippers = from s in _db.Shippers
                              select
                              new
                              {
                                  Shipper = s,
                                  FreightTotals = s.Orders.Sum(o => o.Freight)

                              };
            return Ok(newShippers);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
