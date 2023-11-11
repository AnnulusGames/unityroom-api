namespace Unityroom
{
    public interface IUnityroomApiClient
    {
        void SendScore(
            int boardNo
            , float score
            , ScoreboardWriteMode mode
        );
    }
}