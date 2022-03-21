using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;
namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetrics, CpuMetricsDto>();
            CreateMap<DotNetMetrics, DotNetMetrics>();
            CreateMap<HddMetrics, HddMetricsDto>();
            CreateMap<NetworkMetrics, NetworkMetricsDto>();
            CreateMap<RamMetrics, RamMetricsDto>();
        }
    }
}