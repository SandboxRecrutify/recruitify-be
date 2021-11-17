var candidates = [
    {
        "_id": UUID("c07e12be-30bd-11ec-853c-0392a821ec1f"),
        "Name": "Christopher",
        "Surname": "Elliott",
        "EnglishLevel": NumberInt(0),
        "ProjectLanguage" : NumberInt(0),
        "PhoneNumber": "+375333421342",
        "Email": "elliott@mail.ru",
        "Contacts": [
            {
                "Type": "VK",
                "Value": "id3242344"
            },
            {
                "Type": "Instagram",
                "Value": "@bigbaby"
            }
        ],
        "Location": {
            "City": "Minsk",
            "Country": "Belarus"
        },
        "RegistrationDate": new Date("2021-08-04T00:00:00.000Z"),
        "BestTimeToConnect": [NumberInt(14), NumberInt(15), NumberInt(20)],
        "GoingToExadel": true,
        "ProjectResults": [
            {
                "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
                "Feedbacks": [
                    {
                        "UserName": "Christopher Johnson",
                        "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                        "Type": NumberInt(2),
                        "TextFeedback": "Weak knowledge of the database",
                        "Rating": NumberInt(2),
                        "CreatedOn": new Date("2021-08-15T11:45:17.000Z")
                    }
                ],
                "Status": NumberInt(3),
                "PrimarySkill": {
                        "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                        "Name": ".Net"
                }
            },
            {
                "ProjectId": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
                "Feedbacks": [
                    {
                        "Type": NumberInt(0),
                        "Rating": NumberInt(4),
                        "CreatedOn": new Date("2021-03-12T10:17:36.000Z")
                    },
                    {
                        "UserName": "Justin Walker",
                        "UserId": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
                        "Type": NumberInt(2),
                        "Rating": NumberInt(5),
                        "TextFeedback": "Able to learn, has a good basic knowledge",
                        "CreatedOn": new Date("2021-04-18T10:25:12.000Z")
                    }
                ],
                "Status": NumberInt(1),
                "PrimarySkill": {
                    "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                    "Name": ".Net"
                }
            }
        ],
        "CurrentJob": "LTD Bank-Alfa",
        "AdditionalInfo": "I can only do an internship in the evening"
        },
    {
        "_id": UUID("c89c4d88-30e7-11ec-929c-0f374fa83b31"),
        "Name": "Robert",
        "Surname": "Bruce",
        "EnglishLevel": NumberInt(2),
        "ProjectLanguage": NumberInt(1),
        "PhoneNumber": "+375337349087",
        "Email": "robertio@mail.ru",
        "Contacts": [
                {
                    "Type": "Facebook",
                    "Value": "id=100002956189257"
                }
            ],
        "Location": {
                "City": "Moscow",
                "Country": "Russian Federation"
            },
        "RegistrationDate": new Date("2021-08-02T00:00:00.000Z"),
        "BestTimeToConnect": [NumberInt(21), NumberInt(22)],
        "GoingToExadel": true,
        "ProjectResults": [
            {
                "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
                "Feedbacks": [
                    {
                        "UserName": "Fred Taylor",
                        "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
                        "Type": NumberInt(2),
                        "Rating": NumberInt(4),
                        "TextFeedback": "It is worth considering the admission to the project",
                        "CreatedOn": new Date("2021-08-13T12:10:30.000Z")
                    },
                    {
                        "Type": NumberInt(0),
                        "Rating": NumberInt(5),
                        "CreatedOn": new Date("2021-08-28T11:24:43.000Z")
                    },
                    {
                        "UserName": "Christopher Johnson",
                        "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                        "Type": NumberInt(3),
                        "Rating": NumberInt(4),
                        "TextFeedback": "Demonstrated good knowledge of async programming.",
                        "CreatedOn": new Date("2021-09-28T10:57:34.000Z")
                    }
                ],
                "Status": NumberInt(5),
                "PrimarySkill": {
                    "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                    "Name": "BusinessAnalyst"
                }
            }
        ],
        "CurrentJob": "Cafe Mint",
        "Certificates": "Oracle Certified",
        "AdditionalInfo": "Ready to start anytime"
    },
    {
        "_id": UUID("7d4ab133-4c7b-47c2-a7c6-827bdd010aae"),
        "Name": "Byron",
        "Surname": "Jackson",
        "EnglishLevel": NumberInt(2),
        "ProjectLanguage": NumberInt(1),
        "PhoneNumber": "+375332563489",
        "Email": "jackson@gmail.com",
        "Contacts": [
            {
                "Type": "Instagram",
                "Value": "@jackgreen"
            },
            {
                "Type": "Facebook",
                "Value": "id=100002979189885"
            }
        ],
        "Location": {
            "City": "Vitebsk",
            "Country": "Belarus"
        },
        "RegistrationDate": new Date("2021-08-08T00:00:00.000Z"),
        "BestTimeToConnect": [NumberInt(8), NumberInt(9), NumberInt(18), NumberInt(19), NumberInt(20)],
        "GoingToExadel": false,
        "ProjectResults": [
            {
                "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
                "Feedbacks": [
                    {
                        "UserName": "Christopher Johnson",
                        "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                        "Type": NumberInt(2),
                        "Rating": NumberInt(4),
                        "TextFeedback": "Fully suited to the project",
                        "CreatedOn": new Date("2021-08-16T10:27:18.000Z")
                    }
                ],
                "Status": NumberInt(1),
                "PrimarySkill": {
                    "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                    "Name": "BusinessAnalyst"
                }
            },
            {
                "ProjectId": UUID("2beed73a-30b5-11ec-808a-fb45776a1ed3"),
                "Status": NumberInt(0),

            }
        ],
        "CurrentJob": "LLC Acronis",
        "Certificates": "CompTIA",
        "AdditionalInfo": "Ready to start now"
    },
    {
        "_id": UUID("1e6a306e-30ec-11ec-a7bb-37cfa4f75a73"),
        "Name": "Brian",
        "Surname": "Gaines",
        "EnglishLevel": NumberInt(2),
        "ProjectLanguage": NumberInt(1),
        "PhoneNumber": "+375255486325",
        "Email": "gainesss@gmail.com",
        "Location": {
            "City": "Kiev",
            "Country": "Ukraine"
        },
        "RegistrationDate": new Date("2021-03-07T00:00:00.000Z"),
        "BestTimeToConnect": [NumberInt(14), NumberInt(15), NumberInt(16), NumberInt(17), NumberInt(18)],
        "GoingToExadel": true,
        "ProjectResults": [
            {
                "ProjectId": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
                "Feedbacks": [
                    {
                        "UserName": "Justin Walker",
                        "UserId": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
                        "Type": NumberInt(2),
                        "Rating": NumberInt(3),
                        "TextFeedback": "Has basic skills and knowledge",
                        "CreatedOn": new Date("2021-03-14T10:11:14.000Z")
                    },
                    {
                        "Type": NumberInt(0),
                        "Rating": NumberInt(2),
                        "CreatedOn": new Date("2021-04-07T12:09:53.000Z")
                    },
                    {
                        "UserName": "Christopher Johnson",
                        "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                        "Type": NumberInt(3),
                        "Rating": NumberInt(4),
                        "TextFeedback": "Fully suitable for independent work",
                        "CreatedOn": new Date("2021-04-29T11:43:50.000Z")
                    }
                ],
                "Status": NumberInt(5),
                "PrimarySkill": {
                    "_id": UUID("b7d88aa4-3775-11ec-93ad-f7d63f513914"),
                    "Name": "Java"
                }
            },
            {
                "ProjectId": UUID("2beed73a-30b5-11ec-808a-fb45776a1ed3"),
                "Status": NumberInt(0)
            }
        ],
        "CurrentJob": "LLC Pilot",
        "AdditionalInfo": "I have experience in programming in js"
        },
    {
       "_id": UUID("37982a0e-30ed-11ec-a640-0f587540e9a4"),
       "Name": "Hugh",
       "Surname": "Harvey",
        "EnglishLevel": NumberInt(4),
        "ProjectLanguage": NumberInt(0),
       "PhoneNumber": "+375448536723",
       "Email": "hugharvey@gmail.com",
       "Location": {
           "City": "Grodno",
           "Country": "Belarus"
       },
       "RegistrationDate": new Date("2021-08-08T00:00:00.000Z"),
       "BestTimeToConnect": [NumberInt(14), NumberInt(15), NumberInt(16), NumberInt(20), NumberInt(21)],
       "GoingToExadel": true,
       "ProjectResults": [
           {
               "ProjectId": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
               "Feedbacks": [
                   {
                       "UserName": "Christopher Johnson",
                       "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                       "Type": NumberInt(2),
                       "Rating": NumberInt(3),
                       "TextFeedback": "Has the basics of .Net 5.0",
                       "CreatedOn": new Date("2021-08-13T10:18:34.000Z")
                   },
                   {
                       "Type": NumberInt(0),
                       "Rating": NumberInt(3),
                       "CreatedOn": new Date("2021-08-26T12:14:11.000Z")
                   },
                   {
                       "UserName": "Fred Taylor",
                       "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
                       "Type": NumberInt(3),
                       "Rating": NumberInt(4),
                       "TextFeedback": "Fully owns the knowledge required for the project",
                       "CreatedOn": new Date("2021-09-22T11:12:53.000Z")
                   }
               ],
               "Status": NumberInt(5),
               "PrimarySkill": {
                   "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                   "Name": "JavaScript"
               }
           },
           {
               "ProjectId": UUID("871b1e7a-30b5-11ec-9b40-437a6473123c"),
               "Status": NumberInt(0),
               "PrimarySkill": {
                   "_id": UUID("a9962438-3775-11ec-8787-6b8ecd0a876a"),
                   "Name": "AutomationQA",
               }
           }
       ],
       "CurrentJob": "PLC CosmosTV",
       "AdditionalInfo": "Always available for calls"
    },
    {
        "_id": UUID("cc85a012-5d25-41a0-bbb7-95a6403a6296"),
        "Name": "Anthony",
        "Surname": "Day",
        "EnglishLevel": NumberInt(2),
        "ProjectLanguage": NumberInt(0),
        "PhoneNumber": "+375443468923",
        "Email": "anthonyD@mail.ru",
        "Contacts": [
            {
                "Type": "Facebook",
                "Value": "id=100002956147957"
            }
        ],
        "Location": {
            "City": "Moscow",
            "Country": "Russian Federation"
        },
        "RegistrationDate": new Date("2021-02-09T00:00:00.000Z"),
        "BestTimeToConnect": [NumberInt(6), NumberInt(7), NumberInt(13), NumberInt(14), NumberInt(15), NumberInt(20), NumberInt(21), NumberInt(22)],
        "GoingToExadel": false,
        "ProjectResults": [
            {
                "ProjectId": UUID("7d85284c-30b5-11ec-95cb-230b32afd221"),
                "Status": NumberInt(0),
                "PrimarySkill": {
                    "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                    "Name": "BusinessAnalyst"
               }
            }
        ],
        "CurrentJob": "LLC Huandai",
        "Certificates": "Oracle Certified",
        }
]

