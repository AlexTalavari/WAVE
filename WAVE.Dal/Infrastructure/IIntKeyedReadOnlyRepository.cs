﻿namespace WAVE.Dal.Infrastructure
{
	public interface IIntKeyedReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
	{
		TEntity FindBy(int id);
	}
}