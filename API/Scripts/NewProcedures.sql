create procedure proc_athlete(@Username varchar(50),
								@Name varchar(50),
								@LastName varchar(50),
								@Photo varchar(MAX),
								@Age int,
								@BirthDate date,
								@Pass varchar(50),
								@Nationality varchar(50),
								@Category varchar(50),
								@StatementType nvarchar(50) = '')
as begin

	if @StatementType = 'Insert'
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

	if @StatementType = 'Select'
	begin
		select * from dbo.Athlete
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Athlete
		where Username = @Username
	end

	if @StatementType = 'LogIn'
	begin
		select * from dbo.Athlete
		where Username = @Username and Pass = @Pass
	end

	if @StatementType = 'SearchLastName'
	begin
		select * from dbo.Athlete
		where Name = @Name and LastName = @LastName
	end

	if @StatementType = 'SearchName'
	begin
		select * from dbo.Athlete
		where Name = @Name 	
	end

	if @StatementType = 'Feed'
	begin
		select FollowerID as AthleteUsername,ID,Name,Route,Date,Duration,Kilometers,Type from dbo.friends_Act
		where  AthleteID = @Username
		order by Date asc
	end

	if @StatementType = 'CompCreator'
	begin
		select Id, Name, Route, Date, Privacy, BankAccount, Price, ActivityID from dbo.CompsCreator
		where AthleteUsername = @Username
	end

	if @StatementType = 'ChallCreator'
	begin
		select Id, Name, StartDate, EndDate, Privacy, Kilometers, Type,ActivityID  from dbo.ChallCreator
		where AthleteUsername = @Username
	end

	if @StatementType = 'Update'
	begin
		update dbo.Athlete set Name=@Name,LastName=@LastName,Photo=@Photo,Age=@Age,BirthDate=@BirthDate,Pass=@Pass,Nationality=@Nationality,Category=@Category
		where Username=@Username 	
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Athlete
		where Username = @Username
	end

end
go

