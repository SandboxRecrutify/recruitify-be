var schedule = [
    {
        "_id": UUID("ce33e6c4-30ac-11ec-8d3d-0242ac130003"),
        "UserId": UUID("ce33e6c4-30ac-11ec-8d3d-0242ac130003"),
        "UserName": "Alex",
        "UserSurname": "Anderson",
        "UserEmail": "alexanderson@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
            "Name": ".Net"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-11-22T000:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Christopher",
                        "_id": UUID("c07e12be-30bd-11ec-853c-0392a821ec1f"),
                        "Email": "elliott@mail.ru",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult": 
                        {
                                "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
                                "Status": NumberInt(3),
                                "IsAssignedOnInterview": true,
                                "PrimarySkill": {
                                    "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                                    "Name": ".Net"
                                },
                        },
                    },
            }
        ],
    },
    {
        "_id": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
        "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
        "UserName": "Anthony",
        "UserSurname": "Clark",
        "UserEmail": "anthonyclark@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
            "Name": ".Net"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-08-02T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Christopher",
                        "_id": UUID("c07e12be-30bd-11ec-853c-0392a821ec1f"),
                        "Email": "elliott@mail.ru",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult":
                            {
                                "ProjectId": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
                                "Status": NumberInt(1),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                                    "Name": ".Net"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
        "UserId": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
        "UserName": "Brandon",
        "UserSurname": "Harris",
        "UserEmail": "brandonharris@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
            "Name": "BusinessAnalyst"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-12-02T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Robert",
                        "_id": UUID("c89c4d88-30e7-11ec-929c-0f374fa83b31"),
                        "Email": "robertio@mail.ru",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult": 
                            {
                                "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
                                "Status": NumberInt(5),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                                    "Name": "BusinessAnalyst"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
        "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
        "UserName": "Christopher",
        "UserSurname": "Johnson",
        "UserEmail": "christopherjohnson@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("b7d88aa4-3775-11ec-93ad-f7d63f513914"),
            "Name": "Java"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-11-27T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Brian",
                        "_id": UUID("1e6a306e-30ec-11ec-a7bb-37cfa4f75a73"),
                        "Email": "gainesss@gmail.com",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult": 
                            {
                                "ProjectId": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
                                "Status": NumberInt(5),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("b7d88aa4-3775-11ec-93ad-f7d63f513914"),
                                    "Name": "Java"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3"),
        "UserId": UUID("4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3"),
        "UserName": "David",
        "UserSurname": "Lewis",
        "UserEmail": "davidlewis@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
            "Name": "JavaScript"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-12-02T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Hugh",
                        "_id": UUID("37982a0e-30ed-11ec-a640-0f587540e9a4"),
                        "Email": "hugharvey@gmail.com",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult": 
                            {
                                "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
                                "Status": NumberInt(5),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                                    "Name": "JavaScript"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
        "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
        "UserName": "Fred",
        "UserSurname": "Taylor",
        "UserEmail": "fredtaylor@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("a9962438-3775-11ec-8787-6b8ecd0a876a"),
            "Name": "AutomationQA"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-12-03T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Hugh",
                        "_id": UUID("37982a0e-30ed-11ec-a640-0f587540e9a4"),
                        "Email": "hugharvey@gmail.com",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult": 
                            {
                                "ProjectId": UUID("871b1e7a-30b5-11ec-9b40-437a6473123c"),
                                "Status": NumberInt(0),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("a9962438-3775-11ec-8787-6b8ecd0a876a"),
                                    "Name": "AutomationQA"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
        "UserId": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
        "UserName": "Justin",
        "UserSurname": "Walker",
        "UserEmail": "justinwalker@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
            "Name": ".Net"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-08-02T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Kiril",
                        "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                        "Email": "pupkin@mail.ru",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult":
                            {
                                "ProjectId": UUID("871b1e7a-30b5-11ec-9b40-437a6473123c"),
                                "Status": NumberInt(4),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                                    "Name": ".Net"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("03699464-c0a6-412f-8c83-eca85b359e91"),
        "UserId": UUID("03699464-c0a6-412f-8c83-eca85b359e91"),
        "UserName": "Kevin",
        "UserSurname": "Wils",
        "UserEmail": "kevingarcia@gmail.com",
        "UserPrimarySkill": {
            "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
            "Name": "JavaScript"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-12-02T00:00:00.000Z"),
                "ScheduleCandidateInfo": 
                    {
                        "Name": "Yan",
                        "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                        "Email": "yan@mail.ru",
                        "Skype": "id=100002956147957",
                        "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                        "ProjectResult":
                            {
                                "ProjectId": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
                                "Status": NumberInt(3),
                                "IsAssignedOnInterview": false,
                                "PrimarySkill": {
                                    "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                                    "Name": "JavaScript"
                                },
                            },
                    },
            }
        ],
    },
    {
        "_id": UUID("aa9104bc-a200-44d9-8b55-53aef3132c23"),
        "UserId": UUID("aa9104bc-a200-44d9-8b55-53aef3132c23"),
        "UserName": "Alesia",
        "UserSurname": "Lis",
        "UserEmail": "lis.alesia@mail.ru",
        "UserPrimarySkill": {
            "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
            "Name": ".Net"
        },
        "ScheduleSlots": [
            {
                "AvailableTime": new Date("2021-12-02T00:00:00.000Z"),
                "ScheduleCandidateInfo":
                {
                    "Name": "Yan",
                    "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                    "Email": "yan@mail.ru",
                    "Skype": "id=100002956147957",
                    "BestTimeToConnect": [NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15)],
                    "ProjectResult":
                    {
                        "ProjectId": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
                        "Status": NumberInt(3),
                        "IsAssignedOnInterview": false,
                        "PrimarySkill": {
                            "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                            "Name": "JavaScript"
                        },
                    },
                },
            }
        ],
    }
]
