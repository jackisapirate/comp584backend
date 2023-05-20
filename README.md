# Comp584 Used car frontend project

# Introduction

This project is a front angular project for class comp584.

This is a website showing information of used cars. First of all, we need a car Make. Under this Make, different model belongs to different Make. And different cars can be added under respective model.

##### Such as

Make: Toyota 

Model: Camry, Sales 2334

Car: Toyota Camry Solara SLE, 2007, Black, Good condition

# Operation

There are two users (Admin and Normal User) in our program. **admin** and **user**

2 Roles 2 Users

Admin: users can **view, modify, add, delete** data.

Ordinary: users only can **view** data, but cannot modify, add, or delete data.

```
Admin username: admin
password: *

Ordinary username: user
password: *
```

# Technology

##### Frontend : Angular.js

##### Backend : C#

##### Database: SQL Server

**Server: Azure** (The Frontend, Backend,  and SQL Server are deployed on same Azure Server)

# Progress

- At present, we have been able to use the "api/Account" interface to obtain tokens
- Three tables "Make", "Model" and "Car" are created in my database.
- I can use swagger to call and test these interfaces.
- The Angular (front-end) project can display data information that is stored in the database.
- We added Authentication Middleware. (2023/04/07)



# What I have

- Single Page Application (SPA)
- Model-View-Controller (MVC)
- Object-Relational Mapping (ORM) *
- Authentication (CombinedAuth project helps me to create the User table and Role table)
- Deployment (Azure)
- Authorization (Admin can add, edit, delete data. User can not edit data!)
- Data Entry (Admin can add, edit, delete data.)
- Unit Testing 

# Link

Frontend: <https://github.com/jackisapirate/comp584front> 

Backend: https://github.com/jackisapirate/comp584backend

