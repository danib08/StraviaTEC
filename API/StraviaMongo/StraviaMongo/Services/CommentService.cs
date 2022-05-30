using MongoDB.Driver;
using StraviaMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaMongo.Services
{
    public class CommentService
    {
        private IMongoCollection<Comment> _comments;

        public CommentService(IStraviaSettings settings)
        {
            var cliente = new MongoClient(settings.Server);
            var database = cliente.GetDatabase(settings.Database);
            _comments = database.GetCollection<Comment>(settings.Collection);
        }

        public List<Comment> Get()
        {
            return _comments.Find(d => true).ToList();
        }

        public Comment Create(Comment comment)
        {
            _comments.InsertOne(comment);
            return comment;
        }

        public void Update(string id, Comment comment)
        {
            _comments.ReplaceOne(comment => comment.Id == id, comment);
        }

        public void Delete(string id)
        {
            _comments.DeleteOne(d => d.Id == id);
        }




    }
}
