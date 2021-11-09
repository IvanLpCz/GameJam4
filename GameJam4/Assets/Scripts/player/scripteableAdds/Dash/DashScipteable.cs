using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripteable_Player
{
    [CreateAssetMenu(fileName = "New Dash", menuName = "Dash Data")]
    public class DashScipteable : ScriptableObject
    {
        [SerializeField] public float dashSpeed;
        [SerializeField] public float dashDuration;
    }
}
