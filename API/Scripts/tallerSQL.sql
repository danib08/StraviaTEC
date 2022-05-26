CREATE DATABASE TallerSQL

-----------------------------------------------Tables creation ------------------------------------

CREATE TABLE EMPLOYEE(
Fname varchar(50) NOT NULL,
Minit varchar(1) NOT NULL,
Lname varchar(50) NOT NULL,
Ssn int NOT NULL,
Bdate date NOT NULL,
Address varchar(50),
Sex varchar(1) NOT NULL,
Salary int,
Super_ssn int,
Dno int
)

CREATE TABLE DEPARTMENT(
Dname varchar(50) NOT NULL,
Dnumber int NOT NULL,
Mgr_ssn int,
Mgr_start_date date
)

CREATE TABLE DEPT_LOCATIONS(
Dnumber int NOT NULL,
Dlocation varchar(50) NOT NULL
)

CREATE TABLE WORKS_ON(
Essn int NOT NULL,
Pno int NOT NULL,
Hours decimal(3,1)
)

CREATE TABLE PROJECT(
Pname varchar(50) NOT NULL,
Pnumber int NOT NULL,
Plocation varchar(50),
Dnum int
)

CREATE TABLE DEPENDENT(
Essn int NOT NULL,
Dependent_name varchar(50) NOT NULL,
Sex varchar(1),
BDate date,
Relationship varchar(50)
)

------------------------------------------Primary Keys---------------------------------------------

ALTER TABLE EMPLOYEE
ADD PRIMARY KEY(Ssn)

ALTER TABLE DEPARTMENT
ADD PRIMARY KEY(Dnumber)

ALTER TABLE DEPT_LOCATIONS
ADD CONSTRAINT PK_LOCATION PRIMARY KEY(Dnumber,Dlocation)

ALTER TABLE WORKS_ON
ADD CONSTRAINT PK_WORKS_ON PRIMARY KEY(Essn,Pno)

ALTER TABLE PROJECT
ADD PRIMARY KEY(Pnumber)

ALTER TABLE DEPENDENT
ADD CONSTRAINT PK_DEPENDENT PRIMARY KEY(Essn,Dependent_name)

------------------------------------------Foreign Keys---------------------------------------------

ALTER TABLE EMPLOYEE 
ADD CONSTRAINT Dno_FK FOREIGN KEY(Dno)
REFERENCES DEPARTMENT(Dnumber)

ALTER TABLE DEPT_LOCATIONS 
ADD CONSTRAINT Dnumber_FK FOREIGN KEY(Dnumber)
REFERENCES DEPARTMENT(Dnumber)

ALTER TABLE PROJECT 
ADD CONSTRAINT D_P_number_FK FOREIGN KEY(Dnum)
REFERENCES DEPARTMENT(Dnumber)

ALTER TABLE WORKS_ON
ADD CONSTRAINT EmpSsn_FK FOREIGN KEY(Essn)
REFERENCES EMPLOYEE(Ssn)

ALTER TABLE DEPENDENT
ADD CONSTRAINT Dep_EmpSsn_FK FOREIGN KEY(Essn)
REFERENCES EMPLOYEE(Ssn)


--------------------------------------------INSERTS-------------------------------------------

INSERT INTO DEPARTMENT
(Dname, Dnumber, Mgr_ssn, Mgr_start_date)
VALUES
('Research', 5, 333445555, '1988-05-22'),
('Administration', 4, 987654321, '1995-01-01'),
('Headquarters', 1, 888665555, '1981-06-19');

INSERT INTO EMPLOYEE
(Fname, Minit, Lname, Ssn,Bdate,Address,Sex,Salary,Super_ssn,Dno)
VALUES
('John', 'B', 'Smith', 123456789, '1965-01-09','731 Fondren, Houston, TX','M',30000,333445555,5),
('Franklin', 'T', 'Wong', 333445555, '1955-12-08','638 Voss, Houston, TX','M',40000,888665555,5),
('Alicia', 'J', 'Zelaya', 999887777, '1968-01-19','3321 Castle, Spring, TX','F',25000,987654321,4),
('Jennifer', 'S', 'Wallace', 987654321, '1941-06-20','291 Berry, Bellaire, TX','F',43000,888665555,4),
('Ramesh', 'K', 'Narayan', 666884444, '1962-09-15','975 Fire Oak, Humble, TX','M',38000,333445555,5),
('Joyce', 'A', 'English', 453453453, '1972-07-31','5631 Rise, Houston, TX','F',25000,333445555,5),
('Ahmad', 'V', 'Jabbar', 987987987, '1969-03-29','980 Dallas, Houston, TX','M',25000,987654321,4),
('James', 'E', 'Borg', 888665555, '1937-11-10','450 Stone, Houston, TX','M',55000,Null,1);

INSERT INTO DEPT_LOCATIONS
(Dnumber, Dlocation)
VALUES
(1, 'Houston' ),
(4, 'Stafford' ),
(5, 'Bellaire' ),
(5, 'Sugarland' ),
(5, 'Houston' );

INSERT INTO PROJECT
(Pname, Pnumber, Plocation, Dnum)
VALUES
('ProductX', 1, 'Bellaire', 5),
('ProductY', 2, 'Sugarland', 5),
('ProductZ', 3, 'Houston', 5),
('Computerization', 10, 'Stafford', 4),
('Reorganization', 20, 'Houston', 1),
('Newbenefits', 30, 'Stafford', 4);

