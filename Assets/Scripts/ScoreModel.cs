/// <summary>
/// Data Model for score persistence
/// </summary>
public class ScoreModel
{
    public string FirstPlaceName;
    public int FirstPlaceScore;

    public string SecondPlaceName;
    public int SecondPlaceScore;

    public string ThirdPlaceName;
    public int ThirdPlaceScore;

    public string FourthPlaceName;
    public int FourthPlaceScore;

    public string FifthPlaceName;
    public int FifthPlaceScore;

    public ScoreModel(string firstPlaceName, int firstPlaceScore, string secondPlaceName, int secondPlaceScore, string thirdPlaceName, int thirdPlaceScore, string fourthPlaceName, int fourthPlaceScore, string fifthPlaceName, int fifthPlaceScore)
    {
        FirstPlaceName = firstPlaceName;
        FirstPlaceScore = firstPlaceScore;
        SecondPlaceName = secondPlaceName;
        SecondPlaceScore = secondPlaceScore;
        ThirdPlaceName = thirdPlaceName;
        ThirdPlaceScore = thirdPlaceScore;
        FourthPlaceName = fourthPlaceName;
        FourthPlaceScore = fourthPlaceScore;
        FifthPlaceName = fifthPlaceName;
        FifthPlaceScore = fifthPlaceScore;
    }
}
