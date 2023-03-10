namespace Mimbly.Application.Queries.Mimbox.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxHandler : IRequestHandler<GetByIdMimboxQuery, MimboxByIdVm>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMimboxContactRepository _mimboxContactRepository;
    private readonly IMimboxErrorLogRepository _mimboxErrorLogRepository;
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMimboxLogImageRepository _mimboxLogImageRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxHandler(
        IMimboxRepository mimboxRepository,
        IMimboxLogRepository mimboxLogRepository,
        IMimboxLogImageRepository mimboxLogImageRepository,
        IMimboxContactRepository mimboxContactRepository,
        IMimboxErrorLogRepository mimboxErrorLogRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mimboxLogRepository = mimboxLogRepository;
        _mimboxLogImageRepository = mimboxLogImageRepository;
        _mimboxContactRepository = mimboxContactRepository;
        _mimboxErrorLogRepository = mimboxErrorLogRepository;
        _mapper = mapper;
    }

    public async Task<MimboxByIdVm> Handle(GetByIdMimboxQuery request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        if (mimbox == null)
            throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        var logList = await _mimboxLogRepository.GetMimboxLogsByMimboxId(mimbox.Id);
        var logIds = logList.Select(x => x.Id);
        var logImageList = await _mimboxLogImageRepository.GetMimboxLogImagesByMimboxLogIds(logIds);
        var contactList = await _mimboxContactRepository.GetMimboxContactsByMimboxId(mimbox.Id);
        var errorLogList = await _mimboxErrorLogRepository.GetErrorLogsByMimboxId(mimbox.Id);

        mimbox.ContactList = contactList.ToList();
        mimbox.ErrorLogList = errorLogList.ToList();
        mimbox.LogList = logList.ToList();

        foreach (var log in logList)
        {
            var currentLogImages = logImageList.Where(x => x.MimboxLogId == log.Id).Select(x => x);
            log.ImageList = currentLogImages.ToList();
        }

        var mimboxDto = _mapper.Map<MimboxDto>(mimbox);

        return new MimboxByIdVm { Mimbox = mimboxDto };
    }
}