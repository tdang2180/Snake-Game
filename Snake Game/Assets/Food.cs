using UnityEngine;

public class Food : MonoBehaviour
{
  public BoxCollider2D gridArea;

  private void Start (){
   RandomizePosition(); 
  }

  private void RandomizePosition(){
    //Box Collider 2D comes with Bounds class
    Bounds bounds = this.gridArea.bounds;

    float x = Random.Range(bounds.min.x, bounds.max.x);
    float y = Random.Range(bounds.min.y, bounds.max.y);

    this.transform.position = new Vector3 (Mathf.Round(x), Mathf.Round(y), 0.0f);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    //unity tagging system
    if (other.tag == "Player"){
      RandomizePosition();
    }
  }
}
