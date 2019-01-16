using System;

namespace WAVE.Dal.Infrastructure
{
	public interface IGuidKeyedRepository<TEntity> : IRepository<TEntity> where TEntity:class 
	{
		TEntity FindBy(Guid id);
	}
}