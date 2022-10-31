namespace Mimbly.Application.Commands.Mimbox.CreateMimbox;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimblyCommandHandler : IRequestHandler<CreateMimboxCommand>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public CreateMimblyCommandHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateMimboxCommand request, CancellationToken cancellationToken)
    {
        await request.CreateMimboxRequest.Validate();

        var mimboxEntity = _mapper.Map<Mimbox>(request.CreateMimboxRequest);

        await _mimboxRepository.CreateMimbox(mimboxEntity);

        // This runs a single task. If several entities use Task.WhenAll
        await Task.Run(() => request.CreateMimboxRequest.Validate(), cancellationToken);

        return Unit.Value;
    }
}