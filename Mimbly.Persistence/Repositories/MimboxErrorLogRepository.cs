﻿namespace Mimbly.Persistence.Repositories;

using Application.Common.Interfaces;
using Dapper;
using Mimbly.Domain.Entities.AzureEvents;

public class MimboxErrorLogRepository : IMimboxErrorLogRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxErrorLogRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task UpdateMimboxErrorLog(MimboxErrorLog mimboxErrorLog)
    {
        var sql =
        @"
            UPDATE Mimbox_Error_Log
            SET Discarded = @Discarded
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxErrorLog);
    }
}