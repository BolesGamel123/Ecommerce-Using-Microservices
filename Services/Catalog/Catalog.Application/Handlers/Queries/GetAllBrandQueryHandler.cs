using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IList<BrandResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }
        public async Task<IList<BrandResponseDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAllBrands();

            var brandDtos = _mapper.Map<IList<BrandResponseDto>>(brands.ToList());

            return brandDtos;
        }


    }
}
