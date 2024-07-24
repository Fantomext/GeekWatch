using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStand : MonoBehaviour
{
    [SerializeField] private Item standItem;
    [SerializeField] private Transform standPoint;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _standName;
    [SerializeField] private ItemsSO _itemsPool;
    [SerializeField] private float _score;
    [SerializeField] private bool _canInteract = true;
    [SerializeField] private StandManager _standManager;
    public int id;

    void Start()
    {
        if (PlayerPrefs.HasKey(_standName))
        {
            int id = PlayerPrefs.GetInt(_standName);
            SetItem(_itemsPool.GetById(id));
        }
    }

    private void Update()
    {
        if (standItem)
        {
            standItem.transform.localScale = Vector3.one;
            standItem.transform.localPosition = standPoint.transform.position;
        }
    }

    public void SetItem(Item item)
    {
        standItem = Instantiate(item, standPoint.position, standPoint.rotation);
        _score = standItem.ReturnScore();
        PlayerPrefs.SetInt(_standName, item.ReturnId());
        PlayerPrefs.SetInt(_standName+"Score", (int) _score);
        id = item.ReturnId();
        _text.text = _score.ToString();
        Destroy(standItem);
        _canInteract = false;
        _standManager.CompleteOneStand();
        Destroy(transform.GetComponent<Rigidbody>());
    }

}
