create trigger verifyAthletePost
on dbo.Athlete for insert
as
declare @Username varchar(50),
								@Name varchar(50),
								@LastName varchar(50),
								@Photo varchar(50),
								@Age int,
								@BirthDate date,
								@Pass varchar(50),
								@Nationality varchar(50),
								@Category varchar(50)
if not exists(select * from dbo.Athlete where Username = @Username)
	begin
		insert into dbo.Athlete(Username,Name,LastName,Photo,Age,BirthDate,Pass,Nationality,Category)
	values(@Username,@Name,@LastName,@Photo,@Age,@BirthDate,@Pass,@Nationality,@Category)
	end
else
	begin
		set @err_message = @Username + 'exists raise sev 10'
		RAISERROR(@err_message,10,1)
	end

