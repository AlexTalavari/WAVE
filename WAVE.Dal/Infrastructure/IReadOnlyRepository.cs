﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace WAVE.Dal.Infrastructure
{
	public interface IReadOnlyRepository<TEntity> where TEntity:class 
	{
		IQueryable<TEntity> All();
		TEntity FindBy(Expression<Func<TEntity, bool>> expression);
		IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);
	}
}