CREATE TABLE dbo.Activity(
ID varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
Route varchar(50),
Date date NOT NULL,
Duration time,
Kilometers decimal(5,2) DEFAULT 0,
Type varchar(50),
AthleteUsername varchar(50),
PRIMARY KEY (ID)
)

CREATE TABLE dbo.Activity_In_Challenge(
ActivityID varchar(50) NOT NULL,
ChallengeID varchar(50) NOT NULL
PRIMARY KEY(ActivityID,ChallengeID)
)

CREATE TABLE dbo.Athlete(
Username varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
Photo varchar(200),
Age int,
BirthDate date NOT NULL,
Pass varchar(50) NOT NULL,
Nationality varchar(50) NOT NULL,
Category varchar(50),
PRIMARY KEY (Username)
)

CREATE TABLE Athlete_Followers(
AthleteID varchar(50),
FollowerID varchar(50)
PRIMARY KEY(AthleteID, FollowerID)
)

CREATE TABLE dbo.Athlete_In_Challenge(
AthleteID varchar(50) NOT NULL,
ChallengeID varchar(50) NOT NULL,
Status varchar(50) DEFAULT 'Started'
PRIMARY KEY(AthleteID,ChallengeID)
)

CREATE TABLE dbo.Athlete_In_Competition(
AthleteID varchar(50) NOT NULL,
CompetitionID varchar(50) NOT NULL,
Status varchar(50) DEFAULT 'Started'
PRIMARY KEY(AthleteID,CompetitionID)
)

CREATE TABLE dbo.Challenge(
ID varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
StartDate date NOT NULL,
EndDate date,
Privacy varchar(10),
Kilometers decimal(5,2),
Type varchar(50),
PRIMARY KEY (ID)
)

CREATE TABLE dbo.Competition(
ID varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
Route varchar(50),
Date date NOT NULL,
Privacy varchar(10),
BankAccount varchar(50) NOT NULL,
Price decimal(5,2),
ActivityID varchar(50),
PRIMARY KEY (ID)
)

CREATE TABLE dbo.Competition_Categories(
CompetitionID varchar(50),
Category varchar(10),
PRIMARY KEY(CompetitionID, Category)
)

CREATE TABLE dbo.Groups(
Name varchar(50) NOT NULL,
AdminUsername varchar(50) NOT NULL,
PRIMARY KEY (Name)
)

CREATE TABLE Group_Member(
GroupName varchar(50),
MemberID varchar(50),
PRIMARY KEY(GroupName, MemberID)
)

CREATE TABLE dbo.Sponsor(
Id varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
BankAccount varchar(50) NOT NULL,
CompetitionID varchar(50),
PRIMARY KEY (Id,CompetitionID),
)


ALTER TABLE dbo.Activity
ADD CONSTRAINT Activity_Athlete_FK FOREIGN KEY(AthleteUsername)
REFERENCES dbo.Athlete(Username);

ALTER TABLE dbo.Competition
ADD CONSTRAINT Competition_Activity_FK FOREIGN KEY(ActivityID)
REFERENCES dbo.Activity(ID);

ALTER TABLE dbo.Sponsor
ADD CONSTRAINT Sponsor_compId_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(ID);

ALTER TABLE dbo.Groups
ADD CONSTRAINT Group_Admin_FK FOREIGN KEY(AdminUsername)
REFERENCES dbo.Athlete(Username);

ALTER TABLE dbo.Athlete_In_Challenge
ADD CONSTRAINT AIC_Ath_FK FOREIGN KEY(AthleteID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_In_Challenge
ADD CONSTRAINT AIC_Chal_FK FOREIGN KEY(ChallengeID)
REFERENCES dbo.Challenge(ID)

ALTER TABLE dbo.Athlete_In_Competition
ADD CONSTRAINT AICo_Ath_FK FOREIGN KEY(AthleteID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_In_Competition
ADD CONSTRAINT AICo_Chal_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(ID)

ALTER TABLE dbo.Competition_Categories
ADD CONSTRAINT Comp_Cat_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(ID)

ALTER TABLE dbo.Group_Member
ADD CONSTRAINT Group_Name_FK FOREIGN KEY(GroupName)
REFERENCES dbo.Groups(Name)

ALTER TABLE dbo.Group_Member
ADD CONSTRAINT Group_Member_FK FOREIGN KEY(MemberID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_Followers
ADD CONSTRAINT Ath_Id_FK FOREIGN KEY(AthleteID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_Followers
ADD CONSTRAINT Ath_Follower_FK FOREIGN KEY(FollowerID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Activity_In_Challenge
ADD CONSTRAINT ActCha_Id_FK FOREIGN KEY(ActivityID)
REFERENCES dbo.Activity(ID)

ALTER TABLE dbo.Activity_In_Challenge
ADD CONSTRAINT ActCha_ChId_FK FOREIGN KEY(ChallengeID)
REFERENCES dbo.Challenge(ID)