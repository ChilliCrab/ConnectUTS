using SQLite;

namespace ConnectUTS
{
	public class Account
	{
		[PrimaryKey]
		public string StudentID { get; set; }
		public string Password { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Account: StudentID={0}, Password={1}]", StudentID, Password);
		}
	}
}

