CREATE TABLE dbo.Activity(
ID varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
Route varchar(MAX),
Date datetime NOT NULL,
Duration time,
Kilometers decimal(5,2) DEFAULT 0,
Type varchar(50),
AthleteUsername varchar(50),
)

CREATE TABLE dbo.Activity_In_Challenge(
ActivityID varchar(50) NOT NULL,
ChallengeID varchar(50) NOT NULL
)

CREATE TABLE dbo.Athlete(
Username varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
Photo varchar(MAX),
Age int,
BirthDate date NOT NULL,
Pass varchar(50) NOT NULL,
Nationality varchar(50) NOT NULL,
Category varchar(50),
)

CREATE TABLE Athlete_Followers(
AthleteID varchar(50) NOT NULL,
FollowerID varchar(50) NOT NULL
)

CREATE TABLE dbo.Athlete_In_Challenge(
AthleteID varchar(50) NOT NULL,
ChallengeID varchar(50) NOT NULL,
Status varchar(50) DEFAULT 'Waiting'
)

CREATE TABLE dbo.Athlete_In_Competition(
AthleteID varchar(50) NOT NULL,
CompetitionID varchar(50) NOT NULL,
Status varchar(50) DEFAULT 'Waiting',
Receipt varchar(MAX),
Duration time,
)

CREATE TABLE dbo.Challenge(
ID varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
StartDate date NOT NULL,
EndDate date,
Privacy varchar(10),
Kilometers decimal(5,2),
Type varchar(50)
)

CREATE TABLE dbo.Competition(
ID varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
Route varchar(MAX),
Date date NOT NULL,
Privacy varchar(10),
BankAccount varchar(50) NOT NULL,
Price decimal(5,2),
ActivityID varchar(50),
)

CREATE TABLE dbo.Competition_Categories(
CompetitionID varchar(50) NOT NULL,
Category varchar(10) NOT NULL,
)

CREATE TABLE dbo.Groups(
Name varchar(50) NOT NULL,
AdminUsername varchar(50) NOT NULL,
)

CREATE TABLE Group_Member(
GroupName varchar(50) NOT NULL,
MemberID varchar(50) NOT NULL,
)

CREATE TABLE dbo.Sponsor(
Id varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
BankAccount varchar(50) NOT NULL,
CompetitionID varchar(50)
)

---------------------------------------------------------------------------------------

ALTER TABLE dbo.Activity
ADD PRIMARY KEY (ID)

ALTER TABLE dbo.Activity_In_Challenge
ADD CONSTRAINT PK_ActCh PRIMARY KEY(ActivityID,ChallengeID)

ALTER TABLE dbo.Athlete
ADD PRIMARY KEY (Username)

ALTER TABLE Athlete_Followers
ADD CONSTRAINT PK_ATH_FOLL PRIMARY KEY(AthleteID,FollowerID)

ALTER TABLE dbo.Athlete_In_Challenge
ADD CONSTRAINT PK_AthCh PRIMARY KEY(AthleteID,ChallengeID)

ALTER TABLE dbo.Athlete_In_Competition
ADD CONSTRAINT PK_AthCo PRIMARY KEY(AthleteID,CompetitionID)

ALTER TABLE dbo.Challenge
ADD PRIMARY KEY (ID)

ALTER TABLE dbo.Competition
ADD PRIMARY KEY (ID)

ALTER TABLE dbo.Competition_Categories
ADD CONSTRAINT PK_CompCat PRIMARY KEY(CompetitionID,Category)

ALTER TABLE dbo.Groups
ADD PRIMARY KEY (Name)

ALTER TABLE Group_Member
ADD CONSTRAINT PK_GrMem PRIMARY KEY(GroupName,MemberID)

ALTER TABLE dbo.Sponsor
ADD CONSTRAINT PK_Sponsor PRIMARY KEY (ID)




--------------------------------------------------------------------------------------

ALTER TABLE dbo.Activity
ADD CONSTRAINT Activity_Athlete_FK FOREIGN KEY(AthleteUsername)
REFERENCES dbo.Athlete(Username)



ALTER TABLE dbo.Competition
ADD CONSTRAINT Competition_Activity_FK FOREIGN KEY(ActivityID)
REFERENCES dbo.Activity(ID)


ALTER TABLE dbo.Sponsor
ADD CONSTRAINT Sponsor_compId_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(ID)


ALTER TABLE dbo.Groups
ADD CONSTRAINT Group_Admin_FK FOREIGN KEY(AdminUsername)
REFERENCES dbo.Athlete(Username)


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
