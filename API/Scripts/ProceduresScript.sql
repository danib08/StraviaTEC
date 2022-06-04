-----------------------Activity stored procedures------------------

create procedure get_all_activities
as
begin

select * from dbo.Activity
end
go

create procedure get_activity(@Id varchar(50))
as
begin

select * from dbo.Activity
where Id = @Id

end
go

create procedure post_activity(
								@Id varchar(50),
								@Name varchar(50),
								@Route varchar(MAX),
								@Date datetime,
								@Duration time,
								@Kilometers decimal(5,2),
								@Type varchar(50),
								@AthleteUsername varchar(50)
																
)
as
begin

	insert into dbo.Activity(Id,Name,Route,Date,Duration,Kilometers	,Type,AthleteUsername)
	values(@Id,@Name,@Route,@Date,@Duration,@Kilometers,@Type,@AthleteUsername)
end
go

create procedure put_activity(	@Id varchar(50),
								@Name varchar(50),
								@Route varchar(MAX),
								@Date datetime,
								@Kilometers decimal(5,2),
								@Type varchar(50),
								@AthleteUsername varchar(50))
as
begin
update dbo.Activity set Name=@Name,Route=@Route,Date=@Date,Type=@Type,AthleteUsername=@AthleteUsername
where Id=@Id
end
go 

create procedure delete_activity(@Id varchar(50))
as
begin
delete from dbo.Activity
where Id = @Id
end 
go


----------------------Athlete stored procedures------------------
/*

create procedure get_all_athletes
as
begin

select * from dbo.Athlete
end
go


create procedure get_athlete(@Username varchar(50))
as
begin

select * from dbo.Athlete
where Username = @Username

end
go

create procedure post_athlete(
								@Username varchar(50),
								@Name varchar(50),
								@LastName varchar(50),
								@Photo varchar(MAX),
								@Age int,
								@BirthDate date,
								@Pass varchar(50),
								@Nationality varchar(50),
								@Category varchar(50)
								
)
as
begin

	insert into dbo.Athlete(Username,Name,LastName,Photo,Age,BirthDate,Pass,Nationality,Category)
	values(@Username,@Name,@LastName,@Photo,year(getdate())-year(@BirthDate),@BirthDate,@Pass,@Nationality,@Category)
	if (year(getdate())-year(@BirthDate)) < 15
	begin
		update dbo.Athlete 
		set Category = 'Junior'
		where Username = @Username
	end
	if( 14 < (year(getdate())-year(@BirthDate)) and (year(getdate())-year(@BirthDate)) < 24)
	begin
		update dbo.Athlete 
		set Category = 'Sub-23'
		where Username = @Username
	end
	if( 23 < (year(getdate())-year(@BirthDate)) and (year(getdate())-year(@BirthDate)) < 30)
	begin
		update dbo.Athlete 
		set Category = 'Open'
		where Username = @Username
	end
	if( 30 < (year(getdate())-year(@BirthDate)) and (year(getdate())-year(@BirthDate)) < 41)
	begin
		update dbo.Athlete 
		set Category = 'Master A'
		where Username = @Username
	end
	if( 40 < (year(getdate())-year(@BirthDate)) and (year(getdate())-year(@BirthDate)) < 51)
	begin
		update dbo.Athlete 
		set Category = 'Master B'
		where Username = @Username
	end
	if( 50 < (year(getdate())-year(@BirthDate)))
	begin
		update dbo.Athlete 
		set Category = 'Master C'
		where Username = @Username
	end
	
end
go

create procedure login_athlete( @Username varchar(50),
								@Pass varchar(50)

)
as
begin
select * from dbo.Athlete where Username = @Username and Pass = @Pass 
end 
go


create procedure search_athlete_lastname( @Name varchar(50),
								@LastName varchar(50)

)
as
begin
select * from dbo.Athlete where Name = @Name and LastName = @LastName 
end 
go

create procedure search_athlete_name( @Name varchar(50)

)
as
begin
select * from dbo.Athlete where Name = @Name
end 
go


create procedure put_athlete(@Username varchar(50),
								@Name varchar(50),
								@LastName varchar(50),
								@Photo varchar(MAX),
								@Age int,
								@BirthDate date,
								@Pass varchar(50),
								@Nationality varchar(50),
								@Category varchar(50))
as
begin
update dbo.Athlete set Name=@Name,LastName=@LastName,Photo=@Photo,Age=@Age,BirthDate=@BirthDate,Pass=@Pass,Nationality=@Nationality,Category=@Category
where Username=@Username

end
go 


create procedure delete_athlete(@Username varchar(50))
as
begin
delete from dbo.Athlete
where Username = @Username
end 
go

*/

