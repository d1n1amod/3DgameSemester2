using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CrossHair : MonoBehaviour
{
    [Range(0, 100)]

    public float Value = 10f;
    public float speed = 10f;
    public float margin= 5f;

    public RectTransform Top, Bottom, Left, Right, Center; 

    private void Update()
    {
        float TopValue, BottomValue, LeftValue, RightValue;

        TopValue = Mathf.Lerp(Top.position.y, Center.position.y + margin + Value, speed * Time.deltaTime);
        BottomValue = Mathf.Lerp(Bottom.position.y, Center.position.y - margin - Value, speed * Time.deltaTime);

        LeftValue = Mathf.Lerp(Left.position.x, Center.position.x + margin + Value, speed * Time.deltaTime);
        RightValue = Mathf.Lerp(Right.position.x, Center.position.x - margin - Value, speed * Time.deltaTime);

        Top.position = new Vector2(Top.position.x, TopValue);
        Bottom.position = new Vector2(Bottom.position.x, BottomValue);

        Left.position = new Vector2(LeftValue, Center.position.y);
        Right.position = new Vector2(RightValue, Center.position.y);


    }
}
