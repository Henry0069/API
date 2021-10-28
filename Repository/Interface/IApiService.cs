using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entity;
using View;

namespace Interface
{
    public interface IApiService
    {
      Task<IEnumerable<GetView>> CallRepositoryGet();
      Task<string> CallRepositoryInsert(InfoView info);
      Task<string> CallRepositoryDelete(int Id);
      Task<string> CallRepositoryUpdate(string Name, string City, int Age,int Id);
              
    }
}
