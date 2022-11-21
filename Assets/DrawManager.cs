using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawManager : MonoBehaviour
{
    const float MAX_POINT_DISTANCE = 0.1f;
    
    public List<Vector2> drawPoints = new List<Vector2>();
    public bool isDrawing = false;
    
    [SerializeField] private LineRenderer sourceLine;
    [SerializeField] private ParticleSystem[] effects;

    private LineRenderer curLine;
    private Camera cam;
    private List<LineRenderer> allStrokes = new List<LineRenderer>();
    int currentStroke = 0;

    private void Start()
    {
        cam = Camera.main;
        drawPoints.Clear();
        currentStroke = -1;
    }

    void Update()
    {
        isDrawing = false;

        if (Input.GetMouseButtonDown(0)) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CreateLine(pos);
        }

        if (Input.GetMouseButton(0))
        {
            isDrawing = true;

            if (drawPoints.Count < 1)
            {
                drawPoints.Add((Vector2)Input.mousePosition);
            }
            else
            {
                if (Vector2.Distance(drawPoints[drawPoints.Count - 1], Input.mousePosition) > MAX_POINT_DISTANCE)
                {
                    drawPoints.Add((Vector2)Input.mousePosition);
                }
            }
            UpdateLine();
        }

        if (Input.GetMouseButtonUp(0)) {
            //Gameplay.Instance.manyStrokeChecker.FinishDrawingStroke(drawPoints, currentStroke);
            Reset();
            drawPoints.Clear();
        }
    }

    public void CreateLine(Vector2 position) {
        currentStroke++;

        curLine = Instantiate(sourceLine.gameObject, transform).GetComponent<LineRenderer>();
        allStrokes.Add(curLine);
        drawPoints.Clear();
        //Gameplay.Instance.manyStrokeChecker.StartDrawingStroke(position, currentStroke);
    }

    public void UpdateLine() {
        if (curLine == null) return;

        curLine.positionCount = drawPoints.Count;

        for (int n = 0; n < drawPoints.Count; n++) {
            Vector3 worldP = cam.ScreenToWorldPoint(drawPoints[n]);
            worldP.z = 0;
            curLine.SetPosition(n, worldP);
        }
    }

    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Reset() {
        for (int i = 0; i < allStrokes.Count; i++) {
            Destroy(allStrokes[i].gameObject);
        }
        allStrokes = new List<LineRenderer>();
        currentStroke = -1;
    }
}
