using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllTypeQueryHandler : IRequestHandler<GetAllTypeQuery, IList<TypeResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITypeRepository _typeRepository;

        public GetAllTypeQueryHandler(IMapper mapper, ITypeRepository typeRepository)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
        }
        public async Task<IList<TypeResponseDto>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeRepository.GetAllTypes();

            var typeDtos = _mapper.Map<IList<TypeResponseDto>>(types.ToList());

            return typeDtos;
        }
    }
}
