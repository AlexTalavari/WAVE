using System;

namespace WAVE.Dal.Infrastructure
{
	public interface IGuidKeyedReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity:class
	{
		TEntity FindBy(Guid id);
	}
}