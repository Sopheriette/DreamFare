using UnityEngine;
using UnityEngine.EventSystems;

public class PlateDropZone : MonoBehaviour, IDropHandler
{
    public MixingManager mixingManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject pastryGO = eventData.pointerDrag;

        if (pastryGO != null && pastryGO.GetComponent<PastryDisplay>())
        {
            pastryGO.transform.SetParent(this.transform);
            pastryGO.transform.localPosition = Vector3.zero;

            var drag = pastryGO.GetComponent<DraggablePastry>();
            if (drag != null)
            {
                drag.wasDroppedOnPlate = true;
            }

            if (mixingManager != null)
            {
                mixingManager.NotifyResultPlaced();
            }

            Debug.Log("🍽️ Pastry has been plated.");
        }
    }
}