----------------------Athlete friend's stored procedures------------------


create procedure get_all_followers
as
begin

select * from dbo.Athlete_Followers
end
go

create procedure get_follower(@AthleteID varchar(50),
							@FollowerID varchar(50))
as
begin

select * from dbo.Athlete_Followers
where AthleteID = @AthleteID and FollowerID = @FollowerID
end
go


create procedure get_ath_followers(@AthleteID varchar(50))
as
begin
select * from dbo.Athlete_Followers
where AthleteID = @AthleteID
end
go


create procedure get_aths_follower(@FollowerID varchar(50))
as
begin

select * from dbo.Athlete_Followers
where FollowerID = @FollowerID

end
go


create procedure post_follower(
							@AthleteID varchar(50),
							@FollowerID varchar(50)
)
as
begin
		insert into dbo.Athlete_Followers(AthleteID,FollowerID)
		values(@AthleteID,@FollowerID)
end
go

create procedure delete_follower( @AthleteID varchar(50),
								@FollowerID varchar(50))
as
begin
delete from dbo.Athlete_Followers
where AthleteID = @AthleteID and FollowerID = @FollowerID
end 
go


----------------------Athlete In Challenge stored procedures------------------

create procedure get_all_AICH
as
begin

select * from dbo.Athlete_In_Challenge
end
go


create procedure get_Athlete_Challenge(@AthleteID varchar(50),
							@ChallengeID varchar(50))
as
begin

select * from dbo.Athlete_In_Challenge
where AthleteID = @AthleteID and ChallengeID = @ChallengeID

end
go

create procedure get_One_Athlete_Challenges(@AthleteID varchar(50))
as
begin

select * from dbo.Athlete_In_Challenge
where AthleteID = @AthleteID 
end
go


create procedure get_Athlete_OneChallenge(@ChallengeID varchar(50))
as
begin

select * from dbo.Athlete_In_Challenge
where ChallengeID = @ChallengeID 
end
go

create procedure get_Ath_OneChallenge_Accepted(@ChallengeID varchar(50))
as begin
select * from dbo.Athlete_In_Challenge
where ChallengeID = @ChallengeID and Status = 'En curso'
end
go

create procedure get_not_inscribed(@Username varchar(50))
as begin
select * from dbo.Athlete_In_Challenge
where AthleteID = @Username 
end
go

create procedure get_not_inscribed_Chall(@Username varchar(50))
as begin
select distinct ID, Name, StartDate, EndDate, Privacy, Kilometers, Type
from(Athlete_In_Challenge right join Challenge
on Athlete_In_Challenge.ChallengeID = Challenge.ID)
where AthleteID is null or
ChallengeID not in
(select ChallengeID from Athlete_In_Challenge
where AthleteID = @Username)
end
go



create procedure post_Athlete_Challenge(
							@AthleteID varchar(50),
							@ChallengeID varchar(50),
							@Status varchar(50)
)
as
begin

	insert into dbo.Athlete_In_Challenge(AthleteID,ChallengeID,Status)
	values(@AthleteID,@ChallengeID,@Status)

end
go

create procedure delete_Athlete_Challenge( @AthleteID varchar(50),
								@ChallengeID varchar(50))
