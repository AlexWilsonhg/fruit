using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCrate : MonoBehaviour
{
	public string type;

    [SerializeField] private Canvas containedItemIndicator;
    [SerializeField] private List<AudioClip> hitSounds = new List<AudioClip>();

    private Text itemCountText;
    private Image itemTypeImage;
    private int itemCapacity = 10;
    private int itemCount = 0;
    private AudioSource sfx;

    void Awake()
    {
    	sfx = transform.GetComponent(typeof(AudioSource)) as AudioSource;
    }

    void OnTriggerEnter(Collider other)
    {
    	if(other.tag == "Fruit")
    	{
            Item item = other.transform.GetComponent(typeof(Item)) as Item;

    		if(itemCount == 0)
            {
                SetCrateType(other.gameObject);
            }
            if(item.type == type && itemCount < itemCapacity)
            {
                AcceptItem(other.gameObject);
                other.enabled = false;
            }
            else
            {
                RejectItem(other.gameObject);
            }
    	}
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public int GetItemCapacity()
    {
        return itemCapacity;
    }

    public void ResetCrateType()
    {
        itemCount = 0;
        type = null;
        containedItemIndicator.enabled = false;
    }

    private void AcceptItem(GameObject item)
    {
        itemCount++;
        UpdateIndicator();
        PlayHitSound();

        ParticleSystem p = item.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
        p.Play();

        MeshRenderer m = item.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;
        m.enabled = false;





        Destroy(item, 1.0f);
    }

    private void RejectItem(GameObject item)
    {
        Rigidbody rb = item.transform.GetComponent(typeof(Rigidbody)) as Rigidbody;
        rb.AddForce(new Vector3(100.0f,0.0f,0.0f) + transform.TransformDirection(Vector3.up) * 500.0f);
    }

    private void SetCrateType(GameObject item)
    {
        var i = item.transform.GetComponent(typeof(Item)) as Item;
        type = i.type;
        containedItemIndicator.enabled = true;
        itemCountText = containedItemIndicator.transform.Find("Contained Item Count").GetComponent(typeof(Text)) as Text;
        itemTypeImage = containedItemIndicator.transform.Find("Contained Item Type").GetComponent(typeof(Image)) as Image;

        itemCountText.text = GetItemCount() + " / " + GetItemCapacity();
        itemTypeImage.color = i.color;
    }

    private void UpdateIndicator()
    {
        itemCountText.text = GetItemCount() + " / " + GetItemCapacity();
    }

    private void PlayHitSound()
    {
    	sfx.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)], 1.0f);
    }
}
