<H1>Bookstore App</H1>
This is a WPF application built with MVVM and Entity Framework Core for my Database course at IT-högskolan.
The application is used to administer a bookstore’s inventory, books and authors.

<br>

<h1>About</h1>
The application allows a user to:
<br>
- View stock levels per store<br>
- Update inventory quantities asynchronously<br>
- Manage books (create, edit, delete)<br>
- Assign multiple authors to books<br>
- Manage authors and prevent deletion if books exist

<h1>Installation & Setup</h1>

1) Clone repository and open the solution in Visual Studio.
2) In Solution Explorer, right-click the project BookstoreApp.Infrastructure and select Manage User Secrets.
3) Add the following to secrets.json:
 {
  "ConnectionStrings": {
    "BookstoreDb": "Server=(localdb)\\MSSQLLocalDB;Database=Bookstore_EF;Trusted_Connection=True;"
  }
}
4) Build and run the application. The database will be created and seeded automatically on first startup.


<h2>View the database</h2>

The database is created in SQL Server LocalDB and can be viewed using:

SQL Server Management Studio<br>
SQL Server Object Explorer in Visual Studio

Connect to: (localdb)\MSSQLLocalDB<br>
Database name: Bookstore_EF


<br>
<h2>Inventory view</h2>
<img width="1067" height="546" alt="bokhandel_lagersaldo" src="https://github.com/user-attachments/assets/34a53149-2d7d-4772-871a-6707d0ef4b19" />
<br>
<h2>Edit book</h2>
<img width="377" height="485" alt="bokhandel_redigerabok2" src="https://github.com/user-attachments/assets/54f51643-fad7-4271-9d2e-b18bffcbc1e0" />
