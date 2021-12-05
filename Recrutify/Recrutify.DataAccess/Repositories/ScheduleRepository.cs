﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
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

        public Task<IEnumerable<ScheduleSlot>> GetScheduleSlotsByDatePeriodAsync(Guid userId, DateTime dateStartPeriod)
        {
            var filter = _filterBuilder.Eq(u => u.UserId, userId);
            return GetFindFluentScheduleSlotsByDatePeriod(filter, dateStartPeriod).FirstOrDefaultAsync();
        }

        public Task BulkUpdateScheduleSlotAsync(Guid userId, IEnumerable<DateTime> newDateTime, IEnumerable<DateTime> remoteDateTime)
        {
            var updateBuilder = Builders<Schedule>.Update;
            var filterBuilder = Builders<ScheduleSlot>.Filter;

            var updateModelsWithNewScheduleSlot = newDateTime.Select(ur => new UpdateOneModel<Schedule>(
                                                    _filterBuilder.Eq(u => u.UserId, userId),
                                                    updateBuilder
                                                    .AddToSet(
                                                        nameof(Schedule.ScheduleSlots),
                                                        new ScheduleSlot() { AvailableTime = DateTime.SpecifyKind(ur, DateTimeKind.Utc) })));

            var updateModelsWithRemovedUsers = remoteDateTime.Select(ur => new UpdateOneModel<Schedule>(
                                                    _filterBuilder.Eq(u => u.UserId, userId),
                                                    updateBuilder.PullFilter(p => p.ScheduleSlots, filterBuilder.Eq(x => x.AvailableTime, DateTime.SpecifyKind(ur, DateTimeKind.Utc)))));
            return GetCollection().BulkWriteAsync(updateModelsWithNewScheduleSlot.Union(updateModelsWithRemovedUsers));
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

        private IFindFluent<Schedule, IEnumerable<ScheduleSlot>> GetFindFluentScheduleSlotsByDatePeriod(FilterDefinition<Schedule> filter, DateTime dateStart, int daysNum = 7)
        {
            return GetCollection()
                        .Find(filter)
                        .Project(x => x.ScheduleSlots.Where(x => x.AvailableTime >= dateStart.Date && x.AvailableTime < dateStart.Date.AddDays(daysNum)));
        }
    }
}
