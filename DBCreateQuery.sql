create database AddressBook
go 
use AddressBook 
create table Contact(
	contact_id int identity(1,1),
	full_name varchar(500) not null,
	email_address varchar(1000) not null,
	phone_number int not null, 
	physical_address varchar(2000) null,
	constraint PK_Contact primary key (contact_id)
);

-- input data
insert into Contact (full_name, email_address, phone_number, physical_address)
values
('Kristapor Khachaturian', 'KristaporKhachaturian@gmail.com', 058426871, 'address 1'), 
('Hasmik Hakobyan', 'HasmpikHakobyan@gmail.com', 058425171, 'address 2'),
('Poghos Sargsyan', 'PoghosSargsyan@gmail.com', 045778912, 'address 3')

-- create procedur for get
create procedure GetContact
as 
begin
	select * from Contact
end
go
--test
--exec GetContact

-- create procedure for get single object
create procedure GetContactSingle @ID int
as 
begin 
	select * 
	from Contact
	where contact_id = @ID
end
go
--test
--exec GetContactSingle @ID = 1

-- create procedure for insert
create procedure InsertContact 
	@name varchar(500), @email varchar(1000), @phone int,  @address varchar(2000), 
	@id int output
as 
begin
	SET NOCOUNT ON;
	insert into Contact (full_name, email_address, phone_number, physical_address)
	values
	(@name, @email, @phone, @address)
	set @id = scope_identity()
	return @id
end	
go
--test
--declare @id_ int;
--exec InsertContact @name = 'Poghos Poghosyan', @email = 'PoghosPoghosyan@gmail.com',
--	@phone = 025987415, @address = 'address 4',@id = @id_ output
--select @id_

-- create procedure for delete 
create proc DaleteContact @ID int
as
begin
	delete from Contact where contact_id = @ID
end
go 
--test
--exec DaleteContact @ID = 5

-- create procedure for update 
create proc UpdateContact 
	@ID int, @name varchar(500), @email varchar(1000), @phone int,  @address varchar(2000)
as 
begin
	update Contact
	set full_name = @name, email_address = @email,
		phone_number = @phone, physical_address = @address
	where contact_id = @ID
end
go
--test
--exec UpdateContact @name = 'Poghos Poghosyan', @email = 'PoghosPoghosyan@gmail.com',
--	@phone = 025987415, @address = 'address 4', @ID = 6



