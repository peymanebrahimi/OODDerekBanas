using System.Security.Cryptography;

namespace ConsoleAppCoinFlip;
internal class Player
{
    private string _name = "";
    private string _coinOption = "";

    public string[] CoinValue { get; set; } = ["Heads", "Tails"];

    public Player(string name)
    {
        _name = name;
    }

    public void SetCoinOption(string opponentFlip)
    {
        _coinOption = opponentFlip == "Heads" ? "Tails" : "Heads";
    }

    public string GetRandCoinOption()
    {
        int rand = RandomNumberGenerator.GetInt32(0, 2) < 1 ? 0 : 1;
        _coinOption = CoinValue[rand];
        return _coinOption;
    }

    public void DidPlayerWin(string winningFlip)
    {
        if (_coinOption == winningFlip)
        {
            Console.WriteLine(_name + " won with a flip of " + _coinOption);
        }
        else
        {
            Console.WriteLine(_name + " lost with a flip of " + _coinOption);
        }
    }
}
class Coin
{
    private string _coinOption = "";

    public string[] CoinValue { get; set; } = ["Heads", "Tails"];

    public Coin()
    {
        int rand = RandomNumberGenerator.GetInt32(0, 2) < 1 ? 0 : 1;
        _coinOption = CoinValue[rand];
    }

    public string GetCoinOption()
    {
        return _coinOption;
    }
}
class CoinGame
{
    Player[] players = new Player[2];
    Coin theCoin = new Coin();

    public CoinGame(string player1Name, string player2Name)
    {
        players[0] = new Player(player1Name);
        players[1] = new Player(player2Name);

    }

    public void StartGame()
    {
        int rand = RandomNumberGenerator.GetInt32(0, 2) < 1 ? 0 : 1;
        string playerPick = players[rand].GetRandCoinOption();

        int opponentIndex = rand == 0 ? 1 : 0;
        players[opponentIndex].SetCoinOption(playerPick);

        string winningFlip = theCoin.GetCoinOption();

        players[0].DidPlayerWin(winningFlip);
        players[1].DidPlayerWin(winningFlip);
    }
}
