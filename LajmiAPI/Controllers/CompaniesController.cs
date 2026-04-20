using DataAcces.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompaniesController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerCompanyDto>>> GetCompanies()
    {
        var companies = await _companyRepository.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerCompanyDto>> GetCompany(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null) return NotFound();
        return Ok(company);
    }
}
