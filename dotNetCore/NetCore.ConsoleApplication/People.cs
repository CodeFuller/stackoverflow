using System;

namespace NetCore.ConsoleApplication
{
	public class People
	{
		private readonly Func<Type, IPhone> phoneFactory;
		private IPhone _phone;

		public People(Func<Type, IPhone> phoneFactory) => this.phoneFactory = phoneFactory;

		public void Use<TPhone>(Action<IPhone> config) where TPhone : class, IPhone
		{
			_phone = phoneFactory(typeof(TPhone));
			config(this._phone);
		}

		public IPhone GetPhone()
		{
			return this._phone;
		}
	}
}
