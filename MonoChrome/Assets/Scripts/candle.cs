using UnityEngine;

public class candle : MonoBehaviour
{
    [SerializeField] GameObject candlePrefab;
    [SerializeField] GameObject waypoint;
    [SerializeField] GameObject corridorPf0;
    [SerializeField] GameObject corridorPf2;

    Animator anim;

    private void Awake()
    {
       

    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            {
            Destroy(corridorPf0);
            corridorPf2.transform.localPosition =  new Vector2(4.45f, 4.65f);
            Instantiate(corridorPf2);


            candlePrefab.transform.localPosition = waypoint.transform.localPosition;
            Instantiate(candlePrefab);

            anim.SetBool("isOnFire", true);

        }


    }
}
