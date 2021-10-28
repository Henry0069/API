using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entity;

namespace Interface
{
    public interface IApiRepository
    {
      Task<IEnumerable<Info>> GetData(); 
      Task<string> InsertData(Info info);
      Task<string> DeleteData(int Id);
      Task<string> UpdateData(string Name, string City, int Age, int Id);
      
    }
}
