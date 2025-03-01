namespace SchoonerDice;

public interface IScoreManager
{
    int GetScoreNumberGroup(List<int> rollList, int target);
    int GetScoreThreeOfKind(List<int> rollList);
    int GetScoreFourOfKind(List<int> rollList);
    int GetScoreFullHouse(List<int> rollList);
    int GetScoreSmallStraight(List<int> rollList);
    int GetScoreLargeStraight(List<int> rollList);
    int GetScoreAllDifferent(List<int> rollList);
    int GetScoreSchooner(List<int> rollList);
    int GetScoreChance(List<int> rollList);
}