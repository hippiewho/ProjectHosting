using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
namespace myWebsite.Models
{
    public class JsonObject
    {
        private string projectName = "";
        private string projectDescription = "";
        private string projectURL = "";
        private string projectImagePath = "";
        private IEnumerable<CommitObject> commits;

        private object RawJson = "";

        public string ProjectName { get => projectName; }
        public string ProjectDescription { get => projectDescription; }
        public string ProjectURL { get => projectURL;}
        public IEnumerable<CommitObject> Commits { get => commits;}
        public string ProjectImagePath { get => projectImagePath; }

        public JsonObject(string jsonString)
        {
            this.RawJson = jsonString;
            this.commits = ParseJson(jsonString);
        }
    
        public JsonObject(string jsonString, string name, string description, string url, string imagePath )
        {
            this.RawJson = jsonString;
            this.commits = ParseJson(jsonString);
            this.projectName = name;
            this.projectDescription = description;
            this.projectURL = url;
            this.projectImagePath = imagePath;
        }

        private IEnumerable<CommitObject> ParseJson(string jsonString)
        {
            List<CommitObject> commits = new List<CommitObject>();
            JArray json = JArray.Parse(jsonString);
            
            foreach(JToken item in json)
            {
                commits.Add(new CommitObject(item));
            }
            return commits;
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