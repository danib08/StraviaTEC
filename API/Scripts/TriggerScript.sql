--------------------------------------------------VIEWS----------------------------------


create view friends_Act as
select AthleteID, FollowerID,ID,Name,Route,Date,Duration,Kilometers,Type 
from( Athlete_Followers inner join Activity 
on Athlete_Followers.FollowerID = Activity.AthleteUsername)
go


create view compReport as
select Name, LastName, Age, Category, Duration, CompetitionID
from( Athlete inner join Athlete_In_Competition
on Athlete.Username = Athlete_In_Competition.AthleteID)
go


create view CompsCreator as
select Competition.ID as Id, Competition.Name, Competition.Route, Competition.Date, Competition.Privacy,Competition.BankAccount,Competition.Price,Competition.ActivityID, AthleteUsername 
from(Competition inner join Activity
on Competition.ActivityID = Activity.ID)
go

create view ChallCreator as
select Challenge.ID as Id, Challenge.Name, Challenge.StartDate, Challenge.EndDate, Challenge.Privacy, Challenge.Kilometers, Challenge.Type, Challenge.ActivityID, AthleteUsername
from(Challenge inner join Activity
on Challenge.ActivityID = Activity.ID)
go



----------------------------------------TRIGGERS ----------------------------------
create trigger statusComp
on dbo.Athlete_In_Competition
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN
update dbo.Athlete_In_Competition
set Status = 'No aceptado'
end
go

create trigger statusChall
on dbo.Athlete_In_Challenge
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN
update dbo.Athlete_In_Challenge
set Status = 'En curso'
end
go

/*
create trigger KmChallenge
on dbo.KilometersChall
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN
declare @Username varchar(50)
select @Username = AthleteUsername from inserted
update Athlete_In_Challenge
set Kilometers = (select SUM(ActKM) as actualkm from KilometersChall
where chalid = athchalid and AthleteUsername = 'gabogh99' and chalid = 'Chal1'
GROUP BY chalid)
where AthleteUsername = @Username
end
go

select SUM(ActKM) as actualkm from KilometersChall
where chalid = athchalid and AthleteUsername = 'gabogh99' and chalid = 'Chal1'
GROUP BY chalid

select * from Activity_In_Challenge

insert into dbo.Activity_In_Challenge
(ActivityID,ChallengeID)
values('Act8', 'Chal1')
*/
--------------------------------------------------TRIGGERS ATHLETE----------------------------------


create trigger DupAthlete
on dbo.Athlete instead of insert
as
begin
declare @Username varchar(50)
select @Username = Username from inserted

	if not exists(select top 1 * from Athlete where Username = @Username)
	begin	
		insert into Athlete(Username, Name, LastName, Photo, Age, BirthDate, Pass, Nationality, Category)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go

create trigger DelAthlete
on dbo.Athlete instead of delete
as
begin
declare @Username varchar(50)
select @Username = Username from deleted

	if exists(select top 1 * from Athlete where Username = @Username)
	begin
	
		delete from Activity
		where AthleteUsername = @Username
		delete from Athlete_In_Challenge
		where AthleteID = @Username
		delete from Athlete_Followers
		where AthleteID = @Username or FollowerID = @Username
		delete from Groups
		where AdminUsername = @Username 
		delete from Athlete_In_Competition
		where AthleteID = @Username
		delete from Group_Member
		where MemberID = @Username
		
		
	end
	else
	begin
		print 'Este Usuario no existe'
	end
	
	delete from Athlete
	where Username = @Username
	
end
go

--------------------------------------------Triggers Activity In Challenge---------------------------------

create trigger DupActInChal
on dbo.Activity_In_Challenge instead of insert
as
begin
declare @ActivityID varchar(50)
declare @ChallengeID varchar(50)
select @ActivityID = ActivityID, @ChallengeID = ChallengeID from inserted

	if not exists(select top 1 * from dbo.Activity_In_Challenge where ActivityID = @ActivityID and ChallengeID = @ChallengeID )
	begin	
		insert into dbo.Activity_In_Challenge(ActivityID, ChallengeID)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


--------------------------------------------Triggers ACTIVITY---------------------------------

create trigger DupActivity
on dbo.Activity instead of insert
as
begin
declare @Id varchar(50)
select @Id = Id from inserted

	if not exists(select top 1 * from dbo.Activity where Id = @Id)
	begin	
		insert into dbo.Activity(Id, Name, Route, Date, Duration, Kilometers, Type, AthleteUsername)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


CREATE trigger DelAct
on dbo.Activity instead of delete
as
begin
declare @Id varchar(50)
select @Id = Id from deleted

	if exists(select top 1 * from Activity where Id = @Id)
	begin	
		delete from Competition
		where ActivityID = @Id
		delete from Activity_In_Challenge
		where ActivityID = @Id
		
	end
	else
	begin
		print 'Esta Competición no existe'
	end
	
	delete from Activity
	where Id = @Id
	
end
go



--------------------------------------------Triggers Athlete Followers---------------------------------

create trigger DupFollower
on dbo.Athlete_Followers instead of insert
as
begin
declare @AthleteID varchar(50)
declare @FollowerID varchar(50)
select @AthleteID = AthleteID, @FollowerID = FollowerID from inserted

	if not exists(select top 1 * from dbo.Athlete_Followers where AthleteID = @AthleteID and FollowerID = @FollowerID )
	begin	
		insert into dbo.Athlete_Followers(AthleteID, FollowerID)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go

