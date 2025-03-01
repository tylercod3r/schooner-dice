namespace SchoonerDice;

public static class SchoonerDiceProgram
{
    #region VARIABLE
    private const int DiceCount = 5;
    private const int DiceSideCount = 8;
    
    private const string TextSeparator = "######################################";
    private const string TextSeparator2 = "-----------------";
    private const string TextIntro = "Welcome to SchoonerDice!";
    private const string TextPrompt1 = "Press any key to play!";
    private const string TextPrompt2 = "roll:";
    private const string TextPrompt3 = "scoring:";
    private const string TextPrompt4 = "top category(s):";

    private enum Category
    {
        Ones,
        Twos,
        Threes,
        Fours,
        Fives,
        Sixes,
        Sevens,
        Eights,
        ThreeOfAKind,
        FourOfAKind,
        FullHouse,
        SmallStraight,
        LargeStraight,
        AllDifferent,
        Schooner,
        Chance
    }
    #endregion
    
    #region METHOD - PRIVATE
    private static void Main()
    {
        ShowTextIntro();

        var diceRoll = GetDiceRoll(DiceCount, DiceSideCount);
        ShowTextRoll(diceRoll);

        ShowTextScore(diceRoll, new ScoreManager());
    }

    private static void ShowTextIntro()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine(TextSeparator);
        Console.WriteLine(TextIntro);
        Console.WriteLine(TextSeparator);
        Console.WriteLine(TextPrompt1);
        Console.ReadKey(true);
    }

    private static void ShowTextRoll(List<int> diceRoll)
    {
        Console.WriteLine('\n');
        Console.WriteLine(TextPrompt2);
        Console.WriteLine(TextSeparator2);
        diceRoll.ForEach(Console.WriteLine);
    }

    private static void ShowTextScore(List<int> diceRoll, IScoreManager scoreManager)
    {
        Console.WriteLine('\n');
        Console.WriteLine(TextPrompt3);
        var categories = GetTopCategories(diceRoll, scoreManager);
        Console.WriteLine('\n');
        Console.WriteLine(TextPrompt4);
        Console.WriteLine(TextSeparator2);
        foreach (var category in categories)
        {
            Console.WriteLine($"{category}");
        }
    }
    
    private static List<int> GetDiceRoll(int diceCount, int diceSideCount)
    {
        var rollList = new List<int>();
        var random = new Random();
        var i = 0;
        for (; i < diceCount; i++)
        {
            rollList.Add(random.Next(1, diceSideCount));
        }
        
        return rollList;
    }
    
    private static List<Category> GetTopCategories(List<int> diceRoll, IScoreManager scoreManager)
    {
        var dict = new Dictionary<Category, int>
        {
            { Category.Ones, Score(Category.Ones, diceRoll, scoreManager) },
            { Category.Twos, Score(Category.Twos, diceRoll, scoreManager) },
            { Category.Threes, Score(Category.Threes, diceRoll, scoreManager) },
            { Category.Fours, Score(Category.Fours, diceRoll, scoreManager) },
            { Category.Fives, Score(Category.Fives, diceRoll, scoreManager) },
            { Category.Sixes, Score(Category.Sixes, diceRoll, scoreManager) },
            { Category.Sevens, Score(Category.Sevens, diceRoll, scoreManager) },
            { Category.Eights, Score(Category.Eights, diceRoll, scoreManager) },
            { Category.ThreeOfAKind, scoreManager.GetScoreThreeOfKind(diceRoll) },
            { Category.FourOfAKind, scoreManager.GetScoreFourOfKind(diceRoll) },
            { Category.FullHouse, scoreManager.GetScoreFullHouse(diceRoll) },
            { Category.SmallStraight, scoreManager.GetScoreSmallStraight(diceRoll) },
            { Category.LargeStraight, scoreManager.GetScoreLargeStraight(diceRoll) },
            { Category.AllDifferent, scoreManager.GetScoreAllDifferent(diceRoll) },
            { Category.Schooner, scoreManager.GetScoreSchooner(diceRoll) },
            { Category.Chance, scoreManager.GetScoreChance(diceRoll) }
        };
        
        // show scores
        Console.WriteLine(TextSeparator2);
        foreach(KeyValuePair<Category, int> entry in dict)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
        
        // calc greatest score
        var highestScore = 0;
        foreach (var value in dict.Keys)
        {
            var currentScore = dict[value];
            if (currentScore > highestScore)
            {
                highestScore = currentScore;
            }
        }
        
        // get highest scoring categories
        var categories = new List<Category>();
        foreach (var value in dict.Keys)
        {
            var val = dict[value];
            if (val == highestScore)
            {
                categories.Add(value);
            }
        }
        
        return categories;
    }
    
    private static int Score(Category category, List<int> diceRoll, IScoreManager scoreManager)
    {
        return category switch
        {
            Category.Ones => scoreManager.GetScoreNumberGroup(diceRoll, 1),
            Category.Twos => scoreManager.GetScoreNumberGroup(diceRoll, 2),
            Category.Threes => scoreManager.GetScoreNumberGroup(diceRoll, 3),
            Category.Fours => scoreManager.GetScoreNumberGroup(diceRoll, 4),
            Category.Fives => scoreManager.GetScoreNumberGroup(diceRoll, 5),
            Category.Sixes => scoreManager.GetScoreNumberGroup(diceRoll, 6),
            Category.Sevens => scoreManager.GetScoreNumberGroup(diceRoll, 7),
            Category.Eights => scoreManager.GetScoreNumberGroup(diceRoll, 8),
            Category.ThreeOfAKind => scoreManager.GetScoreThreeOfKind(diceRoll),
            Category.FourOfAKind => scoreManager.GetScoreFourOfKind(diceRoll),
            Category.FullHouse => scoreManager.GetScoreFullHouse(diceRoll),
            Category.SmallStraight => scoreManager.GetScoreSmallStraight(diceRoll),
            Category.LargeStraight => scoreManager.GetScoreLargeStraight(diceRoll),
            Category.AllDifferent => scoreManager.GetScoreAllDifferent(diceRoll),
            Category.Schooner => scoreManager.GetScoreSchooner(diceRoll),
            Category.Chance => scoreManager.GetScoreChance(diceRoll),
            _ => 0
        };
    }
    #endregion
}