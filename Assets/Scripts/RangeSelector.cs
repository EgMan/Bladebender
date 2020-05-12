using UnityEngine;

public class RangeSelector : MonoBehaviour
{
    // Update is called once per frame
    public float circleWidth = .025f;
    public float defaultRange = 1f;
    public float rangeStep = .01f;
    public float circleDisplayTime = .5f;

    public float selectedRange;

    private LineRenderer line;
    private int pendingEraceCircleActions = 0;
    public float getRange()
    {
        return selectedRange;
    }
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        // line.renderer.material.SetColor("_TintColor", new Color(1, 1, 1, 0.5f));
        selectedRange = defaultRange;
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            selectedRange += rangeStep;
            drawCircle(selectedRange, circleWidth);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            selectedRange -= rangeStep;
            selectedRange = selectedRange < 0f ? 0f : selectedRange;
            drawCircle(selectedRange, circleWidth);
        }
    }
    private void drawCircle(float radius, float lineWidth)
    {
        line.enabled = true;
        int segments = 360;
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        int pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 50);
        }

        line.SetPositions(points);

        pendingEraceCircleActions++;
        Invoke("eraceCircleIfTimeUp", circleDisplayTime);
    }
    private void eraceCircleIfTimeUp()
    {
        pendingEraceCircleActions--;
        if (pendingEraceCircleActions <= 0)
        {
            line.enabled = false;
            pendingEraceCircleActions = 0;
        }
    }
}
