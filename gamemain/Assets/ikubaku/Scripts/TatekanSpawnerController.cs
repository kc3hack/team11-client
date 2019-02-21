using System.Collections;
using System.Collections.Generic;
using Assets.ikubaku.Scripts;
using UnityEngine;

public class TatekanSpawnerController : MonoBehaviour
{
    public List<GameObject> TatekansBig = new List<GameObject>();
    public List<GameObject> TatekansMiddle = new List<GameObject>();
    public List<GameObject> TatekansSmall = new List<GameObject>();

    public GameObject TatekanPrefab;
    public float BigCooldown = 10f;
    public float MiddleCooldown = 5f;
    public float SmallCooldown = 3f;
    public IndicatorController BigIndicator;
    public IndicatorController MiddleIndicator;
    public IndicatorController SmallIndicator;

    public GameObject[] SpawnTargets;
    public float SpawnSpreadRange = 1f;

    public ScoreController MainScoreController;

    private GameObject TatekanBig;
    private GameObject TatekanMiddle;
    private GameObject TatekanSmall;
    private int spawn_idx = 0;
    private bool is_fire1_pressed = false;
    private bool is_fire2_pressed = false;
    private bool is_fire3_pressed = false;
    private float cnt_big_cooldown = 0f;
    private float cnt_middle_cooldown = 0f;
    private float cnt_small_cooldown = 0f;
    private Vector3 ofs_spread = Vector3.zero;


    Vector3 get_random_vector(float r)
    {
        return new Vector3(Mathf.Cos(r), Mathf.Sin(r), 0f) * SpawnSpreadRange;
    }

    // Use this for initialization
    void Start()
    {
        TatekanStore.TatekanImages.ForEach(tatekanImage =>
        {
            var ratio = (float) tatekanImage.width / (float) tatekanImage.height;
            var obj = Instantiate(TatekanPrefab);
            obj.SetActive(false);
            obj.GetComponent<SpriteRenderer>().sprite =
                Sprite.Create(
                    tatekanImage,
                    new Rect(0, 0, tatekanImage.width, tatekanImage.height),
                    Vector2.zero
                );

            var size = 500;
            var widthMagnification = (float)size / (float)tatekanImage.width;
            var heightMagnification = (float)size / (float)tatekanImage.height;

            obj.transform.localScale = new Vector3(
                 widthMagnification,
                 heightMagnification,
                1);

            if (ratio == 1)
            {
                TatekansBig.Add(obj);
            }
            else if (ratio > 1)
            {
                TatekansMiddle.Add(obj);
            }
            else
            {
                TatekansSmall.Add(obj);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_fire1_pressed && Input.GetAxis("Fire1") > 0f)
        {
            is_fire1_pressed = true;
            if (cnt_big_cooldown <= 0f)
            {
                cnt_big_cooldown = BigCooldown;
                BigIndicator.ChangeToInactive();

                TatekanBig = TatekansBig[Random.Range(0, TatekansBig.Count - 1)];

                var new_tatekan = Instantiate(TatekanBig);
                new_tatekan.SetActive(true);
                new_tatekan.transform.position = SpawnTargets[spawn_idx].transform.position + ofs_spread;
                MainScoreController.IncreaseTatekan();
                spawn_idx++;
                if (spawn_idx >= SpawnTargets.Length)
                {
                    spawn_idx = 0;
                    ofs_spread = get_random_vector(Random.Range(0f, 2 * Mathf.PI));
                }
            }
        }

        if (Input.GetAxis("Fire1") <= 0f)
        {
            is_fire1_pressed = false;
        }

        if (!is_fire2_pressed && Input.GetAxis("Fire2") > 0f)
        {
            is_fire2_pressed = true;
            if (cnt_middle_cooldown <= 0f)
            {
                cnt_middle_cooldown = MiddleCooldown;
                MiddleIndicator.ChangeToInactive();

                TatekanMiddle = TatekansMiddle[Random.Range(0, TatekansMiddle.Count - 1)];

                var new_tatekan = Instantiate(TatekanMiddle);
                new_tatekan.SetActive(true);
                new_tatekan.transform.position = SpawnTargets[spawn_idx].transform.position + ofs_spread;
                MainScoreController.IncreaseTatekan();
                spawn_idx++;
                if (spawn_idx >= SpawnTargets.Length)
                {
                    spawn_idx = 0;
                    ofs_spread = get_random_vector(Random.Range(0f, 2 * Mathf.PI));
                }
            }
        }

        if (Input.GetAxis("Fire2") <= 0f)
        {
            is_fire2_pressed = false;
        }

        if (!is_fire3_pressed && Input.GetAxis("Fire3") > 0f)
        {
            is_fire3_pressed = true;
            if (cnt_small_cooldown <= 0f)
            {
                cnt_small_cooldown = SmallCooldown;
                SmallIndicator.ChangeToInactive();

                TatekanSmall = TatekansSmall[Random.Range(0, TatekansSmall.Count - 1)];

                var new_tatekan = Instantiate(TatekanSmall);
                new_tatekan.SetActive(true);
                new_tatekan.transform.position = SpawnTargets[spawn_idx].transform.position + ofs_spread;
                MainScoreController.IncreaseTatekan();
                spawn_idx++;
                if (spawn_idx >= SpawnTargets.Length)
                {
                    spawn_idx = 0;
                    ofs_spread = get_random_vector(Random.Range(0f, 2 * Mathf.PI));
                }
            }
        }

        if (Input.GetAxis("Fire3") <= 0f)
        {
            is_fire3_pressed = false;
        }

        // Cooldown
        if (cnt_big_cooldown > 0f)
        {
            cnt_big_cooldown -= Time.deltaTime;
            if (cnt_big_cooldown <= 0f)
            {
                BigIndicator.ChangeToActive();
            }
        }

        if (cnt_middle_cooldown > 0f)
        {
            cnt_middle_cooldown -= Time.deltaTime;
            if (cnt_middle_cooldown <= 0f)
            {
                MiddleIndicator.ChangeToActive();
            }
        }

        if (cnt_small_cooldown > 0f)
        {
            cnt_small_cooldown -= Time.deltaTime;
            if (cnt_small_cooldown <= 0f)
            {
                SmallIndicator.ChangeToActive();
            }
        }
    }
}