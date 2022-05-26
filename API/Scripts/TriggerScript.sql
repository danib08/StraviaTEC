create trigger ageAthlete
on dbo.Athlete
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN
update dbo.Athlete
set Age = year(getdate())-year(BirthDate)
end


ALTER trigger categoryAthlete
on dbo.Athlete
AFTER INSERT
NOT FOR REPLICATION
AS
BEGIN

IF (Age between 0 and 14 ) begin
	update dbo.Athlete
	set Category = 'Junior'
end

IF (Age between 15 and 23 ) begin 
	update dbo.Athlete
	set Category = 'Sub-23'
end

IF (Age between 24 and 30) begin 
	update dbo.Athlete
	set Category = 'Open'
	end
IF (Age between 31 and 40) begin 
	update dbo.Athlete
	set Category = 'Master A'
	end
IF (Age between 41 and 50) begin 
	update dbo.Athlete
	set Category = 'Master B'
	end
IF (51 <= Age) begin  
	update dbo.Athlete
	set Category = 'Master C'	
	end
end
