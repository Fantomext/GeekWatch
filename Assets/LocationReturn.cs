using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

public class LocationReturn : MonoBehaviour
{
    private PlayerInputAction _playerInput;
    [SerializeField] private GameObject _portalGameObject;
    [SerializeField] private Animator _portalAnimator;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private float _teleportTime;
    IEnumerator enumerator;
    PhraseManager _phraseManager;

    private void Awake()
    {
        _playerInput = new PlayerInputAction();

        _playerInput.Player.ChangeLocation.started += ctx => LoadLocation();

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
        if (enumerator == null)
        {
            enumerator = Teleport();
            StartCoroutine(enumerator);
        }
    }

    IEnumerator Teleport()
    {
        if (ItemLoader.Instance._itemId >= 0)
        {
            ItemLoader.Instance.SavedTimer += LevelTimer.Instance.currentTime;
        }
        _playerAnimator.SnapStart();
        
        StartSequence startSequenc = FindAnyObjectByType<StartSequence>();
        if (startSequenc)
        {
            startSequenc.StopALl();
        }

        yield return new WaitForSeconds(1);
        AudioBox.Instance.Play("HandClick");
        _portalGameObject.SetActive(true);
        _portalAnimator.SetTrigger("in");

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("GeekRoom", LoadSceneMode.Single);
        asyncScene.allowSceneActivation = false;
        while (!asyncScene.isDone)
        {
            Debug.Log("Done");
            yield return new WaitForSeconds(_teleportTime);
            asyncScene.allowSceneActivation = true;
            enumerator = null;
        }
    }
}
