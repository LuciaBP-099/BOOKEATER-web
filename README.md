# BookEater Web Application

Information System

Authors: Lucía Brígido Pérez, 
         Mar Selfa Cuñat.

## Project Description
BookEater is a web application built with ASP.NET Core MVC that allows users to manage books, write reviews, organize their personal digital library and join a reading club, where they can chat about books. 
## Key Features
- User and Authentication System: User login/logout and registration functionality to acces their custom library.
- Book Management: Users can add, edit or delete books. View detailed book information and associate books with users' librares.
- Reviews and rating: Write/edit or delete personalized reviews and view reviews from other users.
- MyLibrary: Add books to your personal library and organize books into lists.
- Reading club: Users can chat with other users about books.
- Administration panel
- REST API for Android app consumption
- Data persistence using CSV files
## Technology used
The system is built using:
- ASP.NET Core MVC framework
- SQL Server handles database opeartions
- Microsoft Azure is used to deploy the application and database
- HTML/CSS for basic styling and User Interface design
- CSV as a data storage system
- JSON for the REST API

## Screenshots
Screenshots showing the interface of the web application: 
1. **Home page**
<img width="1437" height="746" alt="Home page" src="https://github.com/user-attachments/assets/5a7a5c51-66ca-4827-9aa8-a6b3eb3013e1" />
The landing page when the user is not logged in.

<img width="1437" height="746" alt="Home page when log in" src="https://github.com/user-attachments/assets/85f0de85-19d3-40f3-bd21-0ef692bb9eec" />
The landing page when the user (admin in this case) is logged in.

2. **Login Screen**
<img width="1437" height="746" alt="Home page when log in" src="https://github.com/user-attachments/assets/33ab413b-96c5-4833-9656-b781505c2340" />

3. **Register Screen** 
<img width="1437" height="746" alt="Home page when log in" src="https://github.com/user-attachments/assets/59fb5d79-74ba-4c70-9bfa-19dd8dec8193" />


4. **My Library**
   
<img width="1437" height="746" alt="My Library emplty" src="https://github.com/user-attachments/assets/8d3177ae-23c7-4863-9343-0d418d9c4083" />
My Library when it is emplty.

<img width="1437" height="746" alt="Home page when log in" src="https://github.com/user-attachments/assets/838db022-6464-412e-a639-b89c383fc39b" />
My Library when there are books saved and reading lists made.

5. **My Reviews**
<img width="1437" height="746" alt="My Library emplty" src="https://github.com/user-attachments/assets/ce9cb5e6-82fb-493f-9f54-0cbe1257b190" />

6. **The Community**
<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/55b3f00a-9387-4b16-8fb5-b9f61eff37e2" />

<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/f73193ec-c735-441b-b967-5f228dc8db59" />
The Hunger Games' community reviews.

<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/14f46429-907d-45c0-89ea-0f945b29051e" />
The Twilight' community.

7. **Reading Club**
<img width="1437" height="779" alt="Reading Club" src="https://github.com/user-attachments/assets/1e5eb75d-92ed-48f8-8321-da1ed31477e5" />

8. **Admin Panel**
<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/3d434e21-d357-4b29-a0ec-8b9b3b93f00c" />


## Webpage
The web application is hosted on Microsoft Azure, providing public access to the system via the url: https://bookeater-g2f5bubzgjapg2he.germanywestcentral-01.azurewebsites.net/

## Database Schema
The project uses a SQL Server database hosted on Microsoft Azure. The schema includes the following tables:
Users: Stores information about the users of the application.
Books: Contains details about the books.
UserBooks: Stores the relations books to users.
Reviews: Stores the reviews of a book.
The database model can be seen in the diagram below, I created it using SQL Server Management Studio (SSMS):

<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/75892a2f-357a-45d0-87bd-2f3ae7efad75" />
