using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.Interfaces;
using LazyVocabulary.Logic.OperationDetails;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LazyVocabulary.Logic.Services
{
    public class SubscriptionService
    {
        private IUnitOfWork _database { get; set; }

        public SubscriptionService(IUnitOfWork database)
        {
            _database = database;
        }

        public ResultWithData<bool> CanSubscribe(string userId, string targetUserId)
        {
            var resultWithData = new ResultWithData<bool>();

            try
            {
                resultWithData.ResultData = _database.Subscriptions
                    .Find(s => s.SubscriberId == userId && s.TargetId == targetUserId)
                    .SingleOrDefault() == null;
                resultWithData.Success = true;
            }
            catch (Exception ex)
            {
                resultWithData.Success = false;
                resultWithData.Message = ex.Message;
                resultWithData.StackTrace = ex.StackTrace;
            }

            return resultWithData;
        }

        public async Task<Result> SubscribeAsync(string userId, string targetUserId)
        {
            var result = new Result();

            try
            {
                var exists = _database.Subscriptions
                    .Find(s => s.SubscriberId == userId && s.TargetId == targetUserId)
                    .SingleOrDefault() != null;

                if (!exists)
                {
                    var subscription = new Subscription
                    {
                        SubscriberId = userId,
                        TargetId = targetUserId,
                        CreatedAt = DateTime.Now,
                    };
                    _database.Subscriptions.Create(subscription);
                    await _database.SaveAsync();
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        public async Task<Result> UnsubscribeAsync(string userId, string targetUserId)
        {
            var result = new Result();

            try
            {
                var subscription = _database.Subscriptions
                    .Find(s => s.SubscriberId == userId && s.TargetId == targetUserId)
                    .SingleOrDefault();

                if (subscription != null)
                {
                    _database.Subscriptions.Delete(subscription.Id);
                    await _database.SaveAsync();
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        public ResultWithData<int> GetSubscribersCountByUserId(string userId)
        {
            var resultWithData = new ResultWithData<int>();

            try
            {
                resultWithData.ResultData = _database.Subscriptions
                    .Find(s => s.TargetId == userId)
                    .Count();
                resultWithData.Success = true;
            }
            catch (Exception ex)
            {
                resultWithData.Success = false;
                resultWithData.Message = ex.Message;
                resultWithData.StackTrace = ex.StackTrace;
            }

            return resultWithData;
        }

        public ResultWithData<int> GetSubscriptionsCountByUserId(string userId)
        {
            var resultWithData = new ResultWithData<int>();

            try
            {
                resultWithData.ResultData = _database.Subscriptions
                    .Find(s => s.SubscriberId == userId)
                    .Count();
                resultWithData.Success = true;
            }
            catch (Exception ex)
            {
                resultWithData.Success = false;
                resultWithData.Message = ex.Message;
                resultWithData.StackTrace = ex.StackTrace;
            }

            return resultWithData;
        }
    }
}
