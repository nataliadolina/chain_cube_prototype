using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CubeSettings")]
public class CubeSettings : ScriptableObject
{
    [SerializeField]
    private float force;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private List<GenericKeyValuePair<int, Color>> twoPowerColorMap;

    public List<GenericKeyValuePair<int, Color>> TwoPowerColorMap { get => twoPowerColorMap; }

    public float Force => force;

    public float JumpForce => jumpForce;
}
