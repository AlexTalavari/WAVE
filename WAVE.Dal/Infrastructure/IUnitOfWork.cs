using System;

namespace WAVE.Dal.Infrastructure
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Rollback();
	}
}