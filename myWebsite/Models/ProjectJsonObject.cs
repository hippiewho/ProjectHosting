using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
namespace myWebsite.Models
{
    public class ProjectJsonObject
    {
        private readonly string _projectName = "";
        private readonly string _projectDescription = "";
        private readonly string _projectUrl = "";
        private readonly string _projectImagePath = "";
        private readonly string _projectId = "";
        private readonly IEnumerable<CommitObject> _commits;

        private object _rawJson = "";

        public string ProjectName { get => _projectName; }
        public string ProjectDescription { get => _projectDescription; }
        public string ProjectUrl { get => _projectUrl;}
        public IEnumerable<CommitObject> Commits => _commits;
        public string ProjectImagePath { get => _projectImagePath; }
        public string ProjectId { get => _projectId; }

        public ProjectJsonObject()
        {
            this._rawJson = "";
            this._commits = ParseJson("");
            this._projectName = "";
            this._projectDescription = "";
            this._projectUrl = "";
            this._projectImagePath = "";
            this._projectId = "";
        }

        public ProjectJsonObject(string projectName, string imagePath, string url, int? id)
        {
            this._rawJson = "";
            this._commits = ParseJson("");
            this._projectName = projectName;
            this._projectImagePath = imagePath;
            this._projectUrl = url;
            this._projectId = id.ToString();
        }

        public ProjectJsonObject(string jsonString, string name, string description, string url, string imagePath, int id )
        {
            this._rawJson = jsonString;
            this._commits = ParseJson(jsonString);
            this._projectName = name;
            this._projectDescription = description;
            this._projectUrl = url;
            this._projectImagePath = imagePath;
            this._projectId = id.ToString();
        }

        private IEnumerable<CommitObject> ParseJson(string jsonString)
        {
            List<CommitObject> commits = new List<CommitObject>();
            try
            {
                JArray json = JArray.Parse(jsonString);
                foreach(JToken item in json)
                {
                    commits.Add(new CommitObject(item));
                }
            } catch (Exception e)
            {
                
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