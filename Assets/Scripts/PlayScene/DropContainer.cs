using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : MonoBehaviour
{
    public List<DropPiece> dropPieces;
    public DropPiece dropPiece;
    public void Init(int type, int value, int noOfPieces)
    {
        for (int i = 0; i < noOfPieces; i++)
        {
            float ranx = Random.Range(-0.2f, 0.2f);
            float rany = Random.Range(-0.2f, 0.2f);
            Vector2 pos = new Vector2(transform.position.x + ranx, transform.position.y + rany);
            //Debug.Log("value of piece: " + value);
            var dp = dropPiece.MakeInstance(this.gameObject);
            dp.Init(type, value);
            dp.transform.position = pos;
            dropPieces.Add(dp);
        }
    }

    public DropPiece GetDropPieces(int i)
    {
        return dropPieces[i];
    }

    public void Dissapear()
    {
        Destroy(this.gameObject, 0.1f);
    }
}
