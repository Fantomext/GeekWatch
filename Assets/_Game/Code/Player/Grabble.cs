using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabble : MonoBehaviour
{
    [SerializeField] PlayerUI _playerUI;
    [SerializeField] private Transform _itemPosition;
    [SerializeField] private Item _takedItem;
    [SerializeField] private PlayerAnimator _playerAnimator;
    private Emission _selectedItem;
    private PlayerInputAction _playerInput;
    [SerializeField] private LayerMask _maskStand;

    private void Awake()
    {
        _playerInput = new PlayerInputAction();

        _playerInput.Player.Interact.started += ctx => Interact();
        _playerInput.Player.Drop.started += ctx => DropItem();

    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody)
            {
                if (hit.rigidbody.TryGetComponent<Emission>(out var item))
                {
                    _selectedItem = item;
                    _selectedItem.HightLight();
                }
            }
            else
            {
                if (_selectedItem)
                {
                    _selectedItem.Extinction();
                    _selectedItem = null;
                }
            }
        }
    }

    public void Interact()
    {
        if (!_takedItem)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3))
            {
                if (hit.rigidbody)
                {
                    if (hit.rigidbody.TryGetComponent<Item>(out var item))
                    {
                        item.Take(_itemPosition);
                        _takedItem = item;
                        _playerAnimator.TakeStart();
                    }

                    else if (hit.rigidbody.TryGetComponent<NoteItem>(out var note))
                    {

                        _playerUI.OpenNote();
                        note.Interact();
                        note.GetComponent<Emission>().Extinction();
                        //item.Take(_itemPosition);
                        //_takedItem = item;
                    }
                }
            }
        }
        else
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3, _maskStand))
            {
                if (hit.rigidbody)
                {
                    if (hit.rigidbody.TryGetComponent<ItemStand>(out var stand))
                    {
                        stand.SetItem(_takedItem);
                        ItemLoader.Instance.SetItem(-1);
                        DestroyItem();
                    }
                }
            }
        }
    }

    public void DropItem()
    {
        if (_takedItem)
        {
            _takedItem.Drop();
            _takedItem = null;
            _playerAnimator.DropStart();
        }
    }

    public void DestroyItem()
    {
        if (_takedItem)
        {
            Destroy(_takedItem.gameObject);
            _takedItem = null;
        }
    }

    public void SetItem(Item item)
    {
        Item newItem = Instantiate(item, _itemPosition);
        newItem.Take(_itemPosition);
        _takedItem = newItem;
    }
}