as
begin
delete from dbo.Athlete_In_Challenge
where AthleteID = @AthleteID and ChallengeID = @ChallengeID
end 
go

----------------------Athlete In Competition stored procedures------------------

create procedure get_all_AICO
as
begin

select * from dbo.Athlete_In_Competition
end
go


create procedure get_Athlete_Competition(@AthleteID varchar(50),
							@CompetitionID varchar(50))
as
begin

select * from dbo.Athlete_In_Competition
where AthleteID = @AthleteID and CompetitionID = @CompetitionID
end
go


create procedure get_OneAth_Competitions(@AthleteID varchar(50))
as begin
select * from dbo.Athlete_In_Competition
where AthleteID = @AthleteID
end
go


create procedure get_Ath_OneCompetition(@CompetitionID varchar(50))
as begin
select * from dbo.Athlete_In_Competition
where CompetitionID = @CompetitionID
end
go


create procedure get_Ath_OneCompetition_Accepted(@CompetitionID varchar(50))
as begin
select * from dbo.Athlete_In_Competition
where CompetitionID = @CompetitionID and Status = 'Aceptado'
end
go

create procedure get_comp_Report(@CompetitionID varchar(50))
as begin
select * from compReport
where CompetitionID = @CompetitionID
order by Duration
end 
go

create procedure get_not_inscribed_Comp(@Username varchar(50))
as begin
select distinct ID, Name, Route, Date, Privacy, BankAccount, Price, ActivityID
from (Athlete_In_Competition right join Competition
on Athlete_In_Competition.CompetitionID = Competition.ID)
where AthleteID is null or
CompetitionID not in 
(select CompetitionID from Athlete_In_Competition 
where AthleteID = @Username)
end
go



create procedure post_Athlete_Competition(
							@AthleteID varchar(50),
							@CompetitionID varchar(50),
							@Status varchar(50),
							@Receipt varchar(200),
							@Duration time
)
as
begin
	insert into dbo.Athlete_In_Competition(AthleteID,CompetitionID,Status,Receipt,Duration)
	values(@AthleteID,@CompetitionID,@Status,@Receipt,@Duration)
end
go


create procedure put_Athlete_Competition(
							@AthleteID varchar(50),
							@CompetitionID varchar(50),
							@Status varchar(50),
							@Receipt varchar(200),
							@Duration time)
as
begin
update dbo.Athlete_In_Competition set AthleteID=@AthleteID,CompetitionID=@CompetitionID,Status=@Status,Receipt=@Receipt,Duration=@Duration
where AthleteID=@AthleteID and CompetitionID=@CompetitionID
end
go



create procedure delete_Athlete_Competition( @AthleteID varchar(50),
								@CompetitionID varchar(50))
as
begin
delete from dbo.Athlete_In_Competition
where AthleteID = @AthleteID and CompetitionID = @CompetitionID
end 
go

----------------------Challenge stored procedures------------------

create procedure get_all_challenges
as
begin

select * from dbo.Challenge
end
go


create procedure get_challenge(@Id varchar(50))
as
begin

select * from dbo.Challenge
where Id = @Id
end
go

/*
create procedure get_challenge_creator(@Username varchar(50))
as
begin
select * from dbo.ChallCreator
where AthleteUsername = @Username
end
go
*/


create procedure post_challenge(
								@Id varchar(50),
								@Name varchar(50),
								@StartDate date,
								@EndDate date,
								@Privacy varchar(50),
								@Kilometers decimal(5,2),
								@Type varchar(50)
																
)
as
begin

	insert into dbo.Challenge(Id,Name,StartDate,EndDate,Privacy,Kilometers,Type)
	values(@Id,@Name,@StartDate,@EndDate,@Privacy,@Kilometers,@Type)
	
end
go



create procedure put_challenge(@Id varchar(50),
								@Name varchar(50),
								@StartDate date,
								@EndDate date,
								@Privacy varchar(50),
								@Kilometers int,
								@Type varchar(50))
