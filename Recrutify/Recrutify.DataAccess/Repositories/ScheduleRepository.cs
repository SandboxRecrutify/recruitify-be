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

        public Task<IEnumerable<ScheduleSlot>> GetScheduleSlotsOfDatePeriodAsync(Guid userId, DateTime periodStartDate, DateTime periodFinishDate)
        {
            var filter = _filterBuilder.Eq(u => u.UserId, userId);
            return GetCollection()
                        .Find(filter)
                        .Project(x => x.ScheduleSlots.Where(x => x.AvailableTime >= periodStartDate && x.AvailableTime < periodFinishDate))
                        .FirstOrDefaultAsync();
        }

        public Task BulkUpdateScheduleSlotsAsync(Guid userId, IEnumerable<DateTime> newListDateTime, IEnumerable<DateTime> removedListDateTime)
        {
            var updateBuilder = Builders<Schedule>.Update;
            var filterBuilder = Builders<ScheduleSlot>.Filter;

            var updateModelsWithNewScheduleSlot = newListDateTime.Select(dt => new UpdateOneModel<Schedule>(
                                                    _filterBuilder.Eq(s => s.UserId, userId),
                                                    updateBuilder
                                                    .AddToSet(
                                                        nameof(Schedule.ScheduleSlots),
                                                        new ScheduleSlot() { AvailableTime = dt })));

            var updateModelsWithRemovedUsers = removedListDateTime.Select(dt => new UpdateOneModel<Schedule>(
                                                    _filterBuilder.Eq(s => s.UserId, userId),
                                                    updateBuilder.PullFilter(s => s.ScheduleSlots, filterBuilder.Eq(ss => ss.AvailableTime, dt))));
            return GetCollection().BulkWriteAsync(updateModelsWithNewScheduleSlot.Union(updateModelsWithRemovedUsers));
        }

        public Task BulkСancelInterviewsAsync(IEnumerable<Interview> cancelledInterviews)
        {
            var updateBuilder = Builders<Schedule>.Update;
            var updateModels = cancelledInterviews.Select(i => new UpdateOneModel<Schedule>(
                _filterBuilder.Eq(s => s.Id, i.UserId),
                updateBuilder.Set("ScheduleSlots.$[scheduleSlot].ScheduleCandidateInfo", BsonNull.Value))
            { ArrayFilters = new List<ArrayFilterDefinition>() { new BsonDocumentArrayFilterDefinition<ScheduleSlot>(new BsonDocument("scheduleSlot.AvailableTime", i.AppoitmentDateTime)) } });
            return GetCollection().BulkWriteAsync(updateModels);
        }

        public Task BulkAppointInterviewsAsync(IEnumerable<Interview> cancelledInterviews, IEnumerable<ScheduleCandidateInfo> candidatesInfo)
        {
            var updateBuilder = Builders<Schedule>.Update;
            var updateModels = cancelledInterviews.Select(i => new UpdateOneModel<Schedule>(
                _filterBuilder.Eq(s => s.Id, i.UserId),
                updateBuilder.Set("ScheduleSlots.$[scheduleSlot].ScheduleCandidateInfo", candidatesInfo.FirstOrDefault(c => c.Id == i.CandidateId)))
            { ArrayFilters = new List<ArrayFilterDefinition>() { new BsonDocumentArrayFilterDefinition<ScheduleSlot>(new BsonDocument("scheduleSlot.AvailableTime", i.AppoitmentDateTime)) } });
            return GetCollection().BulkWriteAsync(updateModels);
        }

        public Task<List<ScheduleShort>> GetNotFreeShuduleSlotsBySlotsAsync(Dictionary<Guid, List<ScheduleSlotShort>> slots)
        {
            var filter = _filterBuilder.In(u => u.UserId, slots.Keys);
            return GetCollection()
                        .Find(filter)
                        .Project(x => new ScheduleShort
                        {
                            UserId = x.UserId,
                            ScheduleSlots = x.ScheduleSlots.Where(
                                s => s.ScheduleCandidateInfo != null
                                     && slots[x.UserId].Select(slotShort => slotShort.AvailableTime).Contains(s.AvailableTime)
                                     && s.ScheduleCandidateInfo.Id == slots[x.UserId].FirstOrDefault(slotShotr => slotShotr.AvailableTime == s.AvailableTime).CandidateId)
                                        .Select(slot => new ScheduleSlotShort() { AvailableTime = slot.AvailableTime, CandidateId = slot.ScheduleCandidateInfo.Id }),
                        }).ToListAsync();
        }

        public Task<List<ScheduleShort>> GetFreeShuduleSlotsBySlotsAsync(Dictionary<Guid, List<ScheduleSlotShort>> slots)
        {
            var filter = _filterBuilder.In(u => u.UserId, slots.Keys);
            return GetCollection()
                        .Find(filter)
                        .Project(x => new ScheduleShort
                        {
                            UserId = x.UserId,
                            ScheduleSlots = x.ScheduleSlots.Where(
                                s => slots[x.UserId].Select(slot => slot.AvailableTime).Contains(s.AvailableTime) && s.ScheduleCandidateInfo == null)
                                    .Select(slot => new ScheduleSlotShort() { AvailableTime = slot.AvailableTime }),
                        }).ToListAsync();
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
