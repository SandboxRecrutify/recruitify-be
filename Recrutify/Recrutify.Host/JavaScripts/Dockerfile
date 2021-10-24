FROM mongo:latest
COPY UploadDataToDB.js /scripts/UploadDataToDB.js
#RUN bash -c "mongo mongodb+srv://BestTeam2:1234qwerty@recruitifyproject.epdnl.mongodb.net/Rec /scripts/UploadDataToDB.js"
#CMD bash -c "mongo mongodb+srv://BestTeam2:123qwerty@recruitifyproject.epdnl.mongodb.net/RecruitifyTest /scripts/UploadDataToDB.js"
CMD bash -c "mongo ${DbConnectionString} /scripts/UploadDataToDB.js"

