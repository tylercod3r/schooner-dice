namespace SchoonerDice;

public class ScoreManager:IScoreManager
{
    #region VARIABLE
    private const int ScoreFullHouse = 25;
    private const int ScoreSmallStraight = 30;
    private const int ScoreLargeStraight = 40;
    private const int ScoreAllDifferent = 35;
    private const int ScoreSchooner = 50;
    #endregion
    
    #region METHOD - PUBLIC
    public int GetScoreNumberGroup(List<int> rollList, int targetNumber)
    {
        var count = rollList.Count(roll => roll == targetNumber);
        return count * targetNumber;
    }
    
    public int GetScoreThreeOfKind(List<int> rollList)
    {
        return GetScoreManyOfKind(rollList, 3);
    }

    public int GetScoreFourOfKind(List<int> rollList)
    {
        return GetScoreManyOfKind(rollList, 4);
    }
    
    public int GetScoreFullHouse(List<int> rollList)
    {
        // make dictionary of assigned values
        var dict = new Dictionary<int, int>();
        foreach (var value in rollList)
        {
            if (!dict.TryAdd(value, 1))
            {
                dict[value]++;
            }
        }

        // confirm both 3-of-kind & a pair exist
        var found3 = false;
        var found2 = false;
        foreach (var key in dict.Keys)
        {
            var val = dict[key];
            switch (val)
            {
                case 2:
                    found2 = true;
                    break;
                case 3:
                    found3 = true;
                    break;
            }
        }

        return (found2 && found3) ? ScoreFullHouse : 0;
    }
    
    public int GetScoreSmallStraight(List<int> rollList)
    {
        return IsRun(rollList, 4) ? ScoreSmallStraight : 0;
    }

    public int GetScoreLargeStraight(List<int> rollList)
    {
        return IsRun(rollList, 5) ? ScoreLargeStraight : 0;
    }
    
    public int GetScoreAllDifferent(List<int> rollList)
    {
        return rollList.Distinct().Count() == rollList.Count ? ScoreAllDifferent : 0;
    }
    
    public int GetScoreSchooner(List<int> rollList)
    {
        return rollList.TrueForAll(i => i.Equals(rollList.FirstOrDefault())) ? ScoreSchooner : 0;
    }
    
    public int GetScoreChance(List<int> rollList)
    {
        var score = 0;
        var index = 0;
        for (; index < rollList.Count; index++)
        {
            score += rollList[index];
        }

        return score;
    }
    #endregion
    
    #region METHOD - PRIVATE
    private static int GetScoreManyOfKind(List<int> rollList, int matchCount)
    {
        // populate dictionary
        var dict = new Dictionary<int, int>();
        foreach (var value in rollList)
        {
            if (!dict.TryAdd(value, 1))
            {
                dict[value]++;
            }
        }
        
        // locate item with matching count, and return sum total
        foreach (var key in dict.Keys)
        {
            var val = dict[key];
            if (val == matchCount)
            {
                return rollList.Sum();
            }
        }

        return 0;
    }
    
    private static bool IsRun(List<int> rollList, int targetRunCount)
    {
        // get distinct list
        var cleanList = rollList.Distinct().ToList();
        
        // get run count
        var runCount = 1;
        for(var i = 0; i < cleanList.Count - 1; i++)
        {
            var val = cleanList[i];
            var nextVal = cleanList[i + 1];
            if (nextVal - val == 1)
            {
                runCount++;
            }
            else
            {
                runCount = 1;
            }
        }

        return runCount == targetRunCount;
    }
    #endregion
}