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
		select FollowerID,ID,Name,Route,Date,Duration,Kilometers,Type from dbo.friends_Act
		where  AthleteID = @Username
	end

	if @StatementType = 'CompCreator'
	begin
		select * from dbo.CompsCreator
		where AthleteUsername = @Username
	end

	if @StatementType = 'ChallCreator'
	begin
		select * from dbo.ChallCreator
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