as
begin
update dbo.Challenge set Name=@Name,StartDate=@StartDate,EndDate=@EndDate,Privacy=@Privacy,Kilometers=@Kilometers,Type=@Type
where Id=@Id

end
go 


create procedure delete_challenge(@Id varchar(50))
as
begin
delete from dbo.Challenge
where Id = @Id
end 
go


----------------------Competition stored procedures------------------

create procedure get_all_competition
as
begin

select * from dbo.Competition
end
go


create procedure get_competition(@Id varchar(50))
as
begin

select * from dbo.Competition
where Id = @Id

end
go

/*
create procedure get_competition_creator(@Username varchar(50))
as
begin
select * from dbo.CompsCreator
where AthleteUsername = @Username
end
go
*/

create procedure post_competition(
								@Id varchar(50),
								@Name varchar(50),
								@Route varchar(MAX),
								@Date date,
								@Privacy varchar(50),
								@BankAccount varchar(50),
								@Price decimal(5,2),
								@ActivityID varchar(50)
)
as
begin

	insert into dbo.Competition(Id,Name,Route,Date,Privacy,BankAccount,Price,ActivityID)
	values(@Id,@Name,@Route,@Date,@Privacy,@BankAccount,@Price,@ActivityID)

end
go


create procedure put_competition(@Id varchar(50),
								@Name varchar(50),
								@Route varchar(MAX),
								@Date date,
								@Privacy varchar(50),
								@BankAccount varchar(50),
								@Price decimal(5,2),
								@ActivityID varchar(50))
as
begin
update dbo.Competition set Name=@Name,Route=@Route,Date=@Date,Privacy=@Privacy,BankAccount=@BankAccount,Price=@Price,ActivityID=@ActivityID
where Id=@Id

end
go


create procedure delete_competition(@Id varchar(50))
as
begin
delete from dbo.Competition
where Id = @Id
end 
go


----------------------Competition categories stored procedures------------------


create procedure get_all_compCategories
as
begin

select * from dbo.Competition_Categories
end
go


create procedure get_compCategory(@CompetitionID varchar(50),
							@Category varchar(50))
as
begin

select * from dbo.Competition_Categories
where CompetitionID = @CompetitionID and Category = @Category
end
go

create procedure get_OnecompCategories(@CompetitionID varchar(50))
as
begin

select * from dbo.Competition_Categories
where CompetitionID = @CompetitionID
end
go

create procedure get_comps_OneCategory(@Category varchar(50))
as
begin

select * from dbo.Competition_Categories
where Category = @Category
end
go


create procedure post_compCategories(@CompetitionID varchar(50),
							@Category varchar(50)
)
as
begin
	insert into dbo.Competition_Categories(CompetitionID,Category)
	values(@CompetitionID,@Category)
end
go


create procedure delete_compCategories( @CompetitionID varchar(50),
								@Category varchar(50))
as
begin
delete from dbo.Competition_Categories
where CompetitionID = @CompetitionID and Category = @Category
end 
go

----------------------Group stored procedures------------------

create procedure get_all_groups
as
begin

select * from dbo.Groups
end
go


create procedure get_group(@Name varchar(50))
as
begin

select * from dbo.Groups
where Name = @Name
end
go

create procedure get_group_byAdmin(@Name varchar(50))
as
begin

select * from dbo.Groups
where AdminUsername = @Name
end
go


create procedure post_group(
							@Name varchar(50),
							@AdminUsername varchar(50)
)
as
	begin
		insert into dbo.Groups(Name,AdminUsername)
		values(@Name,@AdminUsername)

end
go


create procedure put_group(@Name varchar(50),
							@AdminUsername varchar(50))
as
begin
update dbo.Groups set Name=@Name, AdminUsername=@AdminUsername
where Name=@Name

end
go


create procedure delete_group(@Name varchar(50))
as
begin
delete from dbo.Groups
where Name = @Name
end 
go


----------------------Group member stored procedures------------------

