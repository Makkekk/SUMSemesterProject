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
            DiscountValidFrom = discountAgreement.DiscountValidFrom,
            DiscountValidTo = discountAgreement.DiscountValidTo
        };
    }

    public static DiscountAgreement ToEntity(this CreateDiscountAgreementDto discountAgreementDto)
    {
        if (discountAgreementDto == null)
        {
            return null;
        }

        return new DiscountAgreement()
        {
            DiscountId = Guid.NewGuid(),
            DiscountPercentage = discountAgreementDto.discountProcentage,
            AgreementDescription = discountAgreementDto.discountDescription,
            CompanyId = discountAgreementDto.CompanyId,
            DiscountValidFrom = discountAgreementDto.StartDate,
            DiscountValidTo = discountAgreementDto.EndDate
        };
    }
}