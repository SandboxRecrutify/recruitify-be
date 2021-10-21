var courses = [
    {
        "CurrentApplicationsCount": NumberInt(2),
        "Description": "Курс Java Web Development – это введение в разработку программного обеспечения на языке программирования Java и связанных с ним технологиях. В программу входит изучение Java и JDK, основ Servlets API и JS, реализация простых веб-приложений. ",
        "EndDate": new Date("2022-03-30T00:00:00.000Z"),
        "_id": "UUID(\"2beed73a-30b5-11ec-808a-fb45776a1ed3\")",
        "Name": "Java Web Development",
        "PlannedApplicationsCount": NumberInt(4),
        "PrimarySkills": [NumberInt(4), NumberInt(2), NumberInt(6), NumberInt(1)],
        "StartDate": new Date("2022-01-10T00:00:00.000Z")
    },
    {
        "CurrentApplicationsCount": NumberInt(1),
        "Description": "Инженеры Big Data занимаются разработкой распределённых программных решений по обработке и анализу информации. Используемые технологии Big Data гарантируют специалистам больших данных постоянное развитие и востребованность в самых разных сферах разработки ПО. ",
        "EndDate": new Date("2022-04-10T00:00:00.000Z"),
        "_id": "UUID(\"7d85284c-30b5-11ec-95cb-230b32afd221\")",
        "Name": "Big Data",
        "PlannedApplicationsCount": NumberInt(3),
        "PrimarySkills": [NumberInt(0), NumberInt(4)],
        "StartDate": new Date("2022-02-13T00:00:00.000Z")
    },
    {
        "CurrentApplicationsCount": NumberInt(2),
        "Description": "Инженеры Big Data занимаются разработкой распределённых программных решений по обработке и анализу информации. Используемые технологии Big Data гарантируют специалистам больших данных постоянное развитие и востребованность в самых разных сферах разработки ПО. ",
        "EndDate": new Date("2021-05-01T00:00:00.000Z"),
        "_id": "UUID(\"bdbc3af0-f882-4f4f-bbb8-3e87ba16b768\")",
        "Name": "Big Data",
        "PlannedApplicationsCount": NumberInt(2),
        "PrimarySkills": [NumberInt(0), NumberInt(4)],
        "StartDate": new Date("2021-03-10T00:00:00.000Z")
    },
    {
        "CurrentApplicationsCount": NumberInt(1),
        "Description": "Веб-разработчик — это специалист занимающийся написанием, обновлением, исправлением и совершенствованием алгоритмов для приложений, сайтов и отдельных элементов, с использованием разных языков программирования.",
        "EndDate": new Date("2022-05-30T00:00:00.000Z"),
        "_id": "UUID(\"871b1e7a-30b5-11ec-9b40-437a6473123c\")",
        "Name": "BA, JS, .NET Development",
        "PlannedApplicationsCount": NumberInt(4),
        "PrimarySkills": [NumberInt(4), NumberInt(1), NumberInt(8), NumberInt(0)],
        "StartDate": new Date("2022-03-15T00:00:00.000Z")
    },
    {
        "CurrentApplicationsCount": NumberInt(3),
        "Description": "Веб-разработчик — это специалист занимающийся написанием, обновлением, исправлением и совершенствованием алгоритмов для приложений, сайтов и отдельных элементов, с использованием разных языков программирования.",
        "EndDate": new Date("2021-09-30T00:00:00.000Z"),
        "_id": "UUID(\"dd49fe8f-d697-41a5-be23-7066563b85a6\")",
        "Name": "BA, JS, .NET Development",
        "PlannedApplicationsCount": NumberInt(3),
        "PrimarySkills": [NumberInt(4), NumberInt(1), NumberInt(8), NumberInt(0)],
        "StartDate": new Date("2021-08-12T00:00:00.000Z")
    }
];
db.Courses.insertMany(courses);

