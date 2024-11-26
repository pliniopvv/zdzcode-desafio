using Microsoft.AspNetCore.Mvc;
using zdzcode_api.Model;
using zdzcode_api.Repository;

namespace zdzcode_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : GeralController<Categoria>
    {
        private readonly ContextBase _context;
        public CategoriasController(ContextBase context)
        {
            _context = context;
        }
        public override ContextBase GetRepository()
        {
            return _context;
        }
    }
}
