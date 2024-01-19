using JobApplicationSystem.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationSystem.Repository
{
    //public interface IRepository<T> where T : class, IEntity
    public interface IRepository<T> where T : class
    {
        public Task<ActionResult<IEnumerable<T>>> Get();
        public Task<ActionResult<T>> Create(T entity);
        public Task<IActionResult> Update(int id, T entity);
        public Task<ActionResult<IEnumerable<T>>> GetBySpecialization(string specialization);
        //public Task<ActionResult<IEnumerable<T>>> GetByField(string field);
        public Task<IActionResult> Delete(int id);
    }
}
