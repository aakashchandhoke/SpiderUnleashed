using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace SpiderUnleashed {

   
    class LinksCrawler {

        string _search;
        string _site;
        string _fileType;
        int _numberOfLinks;
        WebProxy _webProxy;

        public LinksCrawler() {

            _search = "";
            _site = null;
            _numberOfLinks = 10;
            _fileType = null;
            _webProxy = null;

        }

        public string SearchString {

            set {
                _search = value;
            }

        }

        public string SearchSite {

            set {
                _site = value;
            }

        }

        public string FileType {

            set {
                _fileType = value;
            }

        }

        public int NumberOfLinks {

            set {
                _numberOfLinks = value;
            }

        }

        public WebProxy Proxy {

            set {
                _webProxy = value;
            }

        }

        public string[] GetResults() {

            StringBuilder source = new StringBuilder();
            StringBuilder query = new StringBuilder("https://www.google.co.in/search?q=");

            query.Append(_search);

            int i = 0;
            for (i = 0; i < _numberOfLinks; i += 10)  {
                //Console.WriteLine(i);
                if (_site != null) {
                    query.Append("+site%3A").Append(_site);
                }

                if (_fileType != null) {
                    query.Append("+filetype%3A").Append(_fileType);
                }

                query.Append("&start=").Append(i);

                WebRequest req = HttpWebRequest.Create(query.ToString().Replace(' ', '+'));
                req.Proxy = _webProxy;
                req.Method = "GET";

                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream())) {
                    string temp = reader.ReadToEnd();
                    Debug.WriteLine("retrived " + i );
                    source.Append(Uri.UnescapeDataString(temp));
                }

            }

            MatchCollection matchList = Regex.Matches(source.ToString(), "<h3 class=\"r\"><a href=\"/url(.|\n)*?</h3>");

            string[] results = matchList.Cast<Match>().Select(match => match.Value).ToArray();

            for (i = 0; i < results.Length; i++) {
                results[i] = results[i].Substring(30);
                results[i] = results[i].Substring(0, results[i].IndexOf('&'));
            }

            return results;

        }


    }
}
