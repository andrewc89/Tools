using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;

namespace Tools.WebCrawler
{
    public class WebCrawler
    {
        #region Constructors

        public WebCrawler ()
        {
        }

        /// <summary>
        /// new WebCrawler for domain and directory
        /// </summary>
        /// <param name="Domain">domain to restrict crawling</param>
        /// <param name="Directory">directory from which to start crawling (can == domain)</param>
        public WebCrawler (string Domain, string Directory)
        {
            this.Domain = Domain;
            this.Directory = Directory;
            this.Crawler = new Stack<string>();
            this.Links = new List<string>();
            this.IgnoreList = new List<string>();
        }

        /// <summary>
        /// new Webcrawler for domain and directory
        /// specify username and password for server authentication
        /// </summary>
        /// <param name="Domain">domain to restrict crawling</param>
        /// <param name="Directory">directory from which to start crawling (can == domain)</param>
        /// <param name="UserName">server/network username</param>
        /// <param name="Password">server/network password</param>
        public WebCrawler (string Domain, string Directory, string UserName, string Password)
        {
            this.Domain = Domain;
            this.Directory = Directory;
            this.UserName = UserName;
            this.Password = Password;
            this.Crawler = new Stack<string>();
            this.Links = new List<string>();
            this.IgnoreList = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// crawled domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// directory to crawl
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// network username
        /// </summary>
        private string UserName { get; set; }

        /// <summary>
        /// network password
        /// </summary>
        private string Password { get; set; }

        /// <summary>
        /// stack used for DFS
        /// </summary>
        public Stack<string> Crawler { get; set; }

        /// <summary>
        /// list of links found
        /// </summary>
        public List<string> Links { get; set; }

        /// <summary>
        /// list of strings to ignore in urls
        /// </summary>
        public List<string> IgnoreList { get; set; }

        #endregion

        #region Public Functions 

        /// <summary>
        /// adds strings to ignore list
        /// </summary>
        /// <param name="list"></param>
        public void SetIgnoreList (params string[] list) 
        {
            this.IgnoreList = new List<string>(list);    
        }

        /// <summary>
        /// perform dfs starting at directory
        /// </summary>
        public void Crawl ()
        {
            Crawler.Push(this.Directory);
            while (Crawler.Count > 0)
            {
                string Url = Crawler.Pop();
                if (Url.Contains(Domain))
                {
                    FindLinks(LoadHtml(Url));
                }
            }
        }

        #endregion

        #region Internal Functions

        /// <summary>
        /// given a url, load html contents as string
        /// </summary>
        /// <param name="Url">page url</param>
        /// <returns>html contents of page</returns>
        private string LoadHtml (string Url)
        {
            Debug.WriteLine("Crawling:" + Url);
            try
            {
                var Client = new System.Net.WebClient();
                if (!string.IsNullOrEmpty(UserName))
                {
                    Client.Credentials = new NetworkCredential(UserName, Password);
                }
                return Client.DownloadString(Url);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "";
            }
        }

        /// <summary>
        /// parse page contents, find all links
        /// if not found before: push on stack, add to list
        /// </summary>
        /// <param name="Page">page html contents</param>
        private void FindLinks (string Page)
        {
            HtmlDocument Doc = new HtmlDocument();
            Doc.LoadHtml(Page);
            if (Doc.ParseErrors != null && Doc.ParseErrors.Count() > 0 && Doc.DeclaredEncoding != null && Doc.DeclaredEncoding.Equals(System.Text.Encoding.UTF8))
            {
                foreach (var Error in Doc.ParseErrors)
                {
                    Debug.WriteLine(Error.ToString());
                }
            }
            if (Doc.DocumentNode != null && Doc.DeclaredEncoding != null && Doc.DeclaredEncoding.Equals(System.Text.Encoding.UTF8))
            {
                foreach (var Link in Doc.DocumentNode.Descendants("a")
                    .Select(a => a.GetAttributeValue("href", null))
                    .Where(u => !string.IsNullOrEmpty(u) 
                        && !IgnoreList.Any(s => u.Contains(s))
                        && !Links.Contains((u.StartsWith("/")) ? Domain + u : u)
                        && (u.StartsWith("/") || u.StartsWith(Domain))))                  
                {
                    string Temp = (Link.StartsWith("/")) ? Domain + Link : Link;
                    Links.Add(Temp);
                    Crawler.Push(Temp);                                            
                }
            }                       
        }
        
        #endregion
    }
}