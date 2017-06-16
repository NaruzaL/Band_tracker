# Band Tracker

#### _Week 4 solo project C#, 06.16.2017_

#### By _**Pete Lazuran**_

## Description

This app lets the user track bands and venues. The user is also able to link bands to venues they've played at and vice versa.


|Behavior|User Action/Selection|Description|
|---|:---:|:---:|
|Add a venue|Add venue: Roseland Theater|An add function. |
|Find a venue|Find venue: Roseland Theater|A find function. |
|Delete a venue|Delete venue: Roseland Theater|A delete function. |
|Update a venue's name|Find venue: Roseland Theater|An update function. |
|Add a band|Add band: Gun's and Roses|An add function. |
|Find a band|Find band: Gun's and Roses|A find function. |
|View all bands|bands: Gun's and Roses, Pink Floyd, The Beatles|View the full list of bands in the database. |
|View all venues|venues: Roseland Theater, Arlene Schnitzer Concert Hall, Rose Garden|View the full list of venues in the database. |
|Link a band to many venues|band: Gun's and Roses. venues: Rose Garden, Ladd's Inn, Roseland Theater|A many to many database relationship. |
|Link a venue to many bands|venue: Ladd's Inn. bands: Gun's and Roses, Everclear, Hello Citizen|A many to many database relationship|


## Setup/Installation Requirements
##### (Instructions written for a PC using PowerShell with Mono, ASP.Net 5, and Microsoft SQL server management studio installed)

* Open PowerShell.
* Navigate to the desired path for this file **(_desktop_)**
* Clone this repository (example: **PS C:\Users\pdlaz\Desktop>git clone https://github.com/NaruzaL/Band_tracker.git**).
* Navigate to the file directory in terminal **(..pdlaz\desktop>cd Week-4-Csharp)**.
* Open in a text editor if you wish to view the code.
* To view the site in your local server enter the command "dnx kestrel" in your Terminal.

* **To recreate the database used in this project by scratch in your powershell you must enter the following commands:**
* _sqlcmd -S "(localdb)\mssqllocaldb"_
* _GO_ (at this point your file path should change to "1>", denoting that you have accessed your database server)
*  _CREATE DATABASE_ band_tracker;
* _GO_
* _CREATE TABLE bands (name VARCHAR(255), genre VARCHAR(255) id INT IDENTITY(1,1))_
* _GO_
* _CREATE TABLE venues (name VARCHAR(255)_, id INT IDENTITY(1,1))
* _GO_
* _CREATE TABLE_ bands_venues _(id INT IDENTITY(1,1)_, band_id INT, venue_id INT)
* _GO_


## Known Bugs

None

## Support and contact details

Direct all questions and comments to pdlazuran@gmail.com

## Technologies Used

C#, HTML, Nancy, Razor, Xunit, PowerShell, MSSQL server management studio 2017.

### License

*MIT*

Copyright (c) 2017 **_Pete Lazuran_**
