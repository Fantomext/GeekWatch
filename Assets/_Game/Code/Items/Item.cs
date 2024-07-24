using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int _id;
    [SerializeField] protected int _score;
    [SerializeField] protected GameObject _model;
    [SerializeField] protected Transform _standPosition;
    [SerializeField] protected Rigidbody _rigibody;
    [SerializeField] private Transform _grabPoint;
    [SerializeField] private Collider _collider;
    protected bool _isTaked;

    private void Update()
    {
        if (_isTaked)
        {
            if (Vector3.Distance(_grabPoint.position, _rigibody.transform.parent.position) > 0.1f)
            {
                Vector3 one =  _rigibody.transform.position - _grabPoint.position;
                Vector3 second = _rigibody.transform.parent.position - _rigibody.transform.position;
                Vector3 dir = one + second;
                _rigibody.AddForce(dir * 100f);
                //_rigibody.velocity = Vector3.right * (Mathf.Sin(Time.deltaTime * 1) * 5);
                //_rigibody.angularVelocity = (new Vector3(0f,0f,0f));
                _rigibody.transform.rotation = _grabPoint.transform.rotation;

            }
        }
    }

    public void Take(Transform parent)
    {
        StartCoroutine(TakeDelay(parent));
    }
    
    IEnumerator TakeDelay(Transform parent)
    {
        yield return new WaitForSeconds(0.5f);
        //_rigibody.transform.position = parent.transform.position;
        _rigibody.transform.parent = parent;
        transform.localPosition = Vector3.zero;
        
        _rigibody.interpolation = RigidbodyInterpolation.None;
        _rigibody.useGravity = false;
        _collider.enabled = false;
        SetDrag(10);
        _isTaked = true;
        ItemLoader.Instance.SetItem(_id);
        yield return null;
    }

    public void Drop()
    {
        _collider.enabled = true;
        _rigibody.transform.parent = null;
        _rigibody.constraints = RigidbodyConstraints.None;
        _rigibody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigibody.useGravity = true;
        SetDrag(0);
        _isTaked = false;
        _rigibody.velocity = Vector3.zero;
        ItemLoader.Instance.SetItem(-1);
    }

    public int ReturnId()
    {
        return _id;
    }

    public int ReturnScore()
    {
        return _score;
    }

    private void SetDrag(int drag)
    {
        _rigibody.drag = drag;
    }

}
