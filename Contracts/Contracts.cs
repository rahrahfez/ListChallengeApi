using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ListChallengeApi.Models;

namespace ListChallengeApi.Contracts
{
	public interface IChildRepository
	{
		Task<IEnumerable<Child>> GetAllChildAsync();
		Task<IEnumerable<Child>> GetAllChildValuesByFactoryIdAsync(Guid id);
		Task<Child> GetChildByIdAsync(Guid id);
		Task CreateChildAsync(Child child);
		Task CreateChildInBulkAsync(List<Child> childs);
		Task DeleteAllChildAsync(IEnumerable<Child> child);
		Task DeleteChildByFactoryId(Guid id);
		Task DeleteChildAsync(Child child);
	}
	public interface IFactoryRepository
	{
		Task<IEnumerable<Factory>> GetAllFactoriesAsync();
		Task<IEnumerable<Factory>> GetAllFactoriesByRootId(Guid id);
		Task<Factory> GetFactoryByIdAsync(Guid id);
		Task CreateFactoryAsync(Factory factory);
		Task UpdateFactoryAsync(Factory factory);
		Task DeleteFactoryAsync(Factory factory);
	}
	public interface IRootRepository
	{
		Task<Root> GetRootByIdAsync(Guid id);
		Task<IEnumerable<Root>> GetRootsAsync();
		Task CreateRootAsync(Root root);
		Task DeleteRootAsync(Root root);
	}
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expressions);
		IQueryable<T> FindAll();
		void Create(T entity);
		void DeleteAll(IEnumerable<T> entity);
		void Delete(T entity);
	}
	public interface IRepositoryWrapper
	{
		IRootRepository Root { get; }
		IFactoryRepository Factory { get; }
		IChildRepository Child { get; }
		void Save();
	}
}