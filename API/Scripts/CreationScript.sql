CREATE TABLE dbo.Athlete(
Username varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
Photo varchar(50),
Age int,
BirthDate date NOT NULL,
Pass varchar(50) NOT NULL,
Nationality varchar(50) NOT NULL,
Category varchar(50),
PRIMARY KEY (Username)
)

CREATE TABLE dbo.Challenge(
Id varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
StartDate date NOT NULL,
EndDate date,
Privacy varchar(10),
Kilometers decimal(5,2),
Type varchar(50),
PRIMARY KEY (Id)
)

CREATE TABLE dbo.Activity(
Id varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
Route varchar(50),
Date date NOT NULL,
Kilometers decimal(5,2),
Type varchar(50),
ChallengeID varchar(50),
PRIMARY KEY (Id)
)

CREATE TABLE dbo.Competition(
Id varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
Route varchar(50),
Date date NOT NULL,
Privacy varchar(10),
BankAccount varchar(50) NOT NULL,
Price decimal(5,2),
ActivityID varchar(50),
PRIMARY KEY (Id)
)

CREATE TABLE dbo.Sponsor(
Id varchar(50) NOT NULL,
Name varchar(50) NOT NULL,
BankAccount varchar(50) NOT NULL,
CompetitionID varchar(50),
ChallengeID varchar(50),
PRIMARY KEY (Id),
)

CREATE TABLE dbo.Groups(
Name varchar(50) NOT NULL,
AdminUsername varchar(50) NOT NULL,
PRIMARY KEY (Name)
)

CREATE TABLE dbo.Athlete_In_Challenge(
AthleteID varchar(50) NOT NULL,
ChallengeID varchar(50) NOT NULL
PRIMARY KEY(AthleteID,ChallengeID)
)

CREATE TABLE dbo.Athlete_In_Competition(
AthleteID varchar(50) NOT NULL,
CompetitionID varchar(50) NOT NULL,
Position int,
Time time,
PRIMARY KEY(AthleteID,CompetitionID)
)

CREATE TABLE dbo.Competition_Categories(
CompetitionID varchar(50),
CompCategory varchar(10),
PRIMARY KEY(CompetitionID, CompCategory)
)

CREATE TABLE Group_Member(
GroupName varchar(50),
MemberID varchar(50),
PRIMARY KEY(GroupName, MemberID)
)

CREATE TABLE Athlete_Friends(
AthleteID varchar(50),
FriendID varchar(50)
PRIMARY KEY(AthleteID, FriendID)
)



ALTER TABLE dbo.Activity
ADD CONSTRAINT Activity_Challenge_FK FOREIGN KEY(ChallengeID)
REFERENCES dbo.Challenge(Id);

ALTER TABLE dbo.Competition
ADD CONSTRAINT Competition_Activity_FK FOREIGN KEY(ActivityID)
REFERENCES dbo.Activity(Id);

ALTER TABLE dbo.Sponsor
ADD CONSTRAINT Sponsor_compId_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(Id);

ALTER TABLE dbo.Sponsor
ADD CONSTRAINT Sponsor_chalId_FK FOREIGN KEY(ChallengeID)
REFERENCES dbo.Challenge(Id);

ALTER TABLE dbo.Groups
ADD CONSTRAINT Group_Admin_FK FOREIGN KEY(AdminUsername)
REFERENCES dbo.Athlete(Username);

ALTER TABLE dbo.Athlete_In_Challenge
ADD CONSTRAINT AIC_Ath_FK FOREIGN KEY(AthleteID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_In_Challenge
ADD CONSTRAINT AIC_Chal_FK FOREIGN KEY(ChallengeID)
REFERENCES dbo.Challenge(Id)

ALTER TABLE dbo.Athlete_In_Competition
ADD CONSTRAINT AICo_Ath_FK FOREIGN KEY(AthleteID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_In_Competition
ADD CONSTRAINT AICo_Chal_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(Id)

ALTER TABLE dbo.Competition_Categories
ADD CONSTRAINT Comp_Cat_FK FOREIGN KEY(CompetitionID)
REFERENCES dbo.Competition(Id)

ALTER TABLE dbo.Group_Member
ADD CONSTRAINT Group_Name_FK FOREIGN KEY(GroupName)
REFERENCES dbo.Groups(Name)

ALTER TABLE dbo.Group_Member
ADD CONSTRAINT Group_Member_FK FOREIGN KEY(MemberID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_Friends
ADD CONSTRAINT Ath_Id_FK FOREIGN KEY(AthleteID)
REFERENCES dbo.Athlete(Username)

ALTER TABLE dbo.Athlete_Friends
ADD CONSTRAINT Ath_Friend_FK FOREIGN KEY(FriendID)
REFERENCES dbo.Athlete(Username)