namespace Mimbly.Application.Queries.Company.GetById;

using Mimbly.Application.Contracts.Dtos.Company;

public class CompanyFilteredById
{
    public CompanyDto Company { get; set; }

    public CompanyFilteredById() => Company = new CompanyDto();
}