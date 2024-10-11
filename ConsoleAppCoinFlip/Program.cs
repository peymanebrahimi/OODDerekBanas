// See https://aka.ms/new-console-template for more information
using ConsoleAppCoinFlip;

Console.WriteLine("Hello, World!");

//CoinGame coinGame = new CoinGame("Peyman", "Maryam");

//coinGame.StartGame();
//int[,] numbers2D = { { 9, 99 }, { 3, 33 }, { 5, 55 } };

//foreach (int i in numbers2D)
//{
//    System.Console.Write($"{i} ");
//}
// Output: 9 99 3 33 5 55

int[,,] array3D = new int[,,] { { { 1, 2, 3 }, { 4,   5,  6 } },
                        { { 7, 8, 9 }, { 10, 11, 12 } } };
foreach (int i in array3D)
{
    System.Console.Write($"{i} ");
}
// Output: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
Console.ReadLine();
