/** JsonJokeFeed Class
 * This class connects to an endpoint and retrieves information; it extends JsonFeed.
 * To promote better encapsulation, this object was made to contain the URL for 
 * the Chuck Norris API.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace JokeGenerator
{
    class JsonJokeFeed : JsonFeed
    {
        readonly string chuckNorrisUrl = "https://api.chucknorris.io";

        public JsonJokeFeed(int results)
        {
            _url = chuckNorrisUrl;
            numResults = results;
        }

    }
}
