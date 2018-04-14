namespace WebApiApplication
{
	public interface IDbObjectFactory
	{
		TDbObject Create<TDbObject>() where TDbObject : DbObject<TDbObject>;
	}
}
