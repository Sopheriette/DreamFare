using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;
    public GameObject customerPrefab;
    public Transform[] customerSpawnPoints;
    public Transform customerUIParent;
    public List<CustomerProfile> customerProfiles;

    private List<Customer> activeCustomers = new List<Customer>();
    public float customerWaitTime = 60f;
    public float customerSpawnDelay = 30f;
    public int maxCustomers = 2;

    private void Start()
    {
        StartCoroutine(SpawnCustomerLoop());
    }

    IEnumerator SpawnCustomerLoop()
    {
        while (true)
        {
            if (activeCustomers.Count < maxCustomers)
            {
                SpawnCustomer();
            }

            yield return new WaitForSeconds(customerSpawnDelay);
        }
    }
    void SpawnCustomer()
    {
        

        for (int i = 0; i < customerSpawnPoints.Length; i++)
        {
            bool slotOccupied = false;
            activeCustomers.RemoveAll(c => c == null);

            foreach (var customer in activeCustomers)
            {
                if (customer != null && customer.transform.parent == customerSpawnPoints[i])
                {
                    slotOccupied = true;
                    break;
                }
            }

            if (!slotOccupied)
            {
                GameObject newCustomerGO = Instantiate(customerPrefab, customerSpawnPoints[i]);
                newCustomerGO.transform.localPosition = Vector3.zero;

                Customer newCustomer = newCustomerGO.GetComponent<Customer>();
                newCustomer.AssignRandomPastry(recipeDatabase);

                CustomerDropZone dropZone = newCustomerGO.GetComponent<CustomerDropZone>();
                if (dropZone != null)
                    dropZone.customer = newCustomer;
                StartCoroutine(InitializeCustomer(newCustomer));
                StartCoroutine(HandleCustomerLifetime(newCustomer));
                activeCustomers.Add(newCustomer);
                break;
            }
        }
    }
    private IEnumerator InitializeCustomer(Customer customer)
    {
        yield return new WaitUntil(() => customer.gameObject.activeInHierarchy);

        CustomerProfile chosenProfile = customerProfiles[Random.Range(0, customerProfiles.Count)];
        customer.SetupFromProfile(chosenProfile);  // <-- This sets sprites + dialogue
        customer.AssignRandomPastry(recipeDatabase);
        customer.BeginWaitingDialogue();
    }

    IEnumerator HandleCustomerLifetime(Customer customer)
    {
        float timer = customerWaitTime;

        while (timer > 0 && !customer.hasLeft)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        if (!customer.hasLeft)
        {
            Debug.Log("⌛ Customer got tired of waiting and left.");
            activeCustomers.Remove(customer);
            Destroy(customer.gameObject);
        }
    }

    public void CustomerServed(Customer customer)
    {
        if (activeCustomers.Contains(customer))
        {
            activeCustomers.Remove(customer);
            Destroy(customer.gameObject);
        }
    }
}