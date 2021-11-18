var projects = [
    {
        "_id": UUID("2beed73a-30b5-11ec-808a-fb45776a1ed3"),
        "Name": "Java Web Development",
        "StartDate": new Date("2022-01-10T00:00:00.000Z"),
        "EndDate": new Date("2022-03-30T00:00:00.000Z"),
        "StartRegistrationDate": new Date("2022-01-01T00:00:00.000Z"),
        "EndRegistrationDate": new Date("2022-01-08T00:00:00.000Z"),
        "CurrentApplicationsCount": NumberInt(2),
        "PlannedApplicationsCount": NumberInt(4),
        "Description": "The Java Web Development course is an introduction to software development in the Jav programming language and related technologies. The program includes learning the basics of Java and JDK, Servlets API and JS, implementation of simple web applications.",
        "PrimarySkills": [
            {
                "_id": UUID("b7d88aa4-3775-11ec-93ad-f7d63f513914"),
                "Name": "Java",
                "Description": "JDBC & Hibernate, Java EE / Spring, Java Core",
                "TestLink": "https://exadel.com/tests/java"
            },
            {
                "_id": UUID("c3307088-3775-11ec-8e04-b7d29fa6ce1f"),
                "Name": "ProjectManager",
                "Description": "Scrum, Agile, PMBOK",
                "TestLink": "https://exadel.com/tests/projectmanager"
            },
            {
                "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                "Name": "BusinessAnalyst",
                "Description": "Power BI, Tableau, Miro, Google Sheet, MySQL",
                "TestLink": "https://exadel.com/tests/businessanalyst"
            }
        ],
        "Managers": [
            {
                "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
                "UserName": "Anthony Clark"                
            }
        ],
        "Interviewers": [
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"                   
            },
            {
                "UserId": UUID("4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3"),
                "UserName": "David Lewis"                    
            }
        ],
        "Recruiters": [
            {
                "UserId": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
                "UserName": "Brandon Harris"                    
            },
            {
                "UserId": UUID("03699464-c0a6-412f-8c83-eca85b359e91"),
                "UserName": "Kevin Wils"                    
            }
        ],
        "Mentors": [
            {
                "UserId": UUID("4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3"),
                "UserName": "David Lewis"                    
            },
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"                    
            }
        ],
        "IsActive":true
    },
    {
        "_id": UUID("7d85284c-30b5-11ec-95cb-230b32afd221"),
        "Name": "Big Data",
        "StartDate": new Date("2022-02-13T00:00:00.000Z"),
        "EndDate": new Date("2022-04-10T00:00:00.000Z"),
        "StartRegistrationDate": new Date("2022-02-01T00:00:00.000Z"),
        "EndRegistrationDate": new Date("2022-02-10T00:00:00.000Z"),
        "CurrentApplicationsCount": NumberInt(1),
        "PlannedApplicationsCount": NumberInt(3),
        "Description": "Big Data engineers develop distributed software solutions for information processing and analysis. The technologies used Big Data guarantee constant development and demand for big data specialists in various areas of software development.",
        "PrimarySkills": [
            {
                "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                "Name": ".Net",
                "Description": "OOP, Design patterns, SQL, ASP.NET",
                "TestLink": "https://exadel.com/tests/net"
            }
        ],
        "Managers": [
            {
                "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
                "UserName": "Fred Taylor"
            },
            {
                "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
                "UserName": "Anthony Clark"
            }
        ],
        "Interviewers": [
            
            {
                "UserId": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
                "UserName": "Justin Walker"
            }
        ],
        "Recruiters": [
            {
                "UserId": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
                "UserName": "Brandon Harris"
            }
        ],
        "Mentors": [
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"
            },
            {
                "UserId": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
                "UserName": "Brandon Harris"
            }
        ],
        "IsActive": true
        },
    {
        "_id": UUID("bdbc3af0-f882-4f4f-bbb8-3e87ba16b768"),
        "Name": "Big Data",
        "StartDate": new Date("2021-03-10T00:00:00.000Z"),
        "EndDate": new Date("2021-05-01T00:00:00.000Z"),
        "StartRegistrationDate": new Date("2022-03-01T00:00:00.000Z"),
        "EndRegistrationDate": new Date("2022-03-08T00:00:00.000Z"),
        "CurrentApplicationsCount": NumberInt(2),
        "PlannedApplicationsCount": NumberInt(2),
        "Description": "Big Data engineers develop distributed software solutions for information processing and analysis. The technologies used Big Data guarantee constant development and demand for big data specialists in various areas of software development.",
        "PrimarySkills": [
            {
                "_id": UUID("b7d88aa4-3775-11ec-93ad-f7d63f513914"),
                "Name": "Java",
                "Description": "JDBC & Hibernate, Java EE / Spring, Java Core",
                "TestLink": "https://exadel.com/tests/java"
            }
        ],
        "Managers": [
            {
                "UserId": UUID("ce33e6c4-30ac-11ec-8d3d-0242ac130003"),
                "UserName": "Alex Anderson"
            }
        ],
        "Interviewers": [
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"
            },
            {
                "UserId": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
                "UserName": "Justin Walker"
            }
        ],
        "Recruiters": [
            {
                "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
                "UserName": "Anthony Clark"
            },
            {
                "UserId": UUID("03699464-c0a6-412f-8c83-eca85b359e91"),
                "UserName": "Kevin Wils"
            }
        ],
        "Mentors": [
            {
                "UserId": UUID("4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3"),
                "UserName": "David Lewis"
            }
        ],
        "IsActive": false
    },
    {
        "_id": UUID("871b1e7a-30b5-11ec-9b40-437a6473123c"),
        "Name": "BA, JS, .NET Development",
        "StartDate": new Date("2022-03-15T00:00:00.000Z"),
        "EndDate": new Date("2022-05-30T00:00:00.000Z"),        
        "StartRegistrationDate": new Date("2022-03-01T00:00:00.000Z"),
        "EndRegistrationDate": new Date("2022-03-12T00:00:00.000Z"),
        "CurrentApplicationsCount": NumberInt(1),
        "PlannedApplicationsCount": NumberInt(4),
        "Description": "A Web developer is a specialist engaged in writing, updating, correcting and improving algorithms for applications, sites and individual elements using different programming languages.",
        "PrimarySkills": [
            {
                "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                "Name": ".Net",
                "Description": "OOP, Design patterns, SQL, ASP.NET",
                "TestLink": "https://exadel.com/tests/net"
            },
            {
                "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                "Name": "JavaScript",
                "Description": "HTML, React, Github, Bash, Express.js",
                "TestLink": "https://exadel.com/tests/javascript"
            },
            {
                "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                "Name": "BusinessAnalyst",
                "Description": "Power BI, Tableau, Miro, Google Sheet, MySQL",
                "TestLink": "https://exadel.com/tests/businessanalyst"
            }
        ],
        "Managers": [
            {
                "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
                "UserName": "Fred Taylor"
            },
            {
                "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
                "UserName": "Anthony Clark"
            }
        ],
        "Interviewers": [
            {
                "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
                "UserName": "Fred Taylor"
            },
            {
                "UserId": UUID("71da4db7-36ec-4a6b-bfb3-38d2462bdf2d"),
                "UserName": "Justin Walker"
            }
        ],
        "Recruiters": [
            {
                "UserId": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
                "UserName": "Brandon Harris"
            },
            {
                "UserId": UUID("03699464-c0a6-412f-8c83-eca85b359e91"),
                "UserName": "Kevin Wils"
            },
            {
                "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
                "UserName": "Anthony Clark"
            }
        ],
        "Mentors": [
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"
            }
        ],
        "IsActive": true  
    },
    {
        "_id": UUID("dd49fe8f-d697-41a5-be23-7066563b85a6"),
        "Name": "BA, JS, .NET Development",
        "StartDate": new Date("2021-08-12T00:00:00.000Z"),
        "EndDate": new Date("2021-09-30T00:00:00.000Z"),
        "StartRegistrationDate": new Date("2022-08-01T00:00:00.000Z"),
        "EndRegistrationDate": new Date("2022-08-10T00:00:00.000Z"),
        "CurrentApplicationsCount": NumberInt(3),
        "PlannedApplicationsCount": NumberInt(3),
        "Description": "A Web developer is a specialist engaged in writing, updating, correcting and improving algorithms for applications, sites and individual elements using different programming languages.",
        "PrimarySkills": [
            {
                "_id": UUID("aeb0e468-3774-11ec-83d4-97dbf3c3f8eb"),
                "Name": ".Net",
                "Description": "OOP, Design patterns, SQL, ASP.NET",
                "TestLink": "https://exadel.com/tests/net"
            },
            {
                "_id": UUID("a539cb10-3775-11ec-8803-1b868ded28bd"),
                "Name": "JavaScript",
                "Description": "HTML, React, Github, Bash, Express.js",
                "TestLink": "https://exadel.com/tests/javascript"
            },
            {
                "_id": UUID("d66d9d10-3775-11ec-9f7d-f7d443b3f4f3"),
                "Name": "BusinessAnalyst",
                "Description": "Power BI, Tableau, Miro, Google Sheet, MySQL",
                "TestLink": "https://exadel.com/tests/businessanalyst"
            },
            {
                "_id": UUID("c3307088-3775-11ec-8e04-b7d29fa6ce1f"),
                "Name": "ProjectManager",
                "Description": "Scrum, Agile, PMBOK",
                "TestLink": "https://exadel.com/tests/projectmanager"
            }
        ],
        "Managers": [
            {
                "UserId": UUID("ce33e6c4-30ac-11ec-8d3d-0242ac130003"),
                "UserName": "Alex Anderson"
            }
        ],
        "Interviewers": [
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"
            },
            {
                "UserId": UUID("4f04e857-50ec-42d7-8fe4-0e3d3d8d34b3"),
                "UserName": "David Lewis"
            },
            {
                "UserId": UUID("cb40d31e-7447-4ec0-bc53-ab566f3a7b2e"),
                "UserName": "Fred Taylor"
            }
        ],
        "Recruiters": [
            {
                "UserId": UUID("73e05563-6c9e-4727-9ebb-b08f16cc1001"),
                "UserName": "Anthony Clark"
            }
        ],
        "Mentors": [
            
            {
                "UserId": UUID("d1b13d9f-3799-456b-87e2-1bef7db71cb1"),
                "UserName": "Christopher Johnson"
            },
            {
                "UserId": UUID("6f885f2c-b60d-4743-b381-4e841d48a956"),
                "UserName": "Brandon Harris"
            }
        ],
        "IsActive": false
    }
]