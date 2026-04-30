using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IDiscountAgreementRepository
{
    Task<IEnumerable<DiscountAgreementDto>> GetAllDiscountsAsync();
    
    Task<DiscountAgreementDto> SaveDiscountAgreementAsync(CreateDiscountAgreementDto discountAgreement);
}

public class DiscountAgreementRepository : IDiscountAgreementRepository
{
    private readonly LajmiContext _context;

    public DiscountAgreementRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DiscountAgreementDto>> GetAllDiscountsAsync()
    {
        var res = await _context.DiscountAgreement.ToListAsync();

        return res.Select(d => d.ToDto());
    }

    public async Task<DiscountAgreementDto> SaveDiscountAgreementAsync(CreateDiscountAgreementDto discountAgreement)
    {
        // Gemmer ordre i database
        var newDiscountAgreement = discountAgreement.ToEntity();
        
        
     _context.Add(newDiscountAgreement);
     await _context.SaveChangesAsync();
     return newDiscountAgreement.ToDto();
    }
}