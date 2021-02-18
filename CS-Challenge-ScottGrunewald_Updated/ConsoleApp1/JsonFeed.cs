/** JsonFeed Class
 * This class connects to an endpoint and retrieves information.
 * For specific sites we are creating child objects that inherit from this one.
*/
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    class JsonFeed
    {
        static protected string _url = "";
		static protected int numResults = 0;

		// Text Elements
		static protected readonly string chuckName = "Chuck Norris";
		static protected readonly string randomJokeURL = "jokes/random";
		static protected readonly string categoriesURL = "jokes/categories";
		static protected readonly string categoryEqualsText = "category=";
		static protected readonly string noJokesText = "There are no jokes for that category.";
		static protected readonly string invalidNameText = " INVALID NAME ";

		public JsonFeed() { }
        public JsonFeed(string endpoint, int results)
        {
            _url = endpoint;
			numResults = results;
        }

		public static string[] GetRandomJokes(string firstname, string lastname, string category)
		{
            var client = new HttpClient
            {
                BaseAddress = new Uri(_url)
            };
            string url = randomJokeURL;
			StringBuilder buildUrl = new StringBuilder(url);
			string[] returnValue = new string[numResults];

			// Break flag for the while loop, in case we get no results
			Boolean breakFlag = false;

			if (category != null)
			{
				if (url.Contains('?'))
					buildUrl.Append("&");
				else buildUrl.Append("?");
				buildUrl.Append(categoryEqualsText);
				buildUrl.Append(category);
			}

			int i = 0;

			while (i < numResults && breakFlag == false)
			{
				string joke = "";

				try
				{
					joke = Task.FromResult(client.GetStringAsync(buildUrl.ToString()).Result).Result;
				}
				catch (AggregateException ae)
                {
					// Error message
					Console.WriteLine(noJokesText);
					breakFlag = true;
                }
				// If our query was successful go on to insert the name and add the joke to the return array
				if (breakFlag == false)
				{
					// If there is a random name, sub it in in place of Chuck Norris
					if (firstname != null && lastname != null)
					{
						int index = joke.IndexOf(chuckName);
						// Keep advancing the index to the next instance of "Chuck Norris"
						// In the event that there is no next instance, the result of IndexOf will give
						// a result of -1 and the loop will break.
						while (index > 0)
						{
							// Replace "Chuck Norris" with our random name
							string firstPart = joke.Substring(0, index);
							string secondPart = joke.Substring(0 + index + chuckName.Length, joke.Length - (index + chuckName.Length));
							joke = firstPart + " " + firstname.Trim() + " " + lastname.Trim() + secondPart;
							// Find the index of the next instance of "Chuck Norris"
							index = joke.IndexOf(chuckName, index);
						}
					}
					// Add the new joke to the string array to be returned
					returnValue[i] = JsonConvert.DeserializeObject<dynamic>(joke).value;
				}
				i++;
			}
			// Return all generated jokes
			return returnValue;
		}

		/// <summary>
		/// returns an object that contains name and surname
		/// </summary>
		/// <param name="client2"></param>
		/// <returns></returns>
		public static dynamic GetNames()
		{
            HttpClient client = new HttpClient { BaseAddress = new Uri(_url) };
            var result = client.GetStringAsync("").Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
		}

		public static string[] GetCategories()
		{
            HttpClient client = new HttpClient { BaseAddress = new Uri(_url) };
            return new string[] { Task.FromResult(client.GetStringAsync(categoriesURL).Result).Result };
		}
	}
}
