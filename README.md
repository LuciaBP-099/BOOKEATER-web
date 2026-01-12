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
<img width="1437" height="779" alt="log in" src="https://github.com/user-attachments/assets/ffc836df-1306-47ad-a27f-79977bb3ac16" />

3. **Register Screen** 
<img width="1437" height="779" alt="Register screen" src="https://github.com/user-attachments/assets/70ade60f-2c60-4660-b29c-b3d42de8d63a" />


4. **My Library**
   
<img width="1437" height="746" alt="My Library emplty" src="https://github.com/user-attachments/assets/8d3177ae-23c7-4863-9343-0d418d9c4083" />
My Library when it is emplty.

<img width="1437" height="746" alt="My Library" src="https://github.com/user-attachments/assets/ac6a7bc5-890d-4c1e-a49b-4774f88e1e6b" />
My Library when there are books saved and reading lists made.

5. **My Reviews**
<img width="1437" height="746" alt="My Reviews" src="https://github.com/user-attachments/assets/6f956d3d-d8f4-489a-a905-03b28a4c79a6" />

6. **The Community**
<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/55b3f00a-9387-4b16-8fb5-b9f61eff37e2" />

<img width="1437" height="746" alt="The Hunger Community" src="https://github.com/user-attachments/assets/109bc4d3-bb77-404e-9561-b752049089b1" />
The Hunger Games' community reviews.

<img width="1437" height="779" alt="Twilight Community" src="https://github.com/user-attachments/assets/95dfc0cb-06e3-4b32-9f6c-d4476a451103" />
The Twilight' community.

7. **Reading Club**
<img width="1437" height="779" alt="Reading Club" src="https://github.com/user-attachments/assets/1e5eb75d-92ed-48f8-8321-da1ed31477e5" />

8. **Admin Panel**
<img width="1437" height="779" alt="Admin Panel" src="https://github.com/user-attachments/assets/0115980b-a439-4e8c-af47-4114c7aeabff" />


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
