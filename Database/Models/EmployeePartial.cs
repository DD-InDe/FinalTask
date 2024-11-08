namespace Database.Models;

public partial class Employee
{
    public string FullName => LastName + " " + FirstName;
}