//using DashboardRabitaBank.Controllers;
//using DashboardRabitaBank.Models;
//using DashboardRabitaBank.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace DashboardRabitaBank.Controllers
//{
//    public class FacebookController : Controller
//    {
//        private readonly FacebookService _service;
//        public FacebookController(FacebookService service)
//        {
//            _service = service ?? throw new ArgumentNullException(nameof(service));
//        }
//        public IActionResult RabitaMobile()
//        {
//            try
//            {
//                var reviews = _service.GetRabitaBankMobile();

//                Console.WriteLine($"Posts count: {reviews?.Count ?? 0}");

//                if (reviews == null)
//                {
//                    reviews = new List<FacebookPost>();
//                }

//                return View(reviews);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//                return View(new List<FacebookPost>());
//            }
//        }
//    }
//}


using DashboardRabitaBank.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace RabitaBank.Controllers
{
    public class FacebookController : Controller
    {
        private readonly IMongoCollection<BsonDocument> _postsCollection;
        private static bool _isMappingRegistered = false;

        public FacebookController(IConfiguration configuration)
        {
            // Facebook MongoDB connection məlumatları
            var connectionString = configuration["FacebookMongoSettings:ConnectionString"];
            var databaseName = configuration["FacebookMongoSettings:DatabaseName"];
            var collectionName = configuration["FacebookMongoSettings:RabitaBankCollectionFacebookName"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Facebook MongoDB connection string tapılmadı!");
            }

            // Register mapping once
            if (!_isMappingRegistered)
            {
                RegisterClassMaps();
                _isMappingRegistered = true;
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _postsCollection = database.GetCollection<BsonDocument>(collectionName);
        }

        private void RegisterClassMaps()
        {
            try
            {
                if (!BsonClassMap.IsClassMapRegistered(typeof(FacebookComment)))
                {
                    BsonClassMap.RegisterClassMap<FacebookComment>(cm =>
                    {
                        cm.AutoMap();
                        cm.SetIgnoreExtraElements(true);
                    });
                }

                if (!BsonClassMap.IsClassMapRegistered(typeof(Analysis)))
                {
                    BsonClassMap.RegisterClassMap<Analysis>(cm =>
                    {
                        cm.AutoMap();
                        cm.SetIgnoreExtraElements(true);
                    });
                }
            }
            catch
            {
                // Already registered
            }
        }

        public async Task<IActionResult> Comments()
        {
            try
            {
                // Bütün postları BsonDocument olaraq çək
                var postDocuments = await _postsCollection.Find(_ => true).ToListAsync();

                // Debug üçün
                ViewBag.PostCount = postDocuments?.Count ?? 0;

                // Bütün kommentləri topla
                var allComments = new List<FacebookComment>();

                foreach (var doc in postDocuments)
                {
                    var postUrl = doc.Contains("url") ? doc["url"].AsString : "";
                    var postText = doc.Contains("text") ? doc["text"].AsString : "";

                    if (doc.Contains("comments") && doc["comments"].IsBsonArray)
                    {
                        var commentsArray = doc["comments"].AsBsonArray;

                        foreach (var commentDoc in commentsArray)
                        {
                            if (commentDoc.IsBsonDocument)
                            {
                                var comment = ParseComment(commentDoc.AsBsonDocument, postUrl, postText);
                                allComments.Add(comment);
                            }
                        }
                    }
                }

                // Debug üçün
                ViewBag.CommentCount = allComments.Count;

                // Sentiment statistikası hesabla
                var statistics = new SentimentStatistics
                {
                    PositiveCount = allComments.Count(c => c.Analysis?.Sentiment?.ToLower() == "positive"),
                    NegativeCount = allComments.Count(c => c.Analysis?.Sentiment?.ToLower() == "negative"),
                    NeutralCount = allComments.Count(c => c.Analysis?.Sentiment?.ToLower() == "neutral"),
                    TotalCount = allComments.Count
                };

                ViewBag.Statistics = statistics;

                // Son commentləri əvvəl göstər
                return View(allComments.OrderByDescending(c => c.CommentDate).ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Xəta baş verdi: {ex.Message}";
                ViewBag.ErrorDetails = ex.StackTrace;
                return View(new List<FacebookComment>());
            }
        }

        private FacebookComment ParseComment(BsonDocument doc, string postUrl = "", string postText = "")
        {
            var comment = new FacebookComment
            {
                Id = GetStringValue(doc, "id"),
                _Id = GetStringValue(doc, "_id"),
                UserName = GetStringValue(doc, "userName"),
                CommentText = GetStringValue(doc, "commentText"),
                CommentDate = GetStringValue(doc, "commentDate"),
                FacebookUrl = GetStringValue(doc, "facebookUrl"),
                PostUrl = GetStringValue(doc, "postUrl"),
                ReactionCount = GetStringValue(doc, "reactionCount"),
                UserProfileImageUrl = GetStringValue(doc, "userProfileImageUrl"),
                FeedbackId = GetStringValue(doc, "feedbackId"),
                UserGender = GetStringValue(doc, "userGender"),
                UserUrl = GetStringValue(doc, "userUrl"),
                UserId = GetStringValue(doc, "userId"),
                UserIsVerified = GetBoolValue(doc, "userIsVerified"),
                UserSubscribeStatus = GetStringValue(doc, "userSubscribeStatus"),
                ScrapedAt = GetStringValue(doc, "_scrapedAt")
            };

            // Post məlumatlarını əlavə et
            if (string.IsNullOrEmpty(comment.FacebookUrl))
            {
                comment.FacebookUrl = !string.IsNullOrEmpty(comment.PostUrl) ? comment.PostUrl : postUrl;
            }

            // Parse analysis
            if (doc.Contains("analysis") && doc["analysis"].IsBsonDocument)
            {
                var analysisDoc = doc["analysis"].AsBsonDocument;
                comment.Analysis = new Analysis
                {
                    Done = GetBoolValue(analysisDoc, "done"),
                    Sentiment = GetStringValue(analysisDoc, "sentiment"),
                    Importance = GetStringValue(analysisDoc, "importance"),
                    IsAlarm = GetBoolValue(analysisDoc, "is_alarm"),
                    Confidence = GetDoubleValue(analysisDoc, "confidence"),
                    Purpose = GetStringValue(analysisDoc, "purpose"),
                    Relevance = GetDoubleValue(analysisDoc, "relevance"),
                    ProcessedAt = GetStringValue(analysisDoc, "processedAt")
                };

                // Parse categories
                if (analysisDoc.Contains("categories") && analysisDoc["categories"].IsBsonArray)
                {
                    comment.Analysis.Categories = analysisDoc["categories"].AsBsonArray
                        .Select(c => c.AsString)
                        .ToArray();
                }
            }

            return comment;
        }

        private string GetStringValue(BsonDocument doc, string fieldName)
        {
            try
            {
                return doc.Contains(fieldName) && !doc[fieldName].IsBsonNull
                    ? doc[fieldName].AsString
                    : null;
            }
            catch
            {
                return null;
            }
        }

        private bool GetBoolValue(BsonDocument doc, string fieldName)
        {
            try
            {
                return doc.Contains(fieldName) && !doc[fieldName].IsBsonNull
                    ? doc[fieldName].AsBoolean
                    : false;
            }
            catch
            {
                return false;
            }
        }

        private double GetDoubleValue(BsonDocument doc, string fieldName)
        {
            try
            {
                if (!doc.Contains(fieldName) || doc[fieldName].IsBsonNull)
                    return 0;

                var value = doc[fieldName];
                if (value.IsDouble)
                    return value.AsDouble;
                if (value.IsInt32)
                    return value.AsInt32;
                if (value.IsInt64)
                    return value.AsInt64;
                if (value.IsBsonDocument && value.AsBsonDocument.Contains("$numberDouble"))
                    return double.Parse(value.AsBsonDocument["$numberDouble"].AsString);

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSentimentStats()
        {
            try
            {
                var postDocuments = await _postsCollection.Find(_ => true).ToListAsync();
                var allComments = new List<FacebookComment>();

                foreach (var doc in postDocuments)
                {
                    if (doc.Contains("comments") && doc["comments"].IsBsonArray)
                    {
                        var commentsArray = doc["comments"].AsBsonArray;
                        foreach (var commentDoc in commentsArray)
                        {
                            if (commentDoc.IsBsonDocument)
                            {
                                allComments.Add(ParseComment(commentDoc.AsBsonDocument, "", ""));
                            }
                        }
                    }
                }

                var statistics = new
                {
                    positive = allComments.Count(c => c.Analysis?.Sentiment?.ToLower() == "positive"),
                    negative = allComments.Count(c => c.Analysis?.Sentiment?.ToLower() == "negative"),
                    neutral = allComments.Count(c => c.Analysis?.Sentiment?.ToLower() == "neutral")
                };

                return Json(statistics);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<IActionResult> Posts()
        {
            try
            {
                var postDocuments = await _postsCollection.Find(_ => true).ToListAsync();
                var posts = new List<FacebookPost>();

                foreach (var doc in postDocuments)
                {
                    var post = new FacebookPost
                    {
                        _id = GetStringValue(doc, "_id"),
                        PostId = GetStringValue(doc, "postId"),
                        Text = GetStringValue(doc, "text"),
                        Url = GetStringValue(doc, "url"),
                        TopLevelUrl = GetStringValue(doc, "topLevelUrl"),
                        FacebookUrl = GetStringValue(doc, "facebookUrl"),
                        PageName = GetStringValue(doc, "pageName"),
                        ScrapedAt = GetStringValue(doc, "_scrapedAt")
                    };

                    // Time
                    if (doc.Contains("time") && !doc["time"].IsBsonNull)
                    {
                        try
                        {
                            post.Time = doc["time"].ToUniversalTime();
                        }
                        catch { }
                    }

                    // Likes, Shares, ViewsCount
                    post.Likes = doc.Contains("likes") ? doc["likes"] : BsonNull.Value;
                    post.Shares = doc.Contains("shares") ? doc["shares"] : BsonNull.Value;
                    post.ViewsCount = doc.Contains("viewsCount") ? doc["viewsCount"] : BsonNull.Value;

                    // Comments sayını hesabla
                    if (doc.Contains("comments") && doc["comments"].IsBsonArray)
                    {
                        var commentsArray = doc["comments"].AsBsonArray;
                        var comments = new List<FacebookComment>();

                        foreach (var commentDoc in commentsArray)
                        {
                            if (commentDoc.IsBsonDocument)
                            {
                                comments.Add(ParseComment(commentDoc.AsBsonDocument, post.Url, post.Text));
                            }
                        }

                        post.Comments = comments.ToArray();
                    }

                    posts.Add(post);
                }

                ViewBag.PostCount = posts.Count;
                ViewBag.TotalComments = posts.Sum(p => p.Comments?.Length ?? 0);

                return View(posts.OrderByDescending(p => p.Time).ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Xəta baş verdi: {ex.Message}";
                ViewBag.ErrorDetails = ex.StackTrace;
                return View(new List<FacebookPost>());
            }
        }
    }
}