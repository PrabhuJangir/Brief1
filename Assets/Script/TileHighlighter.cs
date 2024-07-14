using UnityEngine;
using UnityEngine.UI;

public class TileHighlighter : MonoBehaviour
{
    public Text tileInfoText; // Reference to the UI Text element

    void Update()
    {
        DetectTile();
    }

    void DetectTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                tileInfoText.text = $"Tile Position: ({tileInfo.x}, {tileInfo.y})";
            }
        }
        else
        {
            tileInfoText.text = "";
        }
    }
}
