using System;
using System.Collections.Generic;

using SQLite;

namespace ConnectUTS
{
	public class DatabaseSetup
	{
		public static string[] studentIDs = {
			"12567564",
			"12608099",
			"12001011",
			"12315007",
			"12345678",
			"12211221",
			"12344321",
			"12221111",
			"12536666",
			"12340001",
			"12345000",
			"12564869",
			"12479345",
			"12408077",
			"12849404",
			"12121212",
			"12874596",
			"12625485",
			"12115577",
			"12010988" };
		public static string[] passwords = {
			"abc@def",
			"hahalol",
			"sneakypassword",
			"EzPzLemonSqueezy",
			"complexone",
			"simpleone",
			"mediumone",
			"hello2016",
			"cya2015",
			"iLoveSEP",
			"iDontloveit",
			"forthetest",
			"utsstudent",
			"seemyearphone",
			"xamarin",
			"sourcetree",
			"launchpad",
			"OSX10.1",
			"windows10",
			"iphone6s"};
		public static string[] names = {
			"John",
			"Neil",
			"Peter",
			"Suka",
			"Kidd",
			"Alan",
			"Max",
			"Deni",
			"Bucky",
			"Mayrond",
			"Cris",
			"Harry",
			"Matt",
			"Luke",
			"Dale",
			"Aki",
			"Hofman",
			"Natasha",
			"Bill",
			"Jobs"};
		public static string[] nationalities = {
			"German",
			"American",
			"Canadian",
			"German",
			"Chinese",
			"South African",
			"Australian",
			"Nepalese",
			"Russian",
			"Indonesian",
			"American",
			"English",
			"Chinese",
			"New Zealander",
			"Japanese",
			"Japanese",
			"English",
			"Russian",
			"American",
			"American"};
		public static string[] contactNums = {
			"456789876",
			"423432234",
			"430801126",
			"450917899",
			"405837795",
			"481395404",
			"416859296",
			"426354656",
			"423816701",
			"423100932",
			"440987323",
			"423487654",
			"438232222",
			"412245345",
			"409888772",
			"420029981",
			"423810086",
			"415412315",
			"430110114",
			"414494504"};
		public static string[] degrees = {
			"Bachelor of Arts",
			"Bachelor of Business",
			"Master of Science",
			"Bachelor of Education",
			"Master of Design",
			"Bachelor of Economics",
			"Master of Laws",
			"Master of Busines",
			"Bachelor of Engineering",
			"Bachelor of Laws",
			"Bachelor of Communication",
			"Master of Arts",
			"Bachelor of Business",
			"Master of Laws",
			"Bachelor of Engineering",
			"Bachelor of Business",
			"Bachelor of Arts",
			"Master of Science",
			"Bachelor of Engineering",
			"Bachelor of Business"};
		public static string[] years = {
			"3",
			"1",
			"1",
			"2",
			"1",
			"3",
			"2",
			"1",
			"2",
			"1",
			"3",
			"1",
			"3",
			"2",
			"4",
			"1",
			"1",
			"2",
			"4",
			"2"};
		public static string[] interests = {
			"Animals",
			"Art",
			"Cooking",
			"Fashion",
			"Games",
			"Cooking",
			"Art",
			"Technology",
			"Technology",
			"Travel",
			"Sports",
			"Technology",
			"Sports",
			"Art",
			"Cars",
			"Health",
			"Politics",
			"TV",
			"Animals",
			"Cooking"
		};

