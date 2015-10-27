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
			"Germany",
			"America",
			"Canada",
			"Germany",
			"China",
			"South Africa",
			"Australia",
			"Nepal",
			"Russia",
			"Indonesia",
			"America",
			"England",
			"China",
			"New Zealand",
			"Japan",
			"Japan",
			"England",
			"Russia",
			"America",
			"America"};
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

		public static void InitializeDatabase ()
		{
			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var testingDatabase = new SQLiteConnection (System.IO.Path.Combine(path, "Database.db"));
			testingDatabase.CreateTable<Account> ();
			testingDatabase.CreateTable<Profile> ();
			testingDatabase.CreateTable<Accommodation> ();
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
				profile.Interest = String.Empty;
				profile.Year = years [i];

				accom.ID = i.ToString ();
				accom.Address = String.Empty;
				accom.Suburb = String.Empty;
				accom.RentAWeek = String.Empty;
				accom.PreferredContact = String.Empty;
				accom.Description = String.Empty;
				testingDatabase.Insert (account);
				testingDatabase.Insert (accom);
				testingDatabase.Insert (profile);
			}

		}
	}
}

