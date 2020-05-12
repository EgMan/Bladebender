using UnityEngine;

public class KnifeMagic : MonoBehaviour
{
    public static float antigravRandomNoise = 10f;
    public RangeSelector range;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Knife.getKnives(transform.position, range.getRange()).ForEach(knife => enterAntiGrav(knife));
        }
    }
    private void enterAntiGrav(Knife k)
    {
        k.state = Knife.States.AntiGrav;
        k.rb.AddForce(new Vector2(Random.Range(0f, antigravRandomNoise), Random.Range(0f, antigravRandomNoise)));
    }
}
