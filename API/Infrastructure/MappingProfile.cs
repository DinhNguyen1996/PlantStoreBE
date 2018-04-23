using API.Entities;
using API.Models;
using AutoMapper;

namespace API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          CreateMap<CustomerEntity, CustomerDto>();
            CreateMap<DemoEntity, DemoDto>();
            CreateMap<AccessiblePageEntity, AccessiblePageDto>();
            CreateMap<RoleEntity, RoleDto>();
            CreateMap<UserEntity, UserDto>();
            CreateMap<ProductCategoryEntity, ProductCategoryDto>();
            CreateMap<StorageEntity, StorageDto>();
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<SupplierEntity, SupplierDto>();
            CreateMap<WarehousingEntity, WarehousingDto>().ForMember(x => x.ProductList, y => y.Ignore());
            //CreateMap<WarehousingEntity, WarehousingDto>();
            CreateMap<WarehousingEntity, WarehousinForCreationDto>();
            //CreateMap<char, ProductForWareshousingCreationDto>();
            CreateMap<BillEntity, BillDto>();
            CreateMap<BillEntity, BillForCreationDto>();
        }
    }
}
