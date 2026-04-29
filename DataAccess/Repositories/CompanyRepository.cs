
using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;


// Sig mig... Laver vi et interface inde i en klasse her ? Altså et nested interface? Hvilken psykopat har valgt dette?
public interface ICompanyRepository
{
    Task<IEnumerable<CustomerCompanyDto>> GetAllAsync();
    Task<CustomerCompanyDto> GetByIdAsync(Guid id);
    Task<CustomerCompanyDto> CreateAsync(CreateCompanyRequest request);
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

    public async Task<CustomerCompanyDto> CreateAsync(CreateCompanyRequest request)
    {
        var company = new CustomerCompany
        {
            CompanyId = Guid.NewGuid(),
            CompanyName = request.CompanyName,
            Cvr = request.Cvr,
            CompanyAddress = request.CompanyAddress,
            CompanyPhoneNumber = request.CompanyPhoneNumber,
            CompanyEmail = request.CompanyEmail
        };

        _context.CustomerCompany.Add(company);
        await _context.SaveChangesAsync();

        return company.ToDto();
    }
    
    public async Task<CustomerCompany?> GetCompanyByEmail(string email)
    {
        return await _context.CustomerCompany
            .FirstOrDefaultAsync(c => c.CompanyEmail == email);
    }
    
    public async Task AddCompany(CustomerCompany company)
    {
        await _context.CustomerCompany.AddAsync(company);
    }
    
}