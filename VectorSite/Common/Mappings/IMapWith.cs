using AutoMapper;

namespace VectorSite.Common.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            {
                profile.AllowNullCollections = true;
                profile.CreateMap(typeof(T), GetType());
            }
        }
    }
}
