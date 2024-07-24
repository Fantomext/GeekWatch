using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Audio;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private int _countScene;
    private PlayerInputAction _playerInput;
    [SerializeField] private float _cooldownSnap = 20;
    [SerializeField] private GameObject _portalGameObject;
    [SerializeField] private Animator _portalAnim;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private float _teleportTime; 
    public bool _canTeleport;
    IEnumerator enumerator;
    private PhraseManager _phraseManager;

    private void Start()
    {
        _countScene = SceneManager.sceneCountInBuildSettings;
        _phraseManager = FindAnyObjectByType<PhraseManager>();
    }

    private void Awake()
    {
        _playerInput = new PlayerInputAction();

        _playerInput.Player.ChangeLocation.started += ctx => LoadLocation();

    }

    private void Update()
    {
        if (_cooldownSnap <= 0)
        {
            _canTeleport = true;
        }
        else
        {
            _cooldownSnap -= Time.deltaTime;
        }

    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void LoadLocation()
    {
        if (_canTeleport && enumerator == null)
        {
            enumerator = Teleport();
            StartCoroutine(enumerator);
        }
    }

    IEnumerator Teleport()
    {
        StartSequence startSequenc = FindAnyObjectByType<StartSequence>();
        if (startSequenc)
        {
            startSequenc.StopALl();
        }
        int index = Random.Range(2, _countScene);
        _playerAnimator.SnapStart();
        yield return new WaitForSeconds(1f);
        AudioBox.Instance.Play("HandSnap");
        _portalGameObject.SetActive(true);
        _portalAnim.SetTrigger("in");

        _phraseManager.StopAllCoroutines();
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        asyncScene.allowSceneActivation = false;
        while (!asyncScene.isDone)
        {
            yield return new WaitForSeconds(_teleportTime);
            enumerator = null;
            AudioBox.Instance.Stop("HandSnap");
            Debug.Log("Load End");

            asyncScene.allowSceneActivation = true;
            yield break;

        }
        yield return null;
    }
}
