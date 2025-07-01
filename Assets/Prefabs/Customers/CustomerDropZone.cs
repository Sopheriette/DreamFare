using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerDropZone : MonoBehaviour, IDropHandler
{
    public Customer customer;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("🎯 Something was dropped onto a customer!");
        DraggablePastry draggable = eventData.pointerDrag.GetComponent<DraggablePastry>();
        if (draggable != null)
        {
            Debug.Log("✅ Dragged object has DraggablePastry");

            PastryDisplay pastryDisplay = draggable.GetComponent<PastryDisplay>();
            if (pastryDisplay != null)
            {
                Debug.Log($"💡 Pastry dropped with name: {pastryDisplay.pastryData.GetPastryName()}");

                CheckPastry(pastryDisplay.pastryData);

                Destroy(draggable.gameObject);
            }
            else
            {
                Debug.LogWarning("⚠️ No PastryDisplay script found on dropped item!");
            }
        }
    }
    private void CheckPastry(DreamPastry pastry)
    {
        if (customer.desiredPastry == null)
        {
            Debug.LogWarning("⚠️ Customer has no desired pastry set.");
            return;
        }

        if (pastry == customer.desiredPastry)
        {
            Debug.Log("🎉 Exact match! Customer is thrilled!");
            MoneyManager.Instance.AddEmbers(10.00f);
            customer.ReceivePastryResult(true);
            // customer.spriteImage.sprite = customer.dreamAcomplishedSprite;
        }
        else
        {
            int matchCount = 0;
            foreach (var tag in customer.dreamTags)
            {
                if (pastry.dreamTags.Contains(tag))
                    matchCount++;
            }

            if (matchCount > 0)
            {
                Debug.Log($"🙂 Partial match. Customer is okay. Matched {matchCount} tag(s).");
            }
            else
            {
                Debug.Log("❌ No match. Customer is not satisfied.");
            }
            MoneyManager.Instance.AddEmbers(2.50f);
            customer.ReceivePastryResult(false);
        }
    }
}
