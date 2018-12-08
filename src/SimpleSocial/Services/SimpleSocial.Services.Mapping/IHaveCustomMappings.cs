 using AutoMapper;

namespace SimpleSocial.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }

    //Usage:

    //// public void CreateMappings(IMapperConfigurationExpression configuration)
    ////{
    ////    configuration.CreateMap<Joke, IndexJokeViewModel>().ForMember(x => x.CategoryName, x => x.MapFrom(j => j.Category.Name)); 
    ////}
}