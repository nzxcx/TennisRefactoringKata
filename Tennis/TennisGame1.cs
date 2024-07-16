using System;


namespace Tennis
{
    public class TennisGame1(string firstPlayerName, string secondPlayerName) : ITennisGame
    {

        private readonly Player _firstPlayer = new(firstPlayerName, default);
        private readonly Player _secondPlayer = new(secondPlayerName, default);

        private class Player(string name, int score)
        {

            public string Name { get; } = name;

            public int Score { get; private set; } = score;

            public void AddOnePoint()
            {
                Score += 1;
            }

        }

        private enum ScoreDescription
        {

            Love,
            Fifteen,
            Thirty,
            Forty

        }

        public void WonPoint(string playerName)
        {
            if (playerName == _firstPlayer.Name)
            {
                _firstPlayer.AddOnePoint();
            }
            else if (playerName == _secondPlayer.Name)
            {
                _secondPlayer.AddOnePoint();
            }
        }


        public string GetScore()
        {
            return ScoresAreEqual()
                ? HandleEqualScore()
                : HandleNonEqualScore();
        }

        private string HandleNonEqualScore()
        {
            const int Max = 4;
            if (_firstPlayer.Score < Max && _secondPlayer.Score < Max)
            {
                return GetScoreDescription(_firstPlayer.Score) + "-" + GetScoreDescription(_secondPlayer.Score);
            }

            return HandleMinusScore();
        }

        private static string GetScoreDescription(int score)
        {
            return ((ScoreDescription)score).ToString();
        }

        private string HandleMinusScore()
        {
            var diff = _firstPlayer.Score - _secondPlayer.Score;
            return diff switch
            {
                1 => $"Advantage {_firstPlayer.Name}",
                -1 => $"Advantage {_secondPlayer.Name}",
                >= 2 => $"Win for {_firstPlayer.Name}",
                _ => $"Win for {_secondPlayer.Name}"
            };
        }

        private string HandleEqualScore()
        {
            return _firstPlayer.Score switch
            {
                0 => "Love-All",
                1 => "Fifteen-All",
                2 => "Thirty-All",
                _ => "Deuce"
            };
        }

        private bool ScoresAreEqual()
        {
            return _firstPlayer.Score == _secondPlayer.Score;
        }

    }
}
