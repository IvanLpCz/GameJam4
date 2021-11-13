using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Procedural
{
    public class DungeonGenerator : MonoBehaviour
    {
        public GameObject[] startPrefabs;
        public GameObject[] tilePrefabs;
        public GameObject[] exitPrefabs;
        public GameObject[] blockedPrefabs;

        public List<Tile> generetedTiles = new List<Tile>();
        private List<Conector> aviableConectors = new List<Conector>();

        [Header("Debugging Options")]
        public bool useBoxColliders;

        [Header("Generation Limits")]
        [Range(2,100)]public int mainLength = 10;
        [Range(0, 50)]public int brachLength = 5;
        [Range(0, 25)]public int numBranches = 10;
        [Range(0,1f)]public float constructionDelay = 0.1f;

        Transform tileFrom, tileTo, tileRoot;
        Transform container;

        private int attempts;
        private int maxAttempts = 50;
        private void Start()
        {
            StartCoroutine(DungeonBuild());
        }
        IEnumerator DungeonBuild()
        {
            GameObject goContainer = new GameObject("Main Path");
            container = goContainer.transform;
            container.SetParent(transform);
            tileRoot = CreateStartTile();
            tileTo = tileRoot;
            while(generetedTiles.Count < mainLength)
            {
                yield return new WaitForSeconds(constructionDelay);
                tileFrom = tileTo;
                if(generetedTiles.Count == mainLength - 1)
                {
                    tileTo = CreateExitTile();
                }
                else
                {
                    tileTo = CreateTile();
                }
                ConnectTiles();
                CollisionCheck();
                if(attempts >= maxAttempts) { break; }
            }
            foreach(Conector conector in container.GetComponentsInChildren<Conector>())
            {
                if (!conector.isConnected)
                {
                    if (!aviableConectors.Contains(conector))
                    {
                        aviableConectors.Add(conector);
                    }                   
                }
            }
            for (int b = 0; b < numBranches; b++) 
            {
                if(aviableConectors.Count > 0)
                {
                    goContainer = new GameObject("Branch " + (b + 1));
                    container = goContainer.transform;
                    container.SetParent(transform);
                    int availIndex = Random.Range(0, aviableConectors.Count);
                    tileRoot = aviableConectors[availIndex].transform.parent.parent;
                    aviableConectors.RemoveAt(availIndex);
                    tileTo = tileRoot;
                    for (int i = 0; i < brachLength - 1; i++)
                    {
                        yield return new WaitForSeconds(constructionDelay);
                        tileFrom = tileTo;
                        tileTo = CreateTile();
                        ConnectTiles();
                        CollisionCheck();
                        if (attempts >= maxAttempts) { break; }
                    }
                }
                else
                {
                    break;
                }
                CleanupBoxes();
            }

        }

        private void CollisionCheck()
        {
            BoxCollider box = tileTo.GetComponent<BoxCollider>();
            if(box == null)
            {
                box = tileTo.gameObject.AddComponent<BoxCollider>();
                box.isTrigger = true;
            }
            Vector3 offset = (tileTo.right * box.center.x) + (tileTo.up * box.center.y) + (tileTo.forward * box.center.z);
            Vector3 halfExtents = box.bounds.extents;
            List<Collider> hits = Physics.OverlapBox(tileTo.position + offset, halfExtents, Quaternion.identity, LayerMask.GetMask("Tile")).ToList();
            if(hits.Count > 0)
            {
                if(hits.Exists(x => x.transform != tileFrom && x.transform != tileTo))
                {
                    attempts++;
                    int toIndex = generetedTiles.FindIndex(x => x.tile == tileTo);
                    if(generetedTiles[toIndex].conector != null)
                    {
                        generetedTiles[toIndex].conector.isConnected = false;
                    }
                    generetedTiles.RemoveAt(toIndex);
                    DestroyImmediate(tileTo.gameObject);

                    if(attempts >= maxAttempts)
                    {
                        int fromIndex = generetedTiles.FindIndex(x => x.tile == tileFrom);
                        Tile myTileFrom = generetedTiles[fromIndex];
                        if(tileFrom != tileRoot)
                        {
                            if (myTileFrom.conector != null)
                            {
                                myTileFrom.conector.isConnected = false;
                            }
                            aviableConectors.RemoveAll(x => x.transform.parent.parent == tileFrom);
                            generetedTiles.RemoveAt(fromIndex);
                            DestroyImmediate(tileFrom.gameObject);
                            if (myTileFrom.origin != tileRoot)
                            {
                                tileFrom = myTileFrom.origin;
                            }
                            else if (container.name.Contains("Main"))
                            {
                                if(myTileFrom.origin != null)
                                {
                                    tileRoot = myTileFrom.origin;
                                    tileFrom = tileRoot;
                                }
                            }
                            else if (aviableConectors.Count > 0)
                            {
                                int aviableIndex = Random.Range(0, aviableConectors.Count);
                                tileRoot = aviableConectors[aviableIndex].transform.parent.parent;
                                aviableConectors.RemoveAt(aviableIndex);
                                tileFrom = tileRoot;
                            }
                            else { return; }
                        }
                        else if (container.name.Contains("Main"))
                        {
                            if(myTileFrom.origin != null)
                            {
                                tileRoot = myTileFrom.origin;
                                tileFrom = tileRoot;
                            }
                        }
                        else if (aviableConectors.Count > 0)
                        {
                            int aviableIndex = Random.Range(0, aviableConectors.Count);
                            tileRoot = aviableConectors[aviableIndex].transform.parent.parent;
                            aviableConectors.RemoveAt(aviableIndex);
                            tileFrom = tileRoot;
                        }
                        else { return; }
                    }

                }
                if(tileFrom != null)
                {
                    if (generetedTiles.Count == mainLength - 1)
                    {
                        tileTo = CreateExitTile();
                    }
                    else
                    {
                        tileTo = CreateTile();
                    }
                    ConnectTiles();
                    CollisionCheck();
                }

            }
            else
            {
                attempts = 0;
            }
        }

        void CleanupBoxes()
        {
            if (!useBoxColliders)
            {
                foreach(Tile myTile in generetedTiles)
                {
                    BoxCollider box = myTile.tile.GetComponent<BoxCollider>();
                    if(box != null) { Destroy(box);  }
                }
            }
        }
        void ConnectTiles()
        {
            Transform connectFrom = GetRandomConnector(tileFrom);
            if(connectFrom == null) { return; }
            Transform connectTo = GetRandomConnector(tileTo);
            if(connectTo == null) { return; }
            connectTo.SetParent(connectFrom);
            tileTo.SetParent(connectTo);
            connectTo.localPosition = Vector3.zero;
            connectTo.localRotation = Quaternion.identity;
            connectTo.Rotate(0, 180f, 0);
            tileTo.SetParent(container);
            connectTo.SetParent(tileTo);
            connectTo.SetParent(tileTo.Find("Connectors"));
            generetedTiles.Last().conector = connectFrom.GetComponent<Conector>();
        }
        Transform GetRandomConnector(Transform tile)
        {
            if(tile == null) { return null; }
            List<Conector> connectorList = tile.GetComponentsInChildren<Conector>().ToList().FindAll(x => x.isConnected == false);   
            if (connectorList.Count > 0)
            {
                int connectorIndex = Random.Range(0, connectorList.Count);
                connectorList[connectorIndex].isConnected = true;
                if(tile == tileFrom)
                {
                    BoxCollider box = tile.GetComponent<BoxCollider>();
                    if(box == null)
                    {
                        box = tile.gameObject.AddComponent<BoxCollider>();
                        box.isTrigger = true;
                    }
                }
                return connectorList[connectorIndex].transform;
            }
            return null;
        }
        Transform CreateTile()
        {
            int index = Random.Range(0, tilePrefabs.Length);
            GameObject goTile = Instantiate(tilePrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;
            goTile.name = tilePrefabs[index].name;
            Transform origin = generetedTiles[generetedTiles.FindIndex(x => x.tile == tileFrom)].tile;
            generetedTiles.Add(new Tile(goTile.transform, origin));

            return goTile.transform;
        }
        Transform CreateExitTile()
        {
            int index = Random.Range(0, exitPrefabs.Length);
            GameObject goTile = Instantiate(exitPrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;
            goTile.name = "Exit Room";
            Transform origin = generetedTiles[generetedTiles.FindIndex(x => x.tile == tileFrom)].tile;
            generetedTiles.Add(new Tile(goTile.transform, origin));

            return goTile.transform;
        }
        Transform CreateStartTile()
        {
            int index = Random.Range(0, startPrefabs.Length);
            GameObject goTile = Instantiate(startPrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;
            goTile.name = "Start Room";
            float yRot = Random.Range(0, 4) * 90f;
            goTile.transform.Rotate(0, yRot, 0);

            generetedTiles.Add(new Tile(goTile.transform, null));

            return goTile.transform;
        }
    }
}
