using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    GameManager gameManager;

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    WorldTile tileDroppedOn;


    //spawn position
    private Vector2 cardPosition;
    bool moved;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        cardPosition = gameObject.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .3f;
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int dictKey = new Vector3Int(0,0,0);

        //best round x
        if ((worldPosition.x > 0 && worldPosition.x % 1 <= 0.5) || (worldPosition.x < 0 && worldPosition.x % 1 <= -0.5))
            dictKey = new Vector3Int(Mathf.FloorToInt(worldPosition.x), dictKey.y, 0);
        else
        {
            dictKey = new Vector3Int(Mathf.RoundToInt(worldPosition.x), dictKey.y, 0);
        }


        //best round y
        if ((worldPosition.y > 0 && worldPosition.y % 1 <= 0.5) || (worldPosition.y < 0 && worldPosition.y % 1 <= -0.5))
            dictKey = new Vector3Int(dictKey.x, Mathf.FloorToInt(worldPosition.y), 0);
        else
        {
            dictKey = new Vector3Int(dictKey.x, Mathf.RoundToInt(worldPosition.y), 0);
        }


        if (tiles.TryGetValue(dictKey, out tileDroppedOn))
        {
            if (tileDroppedOn.getOccupied())
            {
                Enemy enemyToAttack;
                if (tileDroppedOn.getObject().CompareTag("enemy"))
                {
                    gameManager.playerAttacked = true;

                    enemyToAttack = tileDroppedOn.getObject().GetComponent<Enemy>();
                    gameObject.GetComponent<getCardData>().card.attack(enemyToAttack.GetComponent<Enemy>());
                    Debug.Log("Enemy health: " + enemyToAttack.enemyHealth);
                    Destroy(gameObject);
                }
                else
                    gameObject.transform.position = cardPosition;
            }
            else
                gameObject.transform.position = cardPosition;
        }
        else
            gameObject.transform.position = cardPosition;
        gameObject.transform.localScale = new Vector2(1f, 1f);
    }

    private void Update()
    {  
        if(gameManager.outOfCombat || !gameManager.playerTurn || gameManager.enemyMoving)
        {
            Destroy(gameObject);
        }
        if (gameManager.cancelAttackHit)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -2000);
            moved = true;
        }
        else if (gameManager.playerTurn && !gameManager.cancelAttackHit)
        {
            if (moved)
            {
                gameObject.transform.position = cardPosition;
                moved = false;
            }
        }

        if (tiles.Count == 0)
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;
    }
}