		public static string[] addresses = {
			"12 this st",
			"24 that st",
			"82 the st",
			"99 abc avenue",
			"52 the st",
			"12 hahah st",
			"876 lol st",
			"123 cde avenue",
			"243 ez st",
			"112 pz st",
			"101 zzz avenue",
			"20 lemon st",
			"2 jugsbeer avenue",
			"45 jugsbeer st",
			"119 macca st",
			"454 bigmac avenue",
			"123 newbee st",
			"898 broadway st",
			"15 haha avenue",
			"558 nothing st"};
		public static string[] suburbs = {
			"Burwood",
			"Ultimo",
			"Strathfield",
			"Epping",
			"Zetland",
			"Redfern",
			"Newtown",
			"Burwood",
			"Epping",
			"Redfern",
			"Strathfield",
			"Redfern",
			"Burwood",
			"Zetland",
			"Epping",
			"Strathfield",
			"Burwood",
			"Zetland",
			"Redfern",
			"Ultimo"
		};
		public static string[] rents = {
			"150",
			"280",
			"195",
			"200",
			"250",
			"200",
			"220",
			"180",
			"210",
			"300",
			"210",
			"190",
			"190",
			"240",
			"230",
			"190",
			"260",
			"280",
			"200",
			"340"
		};
		public static string[] contacts = {
			"456789876",
			"423432234",
			"430801126",
			"450917899",
			"405837795",
			"481395404",
			"416859296",
			"426354656",
			"423816701",
			"423100932",
			"440987323",
			"423487654",
			"438232222",
			"412245345",
			"409888772",
			"420029981",
			"423810086",
			"415412315",
			"430110114",
			"414494504"
		};
		public static string[] descriptions = { 
			"shared with 1 boy",
			"n/a",
			"share bill",
			"n/a",
			"girls only",
			"n/a",
			"share bill",
			"n/a",
			"share bill",
			"master room",
			"single room",
			"girls only",
			"$10 extra fee for internet",
			"singe room girls only",
			"share bill",
			"share bill",
			"n/a",
			"280 for 1, 300 for 2",
			"share bill",
			"master room"};

		public static string InitializeDatabase ()
		{
			string message = "Database initatialization done";
				string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				var testingDatabase = new SQLiteConnection (System.IO.Path.Combine (path, "Database.db"));
				testingDatabase.CreateTable<Account> ();
				testingDatabase.CreateTable<Profile> ();
				testingDatabase.CreateTable<Accommodation> ();
			var stuList = testingDatabase.Query<Account> ("SELECT * FROM Account");
			if (stuList.Count == 0) { 
				for (int i = 0; i < studentIDs.Length; i++) {
					Account account = new Account ();
					Profile profile = new Profile ();
					Accommodation accom = new Accommodation ();
					account.StudentID = studentIDs [i];
					account.Password = passwords [i];

					profile.StudentID = studentIDs [i];
					profile.StudentName = names [i];
					profile.Nationality = nationalities [i];
					profile.ContactNumber = contactNums [i];
					profile.Degree = degrees [i];
					profile.Interest = interests[i];
					profile.Year = years [i];
					profile.AccommodationID = i.ToString ();

					accom.ID = i.ToString ();
					accom.Address = addresses [i];
					accom.Suburb = suburbs [i];
					accom.RentAWeek = rents [i];
					accom.PreferredContact = contacts [i];
					accom.Description = descriptions [i];
					testingDatabase.Insert (account);
					testingDatabase.Insert (accom);
					testingDatabase.Insert (profile);
				}
			} else {
				message = "Database already set up";
			}
			return message;
		}

		public static string ResetDatabase()
		{
			string message = "Reset Done";
			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var testingDatabase = new SQLiteConnection (System.IO.Path.Combine (path, "Database.db"));
			testingDatabase.Query<Profile> ("DELETE FROM Profile");
			testingDatabase.Query<Account> ("DELETE FROM Account");
			testingDatabase.Query<Accommodation> ("DELETE FROM Accommodation");
			return message;
		}

		public static string CheckDatabase()
		{
			string message = "";
			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var testingDatabase = new SQLiteConnection (System.IO.Path.Combine (path, "Database.db"));
			var stuList = testingDatabase.Query<Account> ("SELECT * FROM Account");
			if (stuList.Count != 0) {

				message = "there are " + stuList.Count.ToString () + " students registered";

			} else {
				message = "database is empty";
			}
			return message;
		}
	}
}

