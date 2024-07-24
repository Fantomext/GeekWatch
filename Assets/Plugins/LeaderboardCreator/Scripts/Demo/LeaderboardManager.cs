using System.Collections;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        private LeaderboardReference _currentBoard = Leaderboards.MainBoard;

        [Header("Leaderboard Essentials:")]
        //[SerializeField] private TMP_InputField _playerUsernameInput;
        [SerializeField] private Transform _entryDisplayParent;
        [SerializeField] private EntryDisplay _entryDisplayPrefab;
        [SerializeField] private CanvasGroup _leaderboardLoadingPanel;

        [Header("Search Query Essentials:")]
        //[SerializeField] private TMP_Dropdown _timePeriodDropdown;
        //[SerializeField] private TMP_InputField _pageInput, _entriesToTakeInput;
        [SerializeField] private int _defaultPageNumber = 1, _defaultEntriesToTake = 100;

        private string _playerUsername;
        private string _usernameKey = "Username";

        private int _playerScore;
        private string _scoreKey = "PlayerScore";

        //private Coroutine _personalEntryMoveCoroutine;

        void GetPlayerSavedData()
        {
            if (PlayerPrefs.HasKey(_usernameKey))
                _playerUsername = PlayerPrefs.GetString(_usernameKey);

            if (PlayerPrefs.HasKey(_scoreKey))
                _playerScore = PlayerPrefs.GetInt(_scoreKey);

            print(_playerScore);
            if (_playerScore > 0) Submit();
        }

        public void SetPlayerUsername(string name)
        {
            _playerUsername = name;
            PlayerPrefs.SetString(_usernameKey, _playerUsername);
        }
       
        public void SetPlayerScore(int score)
        {
            _playerScore = score;
            PlayerPrefs.SetInt(_scoreKey, _playerScore);
        }
        
        public void Load()
        {
            var timePeriod = Dan.Enums.TimePeriodType.AllTime;

            var pageNumber = _defaultPageNumber; //int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber = Mathf.Max(1, pageNumber);
            //_pageInput.text = pageNumber.ToString();

            var take = _defaultEntriesToTake; //int.TryParse(_entriesToTakeInput.text, out var takeValue) ? takeValue : _defaultEntriesToTake;
            take = Mathf.Clamp(take, 1, 100);
            //_entriesToTakeInput.text = take.ToString();
            
            var searchQuery = new LeaderboardSearchQuery
            {
                Skip = (pageNumber - 1) * take,
                Take = take,
                TimePeriod = timePeriod
            };
            
            //_pageInput.image.color = Color.white;
            //_entriesToTakeInput.image.color = Color.white;

            _currentBoard.GetEntries(searchQuery, OnLeaderboardLoaded, ErrorCallback);
            ToggleLoadingPanel(true);
        }

        public void ChangePageBy(int amount)
        {
            var pageNumber = _defaultPageNumber; //int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber += amount;
            if (pageNumber < 1) return;
            //_pageInput.text = pageNumber.ToString();
        }
        
        private void OnLeaderboardLoaded(Entry[] entries)
        {
            foreach (Transform t in _entryDisplayParent) 
                Destroy(t.gameObject);

            foreach (var t in entries) 
                CreateEntryDisplay(t);
            
            ToggleLoadingPanel(false);
        }
        
        private void ToggleLoadingPanel(bool isOn)
        {
            _leaderboardLoadingPanel.alpha = isOn ? 1f : 0f;
            _leaderboardLoadingPanel.interactable = isOn;
            _leaderboardLoadingPanel.blocksRaycasts = isOn;
        }


        private IEnumerator MoveMenuCoroutine(RectTransform rectTransform, Vector2 anchoredPosition)
        {
            const float duration = 0.25f;
            var time = 0f;
            var startPosition = rectTransform.anchoredPosition;
            while (time < duration)
            {
                time += Time.deltaTime;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, anchoredPosition, time / duration);
                yield return null;
            }
            
            rectTransform.anchoredPosition = anchoredPosition;
            //_personalEntryMoveCoroutine = null;
        }
        
        private void CreateEntryDisplay(Entry entry)
        {
            var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
            entryDisplay.GetComponent<EntryDisplay>().SetEntry(entry);
        }

        private IEnumerator LoadingTextCoroutine(TMP_Text text)
        {
            var loadingText = "Loading";
            for (int i = 0; i < 3; i++)
            {
                loadingText += ".";
                text.text = loadingText;
                yield return new WaitForSeconds(0.25f);
            }
            
            StartCoroutine(LoadingTextCoroutine(text));
        }

        private void InitializeComponents()
        {
            StartCoroutine(LoadingTextCoroutine(_leaderboardLoadingPanel.GetComponentInChildren<TextMeshProUGUI>()));

            //_pageInput.onValueChanged.AddListener(_ => _pageInput.image.color = Color.yellow);
            //_entriesToTakeInput.onValueChanged.AddListener(_ => _entriesToTakeInput.image.color = Color.yellow);

            //_pageInput.placeholder.GetComponent<TextMeshProUGUI>().text = _defaultPageNumber.ToString();
            //_entriesToTakeInput.placeholder.GetComponent<TextMeshProUGUI>().text = _defaultEntriesToTake.ToString();
        }
        
        private void Start()
        {
            InitializeComponents();
            GetPlayerSavedData();
            Load();
        }

        public void Submit()
        {
            _currentBoard.UploadNewEntry(_playerUsername, _playerScore, Callback, ErrorCallback);
        }
        
        public void DeleteEntry()
        {
            _currentBoard.DeleteEntry(Callback, ErrorCallback);
        }

        public void ResetPlayer()
        {
            LeaderboardCreator.ResetPlayer();
        }


        private void Callback(bool success)
        {
            if (success)
                Load();
        }
        
        private void ErrorCallback(string error)
        {
            Debug.LogError(error);
        }
    }
}
