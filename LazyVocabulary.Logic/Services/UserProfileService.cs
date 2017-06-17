using LazyVocabulary.DataAccess.Interfaces;
using LazyVocabulary.Logic.OperationDetails;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LazyVocabulary.Logic.Services
{
    public class UserProfileService : IDisposable
    {
        private IUnitOfWork _database { get; set; }

        public UserProfileService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<Result> UpdateAsync(dynamic profileFromView)
        {
            var result = new Result();

            try
            {
                var profile = _database.UserProfiles
                    .Find(u => u.Id == profileFromView.Id)
                    .Single();

                profile.Name = profileFromView.Name;
                profile.Surname = profileFromView.Surname;
                profile.DateOfBirth = profileFromView.DateOfBirth;
                profile.UpdatedAt = DateTime.Now;

                _database.UserProfiles.Update(profile);
                await _database.SaveAsync();

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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
