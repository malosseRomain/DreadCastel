/* Copyright 2023 Infinity Entertainment
   Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
   and associated documentation files (the �Software�), to deal in the Software without 
   restriction, including without limitation the rights to use, copy, modify, merge, publish, 
   distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
   Software is furnished to do so, subject to the following conditions:
 
   The above copyright notice and this permission notice shall be included in all copies or 
   substantial portions of the Software.
 
   THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
   BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
   DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* Custom datastructure, holding information for rooms.
        prefab -> base prefab for the room
        puzzle -> a set of prefab, for each puzzle
        offset -> the offset which will be applied to the Procedural Room Spawner object
        scale  -> the scale which will be applied to the Procedural Room Spawner object */

[System.Serializable]
public struct offset_t
{
    public Vector3 now;
    public Vector3 next;
};

[System.Serializable]
public struct room_t
{
    public GameObject prefab;
    public GameObject[] puzzle;
    public offset_t offset;
    public Vector3 scale;
}



/* Script for loading rooms dymamically.
   Rooms may be defined in both X and Z axis, the procedural room spawner will accomodate accodingly. */

public class ProceduralRoomSpawner : MonoBehaviour
{

    /* Public variables, shown in the inspector */
    public string targetTag = "Player";

    public room_t[] roomSet;


    /* Utility functions */
    Vector3 VectorCopyNotNull(Vector3 a, Vector3 b) { return new Vector3(b.x == 0 ? a.x : b.x, b.y == 0 ? a.y : b.y, b.z == 0 ? a.z : b.z); }

    room_t GetRandomRoom() { return roomSet[Random.Range(0, roomSet.Length)]; }
    GameObject GetRandomPuzzle(room_t room) { return room.puzzle.Length > 0 ? room.puzzle[Random.Range(0, room.puzzle.Length)] : null; }

    float fabs(float a) { return a > 0 ? a : -a; }


    public GameObject InstantiateRoom(room_t room)
    {
        GameObject roomInstance = Instantiate(room.prefab, transform.position, Quaternion.identity);
        return roomInstance;
    }

    public GameObject InstantiatePuzzle(GameObject puzzle, GameObject parent)
    {
        if (!puzzle) return null;

        GameObject puzzleInstance = Instantiate(puzzle, transform.position, Quaternion.identity);
        //puzzleInstance.transform.parent = parent.transform;
        return puzzleInstance;
    }


    /* Generic Unity functions */
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != targetTag)
            return;

        room_t room = GetRandomRoom();
        GameObject puzzle = GetRandomPuzzle(room);

        transform.position += new Vector3(room.offset.now.x, room.offset.now.y, room.offset.now.z);
        transform.localScale = VectorCopyNotNull(transform.localScale, room.scale);

        GameObject roomInstance = InstantiateRoom(room);
        InstantiatePuzzle(puzzle, roomInstance);

        transform.position += new Vector3(room.offset.next.x, room.offset.next.y, room.offset.next.z);
    }
}
