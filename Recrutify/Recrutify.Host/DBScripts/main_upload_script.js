if (!db.User.exists()) {
	load('./users_collection.js');
    db.User.insertMany(users);
}

if (!db.Candidate.exists()) {
	load('./candidates_collection.js');
    db.Candidate.insertMany(candidates);
}

if (!db.Project.exists()) {
	load('./projects_collections.js');
    db.Project.insertMany(projects);
}