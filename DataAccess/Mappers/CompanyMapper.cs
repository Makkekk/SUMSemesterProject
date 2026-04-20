using DTO;
using Models;

namespace DataAcces.Mappers;

public static class CompanyMapper
{
    public static CustomerCompanyDto ToDto(this CustomerCompany company)
    {
        if (company == null)
            return null;

        return new CustomerCompanyDto
        {
            CompanyId = company.CompanyId,
            CompanyName = company.CompanyName,
            Cvr = company.Cvr,
            Adress = company.CompanyAddress,
            // Vi henter procentdelen fra den aktive DiscountAgreement hvis den findes
            CurrentDiscountPercentage = company.DiscountAgreement?.DiscountPercentage
        };
    }
}