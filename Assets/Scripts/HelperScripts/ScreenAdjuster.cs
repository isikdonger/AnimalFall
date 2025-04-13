using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] GameObject topSpikes;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject coinText;
    [SerializeField] GameObject coinImage;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject backToGameButton;
    [SerializeField] GameObject backToMenuButton;
    private const int WIDTH = 1080, HEIGHT = 2400;
    // Start is called before the first frame update
    void Start()
    {
        // Camera resolution
        int resWidth, resHeight, scale;
        resWidth = Camera.pixelWidth;
        resHeight = Camera.pixelHeight;
        Debug.Log(resHeight + " " + resWidth);
        scale = (resHeight - HEIGHT) / 300 * 25;

        // Top Panel Adjustment
        RectTransform scoreTextRT = scoreText.GetComponent<RectTransform>(), pauseButtonRT = pauseButton.GetComponent<RectTransform>(), 
            coinTextRT = coinText.GetComponent<RectTransform>(), coinImageRT = coinImage.GetComponent<RectTransform>();
        int dimension = 200 + scale;
        scoreTextRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension + 200);
        scoreTextRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension);
        scoreTextRT.anchoredPosition = new Vector2(-(dimension + 200) / 2 - 10, -dimension / 2);
        pauseButtonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension - 50);
        pauseButtonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension - 50);
        pauseButtonRT.anchoredPosition = new Vector2(0, -dimension / 2);
        coinTextRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension + 100);
        coinTextRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension);
        coinTextRT.anchoredPosition = new Vector2((dimension + 100) / 2 + 10, -dimension / 2);
        coinText.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(175 + scale, 0);
        coinImageRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension - 50);
        coinImageRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension - 50);
        coinImageRT.anchoredPosition = new Vector2((dimension - 50) / 2, 0);

        // Top Spike Adjustments
        RectTransform topSpikesRT = topSpikes.GetComponent<RectTransform>();
        int spikeHeight = 200 + scale, spikeWidth = resWidth / 10, xPosition = spikeWidth / 2;
        topSpikesRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spikeHeight);
        topSpikesRT.anchoredPosition = new Vector2(0, -(dimension + spikeHeight / 2));
        topSpikes.GetComponent<BoxCollider2D>().size = new Vector2(resWidth, spikeHeight);
        for (int i = 0; i < 10; i++)
        {
            RectTransform temp = topSpikes.transform.GetChild(i).GetComponent<RectTransform>();
            temp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, spikeWidth);
            temp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spikeHeight);
            temp.anchoredPosition = new Vector2(xPosition, 0);
            xPosition += spikeWidth;
        }

        // Pause Menu Ajustments
        RectTransform backToGameButtonRT = backToGameButton.GetComponent<RectTransform>(), backToMenuButtonRT = backToMenuButton.GetComponent<RectTransform>();
        int width = 200 + scale, height = 150 + scale;
        backToGameButtonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        backToGameButtonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        backToGameButtonRT.anchoredPosition = new Vector2(-width / 2, height / 2);
        backToMenuButtonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        backToMenuButtonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        backToMenuButtonRT.anchoredPosition = new Vector2(width / 2, height / 2);
    }
}
