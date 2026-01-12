[README.md](https://github.com/user-attachments/files/24561944/README.md)
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
<img width="1437" height="746" alt="Home page" src="https://github.com/user-attachments/assets/b513a841-857d-4317-bb42-797ca2a1dbc3" />
The landing page when the user is not logged in.

<img width="1437" height="746" alt="Home page when log in" src="https://github.com/user-attachments/assets/fbb88768-c963-4b92-a7df-0e29f6ecd921" />
The landing page when the user (admin in this case) is logged in.

2. **Login Screen**
<img width="1437" height="779" alt="log in" src="https://github.com/user-attachments/assets/74d4a9a1-8f97-4b74-91e5-21438049922c" />

3. **Register Screen** 
<img width="1437" height="779" alt="Register screen" src="https://github.com/user-attachments/assets/c9391ade-17a2-47b5-a1bf-7edda048c145" />

4. **My Library**
<img width="1437" height="746" alt="My Library emplty" src="https://github.com/user-attachments/assets/a83a5c2a-8d9a-481a-a14f-eeda2920f0f6" />
My Library when it is emplty.

<img width="1437" height="746" alt="My Library" src="https://github.com/user-attachments/assets/ef7d406f-8fdd-4828-b9a5-9e375576e2eb" />
My Library when there are books saved and reading lists made.

5. **My Reviews**
<img width="1437" height="746" alt="My Reviews" src="https://github.com/user-attachments/assets/c7d58a54-8b79-477d-8df5-d7666fc30912" />

6. **My Community**
<img width="1437" height="746" alt="My Community" src="https://github.com/user-attachments/assets/f34ffe67-90e1-40d1-8876-6846420a5556" />

<img width="1437" height="746" alt="The Hunger Community" src="https://github.com/user-attachments/assets/6fd5af1c-a6ad-4c1f-864a-17f1a4f9d9be" />
The Hunger Games' community reviews.

<img width="1437" height="779" alt="Twilight Community" src="https://github.com/user-attachments/assets/74530b50-da3a-48b9-b4f9-79f5f1942f40" />
The Twilight' community.

7. **Reading Club**
<img width="1437" height="779" alt="Reading Club" src="https://github.com/user-attachments/assets/1e5eb75d-92ed-48f8-8321-da1ed31477e5" />

8. **Admin Panel**
<img width="1437" height="779" alt="Admin Panel" src="https://github.com/user-attachments/assets/782118cb-f5ef-48de-abd9-46a472996002" />

## Webpage
The web application is hosted on Microsoft Azure, providing public access to the system via the url: https://bookeater-g2f5bubzgjapg2he.germanywestcentral-01.azurewebsites.net/

## Database Schema
The project uses a SQL Server database hosted on Microsoft Azure. The schema includes the following tables:
Users: Stores information about the users of the application.
Books: Contains details about the books.
UserBooks: Stores the relations books to users.
Reviews: Stores the reviews of a book.
The database model can be seen in the diagram below, I created it using SQL Server Management Studio (SSMS):

![database](https://github.com/user-attachments/assets/682da554-ba8d-40cb-8b6f-90cf6f41cb6b)
