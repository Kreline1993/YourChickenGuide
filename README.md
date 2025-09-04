# YourChickenGuide
A personal project built to learn and apply ASP.NET Core MVC, Entity Framework Core, and MySQL.
This app helps me keep track of my chickens  — their legband IDs, hatch dates, colors, breeds, 
notes, and pedigree (mother/father relationships).

# What It Does
- Add and edit chickens with details like:
	- Legband ID
	- Hatch date
	- Sex (Male/Female/Unknown)
	- Color	
	- Breed (dropdown list)
	- Notes
	- Status (Active/Inactive, Breeder, Breakfast, Freezer, etc.)
- Link chickens to their mother and father using self-referencing foreign keys.
- Search parents by legband ID with autocomplete.
- See an overview list of all active chickens.
- Mark chickens inactive instead of deleting, to preserve history.

# Why I Built This
- To practice building a real CRUD app with ASP.NET MCV and EF Core.
- To practice building and implementing a database.
- To manage my chicken flock in a structured way.

# Tech Stack
- .NET 8 - backend framework
- ASP.NET Core MVC - web framework
- Entity Framework Core - ORM for database access
- MySQL - relational database

# Notes
- This is a learning project – I use it to experiment with EF Core, database design, and ASP.NET MVC.
- The database schema includes self-referencing foreign keys for mother/father tracking.
- The code is intentionally simple, focusing on clarity and CRUD basics.