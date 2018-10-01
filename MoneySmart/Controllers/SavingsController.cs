using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Controllers
{
    [Route("api/savings")]
    public class SavingsController : Controller
    {
        public SavingsController()
        {

        }

        [HttpGet()]
        public IActionResult GetSavings()
        {
            var result = 1000;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSavings(int id)
        {
            var result = (id==0)? 500 : id ;
            return Ok(result);
        }
    }
}
