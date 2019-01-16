namespace WAVE.Dal.Infrastructure
{
	public interface IIntKeyedRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		TEntity FindBy(int id);
        
	}
}