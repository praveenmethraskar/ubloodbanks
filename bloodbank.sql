create database Bloodbank;

create table users(users_id int identity(1,1) primary key, fullname varchar(50),mail varchar(100),
password varchar(20), confirmpassword varchar(50));

select * from users
Go
CREATE OR Alter PROCEDURE [dbo].[SP_Register]
(
@fullname varchar(50),
@mail varchar(100),@password varchar(20),
@confirmpassword varchar(50)
)
as 
Begin 
insert into users(fullname,mail,password,confirmpassword)
values			 (@fullname,@mail,@password,@confirmpassword)
End

Go
create or alter procedure [dbo].[EmployeeLogin]  
(  
@mail varchar(100)=NULL,  
@password varchar(20)=NULL
)
as  
begin  
select mail  from users where mail=@mail and password=@password;
end  


create table profile(p_id int identity(1,1) primary key,users_id int foreign key references users(users_id),
					dob varchar(100),Bloodgroup varchar(10),mobilenumber varchar(10),
					address1 varchar(50), district varchar(50),states varchar(50), pincode bigint,imagepath varchar(100));
select * from profile
drop table profile

Go
CREATE OR Alter PROCEDURE [dbo].[SP_Profile]
(
@users_id int,
@dob varchar(100),
@Bloodgroup varchar(10),@mobilenumber varchar(10),@address1 varchar(50),
@district varchar(50),@states varchar(50), @pincode bigint,@imagepath varchar(100) = null
)
as 
Begin 
insert into profile(users_id,dob,Bloodgroup,mobilenumber,address1,district,states,pincode,imagepath)
values			 (@users_id,@dob,@Bloodgroup ,@mobilenumber,@address1,@district,@states, @pincode,@imagepath)
End


insert into profile(users_id,dob,Bloodgroup,mobilenumber,address1,district,states,pincode,imagepath)
values			 (1,'24-08-1995','O+' ,8897720286,'madannapet','hyderabad','telangana', 500059,'')
select * from profile;
 


Go
create or alter procedure Getusers(@users_id int)
as  
begin  

select users.users_id,users.fullname,users.mail  from users
 where users_id = @users_id;
end 

select * from profile
inner join users on profile.p_id = users.users_id

select users.fullname,users.mail,profile.dob,profile.Bloodgroup,profile.mobilenumber,profile.district,profile.states,profile.pincode  from profile
inner join users on  users.users_id = profile.p_id

Go
create or alter procedure Getprofile(@users_id int)
as  
begin  

select users.fullname,users.mail,profile.dob,profile.Bloodgroup,profile.mobilenumber,profile.address1,profile.district,profile.states,profile.pincode  from profile
inner join users on  users.users_id = profile.p_id
where users.users_id = @users_id;
end 


create table adminuser(admin_id int identity(1,1) primary key, fullname varchar(50),mail varchar(100),
password varchar(20), confirmpassword varchar(50));

select * from adminuser
Go
CREATE OR Alter PROCEDURE [dbo].[SP_Admin1]
(
@fullname varchar(50),
@mail varchar(100),@password varchar(20),
@confirmpassword varchar(50)
)
as 
Begin 
insert into adminuser(fullname,mail,password,confirmpassword)
values			 (@fullname,@mail,@password,@confirmpassword)
End

Go
create or alter procedure [dbo].[AdminLogin]  
(  
@mail varchar(100)=NULL,  
@password varchar(20)=NULL
)
as  
begin  
select mail  from adminuser where mail=@mail and password=@password;
end  


Go
create or alter procedure GetDetails
as  
begin  
select users.fullname,users.mail,profile.dob,profile.Bloodgroup,profile.mobilenumber,profile.address1,profile.district,profile.states,profile.pincode  from profile
inner join users on  users.users_id = profile.p_id
end 


Go
create or alter procedure count(@Bloodgroup varchar)
as  
begin  
select count(profile.Bloodgroup) from profile where Bloodgroup = @Bloodgroup
end 

select count(profile.Bloodgroup) from profile where Bloodgroup = 'O+'

create table Hospitals(Hid int identity(1,1) primary key, HospitalName varchar(MAX));

select * from Hospitals


Go
CREATE OR Alter PROCEDURE [dbo].[SP_Hospitals]
(
@HospitalName varchar(MAX)
)
as 
Begin 
insert into Hospitals(HospitalName) values (@HospitalName)
End

Go
CREATE OR Alter PROCEDURE [dbo].[SP_ListHospitals]
as 
Begin 
select * from Hospitals
End


create table Donations(d_id int identity(1,1) primary key,users_id int foreign key references users(users_id),
					L_date varchar(100),H_id int foreign key references Hospitals(Hid));

Go
CREATE OR Alter PROCEDURE [dbo].[SP_enterdonations]
(
@users_id int,
@L_date varchar(100),
@H_id varchar(10)
)
as 
Begin 
insert into Donations(users_id,L_date,H_id)
values(@users_id,@L_date,@H_id )
End

select * from Donations

select users.fullname,Donations.L_date,Hospital.HospitalName  from Donations
inner join users on  users.users_id = Donations.d_id



select users.fullname,Hospitals.HospitalName,Donations.L_date from Donations
inner join users on  users.users_id = Donations.users_id
inner join Hospitals on Hospitals.Hid  = Donations.H_id where users.users_id = '3'


Go
create or alter procedure ListDonated(@users_id int)
as  
begin  
select users.fullname,Hospitals.HospitalName,Donations.L_date from Donations
inner join users on  users.users_id = Donations.users_id
inner join Hospitals on Hospitals.Hid  = Donations.H_id where users.users_id = @users_id;

end 