using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translater : MonoBehaviour
{
    public GameObject stgText, MMopText, csText, crText, OMopText, lanText, lansText, lansImage, mText, monText, moffText, sText, sonText, soffText, bText;
    public GameObject stgRt, MMopRt, csRt, crRt, OMopRt, monRt, moffRt, sonRt, soffRt;
    public GameObject languagemenu, sound, back;
    Text stg_Text, MMop_Text, cs_Text, cr_Text, lan_Text, lans_Text, lans_Image, OMop_Text, m_Text, mon_Text, moff_Text, s_Text, son_Text, soff_Text, b_Text;
    public Text DeutschText, EnglishText, EspañolText, FrançaisText, ItalianoText, TürkçeText, 中国Text;
    public Image GermanyFlag, UKFlag, SpainFlag, FranceFlag, /*ItalyFlag,*/ TurkeyFlag, ChinaFlag;
    RectTransform stg_Rt, MMop_Rt, cs_Rt, cr_Rt, OMop_Rt, mon_Rt, moff_Rt, son_Rt, soff_Rt;
    void Awake()
    {
        stg_Text = stgText.GetComponent<Text>();
        MMop_Text = MMopText.GetComponent<Text>();
        cs_Text = csText.GetComponent<Text>();
        cr_Text = crText.GetComponent<Text>();
        lan_Text = lanText.GetComponent<Text>();
        lans_Text = lansText.GetComponent<Text>();
        OMop_Text = OMopText.GetComponent<Text>();
        m_Text = mText.GetComponent<Text>();
        mon_Text = monText.GetComponent<Text>();
        moff_Text = moffText.GetComponent<Text>();
        s_Text = sText.GetComponent<Text>();
        son_Text = sonText.GetComponent<Text>();
        soff_Text = soffText.GetComponent<Text>();
        b_Text = bText.GetComponent<Text>();
        stg_Rt = stgRt.GetComponent<RectTransform>();
        MMop_Rt = MMopRt.GetComponent<RectTransform>();
        cs_Rt = csRt.GetComponent<RectTransform>();
        cr_Rt = crRt.GetComponent<RectTransform>();
        OMop_Rt = OMopRt.GetComponent<RectTransform>();
        mon_Rt = monRt.GetComponent<RectTransform>();
        moff_Rt = moffRt.GetComponent<RectTransform>();
        son_Rt = sonRt.GetComponent<RectTransform>();
        soff_Rt = soffRt.GetComponent<RectTransform>();

    }
    public void Deutsch()
    {
        languagemenu.SetActive(false);
        sound.SetActive(true);
        back.SetActive(true);
        stg_Text.text = "Spiel Starten";
        stg_Rt.localPosition = new Vector2(6.67f, 283.5f);
        stg_Rt.sizeDelta = new Vector2(440.13f, 203.8f);
        MMop_Text.text = "Optionen";
        MMop_Rt.localPosition = new Vector2(-57.6f, 79.70005f);
        MMop_Rt.sizeDelta = new Vector2(311.5f, 203.8f);
        cs_Text.text = "Charakterauswahl";
        cs_Rt.localPosition = new Vector2(94.26f, -124.1f);
        cs_Rt.sizeDelta = new Vector2(615.12f, 203.8f);
        cr_Text.text = "Credits";
        cr_Rt.localPosition = new Vector2(-81.03003f, -305.7f);
        cr_Rt.sizeDelta = new Vector2(264.6f, 159.4f);
        OMop_Text.text = "Optionen";
        lan_Text.text = "Sprache:";
        m_Text.text = "Musik";
        mon_Text.text = "Auf";
        mon_Rt.localPosition = new Vector2(30.15f, 14f);
        mon_Rt.sizeDelta = new Vector2(95.56f, 100.15f);
        moff_Text.text = "Aus";
        moff_Rt.localPosition = new Vector2(168.46f, 14f);
        moff_Rt.sizeDelta = new Vector2(83.69f, 100.15f);
        s_Text.text = "Geräusche";
        son_Text.text = "Auf";
        son_Rt.localPosition = new Vector2(30.15f, -99.85f);
        son_Rt.sizeDelta = new Vector2(95.56f, 100.15f);
        soff_Text.text = "Zu";
        soff_Rt.localPosition = new Vector2(168.46f, 99.85f);
        soff_Rt.sizeDelta = new Vector2(83.69f, 100.15f);
        b_Text.text = "Zurück";
        lans_Text.text = DeutschText.text;
    }
    public void English()
    {
        languagemenu.SetActive(false);
        sound.SetActive(true);
        back.SetActive(true);
        stg_Text.text = "Start Game";
        stg_Rt.localPosition = new Vector2(-19.62f, 238.5f);
        stg_Rt.sizeDelta = new Vector2(397.54f, 203.8f);
        MMop_Text.text = "Options";
        MMop_Rt.localPosition = new Vector2(-78.373f, 79.70005f);
        MMop_Rt.sizeDelta = new Vector2(270.004f, 203.8f);
        cs_Text.text = "Character Select";
        cs_Rt.localPosition = new Vector2(72.53f, -124.1f);
        cs_Rt.sizeDelta = new Vector2(571.65f, 203.8f);
        cr_Text.text = "Credits";
        cr_Rt.localPosition = new Vector2(-81.03003f, -305.7f);
        cr_Rt.sizeDelta = new Vector2(264.6f, 159.4f);
        OMop_Text.text = "Options";
        lan_Text.text = "Language:";
        m_Text.text = "Music";
        mon_Text.text = "On";
        mon_Rt.localPosition = new Vector2(12.34f, 14f);
        mon_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
        moff_Text.text = "Off";
        moff_Rt.localPosition = new Vector2(173.68f, 14f);
        moff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
        s_Text.text = "Sound";
        son_Text.text = "On";
        son_Rt.localPosition = new Vector2(12.34f, -99.85f);
        son_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
        soff_Text.text = "Off";
        soff_Rt.localPosition = new Vector2(173.78f, 99.85f);
        soff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
        b_Text.text = "Back";
        lans_Text.text = EnglishText.text;
    }
    public void Español()
    {
        languagemenu.SetActive(false);
        sound.SetActive(true);
        back.SetActive(true);
        stg_Text.text = "Levantar";
        stg_Rt.localPosition = new Vector2(-60.78f, 283.5f);
        stg_Rt.sizeDelta = new Vector2(305.22f, 203.8f);
        MMop_Text.text = "Opciones";
        MMop_Rt.localPosition = new Vector2(-50.92f, 79.70005f);
        MMop_Rt.sizeDelta = new Vector2(324.91f, 203.8f);
        cs_Text.text = "Seleccionar Personaje";
        cs_Rt.localPosition = new Vector2(168.0409f, -124.1f);
        cs_Rt.sizeDelta = new Vector2(762.682f, 203.8f);
        cr_Text.text = "Créditos";
        cr_Rt.localPosition = new Vector2(-87.67297f, -305.7f);
        cr_Rt.sizeDelta = new Vector2(251.314f, 159.4f);
        OMop_Text.text = "Opciones";
        lan_Text.text = "Idioma:";
        m_Text.text = "Musica";
        mon_Text.text = "On";
        mon_Rt.localPosition = new Vector2(12.34f, 14f);
        mon_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
        moff_Text.text = "Off";
        moff_Rt.localPosition = new Vector2(173.68f, 14f);
        moff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
        s_Text.text = "Sonido";
        son_Text.text = "On";
        son_Rt.localPosition = new Vector2(12.34f, -99.85f);
        son_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
        soff_Text.text = "Off";
        soff_Rt.localPosition = new Vector2(173.78f, 99.85f);
        soff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
        b_Text.text = "Atrás";
        lans_Text.text = EspañolText.text;
    }
    public void Français()
    {
        languagemenu.SetActive(false);
        sound.SetActive(true);
        back.SetActive(true);
        stg_Text.text = "Démarrer Jeu";
        stg_Rt.localPosition = new Vector2(20.36f, 283.5f);
        stg_Rt.sizeDelta = new Vector2(467.5f, 203.8f);
        MMop_Text.text = "Options";
        MMop_Rt.localPosition = new Vector2(-77.9f, 79.70005f);
        MMop_Rt.sizeDelta = new Vector2(270.94f, 203.8f);
        cs_Text.text = "Sélection de Personnages";
        cs_Rt.localPosition = new Vector2(231.16f, -124.1f);
        cs_Rt.sizeDelta = new Vector2(888.93f, 203.8f);
        cr_Text.text = "Crédits";
        cr_Rt.localPosition = new Vector2(-81.03003f, -305.7f);
        cr_Rt.sizeDelta = new Vector2(264.6f, 159.4f);
        OMop_Text.text = "Options";
        lan_Text.text = "Langue:";
        m_Text.text = "Musique";
        mon_Text.text = "On";
        mon_Rt.localPosition = new Vector2(12.34f, 14f);
        mon_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
        moff_Text.text = "Off";
        moff_Rt.localPosition = new Vector2(173.68f, 14f);
        moff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
        s_Text.text = "Du Son";
        son_Text.text = "On";
        son_Rt.localPosition = new Vector2(12.34f, -99.85f);
        son_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
        soff_Text.text = "Off";
        soff_Rt.localPosition = new Vector2(173.78f, 99.85f);
        soff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
        b_Text.text = "Retour";
        lans_Text.text = FrançaisText.text;
    }
    //public void Italiano()
    //{
    //    languagemenu.SetActive(false);
    //    sound.SetActive(true);
    //    back.SetActive(true);
    //    stg_Text.text = "Inizia il Gioco";
    //    stg_Rt.localPosition = new Vector2(6.67f, 283.5f);
    //    stg_Rt.sizeDelta = new Vector2(440.13f, 203.8f);
    //    MMop_Text.text = "inizia il Gioco";
    //    MMop_Rt.localPosition = new Vector2(-57.6f, 79.70005f);
    //    MMop_Rt.sizeDelta = new Vector2(311.5f, 203.8f);
    //    cs_Text.text = "Selezione Personaggio";
    //    cs_Rt.localPosition = new Vector2(94.26f, -124.1f);
    //    cs_Rt.sizeDelta = new Vector2(615.12f, 203.8f);
    //    cr_Text.text = "Titoli di Coda";
    //    cr_Rt.localPosition = new Vector2(-81.03003f, -305.7f);
    //    cr_Rt.sizeDelta = new Vector2(264.6f, 159.4f);
    //    OMop_Text.text = "İnizia il Gioco";
    //    lan_Text.text = "Linguaggio:";
    //    m_Text.text = "Musica";
    //    mon_Text.text = "On";
    //    mon_Rt.localPosition = new Vector2(12.34f, 14f);
    //    mon_Rt.sizeDelta = new Vector2(84.37f, 100.15f);
    //    moff_Text.text = "Off";
    //    moff_Rt.localPosition = new Vector2(173.68f, 14f);
    //    moff_Rt.sizeDelta = new Vector2(95.61f, 100.15f);
    //    s_Text.text = "Suono";
    //    son_Text.text = "On";
    //    son_Rt.localPosition = new Vector2(30.15f, -99.85f);
    //    son_Rt.sizeDelta = new Vector2(95.56f, 100.15f);
    //    soff_Text.text = "Off";
    //    soff_Rt.localPosition = new Vector2(168.46f, 99.85f);
    //    soff_Rt.sizeDelta = new Vector2(83.69f, 100.15f);
    //    b_Text.text = "Indietro";
    //    lans_Text.text = ItalianoText.text;
    //}
    public void Türkçe()
    {
        languagemenu.SetActive(false);
        sound.SetActive(true);
        back.SetActive(true);
        stg_Text.text = "Oyunu Başlat";
        stg_Rt.localPosition = new Vector2(14.61f, 283.5f);
        stg_Rt.sizeDelta = new Vector2(456f, 203.8f);
        MMop_Text.text = "Seçenekler";
        MMop_Rt.localPosition = new Vector2(-18.92f, 79.70005f);
        MMop_Rt.sizeDelta = new Vector2(388.87f, 203.8f);
        cs_Text.text = "Karakter Seç";
        cs_Rt.localPosition = new Vector2(7.285f, -124.1f);
        cs_Rt.sizeDelta = new Vector2(441.171f, 203.8f);
        cr_Text.text = "Kreditler";
        cr_Rt.localPosition = new Vector2(-82.726f, -305.7f);
        cr_Rt.sizeDelta = new Vector2(301.209f, 159.4f);
        OMop_Text.text = "Seçenekler";
        lan_Text.text = "Dil:";
        m_Text.text = "Muzik";
        mon_Text.text = "Açık";
        mon_Rt.localPosition = new Vector2(17.97f, 14f);
        mon_Rt.sizeDelta = new Vector2(121.2f, 100.2f);
        moff_Text.text = "Kapalı";
        moff_Rt.localPosition = new Vector2(197.53f, 14f);
        moff_Rt.sizeDelta = new Vector2(152.47f, 100.2f);
        s_Text.text = "Ses";
        son_Text.text = "Açık";
        son_Rt.localPosition = new Vector2(17.97f, -99.85f);
        son_Rt.sizeDelta = new Vector2(121.2f, 100.15f);
        soff_Text.text = "Kapalı";
        soff_Rt.localPosition = new Vector2(197.53f, 99.85f);
        soff_Rt.sizeDelta = new Vector2(152.47f, 100.15f);
        b_Text.text = "Geri";
        lans_Text.text = TürkçeText.text;
    }
    public void 中国()
    {
        languagemenu.SetActive(false);
        sound.SetActive(true);
        back.SetActive(true);
        stg_Text.text = "开始游戏";
        stg_Rt.localPosition = new Vector2(-59.775f, 283.5f);
        stg_Rt.sizeDelta = new Vector2(307.729f, 203.8f);
        MMop_Text.text = "选件";
        MMop_Rt.localPosition = new Vector2(-134.62f, 79.70005f);
        MMop_Rt.sizeDelta = new Vector2(157.5f, 203.8f);
        cs_Text.text = "球员选择";
        cs_Rt.localPosition = new Vector2(-58.35f, -124.1f);
        cs_Rt.sizeDelta = new Vector2(309.9f, 203.8f);
        cr_Text.text = "学分";
        cr_Rt.localPosition = new Vector2(-133.42f, -305.7f);
        cr_Rt.sizeDelta = new Vector2(159.83f, 159.4f);
        OMop_Text.text = "选件";
        lan_Text.text = "语言:";
        m_Text.text = "音乐";
        mon_Text.text = "上";
        mon_Rt.localPosition = new Vector2(30.15f, -99.85f);
        mon_Rt.sizeDelta = new Vector2(95.56f, 100.15f);
        moff_Text.text = "关";
        moff_Rt.localPosition = new Vector2(168.46f, 99.85f);
        moff_Rt.sizeDelta = new Vector2(83.69f, 100.15f);
        s_Text.text = "声音";
        son_Text.text = "上";
        son_Rt.localPosition = new Vector2(30.15f, -99.85f);
        son_Rt.sizeDelta = new Vector2(95.56f, 100.15f);
        soff_Text.text = "关";
        soff_Rt.localPosition = new Vector2(168.46f, 99.85f);
        soff_Rt.sizeDelta = new Vector2(83.69f, 100.15f);
        b_Text.text = "后部";
        lans_Text.text = 中国Text.text;
    }
}