var users = [
    {
        "_id": "UUID(\"ce33e6c4-30ac-11ec-8d3d-0242ac130003\")",
        "Login": "alexanderson@gmail.com",
        "Name": "Alex",
        "Password": "2B8358C34FFFE78D302F826F54A4E9D92BDB657F6DFE01FA3FA22FA89D3347DB",
        "Roles": [NumberInt(0), NumberInt(1)],
        "Salt": "E1F53135E559C253",
        "Surname": "Anderson"
    },
    {
        "_id": "UUID(\"73e05563-6c9e-4727-9ebb-b08f16cc1001\")",
        "Login": "anthonyclark@gmail.com",
        "Name": "Anthony",
        "Password": "646DAE47260A070B28D5E9727BA0D8A6A08CB6BF627602BE4E9229C5B7DAEAE2",
        "Roles": [NumberInt(1), NumberInt(2)],
        "Salt": "84B03D034B409D4E",
        "Surname": "Clark"
    },
    {
        "_id": "UUID(\"6f885f2c-b60d-4743-b381-4e841d48a956\")",
        "Login": "brandonharris@gmail.com",
        "Name": "Brandon",
        "Password": "25077B4E7F514A5FB3E04C662965AA9F0DF15D1FE0384526F608E3A6733D973F",
        "Roles": [NumberInt(2)],
        "Salt": "RET4356433DFG345",
        "Surname": "Harris"
    },
    {
        "_id": "UUID(\"d1b13d9f-3799-456b-87e2-1bef7db71cb1\")",
        "Login": "christopherjohnson@gmail.com",
        "Name": "Christopher",
        "Password": "31144862B60C43C5F045EAAC4D99E9F59DA3CFDAE42B372704D7C458B1E6DA8A",
        "Roles": [NumberInt(4), NumberInt(3)],
        "Salt": "SDFGHJ54645756FH",
        "Surname": "Johnson"
    },
    {
        "_id": "UUID(\"4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3\")",
        "Login": "davidlewis@gmail.com",
        "Name": "David",
        "Password": "C9A1CB6FB1D59764C8B12975E7B156D417141ED07832808F8F489064DDE3DBF8",
        "Roles": [NumberInt(4), NumberInt(3)],
        "Salt": "FDG4354GFD345542",
        "Surname": "Lewis"
    },
    {
        "_id": "UUID(\"cb40d31e-7447-4ec0-bc53-ab566f3a7b2e\")",
        "Login": "fredtaylor@gmail.com",
        "Name": "Fred",
        "Password": "A606B0C8919A0F6EB85240460D6772AA963669688C2CBD0D4A0B329CA238D8B0",
        "Roles": [NumberInt(3)],
        "Salt": "234FGHDS234EG454",
        "Surname": "Taylor"
    },
    {
        "_id": "UUID(\"71da4db7-36ec-4a6b-bfb3-38d2462bdf2d\")",
        "Login": "justinwalker@gmail.com",
        "Name": "Justin",
        "Password": "2B8CB1313018D3FAE4A5A58E2231AF82EFB6BE435574742A728AA7851615976F",
        "Roles": [NumberInt(3)],
        "Salt": "65734FHGFG54634G",
        "Surname": "Walker"
    },
    {
        "_id": "UUID(\"03699464-c0a6-412f-8c83-eca85b359e91\")",
        "Login": "kevingarcia@gmail.com",
        "Name": "Kevin",
        "Password": "4B0BEB558891202DF7BC320DB22F8C7930BA3899D433B57A8E862CA42AF08305",
        "Roles": [NumberInt(2)],
        "Salt": "AS567TYTT4535RFH",
        "Surname": "Wils"
    }
]
db.Users.insertMany(users);

