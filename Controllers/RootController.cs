using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ListChallengeApi.Models;
using ListChallengeApi.Contracts;

namespace ListChallengeServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RootController : ControllerBase
    {
        private readonly IRepositoryWrapper _repo;
        public RootController(
            IRepositoryWrapper repo
        )
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRootsAsync()
        {
            try
            {
                var roots = await _repo.Root.GetRootsAsync();

                return Ok(roots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error { ex.Message }");
            }
        }
        [HttpGet("{id}/factories")]
        public async Task<IActionResult> GetRootsWithFactoriesAsync(Guid id)
        {
            try
            {
                var root = await _repo.Root.GetRootByIdAsync(id);

                if (root == null)
                {
                    return NotFound();
                }

                var factories = await _repo.Factory.GetAllFactoriesByRootId(root.Id);

                var rootWithFactories = new Root {
                    Id = root.Id,
                    Label = root.Label,
                    Factories = factories.ToList()
                };

                return Ok(rootWithFactories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error { ex.Message }");
            }
        }
        [HttpGet("{id}", Name = "RootById")]
        public async Task<IActionResult> GetRootAsync(Guid id)
        {
            try
            {
                var root = await _repo.Root.GetRootByIdAsync(id);
                return Ok(root);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error { ex.Message }");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoot()
        {
            try
            {
                var root = new Root { Id = Guid.NewGuid() };

                await _repo.Root.CreateRootAsync(root);

                return CreatedAtRoute(routeName: "RootById",
                                    routeValues: new { id = root.Id},
                                    value: root);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error { ex.Message }");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRootById(Guid id)
        {
            try
            {
                var root = await _repo.Root.GetRootByIdAsync(id);

                if (root == null)
                {
                    return NotFound();
                }

                await _repo.Root.DeleteRootAsync(root);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error { ex.Message }");
            }
            
        }
    }
}