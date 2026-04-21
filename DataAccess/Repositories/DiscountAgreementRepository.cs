using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Repositories;

public interface IDiscountAgreementRepository
{
    Task<IEnumerable<DiscountAgreementDto>> GetAllDiscountsAsync();
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
        var res = await _context.DiscountAgreement.Include(d => d.CustomerCompany).ToListAsync();

        return res.Select(d => d.ToDto());
    }
}