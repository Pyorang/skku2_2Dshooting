using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    // 응집도를 높혀라
    // 응집도 : '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 캡슐화라고 한다.

    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _bestScoreTextUI;
    [SerializeField] private float _accentDuration = 0.3f;
    [SerializeField] private float _accentStrength = 2.0f;
    private int _currentScore = 0;
    private int _bestScore = 0;

    private void Start()
    {
        Load();
    }
    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        
        if(_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }

        Refresh();
    }

    private void AccentScoreText()
    {
        _currentScoreTextUI.transform.DOKill();

        Sequence mySequence = DOTween.Sequence();

        _currentScoreTextUI.transform.localScale = Vector3.one;

        mySequence.Append(_currentScoreTextUI.transform.DOScale(Vector3.one * _accentStrength, _accentDuration));
        mySequence.Append(_currentScoreTextUI.transform.DOScale(Vector3.one, _accentDuration));
    }
    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
        _bestScoreTextUI.text = $"최고 점수 : {_bestScore:N0}";
    }

    public void Save()
    {
        UserData data = new UserData
        {
            BestScore = _bestScore
        };

        string json = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString("UserData", json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("UserData"))
        {
            string json = PlayerPrefs.GetString("UserData");
            UserData data = JsonUtility.FromJson<UserData>(json);

            if (data != null)
            {
                _bestScore = data.BestScore;
            }
            else
            {
                _bestScore = 0;
            }
        }
        else
        {
            _bestScore = 0;
        }

        Refresh();
    }
}
