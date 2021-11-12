using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Procedural
{
    [System.Serializable]
    public class Tile : MonoBehaviour
    {
        public Transform tile, origin;
        public Conector conector;

        public Tile(Transform _tile, Transform _origin)
        {
            tile = _tile;
            origin = _origin;
        }
    }
}
