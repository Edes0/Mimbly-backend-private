﻿namespace Mimbly.Application.Common.Interfaces;

using Mimbly.Domain.Entities;

public interface ICompanyContactRepository
{
    Task<IEnumerable<CompanyContact>> GetAllCompanyContacts();
    Task<CompanyContact> GetCompanyContactById(Guid id);
    Task CreateCompanyContact(CompanyContact companyContact);
    Task DeleteCompanyContact(CompanyContact companyContact);
    Task UpdateCompanyContact(CompanyContact companyContact);
}