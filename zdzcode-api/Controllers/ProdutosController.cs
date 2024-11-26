using Microsoft.AspNetCore.Mvc;
using zdzcode_api.Model;
using zdzcode_api.Repository;

namespace zdzcode_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : GeralController<Produtos>
    {
        private readonly ContextBase _context;
        public ProdutosController(ContextBase database)
        {
            _context = database;
        }

        public override ContextBase GetRepository()
        {
            return _context;
        }
    }
}
