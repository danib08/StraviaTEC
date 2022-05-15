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
								@Route varchar(50),
								@Date date,
								@Kilometers int,
								@Type varchar(50),
								@ChallengeID varchar(50)
																
)
as
begin
insert into dbo.Activity(Id,Name,Route,Date,Type,ChallengeID)
values(@Id,@Name,@Route,@Date,@Type,@ChallengeID)

end
go

create procedure put_activity(	@Id varchar(50),
								@Name varchar(50),
								@Route varchar(50),
								@Date date,
								@Kilometers int,
								@Type varchar(50),
								@ChallengeID varchar(50))
as
begin
update dbo.Activity set Name=@Name,Route=@Route,Date=@Date,Type=@Type,ChalengeID=@ChallengeID
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
								@Photo varchar(50),
								@Age int,
								@BirthDate date,
								@Pass varchar(50),
								@Nationality varchar(50),
								@Category varchar(50)
)
as
begin
insert into dbo.Athlete(Username,Name,LastName,Photo,Age,BirthDate,Pass,Nationality,Category)
values(@Username,@Name,@LastName,@Photo,@Age,@BirthDate,@Pass,@Nationality,@Category)

end
go

create procedure put_athlete(@Username varchar(50),
								@Name varchar(50),
								@LastName varchar(50),
								@Photo varchar(50),
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

----------------------Athlete friend'ss stored procedures------------------


create procedure get_all_friends
as
begin

select * from dbo.Athlete_Friends
end
go

create procedure get_friend(@AthleteID varchar(50),
							@FriendID varchar(50))
as
begin

select * from dbo.Athlete_Friends
where AthleteID = @AthleteID and FriendID = @FriendID

end
go


create procedure post_friend(
							@AthleteID varchar(50),
							@FriendID varchar(50)
)
as
begin
insert into dbo.Athlete_Friends(AthleteID,FriendID)
values(@AthleteID,@FriendID)

end
go

create procedure delete_friend( @AthleteID varchar(50),
								@FriendID varchar(50))
as
begin
delete from dbo.Athlete_Friends
where AthleteID = @AthleteID and FriendID = @FriendID
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


create procedure post_Athlete_Challenge(
							@AthleteID varchar(50),
							@ChallengeID varchar(50)
)
as
begin
insert into dbo.Athlete_In_Challenge(AthleteID,ChallengeID)
values(@AthleteID,@ChallengeID)
end
go

create procedure delete_friend( @AthleteID varchar(50),
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


create procedure post_Athlete_Competition(
							@AthleteID varchar(50),
							@ChallengeID varchar(50),
							@Position int,
							@Time time
)
as
begin
insert into dbo.Athlete_In_Competition(AthleteID,ChallengeID,Position,Time)
values(@AthleteID,@ChallengeID,@Position,@Time)

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


create procedure post_challenge(
								@Id varchar(50),
								@Name varchar(50),
								@StartDate date,
								@EndDate date,
								@Privacy varchar(50),
								@Kilometers int,
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


create procedure post_competition(
								@Id varchar(50),
								@Name varchar(50),
								@Route varchar(50),
								@Date date,
								@Privacy varchar(50),
								@BankAccount varchar(50),
								@Price decimal(5,2),
								@ActivityID varchar(50)
)
as
begin
insert into dbo.Competition(Id,Name,Route,Date,Privacy,BankAccount,Price,ActivityID)
values(@Id,@Name,@Route,@Date,@Privacy,@BankAccount,@Price,ActivityID)

end
go


create procedure put_competition(@Id varchar(50),
								@Name varchar(50),
								@Route varchar(50),
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


create procedure get_compCategories(@CompetitionID varchar(50),
							@CompCategory varchar(50))
as
begin

select * from dbo.Competition_Categories
where CompetitionID = @CompetitionID and CompCategory = @CompCategory

end
go


create procedure post_compCategories(@CompetitionID varchar(50),
							@CompCategory varchar(50)
)
as
begin
insert into dbo.Competition_Categories(CompetitionID,CompCategory)
values(@CompetitionID,@CompCategory)

end
go

create procedure delete_compCategories( @CompetitionID varchar(50),
								@CompCategory varchar(50))
as
begin
delete from dbo.Competition_Categories
where CompetitionID = @CompetitionID and CompCategory = @CompCategory
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
update dbo.Groups set AdminUsername=@AdminUsername
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
							@CompetitionID varchar(50),
							@ChallengeID varchar(50)
)
as
begin
insert into dbo.Sponsor(Id,Name,BankAccount,CompetitionID,ChallengeID)
values(@Id,@Name,@BankAccount,@CompetitionID,@ChallengeID)

end
go


create procedure put_csponsor(@Id varchar(50),
							@Name varchar(50),
							@BankAccount varchar(50),
							@CompetitionID varchar(50),
							@ChallengeID varchar(50))
as
begin
update dbo.Sponsor set Name=@Name,BankAccount=@BankAccount,CompetitionID=@CompetitionID,ChallengeID=@ChallengeID
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