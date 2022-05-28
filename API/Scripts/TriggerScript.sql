--------------------------------------------------VIEWS----------------------------------


create view friends_Act as
select AthleteID,FollowerID,ID as ActID
from( Athlete_Followers inner join Activity 
on Athlete_Followers.FollowerID = Activity.AthleteUsername)
go

create view competition_pos as
select AthleteID ,CompetitionID ,Status
from dbo.Athlete_In_Competition
go

create view challActivities as
select 



--------------------------------------------------TRIGGERS----------------------------------

create trigger ageAthlete
on dbo.Athlete
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN
update dbo.Athlete
set Age = year(getdate())-year(BirthDate)
end
go


create trigger statusComp
on dbo.Athlete_In_Competition
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN
update dbo.Athlete_In_Competition
set Status = 'Waiting'
end
go


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
		print 'Este usuario ya existe'
	end
end
go

create trigger updAthlete
on dbo.Athlete after update
as begin
declare @Username

if update(Username)
	update dbo.Competition
	set AthleteID = @Username
end
go
		

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
		print 'Esta Actividad ya existe'
	end
end
go

create trigger 

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
		print 'Esta Competicion ya existe'
	end
end
go


create trigger DupChall
on dbo.Challenge instead of insert
as
begin
declare @Id varchar(50)
select @Id = Id from inserted

	if not exists(select top 1 * from dbo.Challenge where Id = @Id)
	begin	
		insert into dbo.Challenge(Id, Name, StartDate, EndDate, Privacy, Kilometers, Type)
		select * from inserted i
	end
	else
	begin
		print 'Este reto ya existe'
	end
end
go




