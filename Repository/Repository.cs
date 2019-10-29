using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ListChallengeApi.Contracts;
using ListChallengeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ListChallengeApi.Repository
{
    public class ChildRepository : RepositoryBase<Child>, IChildRepository
    {
        public ChildRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
        public async Task<Child> GetChildByIdAsync(Guid id)
        {
            return await FindByCondition(child => child.Id.Equals(id))
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Child>> GetAllChildValuesByFactoryIdAsync(Guid id)
        {
            return await FindAll().Where(child => child.FactoryId.Equals(id))
                .ToListAsync();
        }
        public async Task<IEnumerable<Child>> GetAllChildAsync()
        {
            return await FindAll().ToListAsync();
        }
        public async Task CreateChildInBulkAsync(List<Child> childs)
        {
            foreach (var child in childs)
            {
                Create(child);
            }
            await SaveAsync();
        }
        public async Task CreateChildAsync(Child child)
        {
            Create(child);
            await SaveAsync();
        }
        public async Task DeleteAllChildAsync(IEnumerable<Child> child)
        {
            DeleteAll(child);
            await SaveAsync();
        }
        public async Task DeleteChildAsync(Child child)
        {
            Delete(child);
            await SaveAsync();
        }
    }
    public class FactoryRepository : RepositoryBase<Factory>,IFactoryRepository
    {
        public FactoryRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
        public async Task CreateFactoryAsync(Factory factory)
        {
        Create(factory);
        await SaveAsync();
        }
        public async Task UpdateFactoryAsync(Factory factory)
        {
        Update(factory);
        await SaveAsync();
        }
        public async Task DeleteFactoryAsync(Factory factory)
        {
        Delete(factory);
        await SaveAsync();
        }
        public async Task<IEnumerable<Factory>> GetAllFactoriesAsync()
        {
        return await FindAll().ToListAsync();
        }
        public async Task<IEnumerable<Factory>> GetAllFactoriesByRootId(Guid id)
        {
        return await FindAll().Where(factory => factory.RootId.Equals(id))
            .ToListAsync();
        }
        public async Task<Factory> GetFactoryByIdAsync(Guid id)
        {
        return await FindByCondition(factory => factory.Id.Equals(id))
            .SingleOrDefaultAsync();
        }
    }
    public class RootRepository : RepositoryBase<Root>, IRootRepository
    {
        public RootRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
        public async Task<IEnumerable<Root>> GetRootsAsync()
        {
            return await FindAll().ToListAsync();
        }
        public async Task<Root> GetRootByIdAsync(Guid id)
        {
            return await FindByCondition(root => root.Id.Equals(id))
                .FirstOrDefaultAsync();
        }
        public async Task CreateRootAsync(Root root)
        {
            Create(root);
            await SaveAsync();
        }
        public async Task DeleteRootAsync(Root root)
        {
            Delete(root);
            await SaveAsync();
        }
    }
     public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression);
        }
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
        public void DeleteAll(IEnumerable<T> entity)
        {
            foreach (var e in entity)
            {
                this.RepositoryContext.Set<T>().Attach(e);
                this.RepositoryContext.Set<T>().Remove(e);
            }
        }
        public async Task SaveAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private IRootRepository _root;
        private IFactoryRepository _factory;
        private IChildRepository _child;
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IRootRepository Root
        {
            get
            {
                if (_root == null)
                {
                    _root = new RootRepository(_repoContext);
                }
                return _root;
            }
        }
        public IFactoryRepository Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new FactoryRepository(_repoContext);
                }
                return _factory;
            }
        }
        public IChildRepository Child
        {
            get
            {
                if (_child == null)
                {
                    _child = new ChildRepository(_repoContext);
                }
                return _child;
            }
        }
        public void Save() {
            _repoContext.SaveChanges();
        }
    }
}