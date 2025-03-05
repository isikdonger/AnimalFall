using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] GameObject topSpikes;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject coinText;
    [SerializeField] GameObject coinImage;
    [SerializeField] GameObject pauseButton;
    private const int WIDTH = 1080, HEIGHT = 2400;
    // Start is called before the first frame update
    void Start()
    {
        // Camera resolution
        int resWidth, resHeight, multiplier, scale;
        resWidth = Camera.pixelWidth;
        resHeight = Camera.pixelHeight;
        multiplier = resHeight > 2400 ? 1 : -1;
        scale = (resHeight - HEIGHT) / 300 * 25 * multiplier;

        // Top Panel Adjustment
        int dimension = 200 + scale;
        scoreText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension);
        scoreText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension);
        scoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-dimension / 2 - 10, -dimension / 2);
        pauseButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension - 50);
        pauseButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension - 50);
        pauseButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -dimension / 2);
        coinText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension);
        coinText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension + 100);
        coinText.GetComponent<RectTransform>().anchoredPosition = new Vector2((dimension + 100) / 2 + 10, -dimension / 2);
        coinText.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(175 + scale, 0);
        coinImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimension - 50);
        coinImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimension - 50);
        coinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2((dimension - 50) / 2, 0);


        // Top Spike adjustments
        int spikeHeight = 200 + scale, spikeWidth = resWidth / 10, xPosition = spikeWidth / 2;
        topSpikes.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spikeHeight);
        topSpikes.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -(dimension + spikeHeight / 2));
        topSpikes.GetComponent<BoxCollider2D>().size = new Vector2(resWidth, spikeHeight);
        for (int i = 0; i < 10; i++)
        {
            RectTransform temp = topSpikes.transform.GetChild(i).GetComponent<RectTransform>();
            temp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spikeHeight);
            temp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, spikeWidth);
            temp.anchoredPosition = new Vector2(xPosition, 0);
            xPosition += spikeWidth;
        }
    }
}
