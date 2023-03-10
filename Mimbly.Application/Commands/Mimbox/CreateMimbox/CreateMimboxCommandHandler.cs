namespace Mimbly.Application.Commands.Mimbox.CreateMimbox;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxCommandHandler : IRequestHandler<CreateMimboxCommand, Mimbox>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public CreateMimboxCommandHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<Mimbox> Handle(CreateMimboxCommand request, CancellationToken cancellationToken)
    {
        var mimboxEntity = _mapper.Map<Mimbox>(request.CreateMimboxRequest);

        await _mimboxRepository.CreateMimbox(mimboxEntity);

        return mimboxEntity;
    }
}