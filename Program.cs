// See https://aka.ms/new-console-template for more information
using GetSQLTableToCSharpClass;
using System.Data;

Console.WriteLine("Hello, World!");


Sql sql = new Sql();
DataTable dt = sql.GetDatatable("SELECT * FROM CAR");
var ts = (List<Car>)sql.Datatable2List<Car>(dt);

foreach (var c in ts)
{
    Console.WriteLine($"{c.Id} {c.Brand} {c.Model}");
}



var ts2 = (List<Bike>)sql.Datatable2List<Bike>(sql.GetDatatable("SELECT Wheels as AmountOfWheels, [Name] FROM Bike"));

foreach (var b in ts2)
{
    Console.WriteLine($"{b.Name} {b.AmountOfWheels}");
}