create procedure proc_activity(@Id varchar(50),
								@Name varchar(50),
								@Route varchar(MAX),
								@Date datetime,
								@Duration time,
								@Kilometers decimal(5,2),
								@Type varchar(50),
								@AthleteUsername varchar(50),
								@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Activity(Id,Name,Route,Date,Duration,Kilometers	,Type,AthleteUsername)
		values(@Id,@Name,@Route,@Date,@Duration,@Kilometers,@Type,@AthleteUsername)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Activity
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Activity
		where Id = @Id
	end


	if @StatementType = 'Update'
	begin
		update dbo.Activity set Name=@Name,Route=@Route,Date=@Date,Type=@Type,AthleteUsername=@AthleteUsername
		where Id=@Id 	
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Activity
		where Id = @Id
	end
end
go


create procedure proc_athlete_followers(@AthleteID varchar(50),
										@FollowerID varchar(50),
										@StatementType nvarchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Athlete_Followers(AthleteID,FollowerID)
		values(@AthleteID,@FollowerID)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Athlete_Followers
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Athlete_Followers
		where AthleteID = @AthleteID and FollowerID = @FollowerID
	end

	if @StatementType = 'Following'
	begin
		select * from dbo.Athlete_Followers
		where FollowerID = @FollowerID			
	end

	if @StatementType = 'Followed'
	begin
		select * from dbo.Athlete_Followers
		where AthleteID = @AthleteID			
	end


	if @StatementType = 'Delete'
	begin
		delete from dbo.Athlete_Followers
		where AthleteID = @AthleteID and FollowerID = @FollowerID
	end
end
go

create procedure proc_athlete_in_challenge(@AthleteID varchar(50),
										   @ChallengeID varchar(50),
										   @Status varchar(50),
										   @Kilometers decimal(5,2),
										   @StatementType nvarchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Athlete_In_Challenge(AthleteID,ChallengeID,Status,Kilometers)
		values(@AthleteID,@ChallengeID,@Status,@Kilometers)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Athlete_In_Challenge
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Athlete_In_Challenge
		where AthleteID = @AthleteID and ChallengeID = @ChallengeID	
	end

	if @StatementType = 'AthleteChallenges'
	begin
		select * from dbo.Athlete_In_Challenge
		where AthleteID = @AthleteID 			
	end

	if @StatementType = 'ChallengeAthletes'
	begin
		select * from dbo.Athlete_In_Challenge
		where ChallengeID = @ChallengeID 			
	end

	if @StatementType = 'ChalAccepted'
	begin
		select distinct ID,Name,StartDate,EndDate,Privacy,Challenge.Kilometers,Type
		from(Athlete_In_Challenge inner join Challenge
		on Athlete_In_Challenge.ChallengeID = Challenge.ID)
		where AthleteID = @AthleteID and Status = 'En curso'			
	end

	if @StatementType = 'ChalNotInscribed'
	begin
		select distinct ID, Name, StartDate, EndDate, Privacy, Challenge.Kilometers, Type
		from(Athlete_In_Challenge right join Challenge
		on Athlete_In_Challenge.ChallengeID = Challenge.ID)
		where AthleteID is null or
		ChallengeID not in
		(select ChallengeID from Athlete_In_Challenge
		where AthleteID = @AthleteID)			
	end

	if @StatementType = 'Update'
	begin
		update dbo.Athlete_In_Competition set AthleteID=@AthleteID,CompetitionID=@CompetitionID,Status=@Status,Receipt=@Receipt,Duration=@Duration
		where AthleteID=@AthleteID and CompetitionID=@CompetitionID
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Athlete_In_Challenge
		where AthleteID = @AthleteID and ChallengeID = @ChallengeID
	end
end
go


create procedure proc_athlete_in_competition(@AthleteID varchar(50),
											@CompetitionID varchar(50),
											@Status varchar(50),
											@Receipt varchar(200),
											@Duration time,
										    @StatementType nvarchar(50) = '')
as begin

	if @StatementType = 'Select'
	begin
select * from dbo.Athlete_In_Competition
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Athlete_In_Competition
		where AthleteID = @AthleteID and CompetitionID = @CompetitionID
	end

	if @StatementType = 'Insert'
	begin
		insert into dbo.Athlete_In_Competition(AthleteID,CompetitionID,Status,Receipt,Duration)
	values(@AthleteID,@CompetitionID,@Status,@Receipt,@Duration)
	end

	if @StatementType = 'AthleteCompetitions'
	begin
		select * from dbo.Athlete_In_Competition
		where AthleteID = @AthleteID			
	end

	if @StatementType = 'CompetitionAthletes'
	begin
		select * from dbo.Athlete_In_Competition
		where CompetitionID = @CompetitionID 			
	end

	if @StatementType = 'AcceptedToComp'
	begin
		select * from dbo.Athlete_In_Competition
		where CompetitionID = @CompetitionID and Status = 'Aceptado'			
	end

	if @StatementType = 'CompReport'
	begin
		select * from compReport
		where CompetitionID = @CompetitionID
		order by Duration			
	end

	if @StatementType = 'NotSubscribed'
	begin
		select distinct ID, Name, Route, Date, Privacy, BankAccount, Price, ActivityID
		from (Athlete_In_Competition right join Competition
		on Athlete_In_Competition.CompetitionID = Competition.ID)
		where AthleteID is null or
		CompetitionID not in 
		(select CompetitionID from Athlete_In_Competition 
		where AthleteID = @AthleteID)	
	end

	if @StatementType = 'AthleteAcceptedComp'
	begin
		select ID, Name, Route, Date, Privacy, BankAccount, Privacy, BankAccount, Price, ActivityID
		from (Athlete_In_Competition right join Competition
		on Athlete_In_Competition.CompetitionID = Competition.ID)
		where AthleteID = @AthleteID and Status = 'Aceptado'	
	end

	if @StatementType = 'NotAccepted'
	begin
		select Username, Athlete.Name, LastName,Photo,Age,BirthDate,Nationality, Category
		from ((Athlete_In_Competition inner join Competition
		on Athlete_In_Competition.CompetitionID = Competition.ID) inner join Athlete
		on Athlete_In_Competition.AthleteID = Athlete.Username)
		where CompetitionID = @CompetitionID and Status = 'No Aceptado'	
	end

	if @StatementType = 'Update'
	begin
		update dbo.Athlete_In_Competition set AthleteID=@AthleteID,CompetitionID=@CompetitionID,Status=@Status,Receipt=@Receipt,Duration=@Duration
		where AthleteID=@AthleteID and CompetitionID=@CompetitionID
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Athlete_In_Competition
		where AthleteID = @AthleteID and CompetitionID = @CompetitionID
	end
end
go


create procedure proc_challenge(@Id varchar(50),
								@Name varchar(50),
								@StartDate date,
								@EndDate date,
								@Privacy varchar(50),
								@Kilometers decimal(5,2),
								@Type varchar(50),
								@ActivityID varchar(50),
								@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Challenge(Id,Name,StartDate,EndDate,Privacy,Kilometers,Type,ActivityID)
		values(@Id,@Name,@StartDate,@EndDate,@Privacy,@Kilometers,@Type,@ActivityID)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Challenge
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Challenge
		where Id = @Id
	end


	if @StatementType = 'Update'
	begin
		update dbo.Challenge set Name=@Name,StartDate=@StartDate,EndDate=@EndDate,Privacy=@Privacy,Kilometers=@Kilometers,Type=@Type,ActivityID=@ActivityID
		where Id=@Id 	
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Challenge
		where Id = @Id
	end
end
go


create procedure proc_competition(@Id varchar(50),
								@Name varchar(50),
								@Route varchar(MAX),
								@Date date,
								@Privacy varchar(50),
								@BankAccount varchar(50),
								@Price decimal(5,2),
								@ActivityID varchar(50),
								@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Competition(Id,Name,Route,Date,Privacy,BankAccount,Price,ActivityID)
		values(@Id,@Name,@Route,@Date,@Privacy,@BankAccount,@Price,@ActivityID)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Competition
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Competition
		where Id = @Id
	end


	if @StatementType = 'Update'
	begin
		update dbo.Competition set Name=@Name,Route=@Route,Date=@Date,Privacy=@Privacy,BankAccount=@BankAccount,Price=@Price,ActivityID=@ActivityID
		where Id=@Id	
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Competition
		where Id = @Id
	end
end
go


create procedure proc_competition_categories(@CompetitionID varchar(50),
											@Category varchar(50),
											@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Competition_Categories(CompetitionID,Category)
		values(@CompetitionID,@Category)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Competition_Categories
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Competition_Categories
		where CompetitionID = @CompetitionID and Category = @Category
	end


	if @StatementType = 'CompCategories'
	begin
		select * from dbo.Competition_Categories
		where CompetitionID = @CompetitionID	
	end

	if @StatementType = 'CatCompeetition'
	begin
		select * from dbo.Competition_Categories
		where Category = @Category	
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Competition_Categories
		where CompetitionID = @CompetitionID and Category = @Category
	end
end
go


create procedure proc_groups(@Name varchar(50),
							@AdminUsername varchar(50),
							@Oldname varchar(50),
							@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Groups(Name,AdminUsername)
		values(@Name,@AdminUsername)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Groups
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Groups
		where Name = @Name
	end


	if @StatementType = 'GroupAdmin'
	begin
		select * from dbo.Groups
		where AdminUsername = @AdminUsername	
	end

	if @StatementType = 'Update'
	begin
		update dbo.Groups set Name=@Name
		where Name=@Oldname
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Groups
		where Name = @Name
	end
end
go


create procedure proc_group_members(@GroupName varchar(50),
							@MemberID varchar(50),
							@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Group_Member(GroupName,MemberID)
		values(@GroupName,@MemberID)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Group_Member
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Group_Member
		where GroupName = @GroupName and MemberID = @MemberID
	end


	if @StatementType = 'GroupMembers'
	begin
		select * from dbo.Group_Member
		where GroupName = @GroupName	
	end

	if @StatementType = 'MemberGroups'
	begin
		select * from dbo.Group_Member
		where MemberID = @MemberID 
	end

	if @StatementType = 'NotSubscribedGroup'
	begin
		select distinct Name, AdminUsername
		from (Group_Member right join Groups
		on Groups.Name= Group_Member.GroupName)
		where MemberID is null or
		GroupName not in 
		(select GroupName from Group_Member
		where MemberID = MemberID) 
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Group_Member
		where GroupName = @GroupName and MemberID = @MemberID
	end
end
go

create procedure proc_sponsor(@Id varchar(50),
							@Name varchar(50),
							@BankAccount varchar(50),
							@CompetitionID varchar(50),
								@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Sponsor(Id,Name,BankAccount,CompetitionID)
		values(@Id,@Name,@BankAccount,@CompetitionID)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Sponsor
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Sponsor
		where Id = @Id
	end


	if @StatementType = 'Update'
	begin
		update dbo.Sponsor set Name=@Name,BankAccount=@BankAccount,CompetitionID=@CompetitionID
		where Id=@Id	
	end

	if @StatementType = 'Delete'
	begin
		delete from dbo.Sponsor
		where Id = @Id
	end
end
go


create procedure proc_activity_in_Chall(@ActivityID varchar(50),
								@ChallengeID varchar(50),
								@StatementType varchar(50) = '')
as begin

	if @StatementType = 'Insert'
	begin
		insert into dbo.Activity_In_Challenge(ActivityID,ChallengeID)
		values(@ActivityID,@ChallengeID)
	end

	if @StatementType = 'Select'
	begin
		select * from dbo.Activity_In_Challenge
	end

	if @StatementType = 'Select One'
	begin
		select * from dbo.Activity_In_Challenge
		where ActivityID = @ActivityID and ChallengeID = @ChallengeID
	end

	if @StatementType = 'ChallengesAct'
	begin
		select * from dbo.Activity_In_Challenge
		where ActivityID = @ActivityID 
	end

		if @StatementType = 'ActivityChall'
	begin
		select * from dbo.Activity_In_Challenge
		where ChallengeID = @ChallengeID 
	end


	if @StatementType = 'Delete'
	begin
		delete from dbo.Activity_In_Challenge
		where ActivityID = ActivityID and ChallengeID = @ChallengeID
	end
end
go
