using SQLite;

namespace ConnectUTS
{
	public class Account
	{
		[PrimaryKey]
		public string StudentID { get; set; }
		public string Password { get; set; }

		public string StudentName { get; set; }
		public string Nationality { get; set; }
		public string Interest { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Account: StudentID={0}, Password={1}, StudentName={2}, Nationality={3}, Interest={4}]", StudentID, Password, StudentName, Nationality, Interest);
		}
	}
}

