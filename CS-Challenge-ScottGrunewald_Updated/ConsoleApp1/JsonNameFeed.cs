/** JsonNameFeed Class
 * This class connects to an endpoint and retrieves information; it extends JsonFeed.
 * To promote better encapsulation, this object was made to contain the URL for 
 * the random name API.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace JokeGenerator
{
    class JsonNameFeed : JsonFeed
    {
        readonly string namesUrl = "https://www.names.privserv.com/api/";

        public JsonNameFeed()
        {
            _url = namesUrl;
        }
    }
}
