# Hair Salon

#### _03/01/2019_

## Author
 **Theary Im**  
 thearyim@gmail.com

## Description

**_This application allows the user to input stylists and clients to the database. For each stylist, the user can add client who see that sylist. The user can also delete stylists and clients from the database._**

## Specs

1. User can add stylists and stylist's specialties to the database.

    _Example:_

    | Input               | Output              |
    | :--------------------------: | :----------------------------: |
    | Add a new stylist, Add stylist specialties | Add a Stylist Below |
    | Stylist Name: Sophie, Specialties: Cut, Color | Current Sylists: Sophie, Specialties: Cut, Color |
  
2. User can add clients to each stylist.

    _Example:_

    | Input                 | Output                  |
    | :--------------------------: | :----------------------------: |
    | Add a new client             | Add a Client to Sophie         |
    | Client Name: Sonia, Gender .Female .Male .Member .NonMember | Stylist: Sophie, Client list for this stylist: Sonia, Gender: Female, Membership: Member |

3. User can delete stylists and clients (all and single).

    _Example:_

    | Input                | Output             |
    | :------------------: | :----------------: |
    | Stylist: Sophie, Delete Stylist | Stylist Removed | 
    | Client: Sonia, Delete Client | Client Removed |
    | Stylist: Sophie, Jacob, Delete All | All Stylists Removed | 
    | Client: Sonia, Karen, Delete All | All Clients Removed |

4. User can edit stylists name.

    _Example:_

    | Input                 | Output                  |
    | :------------------------: | :----------------------------: |
    | Stylist: Sophie, Edit Stylist | Stylist Name Edited | 
    | Stylist: Bryan, Update | Stylist: Bryan |

5. 4. User can edit clients information.

    _Example:_

    | Input                 | Output                  |
    | :------------------------: | :----------------------------: |
    | Client: Sonia, Edit Client Information | Client Information Edited | 
    | Client: Sonia, Update | Client: Sonia, Gender: Female, Membership: NonMember |

1. User can add stylists to a specialty.

    _Example:_

    | Input               | Output              |
    | :--------------------------: | :----------------------------: |
    | Add a new stylist to this specialty | Add a Stylist Below |
    | Specialties: Cut, Stylist Name: Sophie | Stylists for this specialties: Sophie|
 


## Setup/Installation Requirements
**.NET Core is Required for this project to function.**

Download .NET Core 2.1.3 SDK and .NET Core Runtime 2.0.9 and install them. Download and install .NET Core 1.1:  
https://dotnet.microsoft.com/download/dotnet-core/1.1

Download and install Mono:  
https://www.mono-project.com/

1. Clone this repository:
    "$git clone https://github.com/Thearyim/HairSalon.Solution"

2. Setup the Database. Import the Database using the SQL files and command shown below:

* > CREATE DATABASE hair_salon;
* > USE hair_salon;
* > CREATE TABLE stylist (id serial PRIMARY KEY, name VARCHAR(255));
* > CREATE TABLE client (id serial PRIMARY KEY, name VARCHAR(255)), stylist_id INT;

* > CREATE DATABASE hair_salon_test;
* > USE hair_salon_test;
* > CREATE TABLE stylist (id serial PRIMARY KEY, name VARCHAR(255));
* > CREATE TABLE client (id serial PRIMARY KEY, name VARCHAR(255)), stylist_id INT;

3. Change into the work directory: $ cd HairSalon.Solution

4. To edit the project, open the project in your preferred text editor.

5.   To run the tests, use these commands:
     * $ cd HairSalon.Solution/HairSalon.Tests
     * $ dotnet test

6.  To run the program, first navigate to the location of the HairSalon.cs file then compile and execute:
    * $ cd HairSalon.Solution/HairSalon
    * $ dotnet build
    * $ dotnet run

7. Navigate to http://localhost:5000 in your browser to view the splashpage.


## Known Bugs

_No Known Bugs._

## Technologies Used
* _C#_
* _HTML_
* _CSS_
* _.NET_
* _Git_
* _MAMP_
* _MSTest_


### License

*MIT License*

Copyright (c) 2019 **_Theary Im_**
