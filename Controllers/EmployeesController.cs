using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class EmployeesController : ApiController
    {
        private NORTHWNDEntities _db;

        public EmployeesController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/employees
        [HttpGet, Route("api/employees"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployees()
        {
            //Write a query to return all employees
            var employees = from e in _db.Employees
                            select e;
            return Ok(_db.Employees);
        }

        // GET: api/employees/title/Sales Manager
        [HttpGet, Route("api/employees/title/{title}"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployeesByTitle(string title)
        {
            //Write a query to return all employees with the given Title
            var employeesByTitle = _db.Employees.Where(e => e.Title == title);
            return Ok(employeesByTitle);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
