using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zdzcode_api.Model;
using zdzcode_api.Model.Dto;
using zdzcode_api.Repository;

namespace zdzcode_api.Controllers
{
    public abstract class GeralController<T> : ControllerBase where T : class, IEntity, new()
    {
        [NonAction]
        public abstract ContextBase GetRepository();

        [HttpGet]
        public virtual ActionResult<PagedResult<T>> List([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var _pageNumber = pageNumber ?? 1;
            var _pageSize = pageSize ?? 10;
            var query = GetRepository()
                            .Set<T>();

            var totalItems = query.Count();
            var list = query
                            .Skip((_pageNumber-1) * _pageSize)
                            .Take(_pageSize)
                            .ToList();
            return new PagedResult<T>()
            {
                PageNumber = _pageNumber,
                PageSize = _pageSize,
                TotalItems = totalItems,
                Items = list
            };
        }

        [HttpGet("{id}")]
        public virtual ActionResult<T> Get(int id)
        {
            return GetRepository().Set<T>().Find(id);
        }
        [HttpPost]
        public virtual ActionResult<T> Post(int id, [FromBody] T model)
        {
            GetRepository().Set<T>().Add(model);
            GetRepository().SaveChanges();
            return model;
        }
        [HttpPut("{id}")]
        public virtual ActionResult<T> Put(int id, [FromBody] T model)
        {
            GetRepository().Set<T>().Update(model);
            GetRepository().SaveChanges();
            return model;
        }
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
            var entity = GetRepository().Set<T>().Find(id);
            GetRepository().Set<T>().Remove(entity);
        }

    }
}
