using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListChallengeApi.Contracts;
using ListChallengeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListChallengeApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ChildController : ControllerBase
	{
		private readonly IRepositoryWrapper _repo;
		public ChildController(IRepositoryWrapper repo)
		{
			_repo = repo;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllChildAsync()
		{
			try
			{
				var childs = await _repo.Child.GetAllChildAsync();

				return Ok(childs);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error { ex.Message }");
			}
		}
		[HttpGet("{id})", Name = "ChildById")]
		public async Task<IActionResult> GetChildByIdAsync(Guid id)
		{
			try
			{
					var child = await _repo.Child.GetChildByIdAsync(id);

					if (child == null)
					{
							return NotFound();
					}

					return Ok(child);
			}
			catch (Exception ex)
			{
					return StatusCode(500, $"Internal Server Error { ex.Message }");
			}
		}
		[HttpGet("{id}/values")]
		public async Task<IActionResult> GetAllChildValuesByFactoryId(Guid id)
		{
				try
				{
						var childValues = await _repo.Child.GetAllChildValuesByFactoryIdAsync(id);

						return Ok(childValues);
				}
				catch (Exception ex)
				{
						return StatusCode(500, $"Internal Server Error { ex.Message }");
				}
		}
		[HttpPost]
		public async Task<IActionResult> CreateChildAsync(Child child)
		{
				try
				{
						var childToBeCreated = new Child {
								Id = Guid.NewGuid(),
								FactoryId = child.FactoryId,
								Value = child.Value
						};

						await _repo.Child.CreateChildAsync(childToBeCreated);

						return CreatedAtRoute(
								routeName: "ChildById", 
								routeValues: new { id = childToBeCreated.Id }, 
								value: childToBeCreated);
				}
				catch (Exception ex)
				{
						return StatusCode(500, $"Internal Server Error { ex.Message }");
				}
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteAllChildAsync(IEnumerable<Child> child)
		{
				try
				{
						await _repo.Child.DeleteAllChildAsync(child);

						return NoContent();
				}
				catch (Exception ex)
				{
						return StatusCode(500, $"Internal Server Error { ex.Message }");
				}
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteChildAsync(Child child)
		{
				try
				{
						if (child == null)
						{
								return NotFound();
						}

						await _repo.Child.DeleteChildAsync(child);

						return NoContent();
				}
				catch (Exception ex)
				{
						return StatusCode(500, $"Internal Server Error { ex.Message }");
				}
		}
	}
}