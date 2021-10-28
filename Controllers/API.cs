using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Entity;
using Service;
using Interface;
using View;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private IApiService _service = null;
        public APIController(IApiService service)
        {
           _service = service;
        }

        [HttpGet]
        [Route ("GetApiData")]
         public async Task<IEnumerable<GetView>> GetData()
        {
            var x = await _service.CallRepositoryGet();

            return x as IEnumerable<GetView>;
        } 

        [HttpPost]
        [Route ("InsertApiData")]
        public async Task<string> InsertData(InfoView info)
        {
            var x = await _service.CallRepositoryInsert(info);

            return x;
        }

        [HttpDelete]
        [Route ("DeleteApiData")]
        public async Task<string> DeleteData(int Id)
        {
            if(Id == 0)
            {
               return "Invalid value, the 'ID' field is required";
            }

            var x = await _service.CallRepositoryDelete(Id);

            return x;
        }

        [HttpPut]
        [Route ("UpdateApiData")]
        public async Task<string> UpdateData(string Name, string City, int Age, int Id)
        {
            if(Id == 0)
            {
               return "Invalid value, the 'ID' field is required";
            }

            var x = await _service.CallRepositoryUpdate(Name, City, Age, Id);

            return x;
        }   
    }
}
