using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

interface IArtefact
{
    public int id { get; set; }
    static GameObject model;
    static Transform grabPosition;
    static Transform standPosition;
    static int score;
    static Rigidbody rigibody;

    public void Take();

    public void Drop();

    public void Effect();
}
