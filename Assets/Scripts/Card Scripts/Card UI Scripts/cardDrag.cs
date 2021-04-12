using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Dictionary<Vector3, WorldTile> tiles = new Dictionary<Vector3, WorldTile>();
    WorldTile tileDroppedOn;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas =
        canvas = GetComponentInParent<Canvas>();
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
        Debug.Log("drop");

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int dictKey = new Vector3Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt((int)worldPosition.y), 0);
        if (tiles.TryGetValue(dictKey, out tileDroppedOn))
        {
            if(tileDroppedOn.getOccupied())
            {
                Enemy enemyToAttack;
                if (tileDroppedOn.getObject().CompareTag("enemy"))
                { 
                    enemyToAttack = tileDroppedOn.getObject().GetComponent<Enemy>();
                    Debug.Log(enemyToAttack.enemyHealth);
                    gameObject.GetComponent<getCardData>().card.attack(enemyToAttack.GetComponent<Enemy>());
                    Debug.Log(enemyToAttack.enemyHealth);
                    
                    Destroy(gameObject);
                }
            }
            Debug.Log(dictKey);
        }
        gameObject.transform.localScale = new Vector2(1f, 1f);
    }

    private void Update()
    {
        if (tiles.Count == 0)
            tiles = GameObject.FindGameObjectWithTag("levelCollider").GetComponent<levelToDict>().tiles;
    }
}