create procedure get_all_groupMembers
as
begin

select * from dbo.Group_Member
end
go


create procedure get_groupMember(@GroupName varchar(50),
							@MemberID varchar(50))
as
begin

select * from dbo.Group_Member
where GroupName = @GroupName and MemberID = @MemberID

end
go

create procedure get_One_groupMembers(@GroupName varchar(50))
as
begin

select * from dbo.Group_Member
where GroupName = @GroupName 
end
go

create procedure get_groups_OneMembers(@MemberID varchar(50))
as
begin

select * from dbo.Group_Member
where MemberID = @MemberID 
end
go

create procedure get_not_inscribed_Groups(@Username varchar(50))
as begin
select distinct Name, AdminUsername
from (Group_Member right join Groups
on Groups.Name= Group_Member.GroupName)
where MemberID is null or
GroupName not in 
(select GroupName from Group_Member
where MemberID = @Username)
end
go


create procedure post_groupMember(@GroupName varchar(50),
							@MemberID varchar(50)
)
as
begin

	insert into dbo.Group_Member(GroupName,MemberID)
	values(@GroupName,@MemberID)
end
go


create procedure delete_groupMember( @GroupName varchar(50),
									 @MemberID varchar(50))
as
begin
delete from dbo.Group_Member
where GroupName = @GroupName and MemberID = @MemberID
end 
go


----------------------Sponsor stored procedures------------------

create procedure get_all_sponsors
as
begin

select * from dbo.Sponsor
end
go


create procedure get_sponsors(@Id varchar(50))
as
begin

select * from dbo.Sponsor
where Id = @Id

end
go


create procedure post_sponsors(@Id varchar(50),
							@Name varchar(50),
							@BankAccount varchar(50),
							@CompetitionID varchar(50)
							
)
as
begin

	insert into dbo.Sponsor(Id,Name,BankAccount,CompetitionID)
	values(@Id,@Name,@BankAccount,@CompetitionID)
	
end
go


create procedure put_sponsor(@Id varchar(50),
							@Name varchar(50),
							@BankAccount varchar(50),
							@CompetitionID varchar(50)
)
as
begin
update dbo.Sponsor set Name=@Name,BankAccount=@BankAccount,CompetitionID=@CompetitionID
where Id=@Id

end
go

create procedure delete_sponsors(@Id varchar(50))
as
begin
delete from dbo.Sponsor
where Id = @Id
end 
go

----------------------Activities in Challenge stored procedures------------------


create procedure get_all_ActChallenge
as
begin

select * from dbo.Activity_In_Challenge
end
go


create procedure get_ActChallenge(@ActivityID varchar(50),
							@ChallengeID varchar(50))
as
begin

select * from dbo.Activity_In_Challenge
where ActivityID = @ActivityID and ChallengeID = @ChallengeID
end
go

create procedure get_OneAct_Challenges(@ActivityID varchar(50))
as
begin

select * from dbo.Activity_In_Challenge
where ActivityID = @ActivityID 
end
go

create procedure get_Acts_OneChallenge(@ChallengeID varchar(50))
as
begin

select * from dbo.Activity_In_Challenge
where ChallengeID = @ChallengeID 
end
go



create procedure post_ActChallenge(@ActivityID varchar(50),
							@ChallengeID varchar(50)
)
as
begin

	insert into dbo.Activity_In_Challenge(ActivityID,ChallengeID)
	values(@ActivityID,@ChallengeID)

end
go

create procedure delete_ActChallenge( @ActivityID varchar(50),
									 @ChallengeID varchar(50))
as
begin
delete from dbo.Activity_In_Challenge
where ActivityID = ActivityID and ChallengeID = @ChallengeID
end
go


---------------------------------------------------------------------------------------------

create procedure get_feed(@Username varchar(50))
as
begin
select FollowerID,ID,Name,Route,Date,Duration,Kilometers,Type from dbo.friends_Act
where  AthleteID = @Username
order by Date asc
end
go
