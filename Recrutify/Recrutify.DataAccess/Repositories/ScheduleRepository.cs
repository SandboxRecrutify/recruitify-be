using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(IOptions<MongoSettings> options)
           : base(options)
        {
        }

        public Task<List<Schedule>> GetByUserPrimarySkillAsync(IEnumerable<Guid> userIds, DateTime date, Guid primarySkillId)
        {
            var filter = _filterBuilder.In(u => u.UserId, userIds) & _filterBuilder.Eq(u => u.UserPrimarySkill.Id, primarySkillId);
            return GetFindFluentByDate(filter, date).ToListAsync();
        }

        public Task<Schedule> GetByDatePeriodAsync(Guid userId, DateTime date, int daysNum)
        {
            var filter = _filterBuilder.Eq(u => u.UserId, userId);
            return GetFindFluentByDate(filter, date, daysNum).FirstOrDefaultAsync();
        }

        public Task UpdateOrCancelScheduleCandidateInfosByDictionaryAsync(Dictionary<Guid, ScheduleSlot> scheduleSlots, bool updateFlag)
        {
            var updateBuilder = Builders<Schedule>.Update;
            var updateModels = updateFlag == true ? scheduleSlots.Select(sl => new UpdateOneModel<Schedule>(
                _filterBuilder.Eq(s => s.Id, sl.Key),
                updateBuilder
                .Set("ScheduleSlots.$[scheduleSlots].ScheduleCandidateInfo", sl.Value.ScheduleCandidateInfo))
            { ArrayFilters = new List<ArrayFilterDefinition>() { new BsonDocumentArrayFilterDefinition<ScheduleSlot>(new BsonDocument("scheduleSlots.AvailableTime", sl.Value.AvailableTime)) } })
                : scheduleSlots.Select(sl => new UpdateOneModel<Schedule>(
                _filterBuilder.Eq(s => s.Id, sl.Key),
                updateBuilder
                .Set("ScheduleSlots.$[scheduleSlots].ScheduleCandidateInfo",  BsonNull.Value))
                  { ArrayFilters = new List<ArrayFilterDefinition>() { new BsonDocumentArrayFilterDefinition<ScheduleSlot>(new BsonDocument("scheduleSlots.AvailableTime", sl.Value.AvailableTime)) } });

            return GetCollection().BulkWriteAsync(updateModels);
        }

        public async Task<bool> FreeOrExistByDictAsync(Dictionary<Guid, DateTime> userIdAndDateTime)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder
                .ElemMatch(
                    nameof(Schedule.ScheduleSlots),
                    filterBuilder.And(
                        filterBuilder.Eq(nameof(Schedule), userIdAndDateTime.Select(x => x.Key))
                        & filterBuilder.AnyIn("ScheduleSlots.$[scheduleSlots].AvailableTime", userIdAndDateTime.Select(x => x.Value.ToString()))
                        & filterBuilder.Type("ScheduleSlots.$[scheduleSlots].ScheduleCandidateInfo", BsonType.Null)));
            var slots = await GetBsonDocumentCollection().Find(filter).FirstOrDefaultAsync();
            return slots == null ? false : true;
        }

        public IEnumerable<ScheduleCandidateInfo> GetScheduleCandidateInfos(IEnumerable<Candidate> candidates)
        {
            var scheduleCandidateInfos = new List<ScheduleCandidateInfo>();
            foreach (var candidate in candidates)
            {
                var projectResult = candidate.ProjectResults.Where(x => x.Status <= Status.TechInterviewOneStep).FirstOrDefault();
                scheduleCandidateInfos.Add(new ScheduleCandidateInfo()
                {
                    BestTimeToConnect = candidate.BestTimeToConnect,
                    Email = candidate.Email,
                    Id = candidate.Id,
                    Name = candidate.Name,
                    ProjectResult = new ScheduleCandidateProjectResult()
                    {
                        IsAssignedOnInterview = true,
                        PrimarySkill = projectResult.PrimarySkill,
                        ProjectId = projectResult.ProjectId,
                        Status = projectResult.Status,
                    },
                    Skype = candidate.Contacts.Where(s => s.Type == "Skype").Select(s => s.Value).FirstOrDefault(),
                });
            }

            return scheduleCandidateInfos;
        }

        private IFindFluent<Schedule, Schedule> GetFindFluentByDate(FilterDefinition<Schedule> filter, DateTime date, int daysNum = 1)
        {
            return GetCollection()
                        .Find(filter)
                        .Project(x => new Schedule
                        {
                            UserId = x.UserId,
                            UserName = x.UserName,
                            UserPrimarySkill = x.UserPrimarySkill,
                            ScheduleSlots = x.ScheduleSlots
                                                .Where(
                                                    y => y.AvailableTime >= date.Date &&
                                                    y.AvailableTime < date.Date.AddDays(daysNum)),
                        });
        }
    }
}
