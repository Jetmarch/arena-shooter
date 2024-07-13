using ArenaShooter.Units.Player;
using Zenject;

namespace ArenaShooter.UI
{
    public class ScorePointsController : IInitializable, ILateDisposable
    {
        private ScorePointsStorage _scorePointsStorage;
        private ScorePointsView _scorePointsView;

        public ScorePointsController(ScorePointsStorage scorePointsStorage, ScorePointsView scorePointsView)
        {
            _scorePointsStorage = scorePointsStorage;
            _scorePointsView = scorePointsView;
        }

        public void Initialize()
        {
            _scorePointsStorage.OnScorePointsChanged += SetScorePoints;
            SetScorePoints();
        }

        public void LateDispose()
        {
            _scorePointsStorage.OnScorePointsChanged -= SetScorePoints;
        }

        private void SetScorePoints()
        {
            _scorePointsView.Show($"—чет: {_scorePointsStorage.ScorePoints}");
        }
    }
}