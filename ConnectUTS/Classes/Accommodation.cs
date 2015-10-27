using SQLite;

namespace ConnectUTS
{
	public class Accommodation
	{
		[PrimaryKey]
		public string ID { get; set; }
		public string Address { get; set; }
		public string Suburb { get; set; }
		public string RentAWeek { get; set; }
		public string PreferredContact { get; set; }
		public string Description { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Accommodation: ID={0}, Address={1}, Suburb={2}, RentAWeek={3}, PreferredContact={4}, Description={5}]", ID, Address, Suburb, RentAWeek, PreferredContact, Description);
		}
	}
}