INSERT INTO WORKS_ON
(Essn, Pno, Hours)
VALUES
(123456789, 1, 32.5),
(123456789, 2, 7.5),
(666884444, 3, 40.0),
(453453453, 1, 20.0),
(453453453, 2, 20.0),
(333445555, 2, 10.0),
(333445555, 3, 10.0),
(333445555, 10, 10.0),
(333445555, 20, 10.0),
(999887777, 30, 30.0),
(999887777, 10, 10.0),
(987987987, 10, 35.0),
(987987987, 30, 5.0),
(987654321, 30, 20.0),
(987654321, 20, 15.0),
(888665555, 20, Null);


INSERT INTO DEPENDENT
(Essn, Dependent_name, Sex, BDate, Relationship)
VALUES
(333445555, 'Alice', 'F', '1986-04-05','Daughter'),
(333445555, 'Theodore', 'M', '1983-10-25','Son'),
(333445555, 'Joy', 'F', '1958-05-03','Spouse'),
(987654321, 'Abner', 'M', '1942-02-28','Spouse'),
(123456789, 'Michael', 'M', '1988-01-04','Son'),
(123456789, 'Alice', 'F', '1988-12-30','Daughter'),
(123456789, 'Elizabeth', 'F', '1967-05-05','Spouse');

-------------------------------------------Ejercicio 3 ---------------------------------------

CREATE TABLE TASK(
ID varchar(10) NOT NULL,
Name varchar(50) NOT NULL,
Responsible varchar(50) NOT NULL,
Duration int NOT NULL
)

ALTER TABLE TASK
ADD PRIMARY KEY (ID)

------------------------------------------Ejercicio 4 ------------------------------------------

CREATE TABLE PROJECT_TASK(
Pnum int NOT NULL,
Tname varchar(30) NOT NULL,
Rname varchar(50) NOT NULL,
Tduration int NOT NULL
)

ALTER TABLE PROJECT_TASK
ADD CONSTRAINT PT__FK FOREIGN KEY(Pnum)
REFERENCES PROJECT(Pnumber)

ALTER TABLE PROJECT_TASK
ADD CONSTRAINT PT__PK PRIMARY KEY(Pnum,Tname,Rname)

-------------------------------------------------Ejercicio 5 -------------------------------------

INSERT INTO TASK
(ID, Name, Responsible, Duration)
VALUES
('Task1', 'Design', '123456789', 1),
('Task2', 'Construction', '123456789', 4),
('Task3', 'Test', '999887777', 1),
('Task4', 'Implementation', '888665555', 1),
('Task5', 'UAT', '453453453', 1);

INSERT INTO PROJECT_TASK
(Pnum, TName, Rname, Tduration)
VALUES
(10, 'Design', '123456789', 1),
(10, 'Construction', '123456789', 4),
(10 'Test', '999887777', 1),
(10, 'Implementation', '888665555', 1),
(10, 'UAT', '453453453', 1);

-------------------------------------------------Ejercicio 6 -------------------------------------

select Fname,Minit,Lname from EMPLOYEE where Salary > 25000

-------------------------------------------------Ejercicio 7 -------------------------------------

select TName from PROJECT_TASK where Pnum = 10 and Tduration = 1


-------------------------------------------------Ejercicio 8--------------------------------------

SELECT EMPLOYEE.Lname,EMPLOYEE.Fname, PROJECT.Pname, WORKS_ON.Hours
FROM(( WORKS_ON
INNER JOIN PROJECT ON WORKS_ON.Pno = PROJECT.Pnumber)
INNER JOIN EMPLOYEE ON WORKS_ON.Essn = EMPLOYEE.Ssn)


-------------------------------------------------Ejercicio 9--------------------------------------

SELECT EMPLOYEE.Lname,EMPLOYEE.Fname, COUNT(*)
FROM EMPLOYEE, WORKS_ON 
WHERE EMPLOYEE.Ssn = WORKS_ON.Essn
GROUP BY EMPLOYEE.Lname,EMPLOYEE.Fname
HAVING COUNT(*) > 2



---------------------------------------Ejercicio 11--------------------------------------

create procedure get_dep_employees(@Dname varchar(50))
as
begin
select * FROM EMPLOYEE WHERE EMPLOYEE.Dno =
(select Dnumber FROM DEPARTMENT WHERE @Dname = DEPARTMENT.Dname) 
end
go

-----------------------------------------------Ejercicio 12-FALTA-----------------------
create procedure get_emp_pro_cant(@Pname varchar(50),
									@Salary int)
as
begin
 select SUM(WORKS_ON.Hours) as hours,COUNT(*) as emp FROM WORKS_ON WHERE WORKS_ON.Pno =
(select Pnumber FROM PROJECT WHERE @Pname = PROJECT.Pname) 
end
go


-----------------------------------------------Ejercicio 13------------------------

create trigger verifyAthletePost
on Athlete for update
as
declare @Fname varchar(50),
		@Minit varchar(1) ,
		@Lname varchar(50),
		@Ssn int NOT NULL,
		@Bdate date NOT NULL,
		@Address varchar(50),
		@Sex varchar(1) NOT NULL,
		@Salary int,
		@Super_ssn int,
		@Dno int
