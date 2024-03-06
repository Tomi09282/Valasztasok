using Szabo_Tamas_Valasztasokű;
Valasztas.Beolvas();

Console.WriteLine($"A helyhatósági választáson {Valasztas.Darab()}db képviselő indult a választáson");

Console.WriteLine("adjon Meg 1 nevet:");

string[] nev = Console.ReadLine().Split(' ');
try
{
    Console.WriteLine($"A képviselő {Valasztas.SzavazatokDB(nev[0], nev[1])}db szavazatot kapott.");
}
catch (Exception)
{
    Console.WriteLine("Nincs ilyen név a listában");
}

Valasztas.Arany();
Valasztas.PartArany();
Valasztas.LegtobbSzavazat();
Console.WriteLine();
Valasztas.Gyoztesek();

Console.WriteLine();