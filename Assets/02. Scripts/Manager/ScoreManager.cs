using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 응집도를 높혀라
    // 응집도 : '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 캡슐화라고 한다.

    [SerializeField] private Text _currentScoreTextUI;
    private int _currentScore = 0;

    private void Start()
    {
        Refresh();
        Load();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            Save();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            Load();
        }
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;

        Refresh();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
    }

    public void Save()
    {
        // 유니티에서는 값을 저장할 때 'PlayerPrefs' 모듈을 씁니다.
        // 저장 가능한 자료형은 : int, float, string
        // 저장을 할 때는 저장할 이름(key)과 값(value)이 두형태로 저장을 해요.

        // 저장 : Set
        // 로드 : Get

        PlayerPrefs.SetInt("score", _currentScore);
        Debug.Log("저장됐습니다");
    }

    private void Load()
    {
        int score = PlayerPrefs.GetInt("score");
        Debug.Log($"전판의 점수는 {score:N0}점입니다.");
    }
}
