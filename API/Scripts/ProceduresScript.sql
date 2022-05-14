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