--------------------------------------------Triggers Athlete In Challenge---------------------------------

create trigger DupAICH
on dbo.Athlete_In_Challenge instead of insert
as
begin
declare @AthleteID varchar(50)
declare @ChallengeID varchar(50)
select @AthleteID = AthleteID, @ChallengeID = ChallengeID from inserted

	if not exists(select top 1 * from dbo.Athlete_In_Challenge where AthleteID = @AthleteID and ChallengeID = @ChallengeID )
	begin	
		insert into dbo.Athlete_In_Challenge(AthleteID, ChallengeID, Status,Kilometers)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


--------------------------------------------Triggers Athlete In Competition---------------------------------

create trigger DupAICO
on dbo.Athlete_In_Competition instead of insert
as
begin
declare @AthleteID varchar(50)
declare @CompetitionID varchar(50)
select @AthleteID = AthleteID, @CompetitionID = CompetitionID from inserted

	if not exists(select top 1 * from dbo.Athlete_In_Competition where AthleteID = @AthleteID and CompetitionID = @CompetitionID )
	begin	
		insert into dbo.Athlete_In_Competition(AthleteID, CompetitionID, Status, Receipt, Duration)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


--------------------------------------------Triggers CHALLENGE---------------------------------


create trigger DupChall
on dbo.Challenge instead of insert
as
begin
declare @Id varchar(50)
select @Id = Id from inserted

	if not exists(select top 1 * from dbo.Challenge where Id = @Id)
	begin	
		insert into dbo.Challenge(Id, Name, StartDate, EndDate, Privacy, Kilometers, Type, ActivityID)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go

CREATE trigger DelChall
on dbo.Challenge instead of delete
as
begin
declare @Id varchar(50)
select @Id = Id from deleted
declare @ActID varchar(50)
select @ActID = ActivityID from deleted

	if exists(select top 1 * from Challenge where Id = @Id)
	begin	
		delete from Activity_In_Challenge
		where ChallengeID = @Id
		delete from Athlete_In_Challenge
		where ChallengeID = @Id	

	end
	else
	begin
		print 'Esta Competición no existe'
	end
	
	delete from Challenge
	where Id = @Id
	
end
go


--------------------------------------------Triggers COMPETITION---------------------------------



create trigger DupComp
on dbo.Competition instead of insert
as
begin
declare @Id varchar(50)
select @Id = Id from inserted

	if not exists(select top 1 * from dbo.Competition where Id = @Id)
	begin	
		insert into dbo.Competition(Id, Name, Route, Date, Privacy, BankAccount, Price, ActivityID)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


create trigger DelComp
on dbo.Competition instead of delete
as
begin
declare @Id varchar(50)
select @Id = Id from deleted

	if exists(select top 1 * from Competition where Id = @Id)
	begin	
		delete from Sponsor
		where CompetitionID = @Id
		delete from Competition_Categories
		where CompetitionID = @Id
		delete from Athlete_In_Competition
		where CompetitionID = @Id		
	end
	else
	begin
		print 'Esta Competición no existe'
	end
	
	delete from Competition
	where Id = @Id
	
end
go


--------------------------------------------Triggers COMPETITION Categories---------------------------------

create trigger DupCompCat
on dbo.Competition_Categories instead of insert
as
begin
declare @CompetitionID varchar(50)
declare @Category varchar(50)
select @CompetitionID = CompetitionID, @Category = Category from inserted

	if not exists(select top 1 * from dbo.Competition_Categories where CompetitionID = @CompetitionID and Category = @Category )
	begin	
		insert into dbo.Competition_Categories(CompetitionID, Category)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


--------------------------------------------Triggers GROUP---------------------------------


create trigger DupGroup
on dbo.Groups instead of insert
as
begin
declare @Name varchar(50)
select @Name = Name from inserted

	if not exists(select top 1 * from dbo.Groups where Name = @Name)
	begin	
		insert into dbo.Groups(Name, AdminUsername)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go

create trigger DelGroup
on dbo.Groups instead of delete
as
begin
declare @Name varchar(50)
select @Name = Name from deleted

	if exists(select top 1 * from Groups where Name = @Name)
	begin	
		delete from Group_Member
		where GroupName = @Name
	
	end
	else
	begin
		print 'Este Grupo no existe'
	end
	
	delete from Groups
	where Name = @Name
end
go


--------------------------------------------Triggers Group Member---------------------------------

create trigger DupGroupMem
on dbo.Group_Member instead of insert
as
begin
declare @GroupName varchar(50)
declare @MemberID varchar(50)
select @GroupName = GroupName, @MemberID = MemberID from inserted

	if not exists(select top 1 * from dbo.Group_Member where GroupName = @GroupName and MemberID = @MemberID )
	begin	
		insert into dbo.Group_Member(GroupName, MemberID)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go




--------------------------------------------Triggers SPONSOR---------------------------------



create trigger DupSponsor
on dbo.Sponsor instead of insert
as
begin
declare @Id varchar(50)
select @Id = Id from inserted

	if not exists(select top 1 * from dbo.Sponsor where Id = @Id)
	begin	
		insert into dbo.Sponsor(Id, Name, BankAccount, CompetitionID)
		select * from inserted i
	end
	else
	begin
		select * from sys.messages where  message_id=2601 and language_id = 1033
	end
end
go


