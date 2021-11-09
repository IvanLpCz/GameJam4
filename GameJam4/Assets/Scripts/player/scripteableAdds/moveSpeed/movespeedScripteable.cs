using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripteable_Player
{
    [CreateAssetMenu(fileName = "New Speed", menuName = "Speed Data")]
    public class movespeedScripteable : ScriptableObject
    {
        [SerializeField] public float speed;
    }
}
