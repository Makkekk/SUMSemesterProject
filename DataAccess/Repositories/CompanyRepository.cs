
using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<CustomerCompanyDto>> GetAllAsync();
    Task<CustomerCompanyDto> GetByIdAsync(Guid id);
}

public class CompanyRepository : ICompanyRepository
{
    private readonly LajmiContext _context;

    public CompanyRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerCompanyDto>> GetAllAsync()
    {
        var companies = await _context.CustomerCompany.Include(c => c.DiscountAgreement).ToListAsync();
        return companies.Select(c => c.ToDto());
    }

    public async Task<CustomerCompanyDto> GetByIdAsync(Guid id)
    {
        var company = await _context.CustomerCompany.Include(c => c.DiscountAgreement).FirstOrDefaultAsync(c => c.CompanyId == id);

        return company?.ToDto();
    }
}