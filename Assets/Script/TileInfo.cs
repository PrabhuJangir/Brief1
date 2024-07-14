using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int x;
    public int y;
    private Renderer tileRenderer;
 public void SetTileInfo(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    void Awake()
    {
        tileRenderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        tileRenderer.material.color = color;
    }
    
}
