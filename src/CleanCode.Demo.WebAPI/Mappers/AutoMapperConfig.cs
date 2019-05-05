using AutoMapper;
using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.WebAPI.DTO;

namespace CleanCode.Demo.WebAPI.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ToDoItem, ToDoItemDTO>();
                cfg.CreateMap<ToDoItemDTO, ToDoItem>();
            });

            return config.CreateMapper();
        }
    }
}