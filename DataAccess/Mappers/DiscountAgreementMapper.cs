using DTO;
using Models;

namespace DataAcces.Mappers;

public static class DiscountAgreementMapper
{
    public static DiscountAgreementDto ToDto(this DiscountAgreement discountAgreement)
    {
        if (discountAgreement == null)
            return null;

        return new DiscountAgreementDto()
        {
            DiscountId = discountAgreement.DiscountId,
            DiscountPercentage = discountAgreement.DiscountPercentage,
            AgreementDescription = discountAgreement.AgreementDescription,
            CompanyId = discountAgreement.CompanyId,
            CustomerCompany = discountAgreement.CustomerCompany,
            DiscountValidFrom = discountAgreement.DiscountValidFrom,
            DiscountValidTo = discountAgreement.DiscountValidTo
        };
    }
}