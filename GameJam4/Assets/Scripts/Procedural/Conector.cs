using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Procedural
{
    public class Conector : MonoBehaviour
    {
        public Vector2 size = Vector2.one * 4f;
        public bool isConnected;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector2 halfsize = size * 0.5f;
            Vector3 offset = transform.position + transform.up * halfsize.y;
            Gizmos.DrawLine(offset, offset + transform.forward);

            Vector3 top = transform.up * size.y;
            Vector3 side = transform.right * halfsize.x;

            Vector3 topRight = transform.position + top + side;
            Vector3 topLeft = transform.position + top - side;
            Vector3 botRight = transform.position + side;
            Vector3 botLeft = transform.position - side;

            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, botLeft);
            Gizmos.DrawLine(botLeft, botRight);
            Gizmos.DrawLine(botRight, topRight);

            Gizmos.color *= 0.7f;
            Gizmos.DrawLine(topRight, offset);
            Gizmos.DrawLine(topLeft, offset);
            Gizmos.DrawLine(botLeft, offset);
            Gizmos.DrawLine(botRight, offset);



        }

        internal object ToList()
        {
            throw new NotImplementedException();
        }
    }
}
