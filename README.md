<H1>Bookstore App</H1>
This is a WPF application built with MVVM and Entity Framework Core for my Database course at IT-högskolan.
The application is used to administer a bookstore’s inventory, books and authors.

<br>

<h1>About</h1>
The application allows a user to:
<br>
- View stock levels per store<br>
- Update inventory quantities<br>
- Manage books (create, edit, delete)<br>
- Assign multiple authors to books<br>
- Manage authors and prevent deletion if books exist


<h1>Installation & Setup</h1>
<h3>Requirements</h3> 
<ul> 
 <li>Visual Studio 2022</li> 
 <li>SQL Server LocalDB (installed automatically via Visual Studio – Data storage and processing workload)
</li> </ul> <h3>Step-by-step</h3> <ol> 
 <li>Clone the repository and open the solution in Visual Studio.</li>
 <li>In Solution Explorer, right-click the project <b>BookstoreApp.Infrastructure</b> and select
  <b>Manage User Secrets</b>.</li> 
 <li>Add the following to <code>secrets.json</code>:</li> </ol>
 
```json
{
  "ConnectionStrings": {
    "BookstoreDb": "Server=(localdb)\\MSSQLLocalDB;Database=Bookstore_EF;Trusted_Connection=True;"
  }
}
```

>*Note: You can freely rename the database in secrets.json.* 
<ol start="4"> <li>Build and run the application. The database will be created and seeded automatically on first startup.</li> </ol>


<h3>(Optional) View the database</h3>

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

