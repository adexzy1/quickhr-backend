using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Branch;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> CreateAsync(Branch branchModel)
        {
            await _context.Branches.AddAsync(branchModel);
            await _context.SaveChangesAsync();
            return branchModel;
        }

        public async Task<Branch?> DeleteAsync(Guid slug)
        {
            var branchModel = await _context.Branches.FirstOrDefaultAsync(b => b.Slug == slug);
            if (branchModel == null)
            {
                return null;
            }
            _context.Branches.Remove(branchModel);
            await _context.SaveChangesAsync();
            return branchModel;
        }
        public async Task<List<Branch>> GetAllAsync()
        {
            var branches = _context.Branches;
            return await branches.ToListAsync();
        }


        public async Task<Branch?> GetBySlugAsync(Guid slug)
        {
            var branchModel = await _context.Branches.FirstOrDefaultAsync(b => b.Slug == slug);
            if (branchModel == null)
            {
                return null;
            }
            return branchModel;
        }



        public async Task<Branch?> UpdateAsync(Guid slug, UpdateBranchDto branchDto)
        {
            var branchModel = await _context.Branches.FirstOrDefaultAsync(b => b.Slug == slug);
            if (branchModel == null)
            {
                return null;
            }
            branchModel.Name = branchDto.Name;
            _context.Branches.Update(branchModel);
            await _context.SaveChangesAsync();
            return branchModel;
        }
    }
}