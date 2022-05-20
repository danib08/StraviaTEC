using Microsoft.Extensions.Options;
using MongoAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoAPI.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _commentsCollection;

        public CommentService(
            IOptions<StraviaTECSettings> straviatecDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            straviatecDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                straviatecDatabaseSettings.Value.DatabaseName);

            _commentsCollection = mongoDatabase.GetCollection<Comment>(
                straviatecDatabaseSettings.Value.ActivityCollectionName);
        }

        public async Task<List<Comment>> GetAsync() =>
        await _commentsCollection.Find(_ => true).ToListAsync();

        public async Task<Comment> GetAsync(string actid, string athid) =>
       await _commentsCollection.Find(x => x.ActivityID == actid && x.AthleteID == athid).FirstOrDefaultAsync();


        public async Task CreateAsync(Comment newComment) =>
        await _commentsCollection.InsertOneAsync(newComment);

        public async Task UpdateAsync(string actid, string athid,  Comment updatedComment) =>
            await _commentsCollection.ReplaceOneAsync(x => x.ActivityID == actid &&  x.AthleteID == athid, updatedComment);

        public async Task RemoveAsync(string actid, string athid) =>
            await _commentsCollection.DeleteOneAsync(x => x.ActivityID == actid && x.AthleteID == athid);





    }     

    
}
