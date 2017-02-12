using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            //Write a query to return all customers in the given city
            var citySearch = _db.Customers.Where(c => c.City == city);
            return Ok(citySearch);
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            //Write a query to return all customers from Mexico, Sweden and Germany
            var specialCustomers = _db.Customers.Where(c => c.Country == "Mexico" || c.Country == "Sweden" || c.Country == "Germany");
            return Ok(specialCustomers);
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            //Write a query to return all customers with orders that shipped using the given shipperName
            var customers = _db.Customers;
            var orders = _db.Orders;
            var shippers = _db.Shippers;
            var customerShipped = from c in customers
                                  join o in orders on c.CustomerID equals o.CustomerID
                                  join s in shippers on o.ShipVia equals s.ShipperID
                                  where s.CompanyName.Contains(shipperName)
                                  select c;
            //var customerShipped = customers.Where(c => c.Orders
            //                                            .Any(o => o.Shipper.CompanyName == shipperName));

            return Ok(customerShipped.Distinct());
        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            //Write a query to return all customers with no orders in the Orders table
            //var customers1 = _db.Customers.Select(c => c.CustomerID).Distinct();
            //var customers2 = _db.Orders.Select(o => o.CustomerID).Distinct();
            //var finalList = customers1.Except(customers2);

            var final = from c in _db.Customers
                        where !(from o in _db.Orders
                                select o.CustomerID)
                                .Contains(c.CustomerID)
                        select c;

            return Ok(final);



        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
