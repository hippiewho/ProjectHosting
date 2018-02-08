using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
namespace myWebsite.Models
{
    public class JsonObject
    {
        public string Author = "";
        public List<CommitObject> commits = new List<CommitObject>();
        private object RawJson = "";

        public JsonObject(string jsonString)
        {
            this.RawJson = jsonString;
            ParseJson(jsonString);
        }

        private void ParseJson(string jsonString)
        {
            JArray json = JArray.Parse(jsonString);
            
            foreach(JToken item in json)
            {
                commits.Add(new CommitObject(item));
            }
        }
    }

    public class CommitObject
    {
        private string _author;
        private string _sha;
        private string _date;
        private string _message;


        public CommitObject(JToken commit)
        {
            _sha = commit["sha"].ToString();
            _author = commit["commit"]["author"]["name"].ToString();
            _date = commit["commit"]["author"]["date"].ToString();
            _message = commit["commit"]["message"].ToString();
        }

        public string Author { get => _author; set => _author = value; }
        public string Sha { get => _sha; set => _sha = value; }
        public string Date { get => _date; set => _date = value; }
        public string Message { get => _message; set => _message = value; }
    }
}