var profiles = [
    {
        "AdditionalInfo": "Могу проходить стажеровку только в вечернее время",
        "BestTimeToConnect": "14-16",
        "Certificates": "",
        "Contacts": [
            {
                "Type": "VK",
                "Value": "id3242344"
            },
            {
                "Type": "Instagram",
                "Value": "@Gigbaby"
            }
        ],
        "CoursesResults": [
            {
                "CourseId": "UUID(\"dd49fe8f-d697-41a5-be23-7066563b85a6\")",
                "EndDate": new Date("2021-08-30T00:00:00.000Z"),
                "Feedbacks": [
                    {
                        "Rating": NumberInt(2),
                        "TextFeedback": "Слабые знания БД"
                    }
                ],
                "StartDate": new Date("2021-08-12T00:00:00.000Z"),
                "Status": NumberInt(3)
            },
            {
                "CourseId": "UUID(\"bdbc3af0-f882-4f4f-bbb8-3e87ba16b768\")",
                "EndDate": new Date("2021-04-15T00:00:00.000Z"),
                "Feedbacks": [
                    {
                        "Rating": NumberInt(4),
                        "TextFeedback": "Большенство заданий в тесте выполнено верно"
                    },
                    {
                        "Rating": NumberInt(5),
                        "TextFeedback": "Способен к обучению, имеет хорошие базовые знания"
                    }
                ],
                "StartDate": new Date("2021-03-10T00:00:00.000Z"),
                "Status": NumberInt(1)
            }
        ],
        "CurrentJob": "ЗАО \"Альфа\"",
        "Email": "Elliott@mail.ru",
        "EnglishLevel": NumberInt(0),
        "GoingToExadel": true,
        "_id": "UUID(\"c07e12be-30bd-11ec-853c-0392a821ec1f\")",
        "Location": {
            "City": "Minsk",
            "Country": "Belarus"
        },
        "Name": "Christopher",
        "PhoneNumber": "+375333421342",
        "PrimarySkills": [NumberInt(0), NumberInt(5)],
        "RegistrationDate": new Date("2021-02-12T00:00:00.000Z"),
        "Surname": "Elliott"
    },
    {
        "AdditionalInfo": "Готов приступить к работе в любое время",
        "BestTimeToConnect": "19-20",
        "Certificates": "Oracle Certified",
        "Contacts": [
            {
                "Type": "Facebook",
                "Value": "id=100002956189257"
            }
        ],
        "CoursesResults": [
            {
                "CourseId": "UUID(\"dd49fe8f-d697-41a5-be23-7066563b85a6\")",
                "EndDate": new Date("2021-08-30T00:00:00.000Z"),
                "Feedbacks": [
                    {
                        "Rating": NumberInt(4),
                        "TextFeedback": "Стоит рассмотреть для приема на проект"
                    },
                    {
                        "Rating": NumberInt(5),
                        "TextFeedback": "Тест выполнен отлично"
                    },
                    {
                        "Rating": NumberInt(4),
                        "TextFeedback": "Продемонстрировал хорошие знания асинхронного программирования"
                    }
                ],
                "StartDate": new Date("2021-08-12T00:00:00.000Z"),
                "Status": NumberInt(5)
            }
        ],
        "CurrentJob": "ОАО \"Акронис\"",
        "Email": "Robertio@mail.ru",
        "EnglishLevel": NumberInt(2),
        "GoingToExadel": true,
        "_id": "UUID(\"c89c4d88-30e7-11ec-929c-0f374fa83b31\")",
        "Location": {
            "City": "Moscow",
            "Country": "Russian Federation"
        },
        "Name": "Robert",
        "PhoneNumber": "+375337349087",
        "PrimarySkills": [NumberInt(4), NumberInt(0)],
        "RegistrationDate": new Date("2021-03-11T00:00:00.000Z"),
        "Surname": "Bruce"
    },
    {
        "AdditionalInfo": "Готов приступить к работе в любое время",
        "BestTimeToConnect": "18-20",
        "Certificates": "CompTIA",
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
        "CoursesResults": [
            {
                "CourseId": "UUID(\"dd49fe8f-d697-41a5-be23-7066563b85a6\")",
                "EndDate": new Date("2021-08-20T00:00:00.000Z"),
                "Feedbacks": [
                    {
                        "Rating": NumberInt(4),
                        "TextFeedback": "Полностью подходит для проекта"
                    }
                ],
                "StartDate": new Date("2021-08-12T00:00:00.000Z"),
                "Status": NumberInt(1)
            },
            {
                "CourseId": "UUID(\"2beed73a-30b5-11ec-808a-fb45776a1ed3\")",
                "Status": NumberInt(0)
            }
        ],
        "CurrentJob": "ОАО \"Акронис\"",
        "Email": "Jackson@gmail.com",
        "EnglishLevel": NumberInt(2),
        "GoingToExadel": false,
        "_id": "UUID(\"7d4ab133-4c7b-47c2-a7c6-827bdd010aae\")",
        "Location": {
            "City": "Vitebsk",
            "Country": "Belarus"
        },
        "Name": "Byron",
        "PhoneNumber": "+375332563489",
        "PrimarySkills": [NumberInt(1), NumberInt(8)],
        "RegistrationDate": new Date("2021-01-18T00:00:00.000Z"),
        "Surname": "Jackson"
    },
    {
        "AdditionalInfo": "Имею опыт программирования на js",
        "BestTimeToConnect": "11-20",
        "CoursesResults": [
            {
                "CourseId": "UUID(\"bdbc3af0-f882-4f4f-bbb8-3e87ba16b768\")",
                "EndDate": new Date("2021-05-01T00:00:00.000Z"),
                "Feedbacks": [
                    {
                        "Rating": NumberInt(3),
                        "TextFeedback": "Имеет базовые умения и знания"
                    },
                    {
                        "Rating": NumberInt(3),
                        "TextFeedback": "Тест выполнен частично "
                    },
                    {
                        "Rating": NumberInt(4),
                        "TextFeedback": "Полностью подходит для самостоятельно работы"
                    }
                ],
                "StartDate": new Date("2021-03-10T00:00:00.000Z"),
                "Status": NumberInt(5)
            },
            {
                "CourseId": "UUID(\"2beed73a-30b5-11ec-808a-fb45776a1ed3\")",
                "Status": NumberInt(0)
            }
        ],
        "CurrentJob": "ОАО \"Тракторный завод\"",
        "Email": "gainesss@gmail.com",
        "EnglishLevel": NumberInt(2),
        "GoingToExadel": true,
        "_id": "UUID(\"1e6a306e-30ec-11ec-a7bb-37cfa4f75a73\")",
        "Location": {
            "City": "Kiev",
            "Country": "Ukraine"
        },
        "Name": "Brian",
        "PhoneNumber": "+375255486325",
        "PrimarySkills": [NumberInt(4), NumberInt(8)],
        "RegistrationDate": new Date("2021-03-01T00:00:00.000Z"),
        "Surname": "Gaines"
    },
    {
        "AdditionalInfo": "Всегда доступен для звонков",
        "BestTimeToConnect": "11-20",
        "Contacts": [],
        "CoursesResults": [
            {
                "CourseId": "UUID(\"dd49fe8f-d697-41a5-be23-7066563b85a6\")",
                "EndDate": new Date("2021-08-01T00:00:00.000Z"),
                "Feedbacks": [
                    {
                        "Rating": NumberInt(3),
                        "TextFeedback": "Обладает основами .Net 5.0"
                    },
                    {
                        "Rating": NumberInt(3),
                        "TextFeedback": "Тест выполнен частично "
                    },
                    {
                        "Rating": NumberInt(4),
                        "TextFeedback": "В полной мере обладает знаниями необходимыми для проекта"
                    }
                ],
                "StartDate": new Date("2021-05-01T00:00:00.000Z"),
                "Status": NumberInt(5)
            },
            {
                "CourseId": "UUID(\"871b1e7a-30b5-11ec-9b40-437a6473123c\")",
                "Status": NumberInt(0)
            }
        ],
        "CurrentJob": "ОАО \"CosmosTV\"",
        "Email": "hugharvey@gmail.com",
        "EnglishLevel": NumberInt(4),
        "GoingToExadel": true,
        "_id": "UUID(\"37982a0e-30ed-11ec-a640-0f587540e9a4\")",
        "Location": {
            "City": "Grodno",
            "Country": "Belarus"
        },
        "Name": "Hugh",
        "PhoneNumber": "+375448536723",
        "PrimarySkills": [NumberInt(0)],
        "RegistrationDate": new Date("2021-03-01T00:00:00.000Z"),
        "Surname": "Harvey"
    },
    {
        "BestTimeToConnect": "08-22",
        "Certificates": "Oracle Certified",
        "Contacts": [
            {
                "Type": "Facebook",
                "Value": "id=100002956147957"
            }
        ],
        "CoursesResults": [
            {
                "CourseId": "UUID(\"7d85284c-30b5-11ec-95cb-230b32afd221\")",
                "Status": NumberInt(0)
            }
        ],
        "CurrentJob": "ОАО \"Huandai\"",
        "Email": "AnthonyD@mail.ru",
        "EnglishLevel": NumberInt(2),
        "GoingToExadel": false,
        "_id": "UUID(\"cc85a012-5d25-41a0-bbb7-95a6403a6296\")",
        "Location": {
            "City": "Moscow",
            "Country": "Russian Federation"
        },
        "Name": "Anthony",
        "PhoneNumber": "+375443468923",
        "PrimarySkills": [NumberInt(4)],
        "RegistrationDate": new Date("2021-10-19T00:00:00.000Z"),
        "Surname": "Day"
    }
]
db.Profiles.insertMany(profiles)