using AutoMapper;
using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.Infrastructure.Data;
using CleanCode.Demo.Infrastructure.DomainEvents;
using CleanCode.Demo.WebAPI.Mappers;
using CleanCode.Demo.WebAPI.Models;
using StructureMap;
using System.Data.Entity;

namespace CleanCode.Demo.WebAPI.App_Start
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                }
            );

            For<IDomainEventDispatcher>().Use<DomainEventDispatcher>();
            For(typeof(IRepository<>)).Use(typeof(EfRepository<>));
            For<IMapper>().Use(_ => AutoMapperConfig.GetMapper());
            For<DbContext>().Use<ToDoItemContext>();
        }
    }
}