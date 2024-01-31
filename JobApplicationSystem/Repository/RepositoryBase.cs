using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace JobApplicationSystem.Repository
{
    //public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class, IEntity
    public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected DbSet<T> dbSet;
        private readonly IUnitOfwork _unitOfWork;

        public RepositoryBase(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            dbSet = _unitOfWork.Context.Set<T>();
        }

        //Get Request
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var data = await dbSet.ToListAsync();
            return Ok(data);
        }

        //Create Request
        public async Task<ActionResult<T>> Create(T entity)
        {
            dbSet.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        //GetBySpecialization
        public async Task<ActionResult<IEnumerable<T>>> GetBySpecialization(string specialization)
        {
            var entities = await dbSet.Where(e => EF.Property<string>(e, "Specialization") == specialization)
                .Include(e => EF.Property<string>(e, "Company"))
                .ToListAsync();
            if(!entities.Any())
            {
                return NotFound();
            }

            return Ok(entities);
        }

        ////GetByField

        //public async Task<ActionResult<IEnumerable<T>>> GetByField(string field)
        //{
        //    var entities = await dbSet.Where(e => EF.Property<string>(e, "Field") == field)
        //        .Include(e => EF.Property<string>(e, "Company"))
        //        .ToListAsync();
        //    if (!entities.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(entities);
        //}



        //Update Request
        public async Task<IActionResult> Update(int id, T entity)
        {
            
            var existingOrder = await dbSet.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _unitOfWork.Context.Entry(existingOrder).CurrentValues.SetValues(entity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        //Delete Request
        public async Task<IActionResult> Delete(int id)
        {
            var data = await dbSet.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

       
    }
}
