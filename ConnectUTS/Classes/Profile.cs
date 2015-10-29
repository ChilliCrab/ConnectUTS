using System;
using SQLite;

namespace ConnectUTS
{
	public class Profile
	{
		[PrimaryKey]
		public string StudentID { get; set; }

		public string StudentName { get; set; }
		public string Nationality { get; set; }
		public string ContactNumber { get; set; }
		public string Degree { get; set; }
		public string Year { get; set; }
		public string Interest { get; set; }
		public string AccommodationID { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Profile: StudentID={0}, StudentName={1}, Nationality={2}, ContactNumber={3}, Degree={4}, Year={5}, Interest={6}, AccommodationID={7}]", StudentID, StudentName, Nationality, ContactNumber, Degree, Year, Interest, AccommodationID);
		}

		public int GetRank(Profile user)
		{
			int count = 0;

			if (user.Interest == Interest)
			{
				count += 2;
			}

			if (user.Nationality == Nationality)
			{
				count += 1;
			}

			return count;
		}
	}
}

