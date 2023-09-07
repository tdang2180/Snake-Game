using System.Collections.Generic; 
using UnityEngine;

public class Snake : MonoBehaviour
{
  private Vector2 _direction = Vector2.right;
  private List<Transform> _segments = new List<Transform>();
  public Transform segmentPrefab;
  public int initialSize = 4;
  private void Start(){
    ResetState();
  }

  private void Update(){
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
      _direction = Vector2.up;
    } else if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow)){
      _direction = Vector2.down;
    } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
      _direction = Vector2.left;
    } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
      _direction = Vector2.right;
    }
  }

  private void FixedUpdate() {
    for (int i = _segments.Count - 1; i > 0; i--){
      _segments[i].position = _segments[i-1].position;
    }
    this.transform.position  = new Vector3(
      Mathf.Round(this.transform.position.x) + _direction.x, //x
      Mathf.Round(this.transform.position.y) + _direction.y, //y
      0.0f //z 
    );
  }

  private void Grow(){
    Transform segment = Instantiate(this.segmentPrefab);
    segment.position = _segments[_segments.Count - 1].position;
    _segments.Add(segment);
  }

  private void ResetState(){
    for (int i = 1; i < _segments.Count; i++){
      Destroy(_segments[i].gameObject);
    }

    _segments.Clear();
    //head of snake
    _segments.Add(this.transform);

    for (int i = 1; i<this.initialSize; i++){
      _segments.Add(Instantiate(this.segmentPrefab));
    }
    this.transform.position = Vector3.zero;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    //unity tagging system
    if (other.tag == "Food"){
      Grow();
    } else if (other.tag == "Wall" || other.tag == "Body"){
      ResetState();
    }
  }
}
