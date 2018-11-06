namespace ConsoleApplication1
{
	public abstract class AbstractClass
	{
		public string SayHello(string name)
		{
			return name;
		}

		public abstract string SayHello(string name, string name2);

		private string Surname { get; set; }
	}
}
