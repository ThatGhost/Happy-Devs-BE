create table users (
	id int IDENTITY(1,1) PRIMARY KEY,
	username varchar(255),
	title varchar(255),
	email varchar(255),
	password varchar(255)
);

alter table users
alter column 
email varchar(255) NOT NULL;

alter table users
alter column 
password varchar(255) NOT NULL;

alter table users
alter column 
username varchar(255) NOT NULL;

alter table users 
ADD UNIQUE (email);

alter table users
add bio varchar(1024) null;

alter table users
add profilepicture VARBINARY(MAX) null;

create table activity (
	userId int NOT NULL,
	type int NOT NULL,
	at DATETIME NOT NULL,
	foreign key (userId) references users(id)
);

create table posts (
	id int IDENTITY(1,1) PRIMARY KEY,
	userId int NOT NULL,
	content varchar(2000) NOT NULL DEFAULT '',
	title varchar(256) NOT NULL,
	at DATETIME NOT NULL
	foreign key (userId) references users(id)
);

create table post_comments (
	id int IDENTITY(1,1) PRIMARY KEY,
	postId int NOT NULL,
	userId int NOT NULL,
	content varchar(2000) NOT NULL DEFAULT '',
	at DATETIME NOT NULL
	foreign key (userId) references users(id),
	foreign key (postId) references posts(id)
);