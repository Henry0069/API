using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Entity;
using Interface;
using View;

namespace Service
{
    public class ApiService : IApiService
    {
        private IApiRepository _repo = null;

        public ApiService(IApiRepository repo)
        {
            _repo = repo;
        }

        
        public async Task<IEnumerable<GetView>> CallRepositoryGet()
        {
            var x = await _repo.GetData();

            var configuration = new MapperConfiguration(
              cfg => {
                    
                    cfg.CreateMap<Info,GetView>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._ID_))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src._NAME_))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src._CITY_))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src._AGE_));

                }
            );

            var mapper = new Mapper(configuration);

            IEnumerable<GetView> iv = mapper.Map<IEnumerable<GetView>>(x); 

            return iv;
        }

        public async Task<string> CallRepositoryInsert(InfoView info)
        {
            var info2 = new Info();
            var configuration = new MapperConfiguration(
              cfg => {
                    
                    cfg.CreateMap<InfoView,Info>()
                    .ForMember(dest => dest._NAME_, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest._CITY_, opt => opt.MapFrom(src => src.City))
                    .ForMember(dest => dest._AGE_, opt => opt.MapFrom(src => src.Age));

                }
            );

            var mapper = new Mapper(configuration);
            info2 = mapper.Map<Info>(info);
            
            
            var x = await _repo.InsertData(info2);

            
            return x;
        }

        public async Task<string> CallRepositoryDelete(int Id)
        {
            var x = await _repo.DeleteData(Id);

            return x;
        }

        public async Task<string> CallRepositoryUpdate(string Name, string City, int Age,int Id)
        {
            var x = await _repo.UpdateData(Name, City, Age, Id);

            return x;
        }


    }
}
